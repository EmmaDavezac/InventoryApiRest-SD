using ejemplo_api_rest.Models;
using ejemplo_api_rest.Repository;

namespace ejemplo_api_rest.Services
{
    public class ProductoService:IProductoService
    {
        private  IProductoRepository productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            this.productoRepository = productoRepository;
        }

        public IEnumerable<Producto> GetAll()
        {
            return productoRepository.GetAll();
        }

        public Producto? GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del producto debe ser mayor que cero.");

            var producto = productoRepository.GetById(id);

            if (producto == null)
                throw new KeyNotFoundException($"No se encontró un producto con ID {id}.");

            return producto;
        }

        public Producto Add(Producto producto)
        {
            ValidarProducto(producto, validarId: false);

            productoRepository.Add(producto);
            return producto;
        }

        public bool Update(Producto producto)
        {
            ValidarProducto(producto, validarId: true);

            var actualizado = productoRepository.Update(producto);
            if (!actualizado)
                throw new KeyNotFoundException($"No se encontró el producto con ID {producto.Id} para actualizar.");

            return true;
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del producto debe ser mayor que cero.");

            var eliminado = productoRepository.Delete(id);
            if (!eliminado)
                throw new KeyNotFoundException($"No se encontró el producto con ID {id} para eliminar.");

            return true;
        }

        // 🔑 Método privado de validación
        private void ValidarProducto(Producto producto, bool validarId)
        {
            if (producto == null)
                throw new ArgumentNullException(nameof(producto), "El producto no puede ser nulo.");

            if (validarId && producto.Id <= 0)
                throw new ArgumentException("El ID del producto debe ser mayor que cero.");

            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new ArgumentException("El nombre del producto no puede estar vacío.");

            if (producto.Precio <= 0)
                throw new ArgumentException("El precio del producto debe ser mayor que cero.");

            if (producto.Stock < 0)
                throw new ArgumentException("El stock no puede ser negativo.");
        }
    }
}
