using System;
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
/// File: CommentsController.cs
/// Purpose:Books/Comment
/// Created Date: 1/30/2021 9:47:26 PM
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<Comment>, Repository<Comment>>();
///    container.RegisterType<ICommentService, CommentService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("Comments")]
	public class CommentsController : Controller
	{
		private readonly ICommentService  commentService;
		private readonly IUnitOfWorkAsync unitOfWork;
        private readonly NLog.ILogger logger;
		public CommentsController (
          ICommentService  commentService, 
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
		{
			this.commentService  = commentService;
			this.unitOfWork = unitOfWork;
            this.logger = logger;
		}
        		//GET: Comments/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "Comment", Order = 1)]
		public ActionResult Index() => this.View();

		//Get :Comments/GetData
		//For Index View datagrid datasource url
        
		[HttpPost]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
		 public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.commentService
						               .Query(new CommentQuery().Withfilter(filters)).Include(c => c.Book).Include(c => c.Parent)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

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
			var pagelist = new { total = totalCount, rows = pagerows };
			return Json(pagelist, JsonRequestBehavior.AllowGet);
		}
        [HttpPost]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
        public async Task<JsonResult> GetDataByParentId (int  parentid ,int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {    
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			    var pagerows = (await this.commentService
						               .Query(new CommentQuery().ByParentIdWithfilter(parentid,filters)).Include(c => c.Book).Include(c => c.Parent)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

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
			var pagelist = new { total = totalCount, rows = pagerows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
        public async Task<JsonResult> GetDataByBookId (int  bookid ,int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {    
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			    var pagerows = (await this.commentService
						               .Query(new CommentQuery().ByBookIdWithfilter(bookid,filters)).Include(c => c.Book).Include(c => c.Parent)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

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
			var pagelist = new { total = totalCount, rows = pagerows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }
        //easyui datagrid post acceptChanges 
		[HttpPost]
		public async Task<JsonResult> AcceptChanges(Comment[] comments)
		{
            try{
               this.commentService.ApplyChanges( comments);
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
		 
				//[OutputCache(Duration = 10, VaryByParam = "q")]
		public async Task<JsonResult> GetComments(string q="")
		{
			var commentRepository = this.unitOfWork.RepositoryAsync<Comment>();
			var rows = await commentRepository
                            .Queryable()
                            .Where(n=>n.Content.Contains(q))
                            .OrderBy(n=>n.Content)
                            .Select(n => new { Id = n.Id, Content = n.Content })
                            .ToListAsync();
			return Json(rows, JsonRequestBehavior.AllowGet);
		}
		 
				
		//GET: Comments/Details/:id
		public ActionResult Details(int id)
		{
			
			var comment = this.commentService.Find(id);
			if (comment == null)
			{
				return HttpNotFound();
			}
			return View(comment);
		}
        //GET: Comments/GetItem/:id
        [HttpGet]
        public async Task<JsonResult> GetItem(int id) {
            var  comment = await this.commentService.FindAsync(id);
            return Json(comment,JsonRequestBehavior.AllowGet);
        }
		//GET: Comments/Create
        		public ActionResult Create()
				{
			var comment = new Comment();
			//set default value
			var bookRepository = this.unitOfWork.RepositoryAsync<Book>();
		   			ViewBag.BookId = new SelectList(bookRepository.Queryable().OrderBy(n=>n.Title), "Id", "Title");
		   			var commentRepository = this.unitOfWork.RepositoryAsync<Comment>();
		   			ViewBag.ParentId = new SelectList(commentRepository.Queryable().OrderBy(n=>n.Content), "Id", "Content");
		   			return View(comment);
		}
		//POST: Comments/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(Comment comment)
		{
            if (ModelState.IsValid)
			{
                try{ 
				this.commentService.Insert(comment);
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
			    //DisplaySuccessMessage("Has update a comment record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//var bookRepository = this.unitOfWork.RepositoryAsync<Book>();
			//ViewBag.BookId = new SelectList(await bookRepository.Queryable().OrderBy(n=>n.Title).ToListAsync(), "Id", "Title", comment.BookId);
			//var commentRepository = this.unitOfWork.RepositoryAsync<Comment>();
			//ViewBag.ParentId = new SelectList(await commentRepository.Queryable().OrderBy(n=>n.Content).ToListAsync(), "Id", "Content", comment.ParentId);
			//return View(comment);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItem() {
            var comment = await Task.Run(() => {
                return new Comment();
                });
            return Json(comment, JsonRequestBehavior.AllowGet);
        }

         
		//GET: Comments/Edit/:id
		public ActionResult Edit(int id)
		{
			var comment = this.commentService.Find(id);
			if (comment == null)
			{
				return HttpNotFound();
			}
			var bookRepository = this.unitOfWork.RepositoryAsync<Book>();
			ViewBag.BookId = new SelectList(bookRepository.Queryable().OrderBy(n=>n.Title), "Id", "Title", comment.BookId);
			var commentRepository = this.unitOfWork.RepositoryAsync<Comment>();
			ViewBag.ParentId = new SelectList(commentRepository.Queryable().OrderBy(n=>n.Content), "Id", "Content", comment.ParentId);
			return View(comment);
		}
		//POST: Comments/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(Comment comment)
		{
			if (ModelState.IsValid)
			{
				comment.TrackingState = TrackingState.Modified;
				                try{
				this.commentService.Update(comment);
				                
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result = result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
				
				//DisplaySuccessMessage("Has update a Comment record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//var bookRepository = this.unitOfWork.RepositoryAsync<Book>();
												//var commentRepository = this.unitOfWork.RepositoryAsync<Comment>();
												//return View(comment);
		}
        //删除当前记录
		//GET: Comments/Delete/:id
        [HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
          try{
               await this.commentService.Delete(new int[] { id });
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
               await this.commentService.Delete(id);
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
			var fileName = "comments_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream = await this.commentService.ExportExcel(filterRules,sort, order );
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
        await this.commentService.ImportDataTable(data, Auth.GetGivenName());
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
