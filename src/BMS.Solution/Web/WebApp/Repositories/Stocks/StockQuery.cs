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
/// File: StockQuery.cs
/// Purpose: easyui datagrid filter query 
/// Created Date: 1/31/2021 9:28:20 AM
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
   public class StockQuery:QueryObject<Stock>
   {
		public StockQuery Withfilter(IEnumerable<filterRule> filters)
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
						if (rule.field == "BarCode"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.BarCode.Contains(rule.value));
						}
						if (rule.field == "ISBN"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ISBN.Contains(rule.value));
						}
						if (rule.field == "Title"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Title.Contains(rule.value));
						}
						if (rule.field == "Remark"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Remark.Contains(rule.value));
						}
						if (rule.field == "Qty" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Qty == val);
                                break;
                            case "notequal":
                                And(x => x.Qty != val);
                                break;
                            case "less":
                                And(x => x.Qty < val);
                                break;
                            case "lessorequal":
                                And(x => x.Qty <= val);
                                break;
                            case "greater":
                                And(x => x.Qty > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Qty >= val);
                                break;
                            default:
                                And(x => x.Qty == val);
                                break;
                        }
						}
						if (rule.field == "Price" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Price == val);
                                break;
                            case "notequal":
                                And(x => x.Price != val);
                                break;
                            case "less":
                                And(x => x.Price < val);
                                break;
                            case "lessorequal":
                                And(x => x.Price <= val);
                                break;
                            case "greater":
                                And(x => x.Price > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Price >= val);
                                break;
                            default:
                                And(x => x.Price == val);
                                break;
                        }
						}
						if (rule.field == "PurchasingPrice" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.PurchasingPrice == val);
                                break;
                            case "notequal":
                                And(x => x.PurchasingPrice != val);
                                break;
                            case "less":
                                And(x => x.PurchasingPrice < val);
                                break;
                            case "lessorequal":
                                And(x => x.PurchasingPrice <= val);
                                break;
                            case "greater":
                                And(x => x.PurchasingPrice > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.PurchasingPrice >= val);
                                break;
                            default:
                                And(x => x.PurchasingPrice == val);
                                break;
                        }
						}
						if (rule.field == "PurchaseDate" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.PurchaseDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.PurchaseDate) <= 0);
						    }
						}
						if (rule.field == "UserName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.UserName.Contains(rule.value));
						}
						if (rule.field == "Catetory"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Catetory.Contains(rule.value));
						}
						if (rule.field == "Location"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Location.Contains(rule.value));
						}
						if (rule.field == "Shelves"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Shelves.Contains(rule.value));
						}
						if (rule.field == "Tags"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Tags.Contains(rule.value));
						}
						if (rule.field == "Status"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Status.Contains(rule.value));
						}
						if (rule.field == "Trades" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Trades == val);
                                break;
                            case "notequal":
                                And(x => x.Trades != val);
                                break;
                            case "less":
                                And(x => x.Trades < val);
                                break;
                            case "lessorequal":
                                And(x => x.Trades <= val);
                                break;
                            case "greater":
                                And(x => x.Trades > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Trades >= val);
                                break;
                            default:
                                And(x => x.Trades == val);
                                break;
                        }
						}
						if (rule.field == "InputDate" && !string.IsNullOrEmpty(rule.value) )
						{	
							if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.InputDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.InputDate) <= 0);
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
         public  StockQuery ByBookIdWithfilter(int bookid, IEnumerable<filterRule> filters)
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
						if (rule.field == "BarCode"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.BarCode == rule.value);
                           } 
                           else
                           {
							And(x => x.BarCode.Contains(rule.value));
						    }
                        }
						if (rule.field == "ISBN"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.ISBN == rule.value);
                           } 
                           else
                           {
							And(x => x.ISBN.Contains(rule.value));
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
						if (rule.field == "Remark"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Remark == rule.value);
                           } 
                           else
                           {
							And(x => x.Remark.Contains(rule.value));
						    }
                        }
						if (rule.field == "Qty" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Qty == val);
                                break;
                            case "notequal":
                                And(x => x.Qty != val);
                                break;
                            case "less":
                                And(x => x.Qty < val);
                                break;
                            case "lessorequal":
                                And(x => x.Qty <= val);
                                break;
                            case "greater":
                                And(x => x.Qty > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Qty >= val);
                                break;
                            default:
                                And(x => x.Qty == val);
                                break;
                        }
						}
						if (rule.field == "Price" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Price == val);
                                break;
                            case "notequal":
                                And(x => x.Price != val);
                                break;
                            case "less":
                                And(x => x.Price < val);
                                break;
                            case "lessorequal":
                                And(x => x.Price <= val);
                                break;
                            case "greater":
                                And(x => x.Price > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Price >= val);
                                break;
                            default:
                                And(x => x.Price == val);
                                break;
                        }
						}
						if (rule.field == "PurchasingPrice" && !string.IsNullOrEmpty(rule.value) && rule.value.IsDecimal())
						{
							var val = Convert.ToDecimal(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.PurchasingPrice == val);
                                break;
                            case "notequal":
                                And(x => x.PurchasingPrice != val);
                                break;
                            case "less":
                                And(x => x.PurchasingPrice < val);
                                break;
                            case "lessorequal":
                                And(x => x.PurchasingPrice <= val);
                                break;
                            case "greater":
                                And(x => x.PurchasingPrice > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.PurchasingPrice >= val);
                                break;
                            default:
                                And(x => x.PurchasingPrice == val);
                                break;
                        }
						}
						if (rule.field == "PurchaseDate" && !string.IsNullOrEmpty(rule.value) )
						{	
                            if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.PurchaseDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.PurchaseDate) <= 0);
						    }
                        }
						if (rule.field == "UserName"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.UserName == rule.value);
                           } 
                           else
                           {
							And(x => x.UserName.Contains(rule.value));
						    }
                        }
						if (rule.field == "Catetory"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Catetory == rule.value);
                           } 
                           else
                           {
							And(x => x.Catetory.Contains(rule.value));
						    }
                        }
						if (rule.field == "Location"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Location == rule.value);
                           } 
                           else
                           {
							And(x => x.Location.Contains(rule.value));
						    }
                        }
						if (rule.field == "Shelves"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Shelves == rule.value);
                           } 
                           else
                           {
							And(x => x.Shelves.Contains(rule.value));
						    }
                        }
						if (rule.field == "Tags"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Tags == rule.value);
                           } 
                           else
                           {
							And(x => x.Tags.Contains(rule.value));
						    }
                        }
						if (rule.field == "Status"  && !string.IsNullOrEmpty(rule.value))
						{
                           if (rule.op == "equal")
                           {
                             And(x => x.Status == rule.value);
                           } 
                           else
                           {
							And(x => x.Status.Contains(rule.value));
						    }
                        }
						if (rule.field == "Trades" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Trades == val);
                                break;
                            case "notequal":
                                And(x => x.Trades != val);
                                break;
                            case "less":
                                And(x => x.Trades < val);
                                break;
                            case "lessorequal":
                                And(x => x.Trades <= val);
                                break;
                            case "greater":
                                And(x => x.Trades > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Trades >= val);
                                break;
                            default:
                                And(x => x.Trades == val);
                                break;
                        }
						}
						if (rule.field == "InputDate" && !string.IsNullOrEmpty(rule.value) )
						{	
                            if (rule.op == "between")
                            {
                                var datearray = rule.value.Split(new char[] { '-' });
                                var start = Convert.ToDateTime(datearray[0]);
                                var end = Convert.ToDateTime(datearray[1]);
 
							    And(x => SqlFunctions.DateDiff("d", start, x.InputDate) >= 0);
                                And(x => SqlFunctions.DateDiff("d", end, x.InputDate) <= 0);
						    }
                        }
               }
            }
            return this;
         }    
    }
}
