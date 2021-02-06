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
  /// File: BooksController.cs
  /// Purpose:Books Management/Book
  /// Created Date: 1/31/2021 9:30:44 AM
  /// Author: neo.zhu
  /// Tools: SmartCode MVC5 Scaffolder for Visual Studio 2017
  /// TODO: Registers the type mappings with the Unity container(Mvc.UnityConfig.cs)
  /// <![CDATA[
  ///    container.RegisterType<IRepositoryAsync<Book>, Repository<Book>>();
  ///    container.RegisterType<IBookService, BookService>();
  /// ]]>
  /// Copyright (c) 2012-2018 All Rights Reserved
  /// </summary>
  [Authorize]
  [RoutePrefix("Books")]
  public class BooksController : Controller
  {
    private readonly IBookService bookService;
    private readonly IUnitOfWorkAsync unitOfWork;
    private readonly NLog.ILogger logger;
    public BooksController(
          IBookService bookService,
          IUnitOfWorkAsync unitOfWork,
          NLog.ILogger logger
          )
    {
      this.bookService = bookService;
      this.unitOfWork = unitOfWork;
      this.logger = logger;
    }
    //GET: Books/Index
    //[OutputCache(Duration = 60, VaryByParam = "none")]
    [Route("Index", Name = "Book", Order = 1)]
    public ActionResult Index() => this.View();

    

    //Get :Books/GetData
    //For Index View datagrid datasource url

    [HttpPost]
    //[OutputCache(Duration = 10, VaryByParam = "*")]
    public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
      var pagerows = ( await this.bookService
                           .Query(new BookQuery().Withfilter(filters))
                         .OrderBy(n => n.OrderBy(sort, order))
                         .SelectPageAsync(page, rows, out var totalCount) )
                                       .Select(n => new
                                       {

                                         Id = n.Id,
                                         Title = n.Title,
                                         SubTitle = n.SubTitle,
                                         Pic = n.Pic,
                                         LocalPic = n.LocalPic,
                                         Author = n.Author,
                                         Summary = n.Summary,
                                         Publisher = n.Publisher,
                                         Pubplace = n.Pubplace,
                                         Page = n.Page,
                                         Price = n.Price,
                                         PurchasingPrice = n.PurchasingPrice,
                                         Binding = n.Binding,
                                         ISBN = n.ISBN,
                                         ISBN10 = n.ISBN10,
                                         Keyword = n.Keyword,
                                         Edition = n.Edition,
                                         Impression = n.Impression,
                                         Language = n.Language,
                                         Format = n.Format,
                                         Class = n.Class,
                                         CIP = n.CIP,
                                         Reads = n.Reads,
                                         Popular = n.Popular
                                       }).ToList();
      var pagelist = new { total = totalCount, rows = pagerows };
      return Json(pagelist, JsonRequestBehavior.AllowGet);
    }
    //easyui datagrid post acceptChanges 
    [HttpPost]
    public async Task<JsonResult> AcceptChanges(Book[] books)
    {
      try
      {
        this.bookService.ApplyChanges(books);
        var result = await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
    }

    //GET: Books/Details/:id
    public ActionResult Details(int id)
    {

      var book = this.bookService.Find(id);
      if (book == null)
      {
        return HttpNotFound();
      }
      return View(book);
    }
    //GET: Books/GetItem/:id
    [HttpGet]
    public async Task<JsonResult> GetItem(int id)
    {
      var book = await this.bookService.FindAsync(id);
      return Json(book, JsonRequestBehavior.AllowGet);
    }
    //GET: Books/Create
    public ActionResult Create()
    {
      var book = new Book();
      //set default value
      return View(book);
    }
    //POST: Books/Create
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(Book book)
    {
      if (ModelState.IsValid)
      {
        book.TrackingState = TrackingState.Added;
        foreach (var item in book.BookPictures)
        {
          item.BookId = book.Id;
          item.TrackingState = TrackingState.Added;
        }
        foreach (var item in book.CheckOuts)
        {
          item.BookId = book.Id;
          item.TrackingState = TrackingState.Added;
        }
        foreach (var item in book.Comments)
        {
          item.BookId = book.Id;
          item.TrackingState = TrackingState.Added;
        }
        try
        {
          this.bookService.ApplyChanges(book);
          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }
        //DisplaySuccessMessage("Has update a book record");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //return View(book);
    }

    //新增对象初始化
    [HttpGet]
    public async Task<JsonResult> NewItem()
    {
      var book = await Task.Run(() =>
      {
        return new Book();
      });
      return Json(book, JsonRequestBehavior.AllowGet);
    }


    //GET: Books/Edit/:id
    public ActionResult Edit(int id)
    {
      var book = this.bookService.Find(id);
      if (book == null)
      {
        return HttpNotFound();
      }
      return View(book);
    }
    //POST: Books/Edit/:id
    //To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(Book book)
    {
      if (ModelState.IsValid)
      {
        book.TrackingState = TrackingState.Modified;
        foreach (var item in book.BookPictures)
        {
          item.BookId = book.Id;
        }
        foreach (var item in book.CheckOuts)
        {
          item.BookId = book.Id;
        }
        foreach (var item in book.Comments)
        {
          item.BookId = book.Id;
        }

        try
        {
          this.bookService.ApplyChanges(book);

          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result = result }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
        }

        //DisplaySuccessMessage("Has update a Book record");
        //return RedirectToAction("Index");
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
        //DisplayErrorMessage(modelStateErrors);
      }
      //return View(book);
    }
    //删除当前记录
    //GET: Books/Delete/:id
    [HttpGet]
    public async Task<ActionResult> Delete(int id)
    {
      try
      {
        await this.bookService.Delete(new int[] { id });
        await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
    }

    //Get Detail Row By Id For Edit
    //Get : Books/EditBookPicture/:id
    [HttpGet]
    public async Task<ActionResult> EditBookPicture(int id)
    {
      var bookpictureRepository = this.unitOfWork.RepositoryAsync<BookPicture>();
      var bookpicture = await bookpictureRepository.FindAsync(id);
      var bookRepository = this.unitOfWork.RepositoryAsync<Book>();
      if (bookpicture == null)
      {
        ViewBag.BookId = new SelectList(await bookRepository.Queryable().OrderBy(n => n.Title).ToListAsync(), "Id", "Title");
        //return HttpNotFound();
        return PartialView("_BookPictureEditForm", new BookPicture());
      }
      else
      {
        ViewBag.BookId = new SelectList(await bookRepository.Queryable().ToListAsync(), "Id", "Title", bookpicture.BookId);
      }
      return PartialView("_BookPictureEditForm", bookpicture);
    }
    //Get Create Row By Id For Edit
    //Get : Books/CreateBookPicture
    [HttpGet]
    public async Task<ActionResult> CreateBookPicture(int bookid)
    {
      var bookRepository = this.unitOfWork.RepositoryAsync<Book>();
      ViewBag.BookId = new SelectList(await bookRepository.Queryable().OrderBy(n => n.Title).ToListAsync(), "Id", "Title");
      return PartialView("_BookPictureEditForm");
    }
    //Post Delete Detail Row By Id
    //Get : Books/DeleteBookPicture/:id
    [HttpGet]
    public async Task<ActionResult> DeleteBookPicture(int id)
    {
      try
      {
        var bookpictureRepository = this.unitOfWork.RepositoryAsync<BookPicture>();
        bookpictureRepository.Delete(id);
        var result = await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
    }
    //Get Detail Row By Id For Edit
    //Get : Books/EditCheckOut/:id
    [HttpGet]
    public async Task<ActionResult> EditCheckOut(int id)
    {
      var checkoutRepository = this.unitOfWork.RepositoryAsync<CheckOut>();
      var checkout = await checkoutRepository.FindAsync(id);
      var bookRepository = this.unitOfWork.RepositoryAsync<Book>();
      var employeeRepository = this.unitOfWork.RepositoryAsync<Employee>();
      if (checkout == null)
      {
        ViewBag.BookId = new SelectList(await bookRepository.Queryable().OrderBy(n => n.Title).ToListAsync(), "Id", "Title");
        ViewBag.EmployeeId = new SelectList(await employeeRepository.Queryable().OrderBy(n => n.ShortName).ToListAsync(), "Id", "ShortName");
        //return HttpNotFound();
        return PartialView("_CheckOutEditForm", new CheckOut());
      }
      else
      {
        ViewBag.BookId = new SelectList(await bookRepository.Queryable().ToListAsync(), "Id", "Title", checkout.BookId);
        ViewBag.EmployeeId = new SelectList(await employeeRepository.Queryable().ToListAsync(), "Id", "ShortName", checkout.EmployeeId);
      }
      return PartialView("_CheckOutEditForm", checkout);
    }
    //Get Create Row By Id For Edit
    //Get : Books/CreateCheckOut
    [HttpGet]
    public async Task<ActionResult> CreateCheckOut(int bookid)
    {
      var bookRepository = this.unitOfWork.RepositoryAsync<Book>();
      ViewBag.BookId = new SelectList(await bookRepository.Queryable().OrderBy(n => n.Title).ToListAsync(), "Id", "Title");
      var employeeRepository = this.unitOfWork.RepositoryAsync<Employee>();
      ViewBag.EmployeeId = new SelectList(await employeeRepository.Queryable().OrderBy(n => n.ShortName).ToListAsync(), "Id", "ShortName");
      return PartialView("_CheckOutEditForm");
    }
    //Post Delete Detail Row By Id
    //Get : Books/DeleteCheckOut/:id
    [HttpGet]
    public async Task<ActionResult> DeleteCheckOut(int id)
    {
      try
      {
        var checkoutRepository = this.unitOfWork.RepositoryAsync<CheckOut>();
        checkoutRepository.Delete(id);
        var result = await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
    }
    //Get Detail Row By Id For Edit
    //Get : Books/EditComment/:id
    [HttpGet]
    public async Task<ActionResult> EditComment(int id)
    {
      var commentRepository = this.unitOfWork.RepositoryAsync<Comment>();
      var comment = await commentRepository.FindAsync(id);
      var bookRepository = this.unitOfWork.RepositoryAsync<Book>();
      if (comment == null)
      {
        ViewBag.BookId = new SelectList(await bookRepository.Queryable().OrderBy(n => n.Title).ToListAsync(), "Id", "Title");
        ViewBag.ParentId = new SelectList(await commentRepository.Queryable().OrderBy(n => n.Content).ToListAsync(), "Id", "Content");
        //return HttpNotFound();
        return PartialView("_CommentEditForm", new Comment());
      }
      else
      {
        ViewBag.BookId = new SelectList(await bookRepository.Queryable().ToListAsync(), "Id", "Title", comment.BookId);
        ViewBag.ParentId = new SelectList(await commentRepository.Queryable().ToListAsync(), "Id", "Content", comment.ParentId);
      }
      return PartialView("_CommentEditForm", comment);
    }
    //Get Create Row By Id For Edit
    //Get : Books/CreateComment
    [HttpGet]
    public async Task<ActionResult> CreateComment(int bookid)
    {
      var bookRepository = this.unitOfWork.RepositoryAsync<Book>();
      ViewBag.BookId = new SelectList(await bookRepository.Queryable().OrderBy(n => n.Title).ToListAsync(), "Id", "Title");
      var commentRepository = this.unitOfWork.RepositoryAsync<Comment>();
      ViewBag.ParentId = new SelectList(await commentRepository.Queryable().OrderBy(n => n.Content).ToListAsync(), "Id", "Content");
      return PartialView("_CommentEditForm");
    }
    //Post Delete Detail Row By Id
    //Get : Books/DeleteComment/:id
    [HttpGet]
    public async Task<ActionResult> DeleteComment(int id)
    {
      try
      {
        var commentRepository = this.unitOfWork.RepositoryAsync<Comment>();
        commentRepository.Delete(id);
        var result = await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetMessage() }, JsonRequestBehavior.AllowGet);
      }
    }

    //Get : Books/GetBookPicturesByBookId/:id
    [HttpGet]
    public async Task<JsonResult> GetBookPicturesByBookId(int id)
    {
      var bookpictures = await this.bookService.GetBookPicturesByBookId(id);
      var rows = bookpictures.Select(n => new
      {

        BookTitle = n.Book?.Title,
        Id = n.Id,
        BookId = n.BookId,
        Picture = n.Picture,
        Url = n.Url,
        Description = n.Description
      });
      return Json(rows, JsonRequestBehavior.AllowGet);

    }
    //Get : Books/GetCheckOutsByBookId/:id
    [HttpGet]
    public async Task<JsonResult> GetCheckOutsByBookId(int id)
    {
      var checkouts = await this.bookService.GetCheckOutsByBookId(id);
      var rows = checkouts.Select(n => new
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
        BackQty = n.BackQty,
        BorrowDate = n.BorrowDate.ToString("yyyy-MM-dd HH:mm:ss"),
        BackDate = n.BackDate?.ToString("yyyy-MM-dd HH:mm:ss"),
        ExpiryDate = n.ExpiryDate?.ToString("yyyy-MM-dd HH:mm:ss"),
        Days = n.Days,
        Status = n.Status,
        Notified = n.Notified,
        Expiry = n.Expiry
      });
      return Json(rows, JsonRequestBehavior.AllowGet);

    }
    //Get : Books/GetCommentsByBookId/:id
    [HttpGet]
    public async Task<JsonResult> GetCommentsByBookId(int id)
    {
      var comments = await this.bookService.GetCommentsByBookId(id);
      var rows = comments.Select(n => new
      {

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
      });
      return Json(rows, JsonRequestBehavior.AllowGet);

    }


    //删除选中的记录
    [HttpPost]
    public async Task<JsonResult> DeleteChecked(int[] id)
    {
      try
      {
        await this.bookService.Delete(id);
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
      var fileName = "books_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
      var stream = await this.bookService.ExportExcel(filterRules, sort, order);
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
        await this.bookService.ImportDataTable(data, givenname);
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
