using ejemplo_api_rest.Models;

namespace ejemplo_api_rest.Services
{
    public interface IProductoService
    {
        public IEnumerable<Producto> GetAll();
        public Producto? GetById(int id);
        public Producto Add(Producto producto);
        public bool Update(Producto producto);
        public bool Delete(int id);
    }
}
