using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using LazyCache;
using WebApp.App_Helpers.third_party.api;
using WebApp.Models.Dto;
using WebApp.Services;

namespace WebApp.Controllers
{
  [Authorize]
  [RoutePrefix("Home")]
  public class HomeController : Controller
  {
    private readonly IAppCache cache;
    private readonly IMapper mapper;
    private readonly NLog.ILogger logger;
    private readonly SqlSugar.ISqlSugarClient db;
    private readonly ICompanyService companyService;
    private readonly IBookService bookService;
    private readonly IStockService stockService;
    private readonly ICheckOutService checkOutService;

    public HomeController(
      ICheckOutService checkOutService,
      IStockService stockService,
      IBookService bookService,
      ICompanyService companyService,
      NLog.ILogger logger,
      SqlSugar.ISqlSugarClient db,
      IAppCache cache, IMapper mapper) {
      this.db = db;
      this.cache = cache;
      this.mapper = mapper;
      this.logger = logger;
      this.companyService = companyService;
      this.bookService = bookService;
      this.stockService = stockService;
      this. checkOutService= checkOutService;
    }

    public async Task<ActionResult> Index()
    {
      //this.logger.Debug("访问首页");
      //var result  =await isbnapi.GetISBN("9787212058937");
      var checkout =await this.checkOutService.Queryable().Where(x => x.Status == "Pending")
        .OrderBy(x=>x.Days)
        .ToListAsync();
      ViewBag.Records = checkout;
      return this.View();
    }
    [AllowAnonymous]
    public async Task<ActionResult> PublicPage()
    {
      var model = new PublicDataDto();
      model.Books =await this.bookService.GetTop10();
      model.BooksPageData = await this.bookService.GetPageData(model.Page, model.Size);
      model.Stocks = await this.stockService.GetPageData(0, 10);
      model.Checkouts = await this.checkOutService.GetPageData(0, 10);
      return View(model);
    }
    [AllowAnonymous]
    public ActionResult BlankPage() => this.View();





  }
}