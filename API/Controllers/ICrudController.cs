using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API.Controllers
{

    public interface ICrudController
    {
        /// <summary>
        /// Получает сущность по её Id
        /// </summary>
        /// <param name="id">Id сущности</param>
        IHttpActionResult Get(int id);

        /// <summary>
        /// Сохраняет объект
        /// </summary>
        /// <param name="id">Id редактируемого объекта</param>
        /// <param name="putViewModel"></param>
        IHttpActionResult Put(int id, object putViewModel);

        /// <summary>
        /// Сохраняет объект
        /// </summary>
        /// <param name="postViewModel"></param>
        /// <returns></returns>
        IHttpActionResult Post(object postViewModel);

        /// <summary>
        /// Удаляет объект
        /// </summary>
        /// <param name="id">ID удаляемого объекта</param>
        IHttpActionResult Delete(int id);
    }
}