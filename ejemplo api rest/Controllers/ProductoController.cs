using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ejemplo_api_rest.Models;
using ejemplo_api_rest.Services;
using ejemplo_api_rest.Repository;
using System.Collections.Generic;

namespace ejemplo_api_rest.Controllers
{
    [ApiController]
    [Route("api/inventory")]/// ruta para el getall
    public class ProductoController : ControllerBase
    {
        private IProductoService productoService;
       // private IProductoRepository productoRepository;
       // private bool repositorioIniciado = false;
        public ProductoController(IProductoService service )
        {
               // this.productoRepository = new ProductoRepository();
               // repositorioIniciado = true;
                this.productoService = service;
            
            
        }

        // GET api/inventory
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(productoService.GetAll());
        }

        // GET api/inventory/id
        [HttpGet("{idProducto}")]
        public IActionResult GetById(int idProducto)
        {
            var producto = productoService.GetById(idProducto);
            if (producto == null)
            {
                return NotFound();
            }
            else
            { return Ok(producto); }
        }

        // POST api/inventory
        //Los parametros se especifican a traves del body
        [HttpPost]
        public IActionResult Create([FromBody] Producto producto)
        {
            productoService.Add(producto);
            return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto);
        }


        //PUT api/inventory/idProducto
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Producto producto)
        {
            if (id != producto.Id)
                producto.Id = id;
            var updated = productoService.Update(producto);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        //DELETE api/producto/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = productoService.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok(new { Mensaje = "Producto Eliminado" });
        }

        //GET api/producto/stock/id
        [HttpGet("check/{id}/{cantidad}")]
        public IActionResult ConsultarStock(int id,int cantidad)
        {
            var producto = productoService.GetById(id);
            if (producto == null) { return NotFound(); }
            else  if (producto.Stock >= cantidad)
            {
                return Ok(new { Disponible = true, producto.Stock });
            }
            else
            {
                return Ok(new { Disponible = false, producto.Stock });
            }
        }

        // POST api/inventory/order
        [HttpPost("order")]
        public IActionResult ConfirmarOrden([FromBody] Orden orden)
        {
            var producto = productoService.GetById(orden.IdProducto);
            if (producto == null)
            { return NotFound("Producto no encontrado"); }
            else
            {
                if (producto.Stock < orden.cantidad)
                {
                    return BadRequest("Stock insuficiente");
                }
                else
                {
                    producto.Stock -= orden.cantidad;
                    productoService.Update(producto);
                    return Ok(new { Mensaje = "Orden realizada con éxito", producto });
                }
            }







        }
    }

    }
