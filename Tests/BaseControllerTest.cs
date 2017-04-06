using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using API.App_Start;
using API.Common;
using API.ViewModels;
using Camps.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject.Web.Common;
using NLog;

namespace Tests
{
   public abstract class BaseControllerTest : BaseTest
    {
        private string baseAddress = "http://localhost/";
        private Logger _logger;

        public BaseControllerTest()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        protected void PrepareController(ApiController controller, string userName = null, string role=null)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, baseAddress);
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "products" } });

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            if (role != null)
            {
                Identity identity = new Identity();
                identity.Name = userName;
                identity.IsAuthenticated = true;

                controller.ControllerContext.RequestContext.Principal = 
                    new GenericPrincipal(identity, new string[] { role } );
            }

            controller.Url = new UrlHelper(new HttpRequestMessage());
            controller.Url.Request = request;
        }

        /// <summary>
        /// Отправляет HTTP-запрос на in-memory http-сервер
        /// </summary>
        /// <typeparam name="T">Возвращаемый тип объекта</typeparam>
        /// <param name="url">Относительный урл. Например: api/user?email=var@33kita.ru</param>
        /// <returns></returns>
        public T HttpGet<T>(string url)
        {

            HttpMessageInvoker messageInvoker = new HttpMessageInvoker(new InMemoryHttpContentSerializationHandler(PrepareServer()));

            HttpRequestMessage request = new HttpRequestMessage();
            //request.Content = new ObjectContent<Order>(requestOrder, new JsonMediaTypeFormatter());
            request.RequestUri = new Uri(baseAddress +url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            request.Method = HttpMethod.Get;

            CancellationTokenSource cts = new CancellationTokenSource();

            _logger.Info(request.Method + ": " + request.RequestUri.AbsoluteUri);
            using (HttpResponseMessage response = messageInvoker.SendAsync(request, cts.Token).Result)
            {
                Assert.IsNotNull(response.Content);
                try
                {
                    return response.Content.ReadAsAsync<T>().Result;
                }
                catch (Exception ex)
                {
                    string message = "";
                    try
                    {
                        HttpError error = response.Content.ReadAsAsync<HttpError>().Result;
                        message = error.Message + error.MessageDetail + error.ExceptionMessage + error.StackTrace;
                    }
                    catch (Exception inner)
                    {
                        message = ex.Message;
                    }
                    ErrorLogger.ThrowAndLog(message, ex);
                    return default(T);
                }
                
            }
        }

        public HttpServer PrepareServer()
        {
            HttpConfiguration config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            Bootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.Initialize(NinjectWebCommon.CreateKernel);
            config.DependencyResolver = new NinjectDependencyResolver(NinjectWebCommon.CreateKernel());

            NinjectWebCommon.Start();

            return new HttpServer(config);
        }
    }
}
