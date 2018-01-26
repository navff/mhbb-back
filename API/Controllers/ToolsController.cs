using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Camps.Api.Utils;
using Models.Entities;
using Models.Operations;

namespace API.Controllers
{
    /// <summary>
    /// Инструменты для управления сайтом
    /// </summary>
    [RoutePrefix("api/tools")]
    public class ToolsController : ApiController
    {
        private string _baseUrl;
        private ActivityOperations _activityOperations;
        public ToolsController(ActivityOperations activityOperations)
        {
            _activityOperations = activityOperations;
            _baseUrl = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"];
        }


        /// <summary>
        /// Отдаёт sitemap.xml
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("sitemap")]
        public async Task<HttpResponseMessage> Sitemap()
        {
            var xmlWriter = new SitemapXmlWriter();
            var activities = await _activityOperations.SearchAsync(isChecked: true, page:-1);

            // для активностей формируем урлы
            List<SitemapUrl> urls = GetActivitiesUrls(activities);

            urls.Add(new SitemapUrl
            {
                Priority = SitemapPagePriority.HIGH,
                ChangeFreq = SitemapPageChangeFrec.DAILY,
                Modified = DateTimeOffset.Now,
                Url = _baseUrl + "/"
            });

            urls.Add(new SitemapUrl
            {
                Priority = SitemapPagePriority.HIGH,
                ChangeFreq = SitemapPageChangeFrec.DAILY,
                Modified = DateTimeOffset.Now,
                Url = _baseUrl + "/about"
            });

            urls.Add(new SitemapUrl
            {
                Priority = SitemapPagePriority.HIGH,
                ChangeFreq = SitemapPageChangeFrec.DAILY,
                Modified = DateTimeOffset.Now,
                Url = _baseUrl + "/sponsor"
            });

            urls.Add(new SitemapUrl
            {
                Priority = SitemapPagePriority.HIGH,
                ChangeFreq = SitemapPageChangeFrec.DAILY,
                Modified = DateTimeOffset.Now,
                Url = _baseUrl + "/member"
            });


            // Формируем XML в MemoryStream
            var stream = xmlWriter.CreateXml(urls);
            // Полученный поток преобразуем в файл и отдаём наружу
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(stream.ToArray())
            };
            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "sitemap.xml"
                };
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/xml");
            stream.Close();
            return result;
        }

        private List<SitemapUrl> GetActivitiesUrls(IEnumerable<Activity> activities)
        {
            return activities.Select(a => new SitemapUrl
            {
                Priority = SitemapPagePriority.HIGH,
                ChangeFreq = SitemapPageChangeFrec.WEEKLY,
                Modified = DateTimeOffset.Now,
                Url = _baseUrl + "/act/" + a.Id
            }).ToList();
        }

        

    }
}