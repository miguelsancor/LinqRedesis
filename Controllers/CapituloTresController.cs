using CapacitacionInicial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CapacitacionInicial.Controllers
{
    public class CapituloTresController : ApiController
    {
        private WebflorEntities conexion = new WebflorEntities();

        [Route("Api/ListMoneda")]
        [HttpGet]

        public IHttpActionResult ListMoneda()
        {
            var listPais = conexion.Moneda.OrderBy(x=>x.NomMoneda).ToList();


            return Ok(listPais);
        }

        [Route("Api/GuardarMoneda")]
        [HttpPut]

        public IHttpActionResult GuardarMoneda(Moneda moneda)
        {
            moneda.FechaAuditoria = DateTime.Now;
            conexion.Moneda.Add(moneda);
            conexion.SaveChanges();

            return Ok(moneda);
        }


        [Route("Api/ActualizarMoneda")]
        [HttpPost]

        public IHttpActionResult ActualizarMoneda(Moneda moneda)
        {
            moneda.FechaAuditoria = DateTime.Now;
            conexion.Entry(moneda).State = System.Data.Entity.EntityState.Modified;
            conexion.SaveChanges();

            return Ok(moneda);
        }

        [Route("api/eliminarMoneda")]
        [HttpDelete]

        public IHttpActionResult eliminarMoneda(int IdMoneda)
        {

            try
            {
                var moneda = conexion.Moneda.Find(IdMoneda);

                conexion.Entry(moneda).State = System.Data.Entity.EntityState.Deleted;
                conexion.SaveChanges();
                return Ok(moneda);

            }
            catch (Exception)
            {

                return BadRequest();
            }


        }
    }
}
