using CapacitacionInicial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CapacitacionInicial.Controllers
{
    public class CapituloSeisController : ApiController
    {
        private WebflorEntities conexion = new WebflorEntities();

        [Route("Api/listarSiembraCama")]
        [HttpGet]

        public IHttpActionResult listarSiembraCama()
        {
            try
            {
                var listarSiembraCama = from s in conexion.SiembraCama
                                        group s by new { s.SemanaSiembra, s.Area, s.Planta } into grupo
                                        select new
                                        {
                                            Semana = grupo.Key.SemanaSiembra,
                                            TotalAreas = grupo.Sum(x=>x.Area),
                                            PromedioPlanta = grupo.Average(x=>x.Planta)
   
                                  };

                return Ok(listarSiembraCama);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


    }
}
