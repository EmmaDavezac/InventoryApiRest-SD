using ejemplo_api_rest.Models;

namespace ejemplo_api_rest.Repository
{
    public interface IProductoRepository
    {
       IEnumerable<Producto> GetAll();
        Producto? GetById(int id);
        void Add(Producto producto);
        Boolean Update(Producto producto);
        Boolean Delete(int id);
    }
}
