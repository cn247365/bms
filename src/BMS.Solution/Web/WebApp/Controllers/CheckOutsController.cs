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
using WebApp.Models.Dto;

namespace WebApp.Controllers
{
  /// <summary>
  /// File: CheckOutsController.cs
  /// Purpose:Books Management/Check Out
  /// Created Date: 1/30/2021 9:53:16 PM
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
  /// <![CDATA[
  ///    container.RegisterType<IRepositoryAsync<CheckOut>, Repository<CheckOut>>();
  ///    container.RegisterType<ICheckOutService, CheckOutService>();
  /// ]]>
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  [Authorize]
  [RoutePrefix("CheckOuts")]
  public class CheckOutsController : Controller
  {
    private readonly ICheckOutService checkOutService;
    private readonly IUnitOfWorkAsync unitOfWork;
    private readonly NLog.ILogger logger;
    public CheckOutsController(
          ICheckOutService checkOutService,
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
    {
      this.checkOutService = checkOutService;
      this.unitOfWork = unitOfWork;
      this.logger = logger;
    }
    //GET: CheckOuts/Index
    //[OutputCache(Duration = 60, VaryByParam = "none")]
    [Route("Index", Name = "Check Out", Order = 1)]
    public ActionResult Index() => this.View();


    public async Task<JsonResult> GetBook(int globalid,string barcode)
    {
      var item = await this.checkOutService.Queryable()
        .Where(x => x.GlobalId == globalid && ( x.BarCode == barcode || x.ISBN == barcode ))
        .FirstOrDefaultAsync();
      return Json(item, JsonRequestBehavior.AllowGet);
    }


    //借书
    [HttpPost]
    public async Task<JsonResult> Borrow(BorrowInputDto inputdto)
    {
      try
      {
        await  this.checkOutService.Borrow(inputdto);
        await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
    }


    //还书
    [HttpPost]
    public async Task<JsonResult> Returning(RetuningInputDto inputdto)
    {
      try
      {
        await this.checkOutService.Returning(inputdto);
        await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
    }
    //Get :CheckOuts/GetData
    //For Index View datagrid datasource url

    [HttpPost]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.checkOutService
                           .Query(new CheckOutQuery().Withfilter(filters)).Include(c => c.Book).Include(c => c.Employee)
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new
                                       {

                                         BookTitle = n.Book?.Title,
                                         EmployeeShortName = n.Employee?.ShortName,
                                         Id = n.Id,
                                         EmployeeId = n.EmployeeId,
                                         GlobalId = n.GlobalId,
                                         ShortName = n.ShortName,
                                         DisplayName = n.DisplayName,
                                         BookId = n.BookId,
                                         BarCode = n.BarCode,
                                         ISBN = n.ISBN,
                                         Title = n.Title,
                                         Qty = n.Qty,
                                         BorrowDate = n.BorrowDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                         BackDate = n.BackDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         ExpiryDate = n.ExpiryDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                         Days = n.Days,
                                         Status = n.Status,
                                         Notified = n.Notified,
                                         Expiry = n.Expiry
                                       }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    [HttpPost]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetDataByEmployeeId(int employeeid, int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.checkOutService
                       .Query(new CheckOutQuery().ByEmployeeIdWithfilter(employeeid, filters)).Include(c => c.Book).Include(c => c.Employee)
                     .OrderBy(n => n.OrderBy(sort, order))
                     .SelectPageAsync(page, rows, out var totalCount) )
                                   .Select(n => new
                                   {

                                     BookTitle = n.Book?.Title,
                                     EmployeeShortName = n.Employee?.ShortName,
                                     Id = n.Id,
                                     EmployeeId = n.EmployeeId,
                                     GlobalId = n.GlobalId,
                                     ShortName = n.ShortName,
                                     DisplayName = n.DisplayName,
                                     BookId = n.BookId,
                                     BarCode = n.BarCode,
                                     ISBN = n.ISBN,
                                     Title = n.Title,
                                     Qty = n.Qty,
                                     BorrowDate = n.BorrowDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                     BackDate = n.BackDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                     ExpiryDate = n.ExpiryDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                     Days = n.Days,
                                     Status = n.Status,
                                     Notified = n.Notified,
                                     Expiry = n.Expiry
                                   }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    [HttpPost]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetDataByBookId(int bookid, int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.checkOutService
                       .Query(new CheckOutQuery().ByBookIdWithfilter(bookid, filters)).Include(c => c.Book).Include(c => c.Employee)
                     .OrderBy(n => n.OrderBy(sort, order))
                     .SelectPageAsync(page, rows, out var totalCount) )
                                   .Select(n => new
                                   {

                                     BookTitle = n.Book?.Title,
                                     EmployeeShortName = n.Employee?.ShortName,
                                     Id = n.Id,
                                     EmployeeId = n.EmployeeId,
                                     GlobalId = n.GlobalId,
                                     ShortName = n.ShortName,
                                     DisplayName = n.DisplayName,
                                     BookId = n.BookId,
                                     BarCode = n.BarCode,
                                     ISBN = n.ISBN,
                                     Title = n.Title,
                                     Qty = n.Qty,
                                     BorrowDate = n.BorrowDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                     BackDate = n.BackDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                     ExpiryDate = n.ExpiryDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                     Days = n.Days,
                                     Status = n.Status,
                                     Notified = n.Notified,
                                     Expiry = n.Expiry
                                   }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    //easyui datagrid post acceptChanges 
    [HttpPost]
    public async Task<JsonResult> AcceptChanges(CheckOut[] checkouts)
    {
      try
      {
        this.checkOutService.ApplyChanges(checkouts);
        var result = await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
    }
    //[OutputCache(Duration = 10, VaryByParam = "q")]
    public async Task<JsonResult> GetBooks(string q = "")
    {
      var bookRepository = this.unitOfWork.RepositoryAsync<Book>();
      var rows = await bookRepository
                            .Queryable()
                            .Where(n => n.Title.Contains(q))
                            .OrderBy(n => n.Title)
                            .Select(n => new { Id = n.Id, Title = n.Title })
                            .ToListAsync();
      return Json(rows, JsonRequestBehavior.AllowGet);
    }

    //[OutputCache(Duration = 10, VaryByParam = "q")]
    public async Task<JsonResult> GetEmployees(string q = "")
    {
      var employeeRepository = this.unitOfWork.RepositoryAsync<Employee>();
      var rows = await employeeRepository
                            .Queryable()
                            .Where(n => n.ShortName.Contains(q))
                            .OrderBy(n => n.ShortName)
                            .Select(n => new { Id = n.Id, ShortName = n.ShortName })
                            .ToListAsync();
      return Json(rows, JsonRequestBehavior.AllowGet);
    }


    //GET: CheckOuts/Details/:id
    public ActionResult Details(int id)
    {

      var checkOut = this.checkOutService.Find(id);
      if (checkOut == null)
      {
        return HttpNotFound();
      }
      return View(checkOut);
    }
    //GET: CheckOuts/GetItem/:id
    [HttpGet]
    public async Task<JsonResult> GetItem(int id)
    {
      var checkOut = await this.checkOutService.FindAsync(id);
      return Json(checkOut, JsonRequestBehavior.AllowGet);
    }
    //GET: CheckOuts/Create
    public ActionResult Create()
    {
      var checkOut = new CheckOut();
      //set default value
      var bookRepository = this.unitOfWork.RepositoryAsync<Book>();
      ViewBag.BookId = new SelectList(bookRepository.Queryable().OrderBy(n => n.Title), "Id", "Title");
      var employeeRepository = this.unitOfWork.RepositoryAsync<Employee>();
      ViewBag.EmployeeId = new SelectList(employeeRepository.Queryable().OrderBy(n => n.ShortName), "Id", "ShortName");
      return View(checkOut);
    }
    //POST: CheckOuts/Create
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CheckOut checkOut)
    {
      if (ModelState.IsValid)
      {
        try
        {
          this.checkOutService.Insert(checkOut);
          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }
        //DisplaySuccessMessage("Has update a checkOut record");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //var bookRepository = this.unitOfWork.RepositoryAsync<Book>();
      //ViewBag.BookId = new SelectList(await bookRepository.Queryable().OrderBy(n=>n.Title).ToListAsync(), "Id", "Title", checkOut.BookId);
      //var employeeRepository = this.unitOfWork.RepositoryAsync<Employee>();
      //ViewBag.EmployeeId = new SelectList(await employeeRepository.Queryable().OrderBy(n=>n.ShortName).ToListAsync(), "Id", "ShortName", checkOut.EmployeeId);
      //return View(checkOut);
    }

    //新增对象初始化
    [HttpGet]
    public async Task<JsonResult> NewItem()
    {
      var checkOut = await Task.Run(() =>
      {
        return new CheckOut();
      });
      return Json(checkOut, JsonRequestBehavior.AllowGet);
    }


    //GET: CheckOuts/Edit/:id
    public ActionResult Edit(int id)
    {
      var checkOut = this.checkOutService.Find(id);
      if (checkOut == null)
      {
        return HttpNotFound();
      }
      var bookRepository = this.unitOfWork.RepositoryAsync<Book>();
      ViewBag.BookId = new SelectList(bookRepository.Queryable().OrderBy(n => n.Title), "Id", "Title", checkOut.BookId);
      var employeeRepository = this.unitOfWork.RepositoryAsync<Employee>();
      ViewBag.EmployeeId = new SelectList(employeeRepository.Queryable().OrderBy(n => n.ShortName), "Id", "ShortName", checkOut.EmployeeId);
      return View(checkOut);
    }
    //POST: CheckOuts/Edit/:id
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(CheckOut checkOut)
    {
      if (ModelState.IsValid)
      {
        checkOut.TrackingState = TrackingState.Modified;
        try
        {
          this.checkOutService.Update(checkOut);

          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result = result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }

        //DisplaySuccessMessage("Has update a CheckOut record");
        //return RedirectToAction("Index");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //var bookRepository = this.unitOfWork.RepositoryAsync<Book>();
      //var employeeRepository = this.unitOfWork.RepositoryAsync<Employee>();
      //return View(checkOut);
    }
    //删除当前记录
    //GET: CheckOuts/Delete/:id
    [HttpGet]
    public async Task<ActionResult> Delete(int id)
    {
      try
      {
        await this.checkOutService.Delete(new int[] { id });
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
    public async Task<JsonResult> DeleteChecked(int[] id)
    {
      try
      {
        await this.checkOutService.Delete(id);
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
    public async Task<ActionResult> ExportExcel(string filterRules = "", string sort = "Id", string order = "asc")
    {
      var fileName = "checkouts_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
      var stream = await this.checkOutService.ExportExcel(filterRules, sort, order);
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
        await this.checkOutService.ImportDataTable(data, givenname);
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
