﻿using System;
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

namespace WebApp.Services
{
/// <summary>
/// File: CommentService.cs
/// Purpose: Within the service layer, you define and implement 
/// the service interface and the data contracts (or message types).
/// One of the more important concepts to keep in mind is that a service
/// should never expose details of the internal processes or 
/// the business entities used within the application. 
/// Created Date: 1/30/2021 9:47:26 PM
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    public class CommentService : Service< Comment >, ICommentService
    {
        private readonly IRepositoryAsync<Comment> repository;
		private readonly IDataTableImportMappingService mappingservice;
        private readonly NLog.ILogger logger;
        public  CommentService(
          IRepositoryAsync< Comment> repository,
          IDataTableImportMappingService mappingservice,
          NLog.ILogger logger
          )
            : base(repository)
        {
            this.repository=repository;
			this.mappingservice = mappingservice;
            this.logger = logger;
        }
                 public async  Task<IEnumerable<Comment>> GetByParentId(int  parentid) => await repository.GetByParentId(parentid);
                  public async  Task<IEnumerable<Comment>> GetByBookId(int  bookid) => await repository.GetByBookId(bookid);
                   
        
        		 
                private async Task<int> getBookIdByTitle(string title)
        {
            var bookRepository = this.repository.GetRepositoryAsync<Book>();
            var book = await  bookRepository.Queryable().Where(x => x.Title == title).FirstOrDefaultAsync();
            if (book == null)
            {
                throw new Exception("not found ForeignKey:BookId with " + title);
            }
            else
            {
                return book.Id;
            }
        }
                private async Task<int> getParentIdByContent(string content)
        {
            var commentRepository = this.repository.GetRepositoryAsync<Comment>();
            var comment = await  commentRepository.Queryable().Where(x => x.Content == content).FirstOrDefaultAsync();
            if (comment == null)
            {
                throw new Exception("not found ForeignKey:ParentId with " + content);
            }
            else
            {
                return comment.Id;
            }
        }
                public async Task ImportDataTable(DataTable datatable,string username)
        {
            var mapping = await this.mappingservice.Queryable()
                              .Where(x => x.EntitySetName == "Comment" && 
                                 (x.IsEnabled == true  || (x.IsEnabled == false &&  x.DefaultValue != null))
                                 ).ToListAsync();
            if (mapping.Count == 0)
            {
                throw new KeyNotFoundException("没有找到Comment对象的Excel导入配置信息，请执行[系统管理/Excel导入配置]");
            }
            foreach (DataRow row in datatable.Rows)
            {
                
                var requiredfield = mapping.Where(x => x.IsRequired == true && x.IsEnabled==true && x.DefaultValue==null).FirstOrDefault()?.SourceFieldName;
                if (requiredfield != null ||
                      (!row.IsNull(requiredfield) &&
                       !string.IsNullOrEmpty(row[requiredfield].ToString())
                      )
                    )
                {
                    var item = new Comment();
                    var commenttype = item.GetType();
                    foreach (var field in mapping)
                    {
						var defval = field.DefaultValue;
						var contain = datatable.Columns.Contains(field.SourceFieldName ?? "");
						if (contain &&
                           !row.IsNull(field.SourceFieldName) &&
                           !string.IsNullOrEmpty(row[field.SourceFieldName].ToString())
                        )
						{
							var propertyInfo = commenttype.GetProperty(field.FieldName);
                                                        //关联外键查询获取Id
                            switch (field.FieldName) {
                                                                 case "BookId":
                                     var book_title =  row[field.SourceFieldName].ToString();
                                     var bookid = await this.getBookIdByTitle(book_title);
                                     propertyInfo.SetValue(item, Convert.ChangeType(bookid, propertyInfo.PropertyType), null);
                                     break;
                                                                case "ParentId":
                                     var comment_content =  row[field.SourceFieldName].ToString();
                                     var parentid = await this.getParentIdByContent(comment_content);
                                     propertyInfo.SetValue(item, Convert.ChangeType(parentid, propertyInfo.PropertyType), null);
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
							var propertyInfo = commenttype.GetProperty(field.FieldName);
							if (string.Equals(defval, "now", StringComparison.OrdinalIgnoreCase) && (propertyInfo.PropertyType ==typeof(DateTime) || propertyInfo.PropertyType == typeof(Nullable<DateTime>)))
                            {
                                var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                                var safeValue = Convert.ChangeType(DateTime.Now, safetype);
                                propertyInfo.SetValue(item, safeValue, null);
                            }
                            else if(string.Equals(defval, "guid", StringComparison.OrdinalIgnoreCase))
                            {
                                propertyInfo.SetValue(item, Guid.NewGuid().ToString(), null);
                            }
                            else if(string.Equals(defval, "user", StringComparison.OrdinalIgnoreCase))
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
				public async Task<Stream> ExportExcel(string filterRules = "",string sort = "Id", string order = "asc")
        {
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            var expcolopts= await this.mappingservice.Queryable()
                   .Where(x => x.EntitySetName == "Comment")
                   .Select(x =>new ExpColumnOpts()
                   {
                      EntitySetName = x.EntitySetName,
                      FieldName = x.FieldName,
                      IgnoredColumn=x.IgnoredColumn,
                      SourceFieldName=x.SourceFieldName
                   }).ToArrayAsync();
            
            var comments  = await this.Query(new CommentQuery().Withfilter(filters)).Include(p => p.Book).Include(p => p.Parent).OrderBy(n=>n.OrderBy(sort,order)).SelectAsync();
            
            var datarows = comments .Select(  n => new { 

    BookTitle = n.Book?.Title,
    ParentContent = n.Parent?.Content,
    Id = n.Id,
    Content = n.Content,
    UserName = n.UserName,
    DisplayName = n.DisplayName,
    PublishDate = n.PublishDate.ToString("yyyy-MM-dd HH:mm:ss"),
    Evaluate = n.Evaluate,
    ParentId = n.ParentId,
    BookId = n.BookId
}).ToList();
            return await NPOIHelper.ExportExcelAsync("Comment", datarows,expcolopts);
        }
        public async Task Delete(int[] id) {
            var items = await this.Queryable().Where(x => id.Contains(x.Id)).ToListAsync();
            foreach (var item in items)
            {
               this.Delete(item);
            }

        }
    }
}