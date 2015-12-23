using NorthwindService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;

namespace NorthwindService
{
  public static class WebApiConfig
  {
    //public static void Register(HttpConfiguration config)
    //{
    //  // Web API 配置和服务

    //  // Web API 路由
    //  config.MapHttpAttributeRoutes();

    //  config.Routes.MapHttpRoute(
    //      name: "DefaultApi",
    //      routeTemplate: "api/{controller}/{id}",
    //      defaults: new { id = RouteParameter.Optional }
    //  );
    //}

    public static void Register(HttpConfiguration config)
    {
      var cors = new EnableCorsAttribute("*", "accept,content-type,origin,x-my-header", "get");
      config.EnableCors(cors);

      // New code:
      ODataModelBuilder builder = new ODataConventionModelBuilder();
      builder.EntitySet<Product>("Products");
      builder.EntitySet<Category>("Categories");
      
      config.MapODataServiceRoute(
          routeName: "ODataRoute",
          routePrefix: null,
          model: builder.GetEdmModel());
    }
  }
}
