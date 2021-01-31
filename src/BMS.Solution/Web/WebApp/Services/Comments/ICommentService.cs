﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Repository.Pattern.Repositories;
using System.Threading.Tasks;
using Service.Pattern;
using WebApp.Models;
using WebApp.Repositories;
using System.Data;
using System.IO;
namespace WebApp.Services
{
/// <summary>
/// File: ICommentService.cs
/// Purpose: Service interfaces. Services expose a service interface
/// to which all inbound messages are sent. You can think of a service interface
/// as a façade that exposes the business logic implemented in the application
/// Created Date: 1/30/2021 9:47:25 PM
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    public interface ICommentService:IService<Comment>
    {
         Task<IEnumerable<Comment>> GetByParentId(int  parentid);
         Task<IEnumerable<Comment>> GetByBookId(int  bookid);
 
		Task ImportDataTable(DataTable datatable,string username="");
		Task<Stream> ExportExcel( string filterRules = "",string sort = "Id", string order = "asc");
	    Task Delete(int[] id);
    }
}