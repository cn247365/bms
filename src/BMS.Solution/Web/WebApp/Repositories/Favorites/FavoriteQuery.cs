using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity.SqlServer;
using Repository.Pattern.Repositories;
using Repository.Pattern.Ef6;
using System.Web.WebPages;
using WebApp.Models;

namespace WebApp.Repositories
{
/// <summary>
/// File: FavoriteQuery.cs
/// Purpose: easyui datagrid filter query 
/// Created Date: 1/30/2021 9:49:57 PM
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
   public class FavoriteQuery:QueryObject<Favorite>
   {
		public FavoriteQuery Withfilter(IEnumerable<filterRule> filters)
        {
           if (filters != null)
           {
               foreach (var rule in filters)
               {
						if (rule.field == "Id" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Id == val);
                                break;
                            case "notequal":
                                And(x => x.Id != val);
                                break;
                            case "less":
                                And(x => x.Id < val);
                                break;
                            case "lessorequal":
                                And(x => x.Id <= val);
                                break;
                            case "greater":
                                And(x => x.Id > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Id >= val);
                                break;
                            default:
                                And(x => x.Id == val);
                                break;
                        }
						}
						if (rule.field == "GlobalId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.GlobalId == val);
                                break;
                            case "notequal":
                                And(x => x.GlobalId != val);
                                break;
                            case "less":
                                And(x => x.GlobalId < val);
                                break;
                            case "lessorequal":
                                And(x => x.GlobalId <= val);
                                break;
                            case "greater":
                                And(x => x.GlobalId > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.GlobalId >= val);
                                break;
                            default:
                                And(x => x.GlobalId == val);
                                break;
                        }
						}
						if (rule.field == "ShortName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ShortName.Contains(rule.value));
						}
						if (rule.field == "DisplayName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.DisplayName.Contains(rule.value));
						}
						if (rule.field == "BookId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.BookId == val);
                                break;
                            case "notequal":
                                And(x => x.BookId != val);
                                break;
                            case "less":
                                And(x => x.BookId < val);
                                break;
                            case "lessorequal":
                                And(x => x.BookId <= val);
                                break;
                            case "greater":
                                And(x => x.BookId > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.BookId >= val);
                                break;
                            default:
                                And(x => x.BookId == val);
                                break;
                        }
						}
						if (rule.field == "Title"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Title.Contains(rule.value));
						}
						if (rule.field == "AddDate" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.AddDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.AddDate) <= 0);
						    }
						}
						if (rule.field == "CreatedDate" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.CreatedDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.CreatedDate) <= 0);
						    }
						}
						if (rule.field == "CreatedBy"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CreatedBy.Contains(rule.value));
						}
						if (rule.field == "LastModifiedDate" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.LastModifiedDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.LastModifiedDate) <= 0);
						    }
						}
						if (rule.field == "LastModifiedBy"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LastModifiedBy.Contains(rule.value));
						}
						if (rule.field == "TenantId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.TenantId == val);
                                break;
                            case "notequal":
                                And(x => x.TenantId != val);
                                break;
                            case "less":
                                And(x => x.TenantId < val);
                                break;
                            case "lessorequal":
                                And(x => x.TenantId <= val);
                                break;
                            case "greater":
                                And(x => x.TenantId > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.TenantId >= val);
                                break;
                            default:
                                And(x => x.TenantId == val);
                                break;
                        }
						}
     
               }
           }
            return this;
        }
         public  FavoriteQuery ByBookIdWithfilter(int bookid, IEnumerable<filterRule> filters)
         {
            And(x => x.BookId == bookid);
            if (filters != null)
           {
               foreach (var rule in filters)
               {     
						if (rule.field == "Id" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Id == val);
                                break;
                            case "notequal":
                                And(x => x.Id != val);
                                break;
                            case "less":
                                And(x => x.Id < val);
                                break;
                            case "lessorequal":
                                And(x => x.Id <= val);
                                break;
                            case "greater":
                                And(x => x.Id > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Id >= val);
                                break;
                            default:
                                And(x => x.Id == val);
                                break;
                        }
						}
						if (rule.field == "GlobalId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.GlobalId == val);
                                break;
                            case "notequal":
                                And(x => x.GlobalId != val);
                                break;
                            case "less":
                                And(x => x.GlobalId < val);
                                break;
                            case "lessorequal":
                                And(x => x.GlobalId <= val);
                                break;
                            case "greater":
                                And(x => x.GlobalId > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.GlobalId >= val);
                                break;
                            default:
                                And(x => x.GlobalId == val);
                                break;
                        }
						}
						if (rule.field == "ShortName"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.ShortName == rule.value);
                           } 
                           else
                           {
							And(x => x.ShortName.Contains(rule.value));
						    }
                        }
						if (rule.field == "DisplayName"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.DisplayName == rule.value);
                           } 
                           else
                           {
							And(x => x.DisplayName.Contains(rule.value));
						    }
                        }
						if (rule.field == "BookId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.BookId == val);
                                break;
                            case "notequal":
                                And(x => x.BookId != val);
                                break;
                            case "less":
                                And(x => x.BookId < val);
                                break;
                            case "lessorequal":
                                And(x => x.BookId <= val);
                                break;
                            case "greater":
                                And(x => x.BookId > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.BookId >= val);
                                break;
                            default:
                                And(x => x.BookId == val);
                                break;
                        }
						}
						if (rule.field == "Title"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Title == rule.value);
                           } 
                           else
                           {
							And(x => x.Title.Contains(rule.value));
						    }
                        }
						if (rule.field == "AddDate" && !string.IsNullOrEmpty(rule.value) )
						{	
                            if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.AddDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.AddDate) <= 0);
						    }
                        }
               }
            }
            return this;
         }    
    }
}
