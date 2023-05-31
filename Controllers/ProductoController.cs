using ProductApi.Domain;
using ProductApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProductApi.Controllers
{
    public class ProductoController : ApiController
    {
        private IProductoDomain _Domain;
        public ProductoController(IProductoDomain Domain)
        {
            _Domain = Domain;
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreaProducto([FromBody]ClsProducto producto)
        {
            try
            {
                var response = await _Domain.Crea_Mod_Producto(producto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> ModProducto([FromBody] ClsProducto producto)
        {
            try
            {
                var response = await _Domain.Crea_Mod_Producto(producto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> ListaProductos_nombre(string nombre, string orden)
        {
            try
            {
                var response = await _Domain.ListaProductos_nombre(nombre, orden);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> ListaProductos_descrip(string descrip)
        {
            try
            {
                var response = await _Domain.ListaProductos_descrip(descrip);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> ListaProductos_categoria(string categoria, string orden)
        {
            try
            {
                var response = await _Domain.ListaProductos_categoria(categoria, orden);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete]
        public async Task<IHttpActionResult> EliminaProducto(int productoId)
        {
            try
            {
                var response = await _Domain.EliminaProducto(productoId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreaCategoria([FromBody] ClsCategoria categoria)
        {
            try
            {
                var response = await _Domain.Crea_Mod_Categoria(categoria);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> ModCategoria([FromBody] ClsCategoria categoria)
        {
            try
            {
                var response = await _Domain.Crea_Mod_Categoria(categoria);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> ListaCategorias()
        {
            try
            {
                var response = await _Domain.ListaCategorias();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete]
        public async Task<IHttpActionResult> EliminaCategoria(int categoriaId)
        {
            try
            {
                var response = await _Domain.EliminaCategoria(categoriaId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

    }
}
