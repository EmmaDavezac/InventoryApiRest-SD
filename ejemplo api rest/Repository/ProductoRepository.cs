using ejemplo_api_rest.Models;
using System.Collections.Generic;
namespace ejemplo_api_rest.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private List<Producto> productos = new List<Producto>();
        public ProductoRepository()
        {
            // Inicializar la lista con 10 productos de ejemplo
           productos = new List<Producto>
            {
                new Producto { Id = 1, Nombre = "Mouse Gamer", Precio = 3500, Stock = 15 },
                new Producto { Id = 2, Nombre = "Teclado Mecánico", Precio = 9500, Stock = 10 },
                new Producto { Id = 3, Nombre = "Monitor 24''", Precio = 45000, Stock = 5 },
                new Producto { Id = 4, Nombre = "Auriculares", Precio = 7200, Stock = 20 },
                new Producto { Id = 5, Nombre = "Notebook", Precio = 250000, Stock = 3 },
                new Producto { Id = 6, Nombre = "Tablet", Precio = 85000, Stock = 7 },
                new Producto { Id = 7, Nombre = "Pendrive 64GB", Precio = 2800, Stock = 50 },
                new Producto { Id = 8, Nombre = "Disco SSD 1TB", Precio = 68000, Stock = 8 },
                new Producto { Id = 9, Nombre = "Impresora", Precio = 38000, Stock = 4 },
                new Producto { Id = 10, Nombre = "Placa de Video RTX 4060", Precio = 350000, Stock = 2 }
            };
        }

        public IEnumerable<Producto> GetAll()
        {
            return productos;
        }

        public Producto? GetById(int id)
        {
            var producto = productos.FirstOrDefault(p => p.Id == id);
            return producto;
        }

        public void Add(Producto producto)
        {
            producto.Id = productos.Max(p => p.Id) + 1;
            productos.Add(producto);
        }

        public Boolean Update(Producto producto)
        {
            var existente = productos.FirstOrDefault(p => p.Id == producto.Id);
            if (existente != null)
            {
                existente.Nombre = producto.Nombre;
                existente.Precio = producto.Precio;
                existente.Stock = producto.Stock;
                return true;
            }
            else return false;
          
        }

        public Boolean Delete(int id)
        {

            var producto = productos.FirstOrDefault(p => p.Id == id);
            if (producto != null)
            {
                productos.Remove(producto);
                return true;
            }
            return false;
        }
    }
}

