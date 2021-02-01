using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using RestClient.Net;
using RestClient.Net.Abstractions;
using RestClient.Net.Abstractions.Extensions;

namespace WebApp.App_Helpers.third_party.api
{
  public static class isbnapi
  {
    private const string host = "https://jisuisbn.market.alicloudapi.com";
    private const string path = "/isbn/query";
    private const string method = "get";
    private const string appcode = "e37a9c2841974dbbad7e517f1d36940e";

    public static async Task<isbn_response> GetISBN(string isbn)
    {
      try
      {
        var url = $"{host}{path}?isbn={isbn}";
        var client = new Client(new NewtonsoftSerializationAdapter(), new Uri(url));
        client.SetJsonContentTypeHeader();
        //client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
        var requesthead = new RequestHeadersCollection();
        requesthead.Add("Authorization", "APPCODE " + appcode);
        var result =await client.GetAsync<isbn_response>(new Uri(url), requesthead);
        NLog.LogManager.GetCurrentClassLogger().Info($"调用ISBN接口成功:{isbn}");
        return result.Body;
      }catch(Exception e)
      {
        NLog.LogManager.GetCurrentClassLogger().Error($"调用ISBN接口异常:{isbn}");
        return null;
      }
    }

  }

  public class sellerinfo
  {
    public string seller { get; set; }
    public string price { get; set; }
    public string link { get; set; }
  }

  public class isbn_result
  {
    public string title { get; set; }
    public string subtitle { get; set; }
    public string pic { get; set; }
    public string author { get; set; }
    public string summary { get; set; }
    public string publisher { get; set; }
    public string pubplace { get; set; }
    public string pubdate { get; set; }
    public int? page { get; set; }
    public decimal? price { get; set; }
    public string binding { get; set; }
    public string isbn { get; set; }
    public string isbn10 { get; set; }
    public string keyword { get; set; }
    public string cip { get; set; }
    public string edition { get; set; }
    public string impression { get; set; }
    public string language { get; set; }
    public string format { get; set; }
    public string @class { get; set; }
    public IEnumerable<sellerinfo> sellerlist { get; set; }
  }

  public class isbn_response
  {
    public int status { get; set; }
    public string msg { get; set; }
    public isbn_result result { get; set; }
  }



  public class NewtonsoftSerializationAdapter : ISerializationAdapter
  {
    #region Implementation
    public TResponseBody Deserialize<TResponseBody>(Response response)
    {
      //Note: on some services the headers should be checked for encoding 
      var markup = Encoding.UTF8.GetString(response.GetResponseData());

      object markupAsObject = markup;

      if (typeof(TResponseBody) == typeof(string))
      {
        return (TResponseBody)markupAsObject;
      }

      return JsonConvert.DeserializeObject<TResponseBody>(markup);
    }

    public byte[] Serialize<TRequestBody>(TRequestBody value, IHeadersCollection requestHeaders)
    {
      var json = JsonConvert.SerializeObject(value);

      var binary = Encoding.UTF8.GetBytes(json);

      return binary;
    }
    #endregion
  }
}