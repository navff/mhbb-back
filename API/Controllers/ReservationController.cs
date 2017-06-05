using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using API.Common;
using API.Models;
using API.ViewModels;
using AutoMapper;
using Camps.Tools;
using Models.Entities;
using Models.Operations;

namespace API.Controllers
{
    /// <summary>
    /// Заявки на бронирование активности. Людям нравится активность и они сообщают 
    /// о том, что оно им надо.
    /// </summary>
    [RoutePrefix("api/reservation")]
    public class ReservationController : ApiController
    {
        private readonly ReservationOperations _reservationOperations;

        public ReservationController(ReservationOperations reservationOperations)
        {
            _reservationOperations = reservationOperations;
        }

        /// <summary>
        /// Получение заявки
        /// </summary>
        [HttpGet]
        [ResponseType(typeof(ReservationViewModelGet))]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var reservation = await _reservationOperations.GetAsync(id);
                if (reservation == null) return this.Result404("This reservation is not found");

                var result = Mapper.Map<ReservationViewModelGet>(reservation);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT GET RESERVATION", ex);
                throw;
            }
        }

        /// <summary>
        /// Обновление. Могут только менеджеры и админы портала
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [RESTAuthorize(Role.PortalAdmin, Role.PortalManager)]
        [ResponseType(typeof(ReservationViewModelGet))]
        public async Task<IHttpActionResult> Put(int id, ReservationViewModelPost viewModel)
        {
            try
            {
                var res = await _reservationOperations.GetAsync(id);
                if (res == null) return this.Result404("This reservation is not found");

                var reservation = Mapper.Map<Reservation>(viewModel);
                reservation.Id = id;
                await _reservationOperations.UpdateAsync(reservation);
                return await Get(id);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT PUT RESERVATION", ex);
                throw;
            }
        }

        /// <summary>
        /// Добавление
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ReservationViewModelGet))]
        public async Task<IHttpActionResult> Post(ReservationViewModelPost viewModel)
        {
            try
            {
                var reservation = Mapper.Map<Reservation>(viewModel);
                var result = await _reservationOperations.AddAsync(reservation);
                return await Get(result.Id);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT POST RESERVATION", ex);
                throw;
            }
        }

        /// <summary>
        /// Удаление. Могут только менеджеры и админы портала
        /// </summary>
        [HttpDelete]
        [RESTAuthorize(Role.PortalAdmin, Role.PortalManager)]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var reservation = await _reservationOperations.GetAsync(id);
                if (reservation == null) return this.Result404("This reservation is not found");

                await _reservationOperations.DeleteAsync(id);
                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT DELETE RESERVATION", ex);
                throw;
            }
        }


        /// <summary>
        /// Ищет по телефону, имени, email
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("search")]
        [ResponseType(typeof(IEnumerable<ReservationViewModelGet>))]
        public async Task<IHttpActionResult> Search(string word)
        {
            try
            {
                var reservations = await _reservationOperations.SearchAsync(word);
                var result = Mapper.Map<IEnumerable<ReservationViewModelGet>>(reservations);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT SEARCH RESERVATION", ex);
                throw;
            }
        }
    }
}