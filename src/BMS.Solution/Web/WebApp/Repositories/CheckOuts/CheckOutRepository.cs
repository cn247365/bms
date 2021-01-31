using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Repository.Pattern.Repositories;
using System.Threading.Tasks;
using WebApp.Models;
namespace WebApp.Repositories
{
/// <summary>
/// File: CheckOutRepository.cs
/// Purpose: The repository and unit of work patterns are intended
/// to create an abstraction layer between the data access layer and
/// the business logic layer of an application.
/// Created Date: 1/30/2021 9:53:15 PM
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
  public static class CheckOutRepository  
    {
                 public static async Task<IEnumerable<CheckOut>> GetByEmployeeId(this IRepositoryAsync<CheckOut> repository, int employeeid)
          => await repository
                .Queryable()
                .Where(x => x.EmployeeId==employeeid).ToListAsync();
              
          
                 public static async Task<IEnumerable<CheckOut>> GetByBookId(this IRepositoryAsync<CheckOut> repository, int bookid)
          => await repository
                .Queryable()
                .Where(x => x.BookId==bookid).ToListAsync();
              
          
                 
	}
}



