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
using WebApp.Models.Dto;

namespace WebApp.Services
{
  /// <summary>
  /// File: CheckOutService.cs
  /// Purpose: Within the service layer, you define and implement 
  /// the service interface and the data contracts (or message types).
  /// One of the more important concepts to keep in mind is that a service
  /// should never expose details of the internal processes or 
  /// the business entities used within the application. 
  /// Created Date: 1/30/2021 9:53:16 PM
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  public class CheckOutService : Service<CheckOut>, ICheckOutService
  {
    private readonly IRepositoryAsync<CheckOut> repository;
    private readonly IDataTableImportMappingService mappingservice;
    private readonly NLog.ILogger logger;
    private readonly IEmployeeService employeeService;
    private readonly IBookService bookService;
    private readonly IStockService stockService;
    public CheckOutService(
      IEmployeeService employeeService,
      IBookService bookService,
      IStockService stockService,
      IRepositoryAsync<CheckOut> repository,
      IDataTableImportMappingService mappingservice,
      NLog.ILogger logger
      )
        : base(repository)
    {
      this.repository = repository;
      this.mappingservice = mappingservice;
      this.logger = logger;
      this.employeeService = employeeService;
      this.bookService = bookService;
      this.stockService = stockService;
    }
    public async Task<IEnumerable<CheckOut>> GetByEmployeeId(int employeeid) => await repository.GetByEmployeeId(employeeid);
    public async Task<IEnumerable<CheckOut>> GetByBookId(int bookid) => await repository.GetByBookId(bookid);



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
    private async Task<int> getEmployeeIdByShortName(string shortname)
    {
      var employeeRepository = this.repository.GetRepositoryAsync<Employee>();
      var employee = await employeeRepository.Queryable().Where(x => x.ShortName == shortname).FirstOrDefaultAsync();
      if (employee == null)
      {
        throw new Exception("not found ForeignKey:EmployeeId with " + shortname);
      }
      else
      {
        return employee.Id;
      }
    }
    public async Task ImportDataTable(DataTable datatable, string username)
    {
      var mapping = await this.mappingservice.Queryable()
                        .Where(x => x.EntitySetName == "CheckOut" &&
                           ( x.IsEnabled == true || ( x.IsEnabled == false && x.DefaultValue != null ) )
                           ).ToListAsync();
      if (mapping.Count == 0)
      {
        throw new KeyNotFoundException("没有找到CheckOut对象的Excel导入配置信息，请执行[系统管理/Excel导入配置]");
      }
      foreach (DataRow row in datatable.Rows)
      {

        var requiredfield = mapping.Where(x => x.IsRequired == true && x.IsEnabled == true && x.DefaultValue == null).FirstOrDefault()?.SourceFieldName;
        if (requiredfield != null ||
              ( !row.IsNull(requiredfield) &&
               !string.IsNullOrEmpty(row[requiredfield].ToString())
              )
            )
        {
          var item = new CheckOut();
          var checkouttype = item.GetType();
          foreach (var field in mapping)
          {
            var defval = field.DefaultValue;
            var contain = datatable.Columns.Contains(field.SourceFieldName ?? "");
            if (contain &&
                           !row.IsNull(field.SourceFieldName) &&
                           !string.IsNullOrEmpty(row[field.SourceFieldName].ToString())
                        )
            {
              var propertyInfo = checkouttype.GetProperty(field.FieldName);
              //关联外键查询获取Id
              switch (field.FieldName)
              {
                case "BookId":
                  var book_title = row[field.SourceFieldName].ToString();
                  var bookid = await this.getBookIdByTitle(book_title);
                  propertyInfo.SetValue(item, Convert.ChangeType(bookid, propertyInfo.PropertyType), null);
                  break;
                case "EmployeeId":
                  var employee_shortname = row[field.SourceFieldName].ToString();
                  var employeeid = await this.getEmployeeIdByShortName(employee_shortname);
                  propertyInfo.SetValue(item, Convert.ChangeType(employeeid, propertyInfo.PropertyType), null);
                  break;
                default:
                  var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                  var safeValue = Convert.ChangeType(row[field.SourceFieldName], safetype);
                  propertyInfo.SetValue(item, safeValue, null);
                  break;
              }
            }
            else if (!string.IsNullOrEmpty(defval))
            {
              var propertyInfo = checkouttype.GetProperty(field.FieldName);
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
          this.Insert(item);
        }
      }
    }
    public async Task<Stream> ExportExcel(string filterRules = "", string sort = "Id", string order = "asc")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var expcolopts = await this.mappingservice.Queryable()
             .Where(x => x.EntitySetName == "CheckOut")
             .Select(x => new ExpColumnOpts()
             {
               EntitySetName = x.EntitySetName,
               FieldName = x.FieldName,
               IgnoredColumn = x.IgnoredColumn,
               SourceFieldName = x.SourceFieldName
             }).ToArrayAsync();

      var checkouts = await this.Query(new CheckOutQuery().Withfilter(filters)).Include(p => p.Book).Include(p => p.Employee).OrderBy(n => n.OrderBy(sort, order)).SelectAsync();

      var datarows = checkouts.Select(n => new
      {

        BookTitle = n.Book?.Title,
        EmployeeShortName = n.Employee?.ShortName,
        Id = n.Id,
        EmployeeId = n.EmployeeId,
        GlobalId = n.GlobalId,
        ShortName = n.ShortName,
        DisplayName = n.DisplayName,
        BookId = n.BookId,
        BarCode = n.BarCode,
        ISBN = n.ISBN,
        Title = n.Title,
        Qty = n.Qty,
        BorrowDate = n.BorrowDate.ToString("yyyy-MM-dd HH:mm:ss"),
        BackDate = n.BackDate?.ToString("yyyy-MM-dd HH:mm:ss"),
        ExpiryDate = n.ExpiryDate?.ToString("yyyy-MM-dd HH:mm:ss"),
        Days = n.Days,
        Status = n.Status,
        Notified = n.Notified,
        Expiry = n.Expiry
      }).ToList();
      return await NPOIHelper.ExportExcelAsync("Check Out", datarows, expcolopts);
    }
    public async Task Delete(int[] id)
    {
      var items = await this.Queryable().Where(x => id.Contains(x.Id)).ToListAsync();
      foreach (var item in items)
      {
        this.Delete(item);
      }

    }

    public async Task<IEnumerable<CheckOut>> GetPageData(int page, int size)
    {
        var result = await this.Queryable().OrderByDescending(x => x.Id)
          .Skip(page * size)
          .Take(size)
          .Include(x => x.Book)
          .ToListAsync();
        return result;
       
    }

    public async Task Borrow(BorrowInputDto inputdto)
    {
      var isExist = await this.isExists(inputdto.GlobalId);
      if (!isExist)
      {
        throw new Exception($"{inputdto.GlobalId},员工不存在,不允许借书.");
      }
      var isAvailable = await this.isAvailable(inputdto.BarCode);
      if (!isAvailable)
      {
        throw new Exception($"{inputdto.BarCode},图书不存在,请联系管理员.");
      }
      var emp =await this.employeeService.Queryable().Where(x => x.GlobalId == inputdto.GlobalId).FirstAsync();
      var stock=await this.stockService.Queryable().Where(x => ( x.BarCode == inputdto.BarCode || x.ISBN == inputdto.BarCode ) && x.Qty > 0)
        .Include(x=>x.Book)
        .FirstAsync();

      var checkout = new CheckOut();
      checkout.GlobalId = emp.GlobalId;
      checkout.Employee = emp;
      checkout.EmployeeId = emp.Id;
      checkout.Phone = inputdto.Phone;
      checkout.ShortName = emp.ShortName;
      checkout.DisplayName = emp.DisplayName;
      checkout.BorrowDate = DateTime.Now;
      checkout.ExpiryDate = DateTime.Now.AddMonths(1);
      checkout.Expiry = false;
      checkout.Days = ( checkout.ExpiryDate.Value - checkout.BorrowDate ).Days;
      checkout.Qty = 1;
      checkout.Book = stock.Book;
      checkout.BookId = stock.BookId;
      checkout.BarCode = stock.BarCode;
      checkout.ISBN = stock.ISBN;
      checkout.Status = "Pending";
      checkout.Title = stock.Title;
      stock.Qty = stock.Qty - 1;
      
      if (stock.Qty == 0)
      {
        stock.Status = "Out of Stock";
      }

      emp.Phone = inputdto.Phone;
      var book =await this.bookService.FindAsync(stock.BookId);
      book.Reads = book.Reads + 1;
      this.bookService.Update(book);
      this.employeeService.Update(emp);
      this.stockService.Update(stock);
      this.Insert(checkout);
      this.logger.Info($"Borrow:{inputdto.GlobalId},{emp.DisplayName},{inputdto.BarCode},{stock.Title}");
    }
    private async Task<bool> isExists(int globalid)
    {
      return await this.employeeService.Queryable().Where(x => x.GlobalId == globalid).AnyAsync();
    }
    private async Task<bool> isAvailable(string barcode)
    {
      return await this.stockService.Queryable().Where(x => (x.BarCode == barcode || x.ISBN == barcode) && x.Qty > 0).AnyAsync();
    }
    private async Task<bool> isReturningAvailable(string barcode)
    {
      return await this.Queryable().Where(x => ( x.BarCode == barcode || x.ISBN == barcode ) && x.Status== "Pending").AnyAsync();
    }
    public async Task Returning(RetuningInputDto inputdto) {
      var isExist = await this.isExists(inputdto.GlobalId);
      if (!isExist)
      {
        throw new Exception($"{inputdto.GlobalId},员工不存在,请检查.");
      }
      var isAvailable = await this.isReturningAvailable(inputdto.BarCode);
      if (!isAvailable)
      {
        throw new Exception($"{inputdto.BarCode},没有找到借书记录,请联系管理员.");
      }
      var checkout=await this.Queryable().Where(x => ( x.BarCode == inputdto.BarCode || x.ISBN == inputdto.BarCode ) && x.Status == "Pending").FirstAsync();
      var stock = await this.stockService.Queryable().Where(x => ( x.BarCode == inputdto.BarCode || x.ISBN == inputdto.BarCode ))
         .FirstAsync();
      stock.Qty = stock.Qty + checkout.Qty;
      this.stockService.Update(stock);
      checkout.BackDate = DateTime.Now;
      checkout.BackQty = checkout.Qty;
      checkout.Status = "Returned";
      checkout.Days = ( checkout.BackDate.Value - checkout.BorrowDate ).Days;
      this.Update(checkout);

    }
  }
}