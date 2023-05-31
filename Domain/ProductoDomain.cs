using ProductApi.Models;
using ProductApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProductApi.Domain
{
    public class ProductoDomain : IProductoDomain
    {
        private readonly IProductoRepository _ProductoRepository;
        public ProductoDomain(IProductoRepository ProductoRepository)
        {
            _ProductoRepository = ProductoRepository;
        }

        public async Task<string> Crea_Mod_Producto(ClsProducto producto)
        {
            return await _ProductoRepository.Crea_Mod_Producto(producto);
        }
        public async Task<List<ClsProducto>> ListaProductos_nombre(string nombre, string orden)
        {
            return await _ProductoRepository.ListaProductos_nombre(nombre, orden);
        }
        public async Task<List<ClsProducto>> ListaProductos_descrip(string descrip)
        {
            return await _ProductoRepository.ListaProductos_descrip(descrip);
        }
        public async Task<List<ClsProducto>> ListaProductos_categoria(string categoria, string orden)
        {
            return await _ProductoRepository.ListaProductos_categoria(categoria, orden);
        }
        public async Task<string> EliminaProducto(int productoId)
        {
            return await _ProductoRepository.EliminaProducto(productoId);
        }
        public async Task<string> Crea_Mod_Categoria(ClsCategoria categoria)
        {
            return await _ProductoRepository.Crea_Mod_Categoria(categoria);
        }
        public async Task<List<ClsCategoria>> ListaCategorias()
        {
            return await _ProductoRepository.ListaCategorias();
        }
        public async Task<string> EliminaCategoria(int categoriaId)
        {
            return await _ProductoRepository.EliminaCategoria(categoriaId);
        }
    }
}