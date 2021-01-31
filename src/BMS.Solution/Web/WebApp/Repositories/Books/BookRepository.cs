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
/// File: BookRepository.cs
/// Purpose: The repository and unit of work patterns are intended
/// to create an abstraction layer between the data access layer and
/// the business logic layer of an application.
/// Created Date: 1/31/2021 9:30:43 AM
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
  public static class BookRepository  
    {
                        public static async Task<IEnumerable<BookPicture>>   GetBookPicturesByBookId (this IRepositoryAsync<Book> repository,int bookid)
          => await  repository.GetRepositoryAsync<BookPicture>()
                    .Queryable()
                    .Include(x => x.Book)
                    .Where(n => n.BookId == bookid)
                    .ToListAsync();
        
                public static async Task<IEnumerable<CheckOut>>   GetCheckOutsByBookId (this IRepositoryAsync<Book> repository,int bookid)
          => await  repository.GetRepositoryAsync<CheckOut>()
                    .Queryable()
                    .Include(x => x.Book).Include(x => x.Employee)
                    .Where(n => n.BookId == bookid)
                    .ToListAsync();
        
                public static async Task<IEnumerable<Comment>>   GetCommentsByBookId (this IRepositoryAsync<Book> repository,int bookid)
          => await  repository.GetRepositoryAsync<Comment>()
                    .Queryable()
                    .Include(x => x.Book).Include(x => x.Parent)
                    .Where(n => n.BookId == bookid)
                    .ToListAsync();
        
         
	}
}



