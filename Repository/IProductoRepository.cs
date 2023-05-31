using ProductApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProductApi.Repository
{
    public interface IProductoRepository
    {
        Task<string> Crea_Mod_Producto(ClsProducto producto);
        Task<List<ClsProducto>> ListaProductos_nombre(string nombre, string orden);
        Task<List<ClsProducto>> ListaProductos_descrip(string descrip);
        Task<List<ClsProducto>> ListaProductos_categoria(string categoria, string orden);
        Task<string> EliminaProducto(int productoId);
        Task<string> Crea_Mod_Categoria(ClsCategoria categoria);
        Task<List<ClsCategoria>> ListaCategorias();
        Task<string> EliminaCategoria(int categoriaId);
    }
}