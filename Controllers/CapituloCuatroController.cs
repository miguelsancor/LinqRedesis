using CapacitacionInicial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CapacitacionInicial.Controllers
{
    public class CapituloCuatroController : ApiController
    {
        private WebflorEntities conexion = new WebflorEntities();


        [Route("Api/listarImpuesto")]
        [HttpGet]

        public IHttpActionResult listarImpuesto()
        {
            try
            {
                var listImpuesto = conexion.Impuesto.OrderBy(x => x.Descripcion).ToList();

                return Ok(listImpuesto);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("Api/guardarImpuesto")]
        [HttpPut]

        public IHttpActionResult guardarImpuesto(Impuesto impuesto)
        {
            try
            {
                impuesto.FechaAuditoria = DateTime.Now;
                conexion.Impuesto.Add(impuesto);
                conexion.SaveChanges();
                return Ok(impuesto);

            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [Route("api/actualizarImpuesto")]
        [HttpPost]

        public IHttpActionResult actualizarImpuesto(Impuesto impuesto)
        {
            try
            {

                impuesto.FechaAuditoria = DateTime.Now;
                conexion.Entry(impuesto).State = System.Data.Entity.EntityState.Modified;
                conexion.SaveChanges();

                return Ok(impuesto);
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }

        [Route("api/eliminarImpuesto")]
        [HttpDelete]

        public IHttpActionResult eliminarImpuesto(int IdImpuesto)
        {

            try
            {
                var impuesto = conexion.Impuesto.Find(IdImpuesto);

                conexion.Entry(impuesto).State = System.Data.Entity.EntityState.Deleted;
                conexion.SaveChanges();
                return Ok(impuesto);

            }
            catch (Exception)
            {

                return BadRequest();
            }


        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                conexion.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
