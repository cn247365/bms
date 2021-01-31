using System;
using System.Data;
using System.Reflection;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Repository.Pattern.Repositories;
using Repository.Pattern.Infrastructure;
using Service.Pattern;
using System.Text.RegularExpressions;
using WebApp.Models;
using WebApp.Repositories;
using WebApp.App_Helpers.third_party.api;
using AutoMapper;

namespace WebApp.Services
{
  /// <summary>
  /// File: StockService.cs
  /// Purpose: Within the service layer, you define and implement 
  /// the service interface and the data contracts (or message types).
  /// One of the more important concepts to keep in mind is that a service
  /// should never expose details of the internal processes or 
  /// the business entities used within the application. 
  /// Created Date: 1/31/2021 9:28:21 AM
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  public class StockService : Service<Stock>, IStockService
  {
    private readonly IRepositoryAsync<Stock> repository;
    private readonly IDataTableImportMappingService mappingservice;
    private readonly IBookService bookService;
    private readonly NLog.ILogger logger;
    private readonly IMapper mapper;
    public StockService(
       IMapper mapper,
      IBookService bookService,
      IRepositoryAsync<Stock> repository,
      IDataTableImportMappingService mappingservice,
      NLog.ILogger logger
      )
        : base(repository)
    {
      this.mapper = mapper;
      this.bookService = bookService;
      this.repository = repository;
      this.mappingservice = mappingservice;
      this.logger = logger;
    }
    public async Task<IEnumerable<Stock>> GetByBookId(int bookid) => await repository.GetByBookId(bookid);



    private async Task<int> getBookIdByTitle(string title)
    {
      var bookRepository = this.repository.GetRepositoryAsync<Book>();
      var book = await bookRepository.Queryable().Where(x => x.Title == title).FirstOrDefaultAsync();
      if (book == null)
      {
        throw new Exception("not found ForeignKey:BookId with " + title);
      }
      else
      {
        return book.Id;
      }
    }
    public async Task ImportDataTable(DataTable datatable, string username)
    {
      var mapping = await this.mappingservice.Queryable()
                        .Where(x => x.EntitySetName == "Stock" &&
                           ( x.IsEnabled == true || ( x.IsEnabled == false && x.DefaultValue != null ) )
                           ).ToListAsync();
      if (mapping.Count == 0)
      {
        throw new KeyNotFoundException("没有找到Stock对象的Excel导入配置信息，请执行[系统管理/Excel导入配置]");
      }
      var list = new List<Stock>();
      foreach (DataRow row in datatable.Rows)
      {

        var requiredfield = mapping.Where(x => x.IsRequired == true && x.IsEnabled == true && x.DefaultValue == null).FirstOrDefault()?.SourceFieldName;
        if (requiredfield != null ||
              ( !row.IsNull(requiredfield) &&
               !string.IsNullOrEmpty(row[requiredfield].ToString())
              )
            )
        {
          var item = new Stock();
          var stocktype = item.GetType();
          foreach (var field in mapping)
          {
            var defval = field.DefaultValue;
            var contain = datatable.Columns.Contains(field.SourceFieldName ?? "");
            if (contain &&
                           !row.IsNull(field.SourceFieldName) &&
                           !string.IsNullOrEmpty(row[field.SourceFieldName].ToString())
                        )
            {
              var propertyInfo = stocktype.GetProperty(field.FieldName);
              var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
              var safeValue = Convert.ChangeType(row[field.SourceFieldName], safetype);
              propertyInfo.SetValue(item, safeValue, null);

            }
            else if (!string.IsNullOrEmpty(defval))
            {
              var propertyInfo = stocktype.GetProperty(field.FieldName);
              if (string.Equals(defval, "now", StringComparison.OrdinalIgnoreCase) && ( propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(Nullable<DateTime>) ))
              {
                var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                var safeValue = Convert.ChangeType(DateTime.Now, safetype);
                propertyInfo.SetValue(item, safeValue, null);
              }
              else if (string.Equals(defval, "guid", StringComparison.OrdinalIgnoreCase))
              {
                propertyInfo.SetValue(item, Guid.NewGuid().ToString(), null);
              }
              else if (string.Equals(defval, "user", StringComparison.OrdinalIgnoreCase))
              {
                propertyInfo.SetValue(item, username, null);
              }
              else
              {
                var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                var safeValue = Convert.ChangeType(defval, safetype);
                propertyInfo.SetValue(item, safeValue, null);
              }
            }
          }
          if(string.IsNullOrEmpty(item.Title) || string.IsNullOrEmpty(item.ISBN) || string.IsNullOrEmpty(item.BarCode))
          {
            throw new Exception("Book Name,ISBN,BarCode not allowed empty!");
          }
          var isexists =await this.bookService.Queryable().Where(x => x.ISBN == item.ISBN).AnyAsync();
          if (isexists)
          {
            var book = await this.bookService.Queryable().Where(x => x.ISBN == item.ISBN).FirstAsync();
            item.BookId = book.Id;
            item.Price = book.Price;
            book.PurchasingPrice = item.PurchasingPrice;
            this.bookService.Update(book);
          }
          else
          {
            var isbn = await isbnapi.GetISBN(item.ISBN);
            if (isbn == null)
            {
              var book = this.mapper.Map<Book>(item);
              item.Book = book;
              item.BookId = book.Id;
              this.bookService.Insert(book);
            }
            else
            {
              var book = this.mapper.Map<Book>(isbn);
              item.Book = book;
              item.BookId = book.Id;
              this.bookService.Insert(book);
            }
           
          }

          list.Add(item);
        }
      }

      this.InsertRange(list);
    }
    public async Task<Stream> ExportExcel(string filterRules = "", string sort = "Id", string order = "asc")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var expcolopts = await this.mappingservice.Queryable()
             .Where(x => x.EntitySetName == "Stock")
             .Select(x => new ExpColumnOpts()
             {
               EntitySetName = x.EntitySetName,
               FieldName = x.FieldName,
               IgnoredColumn = x.IgnoredColumn,
               SourceFieldName = x.SourceFieldName
             }).ToArrayAsync();

      var stocks = await this.Query(new StockQuery().Withfilter(filters)).Include(p => p.Book).OrderBy(n => n.OrderBy(sort, order)).SelectAsync();

      var datarows = stocks.Select(n => new
      {

        BookTitle = n.Book?.Title,
        Id = n.Id,
        BookId = n.BookId,
        BarCode = n.BarCode,
        ISBN = n.ISBN,
        Title = n.Title,
        Remark = n.Remark,
        Qty = n.Qty,
        Price = n.Price,
        PurchasingPrice = n.PurchasingPrice,
        PurchaseDate = n.PurchaseDate?.ToString("yyyy-MM-dd HH:mm:ss"),
        UserName = n.UserName,
        Catetory = n.Catetory,
        Location = n.Location,
        Shelves = n.Shelves,
        Tags = n.Tags,
        Status = n.Status,
        Trades = n.Trades,
        InputDate = n.InputDate.ToString("yyyy-MM-dd HH:mm:ss")
      }).ToList();
      return await NPOIHelper.ExportExcelAsync("Stock", datarows, expcolopts);
    }
    public async Task Delete(int[] id)
    {
      var items = await this.Queryable().Where(x => id.Contains(x.Id)).ToListAsync();
      foreach (var item in items)
      {
        this.Delete(item);
      }

    }
  }
}