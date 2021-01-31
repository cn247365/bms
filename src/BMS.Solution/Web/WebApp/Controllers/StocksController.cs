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
/// File: StocksController.cs
/// Purpose:Books Mangement/Stock
/// Created Date: 1/31/2021 9:28:22 AM
/// Author: neo.zhu
/// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
/// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
/// <![CDATA[
///    container.RegisterType<IRepositoryAsync<Stock>, Repository<Stock>>();
///    container.RegisterType<IStockService, StockService>();
/// ]]>
/// Copyright (c) 2012-2018 All Rights Reserved
/// </summary>
    [Authorize]
    [RoutePrefix("Stocks")]
	public class StocksController : Controller
	{
		private readonly IStockService  stockService;
		private readonly IUnitOfWorkAsync unitOfWork;
        private readonly NLog.ILogger logger;
		public StocksController (
          IStockService  stockService, 
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
		{
			this.stockService  = stockService;
			this.unitOfWork = unitOfWork;
            this.logger = logger;
		}
        		//GET: Stocks/Index
        //[OutputCache(Duration = 60, VaryByParam = "none")]
        [Route("Index", Name = "Stock", Order = 1)]
		public ActionResult Index() => this.View();

		//Get :Stocks/GetData
		//For Index View datagrid datasource url
        
		[HttpPost]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
		 public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
		{
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			var pagerows  = (await this.stockService
						               .Query(new StockQuery().Withfilter(filters)).Include(s => s.Book)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    BookTitle = n.Book?.Title,
    Id = n.Id,
    BookId = n.BookId,
    BarCode = n.BarCode,
    ISBN = n.ISBN,
    Title = n.Title,
    Remark = n.Remark,
    Qty = n.Qty,
    Price = n.Price,
    PurchasingPrice = n.PurchasingPrice,
    PurchaseDate = n.PurchaseDate?.ToString("yyyy-MM-dd HH:mm:ss"),
    UserName = n.UserName,
    Catetory = n.Catetory,
    Location = n.Location,
    Shelves = n.Shelves,
    Tags = n.Tags,
    Status = n.Status,
    Trades = n.Trades,
    InputDate = n.InputDate.ToString("yyyy-MM-dd HH:mm:ss")
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
			return Json(pagelist, JsonRequestBehavior.AllowGet);
		}
        [HttpPost]
        //[OutputCache(Duration = 10, VaryByParam = "*")]
        public async Task<JsonResult> GetDataByBookId (int  bookid ,int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {    
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
			    var pagerows = (await this.stockService
						               .Query(new StockQuery().ByBookIdWithfilter(bookid,filters)).Include(s => s.Book)
							           .OrderBy(n=>n.OrderBy(sort,order))
							           .SelectPageAsync(page, rows, out var totalCount))
                                       .Select(  n => new { 

    BookTitle = n.Book?.Title,
    Id = n.Id,
    BookId = n.BookId,
    BarCode = n.BarCode,
    ISBN = n.ISBN,
    Title = n.Title,
    Remark = n.Remark,
    Qty = n.Qty,
    Price = n.Price,
    PurchasingPrice = n.PurchasingPrice,
    PurchaseDate = n.PurchaseDate?.ToString("yyyy-MM-dd HH:mm:ss"),
    UserName = n.UserName,
    Catetory = n.Catetory,
    Location = n.Location,
    Shelves = n.Shelves,
    Tags = n.Tags,
    Status = n.Status,
    Trades = n.Trades,
    InputDate = n.InputDate.ToString("yyyy-MM-dd HH:mm:ss")
}).ToList();
			var pagelist = new { total = totalCount, rows = pagerows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }
        //easyui datagrid post acceptChanges 
		[HttpPost]
		public async Task<JsonResult> AcceptChanges(Stock[] stocks)
		{
            try{
               this.stockService.ApplyChanges( stocks);
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
		 
				
		//GET: Stocks/Details/:id
		public ActionResult Details(int id)
		{
			
			var stock = this.stockService.Find(id);
			if (stock == null)
			{
				return HttpNotFound();
			}
			return View(stock);
		}
        //GET: Stocks/GetItem/:id
        [HttpGet]
        public async Task<JsonResult> GetItem(int id) {
            var  stock = await this.stockService.FindAsync(id);
            return Json(stock,JsonRequestBehavior.AllowGet);
        }
		//GET: Stocks/Create
        		public ActionResult Create()
				{
			var stock = new Stock();
			//set default value
			var bookRepository = this.unitOfWork.RepositoryAsync<Book>();
		   			ViewBag.BookId = new SelectList(bookRepository.Queryable().OrderBy(n=>n.Title), "Id", "Title");
		   			return View(stock);
		}
		//POST: Stocks/Create
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(Stock stock)
		{
            if (ModelState.IsValid)
			{
                try{ 
				this.stockService.Insert(stock);
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
			    //DisplaySuccessMessage("Has update a stock record");
			}
			else {
			   var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			   return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			   //DisplayErrorMessage(modelStateErrors);
			}
			//var bookRepository = this.unitOfWork.RepositoryAsync<Book>();
			//ViewBag.BookId = new SelectList(await bookRepository.Queryable().OrderBy(n=>n.Title).ToListAsync(), "Id", "Title", stock.BookId);
			//return View(stock);
		}

        //新增对象初始化
        [HttpGet]
        public async Task<JsonResult> NewItem() {
            var stock = await Task.Run(() => {
                return new Stock();
                });
            return Json(stock, JsonRequestBehavior.AllowGet);
        }

         
		//GET: Stocks/Edit/:id
		public ActionResult Edit(int id)
		{
			var stock = this.stockService.Find(id);
			if (stock == null)
			{
				return HttpNotFound();
			}
			var bookRepository = this.unitOfWork.RepositoryAsync<Book>();
			ViewBag.BookId = new SelectList(bookRepository.Queryable().OrderBy(n=>n.Title), "Id", "Title", stock.BookId);
			return View(stock);
		}
		//POST: Stocks/Edit/:id
		//To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(Stock stock)
		{
			if (ModelState.IsValid)
			{
				stock.TrackingState = TrackingState.Modified;
				                try{
				this.stockService.Update(stock);
				                
				var result = await this.unitOfWork.SaveChangesAsync();
                return Json(new { success = true,result = result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
                }
				
				//DisplaySuccessMessage("Has update a Stock record");
				//return RedirectToAction("Index");
			}
			else {
			var modelStateErrors =string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
			return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
			//DisplayErrorMessage(modelStateErrors);
			}
						//var bookRepository = this.unitOfWork.RepositoryAsync<Book>();
												//return View(stock);
		}
        //删除当前记录
		//GET: Stocks/Delete/:id
        [HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
          try{
               await this.stockService.Delete(new int[] { id });
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
               await this.stockService.Delete(id);
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
			var fileName = "stocks_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
			var stream = await this.stockService.ExportExcel(filterRules,sort, order );
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
      var givenname = (string)ViewBag.GivenName;
      try
      {

        var ext = Path.GetExtension(uploadfilename);
        var newfileName = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}_{uploadfile.FileName.Replace(ext, "")}{ext}";//重组成新的文件名
        var stream = new MemoryStream();
        await uploadfile.InputStream.CopyToAsync(stream);
        stream.Seek(0, SeekOrigin.Begin);
        uploadfile.InputStream.Seek(0, SeekOrigin.Begin);
        var data = await NPOIHelper.GetDataTableFromExcelAsync(stream, ext);
        await this.stockService.ImportDataTable(data, givenname);
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
