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
/// File: EmployeeRepository.cs
/// Purpose: The repository and unit of work patterns are intended
/// to create an abstraction layer between the data access layer and
/// the business logic layer of an application.
/// Created Date: 1/30/2021 7:58:40 PM
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
  public static class EmployeeRepository  
    {
                        public static async Task<IEnumerable<CheckOut>>   GetCheckOutsByEmployeeId (this IRepositoryAsync<Employee> repository,int employeeid)
          => await  repository.GetRepositoryAsync<CheckOut>()
                    .Queryable()
                    .Include(x => x.Book).Include(x => x.Employee)
                    .Where(n => n.EmployeeId == employeeid)
                    .ToListAsync();
        
         
	}
}



