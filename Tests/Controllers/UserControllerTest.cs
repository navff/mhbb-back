using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using API;
using API.App_Start;
using API.Common;
using API.Controllers;
using API.Models;
using API.Operations;
using API.ViewModels;
using Ninject.Web.Common;

namespace Tests.Controllers
{
    [TestClass]
    public class UserControllerTest : BaseControllerTest
    {
        private UserController _controller;

        public UserControllerTest()
        {
            UserOperations userOperations = new UserOperations(_context);
            _controller = new UserController(userOperations);
            MapperMappings.Map();
        }

        [TestMethod]
        public void GetByEmail_Ok_Test()
        {
            var user = _context.Users.First();
            var result = _controller.Get(user.Email).Result;
            var userFromResult = ( (OkNegotiatedContentResult<User>) result).Content;
            Assert.IsNotNull(userFromResult);
            Assert.AreEqual(user.Email, userFromResult.Email);
            Assert.AreEqual(user.AuthToken, userFromResult.AuthToken);
            Assert.AreEqual(user.CityId, userFromResult.CityId);
            Assert.AreEqual(user.Phone, userFromResult.Phone);
        }

        [TestMethod]
        public void Put_Ok_Test()
        {
            PrepareController(_controller, userName:"var@33kita.ru", role:"PortalAdmin");
            var user = _context.Users.First();
            var rndString = Guid.NewGuid().ToString();

            var result = _controller.Put(user.Email, new UserViewModelPut
            {
                Email = user.Email,
                Name = rndString,
                Phone = rndString,
                Role = Role.PortalAdmin,
                CityId = user.CityId
            }).Result;

            var userFromResult = ((OkNegotiatedContentResult<User>)result).Content;
            Assert.IsNotNull(userFromResult);
            Assert.AreEqual(user.Email, userFromResult.Email);
            Assert.AreEqual(user.AuthToken, userFromResult.AuthToken);
            Assert.AreEqual(user.CityId, userFromResult.CityId);
            Assert.AreEqual(rndString, userFromResult.Phone);
        }

        [TestMethod]
        public void Put_WrongUser_Test()
        {
            PrepareController(_controller, userName: "sdsdsdsdsdsd@33kita.ru", role: "RegisteredUser");
            var user = _context.Users.First();
            var rndString = Guid.NewGuid().ToString();

            var result = _controller.Put(user.Email, new UserViewModelPut
            {
                Email = user.Email,
                Name = rndString,
                Phone = rndString,
                Role = Role.PortalAdmin,
                CityId = user.CityId
            }).Result;

            Assert.IsInstanceOfType(result, typeof(ResponseMessageResult));
            var responseMessageResult = (ResponseMessageResult) result;
            Assert.IsTrue(responseMessageResult.Response.StatusCode == HttpStatusCode.Unauthorized); 

        }

        [TestMethod]
        public void Register_Ok_User()
        {
            var rndString = Guid.NewGuid().ToString();

            var result = _controller.Register(new UserRegisterViewModel
            {
                Email = rndString + "@33kita.ru"
            }).Result;

            var userFromResult = ((OkNegotiatedContentResult<User>)result).Content;
            Assert.IsNotNull(userFromResult);
            Assert.AreEqual(rndString+"@33kita.ru", userFromResult.Email);
        }

        [TestMethod]
        public void Register_EmptyEmail_User()
        {
            var result = _controller.Register(new UserRegisterViewModel
            {
                Email = ""
            }).Result;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestHttp()
        {

            string baseAddress = "http://localhost/";

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

            HttpServer server = new HttpServer(config);


            HttpMessageInvoker messageInvoker = new HttpMessageInvoker(new InMemoryHttpContentSerializationHandler(server));

            HttpRequestMessage request = new HttpRequestMessage();
            //request.Content = new ObjectContent<Order>(requestOrder, new JsonMediaTypeFormatter());
            request.RequestUri = new Uri(baseAddress + "api/user?email=var@33kita.ru");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            request.Method = HttpMethod.Get;

            CancellationTokenSource cts = new CancellationTokenSource();

            using (HttpResponseMessage response = messageInvoker.SendAsync(request, cts.Token).Result)
            {
                Assert.IsNotNull(response.Content);
                var result = response.Content.ReadAsAsync<UserViewModelGet>().Result;
                Assert.AreEqual("var@33kita.ru", result.Email);
            }


        }

    }
}
