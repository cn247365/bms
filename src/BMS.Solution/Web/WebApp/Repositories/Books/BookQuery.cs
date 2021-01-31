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
/// File: BookQuery.cs
/// Purpose: easyui datagrid filter query 
/// Created Date: 1/31/2021 9:30:43 AM
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
   public class BookQuery:QueryObject<Book>
   {
		public BookQuery Withfilter(IEnumerable<filterRule> filters)
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
						if (rule.field == "Title"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Title.Contains(rule.value));
						}
						if (rule.field == "SubTitle"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.SubTitle.Contains(rule.value));
						}
						if (rule.field == "Pic"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Pic.Contains(rule.value));
						}
						if (rule.field == "LocalPic"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.LocalPic.Contains(rule.value));
						}
						if (rule.field == "Author"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Author.Contains(rule.value));
						}
						if (rule.field == "Summary"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Summary.Contains(rule.value));
						}
						if (rule.field == "Publisher"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Publisher.Contains(rule.value));
						}
						if (rule.field == "Pubplace"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Pubplace.Contains(rule.value));
						}
						if (rule.field == "Page" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Page == val);
                                break;
                            case "notequal":
                                And(x => x.Page != val);
                                break;
                            case "less":
                                And(x => x.Page < val);
                                break;
                            case "lessorequal":
                                And(x => x.Page <= val);
                                break;
                            case "greater":
                                And(x => x.Page > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Page >= val);
                                break;
                            default:
                                And(x => x.Page == val);
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
						if (rule.field == "Binding"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Binding.Contains(rule.value));
						}
						if (rule.field == "ISBN"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ISBN.Contains(rule.value));
						}
						if (rule.field == "ISBN10"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ISBN10.Contains(rule.value));
						}
						if (rule.field == "Keyword"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Keyword.Contains(rule.value));
						}
						if (rule.field == "Edition"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Edition.Contains(rule.value));
						}
						if (rule.field == "Impression"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Impression.Contains(rule.value));
						}
						if (rule.field == "Language"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Language.Contains(rule.value));
						}
						if (rule.field == "Format"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Format.Contains(rule.value));
						}
						if (rule.field == "Class"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Class.Contains(rule.value));
						}
						if (rule.field == "CIP"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CIP.Contains(rule.value));
						}
						if (rule.field == "Reads" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Reads == val);
                                break;
                            case "notequal":
                                And(x => x.Reads != val);
                                break;
                            case "less":
                                And(x => x.Reads < val);
                                break;
                            case "lessorequal":
                                And(x => x.Reads <= val);
                                break;
                            case "greater":
                                And(x => x.Reads > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Reads >= val);
                                break;
                            default:
                                And(x => x.Reads == val);
                                break;
                        }
						}
						if (rule.field == "Popular" && !string.IsNullOrEmpty(rule.value) && rule.value.IsInt())
						{
							var val = Convert.ToInt32(rule.value);
							switch (rule.op) {
                            case "equal":
                                And(x => x.Popular == val);
                                break;
                            case "notequal":
                                And(x => x.Popular != val);
                                break;
                            case "less":
                                And(x => x.Popular < val);
                                break;
                            case "lessorequal":
                                And(x => x.Popular <= val);
                                break;
                            case "greater":
                                And(x => x.Popular > val);
                                break;
                            case "greaterorequal" :
                                And(x => x.Popular >= val);
                                break;
                            default:
                                And(x => x.Popular == val);
                                break;
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
    }
}
