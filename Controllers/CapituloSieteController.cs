using CapacitacionInicial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CapacitacionInicial.Controllers
{
    public class CapituloSieteController : ApiController
    {
        private WebflorEntities conexion = new WebflorEntities();


        [Route("Api/listarPedido")]
        [HttpGet]

        public IHttpActionResult listarPedido()
        {
            try
            {
                var lstResult = from p in conexion.Pedido
                                select new
                                {
                                    p.IdPedido,
                                    p.IdCliente,
                                    p.FechaOrden,
                                    items = (from i in conexion.PedidoItem where i.IdPedido == p.IdPedido select new {i.IdPedidoItem, i.IdTipoCaja })
                                };

                return Ok(lstResult);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("Api/listarPedido2")]
        [HttpGet]

        public IHttpActionResult listarPedido2()
        {
            try
            {
                var lstResult = from p in conexion.Pedido
                                select new
                                {
                                    p.IdPedido,
                                    p.IdCliente,
                                    p.FechaOrden,
                                    cantidad = (conexion.PedidoItem.Count(i => i.IdPedido == p.IdPedido)),
                                   
                                };

                return Ok(lstResult);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("Api/listarPedido3")]
        [HttpGet]

        public IHttpActionResult listarPedido3()
        {
            try
            {
                var lstResult = from p in conexion.Pedido
                                select new
                                {
                                    p.IdPedido,
                                    p.IdCliente,
                                    p.FechaOrden,
                                    sumatoria = (conexion.PedidoItem.Where(i => i.IdPedido == p.IdPedido).Sum(x => x.ValorTotal)),

                                };

                return Ok(lstResult);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("Api/listarPedido4")]
        [HttpGet]

        public IHttpActionResult listarPedido4()
        {
            try
            {
                var lstResult = from p in conexion.Pedido
                                select new
                                {
                                    p.IdPedido,
                                    p.IdCliente,
                                    p.FechaOrden,
                                    sumatoria = (conexion.PedidoItem.Where(i => i.IdPedido == p.IdPedido).Sum(x => x.ValorTotal)),

                                };
                var lstResultDos = from l in lstResult
                                   join c in conexion.Cliente
                                   on l.IdCliente equals c.IdCliente
                                   select new
                                   {
                                       l.IdPedido,
                                       l.IdCliente,
                                       l.FechaOrden,
                                       l.sumatoria,
                                       c.NomCliente
                                   };

                return Ok(lstResultDos);
            }
            catch (Exception)
            {
                return BadRequest();
            }



        }


        [Route("Api/listarCliente")]
        [HttpGet]

        public IHttpActionResult listarCliente()
        {
            try
            {
                var lstResult = from c in conexion.Cliente
                                where c.EstadoCliente == true
                                select new
                                {
                                    c.IdCliente,
                                    c.NomCliente,
                                    c.EstadoCliente,
                                    Contactos = (from cc in conexion.ClienteContacto where c.IdCliente == cc.IdCliente select new { cc.IdClienteContacto, cc.NomContacto}),
                                    Sucursal = (from s in conexion.ClienteSucursal where c.IdCliente == s.IdCliente select new { s.IdClienteSucursal, s.NomSucursal })
                                };
                
                return Ok(lstResult);
            }
            catch (Exception)
            {
                return BadRequest();
            }
       }

        [Route("Api/listarCliente2")]
        [HttpGet]

        public IHttpActionResult listarCliente2()
        {
            try
            {
                var lstResult = from c in conexion.Cliente
                                select new
                                {
                                    c.IdCliente,
                                    c.NomCliente,
                                    cantidadContactos = (conexion.ClienteContacto.Count(cc => cc.IdCliente == c.IdCliente)),
                                };

                var lstResultDos = from l in lstResult
                                   join c in conexion.Cliente
                                   on l.IdCliente equals c.IdCliente
                                   where l.cantidadContactos >= 2
                                   select new
                                   {
                                       l.IdCliente,
                                       l.NomCliente,
                                       CantidadContatosMayorDos = l.cantidadContactos
                                   };

                return Ok(lstResultDos);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
