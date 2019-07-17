using CapacitacionInicial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CapacitacionInicial.Controllers
{
    public class CapituloCincoController : ApiController
    {
        private WebflorEntities conexion = new WebflorEntities();


        [Route("Api/listarFactura")]
        [HttpGet]

        public IHttpActionResult listarFactura()
        {
            try
            {
                var listFactura = from f in conexion.Factura
                                  group f by f.IdCliente into grupo
                                  select new
                                  {
                                      cliente = grupo.Key,
                                      cantidad = grupo.Count()
                                  };

                return Ok(listFactura);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("Api/listarFacturaDos")]
        [HttpGet]

        public IHttpActionResult listarFacturaDos()
        {
            try
            {
                var listFactura = conexion.Factura.Select(f => f.TotalFactura).Sum();

                return Ok(listFactura);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [Route("Api/listarFacturaTres")]
        [HttpGet]

        public IHttpActionResult listarFacturaTres()
        {
            try
            {
                var listFactura = from f in conexion.Factura
                                  group f by f.IdCliente into grupo
                                  select new
                                  {
                                      cliente = grupo.Key,
                                      total = grupo.Sum(x => x.TotalFactura)
                                  };

                return Ok(listFactura);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("Api/listarPedidoCuatro")]
        [HttpGet]

        public IHttpActionResult listarPedidoCuatro()
        {
            try
            {
                var listPedido = from p in conexion.Pedido
                                  group p by p.IdCliente into grupo
                                  select new
                                  {
                                      cliente = grupo.Key,
                                      total = grupo.Sum(x => x.IdPedido)
                                  };

                return Ok(listPedido);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


    }
}
