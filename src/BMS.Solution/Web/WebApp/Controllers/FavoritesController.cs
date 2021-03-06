﻿using System;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Infrastructure;
using Z.EntityFramework.Plus;
using TrackableEntities;
using WebApp.Models;
using WebApp.Services;
using WebApp.Repositories;
namespace WebApp.Controllers
{
/// <summary>
/// File: FavoritesController.cs
/// Purpose:Books/Favorite
/// Created Date: 1/30/2021 9:49:57 PM
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<Favorite>, Repository<Favorite>>();
///    container.RegisterType<IFavoriteService, FavoriteService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("Favorites")]
	public class FavoritesController : Controller
	{
		private readonly IFavoriteService  favoriteService;
		private readonly IUnitOfWorkAsync unitOfWork;
        private readonly NLog.ILogger logger;
		public FavoritesController (
          IFavoriteService  favoriteService, 
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
		{
			this.favoriteService  = favoriteService;
			this.unitOfWork = unitOfWork;
            this.logger = logger;
		}
        		//GET: Favorites/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "Favorite", Order = 1)]
		public ActionResult Index() => this.View();

		//Get :Favorites/GetData
		//For Index View datagrid datasource url
        
		[HttpPost]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
		 public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.favoriteService
						               .Query(new FavoriteQuery().Withfilter(filters)).Include(f => f.Book)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    BookTitle = n.Book?.Title,
    Id = n.Id,
    GlobalId = n.GlobalId,
    ShortName = n.ShortName,
    DisplayName = n.DisplayName,
    BookId = n.BookId,
    Title = n.Title,
    AddDate = n.AddDate.ToString("yyyy-MM-dd HH:mm:ss")
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
			return Json(pagelist, JsonRequestBehavior.AllowGet);
		}
        [HttpPost]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
        public async Task<JsonResult> GetDataByBookId (int  bookid ,int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {    
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			    var pagerows = (await this.favoriteService
						               .Query(new FavoriteQuery().ByBookIdWithfilter(bookid,filters)).Include(f => f.Book)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    BookTitle = n.Book?.Title,
    Id = n.Id,
    GlobalId = n.GlobalId,
    ShortName = n.ShortName,
    DisplayName = n.DisplayName,
    BookId = n.BookId,
    Title = n.Title,
    AddDate = n.AddDate.ToString("yyyy-MM-dd HH:mm:ss")
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }
        //easyui datagrid post acceptChanges 
		[HttpPost]
		public async Task<JsonResult> AcceptChanges(Favorite[] favorites)
		{
            try{
               this.favoriteService.ApplyChanges( favorites);
               var result = await this.unitOfWork.SaveChangesAsync();
			   return Json(new {success=true,result}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
            }
        }
				//[OutputCache(Duration = 10, VaryByParam = "q")]
		public async Task<JsonResult> GetBooks(string q="")
		{
			var bookRepository = this.unitOfWork.RepositoryAsync<Book>();
			var rows = await bookRepository
                            .Queryable()
                            .Where(n=>n.Title.Contains(q))
                            .OrderBy(n=>n.Title)
                            .Select(n => new { Id = n.Id, Title = n.Title })
                            .ToListAsync();
			return Json(rows, JsonRequestBehavior.AllowGet);
		}
		 
				
		//GET: Favorites/Details/:id
		public ActionResult Details(int id)
		{
			
			var favorite = this.favoriteService.Find(id);
			if (favorite == null)
			{
				return HttpNotFound();
			}
			return View(favorite);
		}
        //GET: Favorites/GetItem/:id
        [HttpGet]
        public async Task<JsonResult> GetItem(int id) {
            var  favorite = await this.favoriteService.FindAsync(id);
            return Json(favorite,JsonRequestBehavior.AllowGet);
        }
		//GET: Favorites/Create
        		public ActionResult Create()
				{
			var favorite = new Favorite();
			//set default value
			var bookRepository = this.unitOfWork.RepositoryAsync<Book>();
		   			ViewBag.BookId = new SelectList(bookRepository.Queryable().OrderBy(n=>n.Title), "Id", "Title");
		   			return View(favorite);
		}
		//POST: Favorites/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(Favorite favorite)
		{
            if (ModelState.IsValid)
			{
                try{ 
				this.favoriteService.Insert(favorite);
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
			    //DisplaySuccessMessage("Has update a favorite record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//var bookRepository = this.unitOfWork.RepositoryAsync<Book>();
			//ViewBag.BookId = new SelectList(await bookRepository.Queryable().OrderBy(n=>n.Title).ToListAsync(), "Id", "Title", favorite.BookId);
			//return View(favorite);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItem() {
            var favorite = await Task.Run(() => {
                return new Favorite();
                });
            return Json(favorite, JsonRequestBehavior.AllowGet);
        }

         
		//GET: Favorites/Edit/:id
		public ActionResult Edit(int id)
		{
			var favorite = this.favoriteService.Find(id);
			if (favorite == null)
			{
				return HttpNotFound();
			}
			var bookRepository = this.unitOfWork.RepositoryAsync<Book>();
			ViewBag.BookId = new SelectList(bookRepository.Queryable().OrderBy(n=>n.Title), "Id", "Title", favorite.BookId);
			return View(favorite);
		}
		//POST: Favorites/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(Favorite favorite)
		{
			if (ModelState.IsValid)
			{
				favorite.TrackingState = TrackingState.Modified;
				                try{
				this.favoriteService.Update(favorite);
				                
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result = result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
				
				//DisplaySuccessMessage("Has update a Favorite record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//var bookRepository = this.unitOfWork.RepositoryAsync<Book>();
												//return View(favorite);
		}
        //删除当前记录
		//GET: Favorites/Delete/:id
        [HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
          try{
               await this.favoriteService.Delete(new int[] { id });
               await this.unitOfWork.SaveChangesAsync();
               return Json(new { success = true }, JsonRequestBehavior.AllowGet);
           }
           catch (Exception e)
           {
                return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
           }
		}
		 
       
 

        //删除选中的记录
        [HttpPost]
        public async Task<JsonResult> DeleteChecked(int[] id) {
           try{
               await this.favoriteService.Delete(id);
               await this.unitOfWork.SaveChangesAsync();
               return Json(new { success = true }, JsonRequestBehavior.AllowGet);
           }
           catch (Exception e)
           {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
           }
        }
		//导出Excel
		[HttpPost]
		public async Task<ActionResult> ExportExcel( string filterRules = "",string sort = "Id", string order = "asc")
		{
			var fileName = "favorites_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream = await this.favoriteService.ExportExcel(filterRules,sort, order );
            this.logger.Info($"导出完成,文件名:{fileName}");
			return File(stream, "application/vnd.ms-excel", fileName);
		}
        //导入数据
    [HttpPost]
    public async Task<JsonResult> ImportData()
    {
      var watch = new Stopwatch();
      watch.Start();
      var uploadfile = this.Request.Files[0];
      var uploadfilename = uploadfile.FileName;
      var model = this.Request.Form["model"] ?? "model";
      var autosave = Convert.ToBoolean(this.Request.Form["autosave"] ?? "false");
      try
      {

        var ext = Path.GetExtension(uploadfilename);
        var newfileName = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}_{uploadfile.FileName.Replace(ext, "")}{ext}";//重组成新的文件名
        var stream = new MemoryStream();
        await uploadfile.InputStream.CopyToAsync(stream);
        stream.Seek(0, SeekOrigin.Begin);
        uploadfile.InputStream.Seek(0, SeekOrigin.Begin);
        var data = await NPOIHelper.GetDataTableFromExcelAsync(stream, ext);
        await this.favoriteService.ImportDataTable(data, Auth.GetGivenName());
        await this.unitOfWork.SaveChangesAsync();
        if (autosave)
        {
          var folder = this.Server.MapPath($"/UploadFiles/{model}");
          if (!Directory.Exists(folder))
          {
            Directory.CreateDirectory(folder);
          }
          var savepath = Path.Combine(folder, newfileName);
          uploadfile.SaveAs(savepath);
        }
        watch.Stop();
        //获取当前实例测量得出的总运行时间（以毫秒为单位）
        var elapsedTime = watch.ElapsedMilliseconds.ToString();
        this.logger.Info($"导入成功,文件名:{uploadfilename},耗时:{elapsedTime}");
        return Json(new { success = true, filename = newfileName, elapsedTime = elapsedTime }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        var message = e.GetMessage();
        this.logger.Error(e, $"导入失败,文件名:{uploadfilename}");
        return this.Json(new { success = false, filename = uploadfilename, message = message }, JsonRequestBehavior.AllowGet);
      }
    }
		 
	}
}
