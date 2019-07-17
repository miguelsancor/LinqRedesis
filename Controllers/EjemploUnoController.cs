using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CapacitacionInicial.Controllers
{
    public class EjemploUnoController : ApiController
    {

        [HttpGet]
        public IHttpActionResult Ejemplo1()
        {
            var objeto = new { Status = true, mensaje = "Prueba 2" };

            return Ok(objeto);
        }

        [HttpGet]
        public IHttpActionResult Ejemplo2(string palabra)
        {
            int contador = 0;
            char c;
            foreach (char v in palabra)
            {
                c = Char.ToLower(v);
                if (c == 'a' || c == 'e' || c == 'i' || c == '0' || c == 'u')
                    contador++;
            }
            return Ok("Cantidad de Vocales: "+contador);

        }

        [Route("api/tareaUno")]
        [HttpGet]
        public IHttpActionResult tareaUno(string parrafo)
        {
            int palabras = 0;
            int caracterestotales = 0;
            int contador = 0;
            char c;

            for (int i = 0; i < parrafo.Length; i++)
            {
                if (parrafo[i] == ' ' || parrafo[i] == '.')
                {
                    palabras++;
                }
            }
            int[] PalabrasCaracteres = new int[2];
            caracterestotales = parrafo.Length - palabras;
            PalabrasCaracteres[0] = palabras+1;
            PalabrasCaracteres[1] = caracterestotales;

            foreach (char v in parrafo)
            {
                c = Char.ToLower(v);
                if (c == 'a' || c == 'e' || c == 'i' || c == '0' || c == 'u')
                    contador++;
            }

            var Result = "Cantidad Palabras :" + PalabrasCaracteres[0] + " Cantidad Caracteres :" + PalabrasCaracteres[1] + " Cantidad de Vocales: " + contador;

            return Ok(Result);
        }
    }
}
