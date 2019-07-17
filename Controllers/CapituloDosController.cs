using CapacitacionInicial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CapacitacionInicial.Controllers
{
    public class CapituloDosController : ApiController
    {
        private WebflorEntities conexion = new WebflorEntities();

        [HttpGet]
        public IHttpActionResult listPais()
        {
            var listPais = conexion.Pais.OrderBy(p => p.NomPais).Where(x => x.IdPais > 2);

            return Ok(listPais);
        }
        [Route("api/listPais2")]
        [HttpGet]
        public IHttpActionResult listPais2(int pais)
        {
            var objPais = conexion.Pais.Find(pais);

            return Ok(objPais);
        }

        [Route ("api/listPais3")]
        [HttpGet]
        public IHttpActionResult listPais3(string pais)
        {
            var objPais = conexion.Pais.FirstOrDefault(x => x.CodigoGenerico == pais);

            return Ok(objPais);
        }
        [Route("api/listPais4")]
        [HttpGet]
        public IHttpActionResult listPais4()
        {
            var listPais = from p in conexion.Pais
                           where p.IdPais > 2
                           orderby p.NomPais
                           select p.NomPais;

            return Ok(listPais);
        }

        [Route("api/listPais5")]
        [HttpGet]
        public IHttpActionResult listPais5()
        {
            var listPais = from p in conexion.Pais
                           where p.IdPais > 2
                           orderby p.NomPais
                           select new { p.NomPais,
                               poblacion = 3

                           };


            return Ok(listPais);
        }

        [Route("api/listPais6")]
        [HttpGet]
        public IHttpActionResult listPais6()
        {
            var listPais = from p in conexion.Pais
                          join c in conexion.Ciudad
                          on p.IdPais equals c.IdPais
                           orderby p.NomPais, c.NomCiudad descending
                           where c.NomCiudad.Contains("ME")
                           select new
                           {
                               p.NomPais,
                               c.NomCiudad
                           };

            return Ok(listPais);
        }


        [Route("api/listInvernadero1")]
        [HttpGet]
        public IHttpActionResult listInvernadero1()
        {
            var listInvernadero = from p in conexion.Invernadero
                           join c in conexion.CamaFisica
                           on p.IdInvernadero equals c.IdInvernadero
                                  orderby p.NomInvernadero descending
                           where p.NomInvernadero.Contains("14B")
                           select new
                           {
                               p.IdInvernadero,
                               p.NomInvernadero,
                               c.NumeroCama

                           };

            return Ok(listInvernadero);
        }

        [Route("api/listInvernadero2")]
        [HttpGet]
        public IHttpActionResult listInvernadero2(int iIdInvernadero)
        {
            var totalAreaUtilizada = conexion.CamaFisica.Select(v => v.AreaUtilizada).Sum();

            var listInvernadero = from p in conexion.Invernadero
                                  join c in conexion.CamaFisica
                                  on p.IdInvernadero equals c.IdInvernadero
                                  where c.Valvula > 0 && p.IdInvernadero == iIdInvernadero
                                  orderby p.NomInvernadero descending
                                  
                                  select new
                                  {
                                      p.IdInvernadero,
                                      p.NomInvernadero,
                                      nave = c.PickNave,
                                      c.NumeroCama,
                                      sitemaSiembra = c.PickSistemaSiembra,
                                      c.Largo,
                                      c.Valvula,
                                      Suma = totalAreaUtilizada
                                  };

            return Ok(listInvernadero);
        }

        [Route("api/crearPais")]
        [HttpPut]

        public IHttpActionResult crearPais(Pais pais)
        {
            pais.FechaAuditoria = DateTime.Now;
            conexion.Pais.Add(pais);
            conexion.SaveChanges();
            return Ok(pais);
        }

        [Route("api/editarPais")]
        [HttpPost]

        public IHttpActionResult editarPais(Pais pais)
        {
            pais.FechaAuditoria = DateTime.Now;
            conexion.Entry(pais).State= System.Data.Entity.EntityState.Modified;
            conexion.SaveChanges();
            return Ok(pais);
        }

        [Route("api/eliminarPais")]
        [HttpDelete]

        public IHttpActionResult eliminarPais(int IdPais)
        {

            try
            {
                var pais = conexion.Pais.Find(IdPais);

                conexion.Entry(pais).State = System.Data.Entity.EntityState.Deleted;
                conexion.SaveChanges();
                return Ok(pais);

            }
            catch (Exception)
            {

                return BadRequest();
            }
           
           
        }
    }
}
