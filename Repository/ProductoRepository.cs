using ProductApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ModelBinding;

namespace ProductApi.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        public async Task<string> Crea_Mod_Producto(ClsProducto producto)
        {
            string resp = "";
            try
            {
                using (ProductApiEntities db = new ProductApiEntities())
                {
                    if (producto.P_ID == 0)
                    {
                        var oIMG = new PRODUCTO_IMAGEN();
                        oIMG.IMG_DATA = producto.IMAGEN.IMG_DATA;
                        oIMG.IMG_NOMBRE = producto.IMAGEN.IMG_NOMBRE;
                        oIMG.IMG_EXTENSION = producto.IMAGEN.IMG_EXTENSION;
                        db.PRODUCTO_IMAGEN.Add(oIMG);
                        db.SaveChanges();
                        int imagenId = oIMG.IMG_ID;

                        var oPRODUCTO = new PRODUCTO();
                        oPRODUCTO.P_NOMBRE = producto.P_NOMBRE;
                        oPRODUCTO.P_DESCRIPCION = producto.P_DESCRIPCION;
                        oPRODUCTO.P_CATEGORIA_ID = producto.CATEGORIA.C_ID;
                        oPRODUCTO.P_IMAGEN_ID = imagenId;
                        db.PRODUCTO.Add(oPRODUCTO);
                        db.SaveChanges();
                        int productoId = oPRODUCTO.P_ID;
                        resp = "Producto almacenado correctamente";
                    }
                    else
                    {
                        var oIMG = db.PRODUCTO_IMAGEN.Find(producto.IMAGEN.IMG_ID);
                        oIMG.IMG_DATA = producto.IMAGEN.IMG_DATA;
                        oIMG.IMG_NOMBRE = producto.IMAGEN.IMG_NOMBRE;
                        oIMG.IMG_EXTENSION = producto.IMAGEN.IMG_EXTENSION;
                        db.Entry(oIMG).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        var oPRODUCTO = db.PRODUCTO.Find(producto.P_ID);
                        oPRODUCTO.P_NOMBRE = producto.P_NOMBRE;
                        oPRODUCTO.P_DESCRIPCION = producto.P_DESCRIPCION;
                        oPRODUCTO.P_CATEGORIA_ID = producto.CATEGORIA.C_ID;
                        oPRODUCTO.P_IMAGEN_ID = producto.IMAGEN.IMG_ID;
                        db.Entry(oPRODUCTO).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        resp = "Producto modificado correctamente";
                    }
                }
            }
            catch (Exception ex)
            {
                resp = ex.Message;
            }
            return resp;
        }

        public async Task<List<ClsProducto>> ListaProductos_nombre(string nombre, string orden)
        {
            List<ClsProducto> resp = new List<ClsProducto>();
            var result = (dynamic)null;
            try
            {
                using (ProductApiEntities db = new ProductApiEntities())
                {
                    if (orden == "asc")
                    {
                        result = await (from prod in db.PRODUCTO
                                        join cat in db.CATEGORIA on prod.P_CATEGORIA_ID equals cat.C_ID
                                        join img in db.PRODUCTO_IMAGEN on prod.P_IMAGEN_ID equals img.IMG_ID
                                        where prod.P_NOMBRE.Contains(nombre)
                                        orderby prod.P_NOMBRE ascending
                                        select new
                                        {
                                            P_ID = prod.P_ID,
                                            P_NOMBRE = prod.P_NOMBRE,
                                            P_DESCRIPCION = prod.P_DESCRIPCION,
                                            C_ID = cat.C_ID,
                                            C_DESCRIPCION = cat.C_DESCRIPCION,
                                            IMG_ID = img.IMG_ID,
                                            IMG_DATA = img.IMG_DATA,
                                            IMG_NOMBRE = img.IMG_NOMBRE,
                                            IMG_EXTENSION = img.IMG_EXTENSION
                                        }).ToListAsync();
                    }
                    else
                    {
                        result = await (from prod in db.PRODUCTO
                                        join cat in db.CATEGORIA on prod.P_CATEGORIA_ID equals cat.C_ID
                                        join img in db.PRODUCTO_IMAGEN on prod.P_IMAGEN_ID equals img.IMG_ID
                                        where prod.P_NOMBRE.Contains(nombre)
                                        orderby prod.P_NOMBRE descending
                                        select new
                                        {
                                            P_ID = prod.P_ID,
                                            P_NOMBRE = prod.P_NOMBRE,
                                            P_DESCRIPCION = prod.P_DESCRIPCION,
                                            C_ID = cat.C_ID,
                                            C_DESCRIPCION = cat.C_DESCRIPCION,
                                            IMG_ID = img.IMG_ID,
                                            IMG_DATA = img.IMG_DATA,
                                            IMG_NOMBRE = img.IMG_NOMBRE,
                                            IMG_EXTENSION = img.IMG_EXTENSION
                                        }).ToListAsync();
                    }
                    foreach (var item in result)
                    {
                        ClsProducto producto = new ClsProducto();
                        ClsCategoria categoria = new ClsCategoria();
                        ClsProductoImg imagen = new ClsProductoImg();
                        producto.P_ID = item.P_ID;
                        producto.P_NOMBRE = item.P_NOMBRE;
                        producto.P_DESCRIPCION = item.P_DESCRIPCION;

                        categoria.C_ID = item.C_ID;
                        categoria.C_DESCRIPCION = item.C_DESCRIPCION;
                        producto.CATEGORIA = categoria;

                        imagen.IMG_ID = item.IMG_ID;
                        imagen.IMG_DATA = item.IMG_DATA;
                        imagen.IMG_NOMBRE = item.IMG_NOMBRE;
                        imagen.IMG_EXTENSION = item.IMG_EXTENSION;
                        producto.IMAGEN = imagen;
                        resp.Add(producto);
                    }
                }
            }
            catch (Exception ex)
            {
                resp = null;
            }
            return resp;
        }

        public async Task<List<ClsProducto>> ListaProductos_descrip(string descrip)
        {
            List<ClsProducto> resp = new List<ClsProducto>();
            var result = (dynamic)null;
            try
            {
                using (ProductApiEntities db = new ProductApiEntities())
                {
                    result = await (from prod in db.PRODUCTO
                                    join cat in db.CATEGORIA on prod.P_CATEGORIA_ID equals cat.C_ID
                                    join img in db.PRODUCTO_IMAGEN on prod.P_IMAGEN_ID equals img.IMG_ID
                                    where prod.P_DESCRIPCION.Contains(descrip)
                                    orderby prod.P_NOMBRE descending
                                    select new
                                    {
                                        P_ID = prod.P_ID,
                                        P_NOMBRE = prod.P_NOMBRE,
                                        P_DESCRIPCION = prod.P_DESCRIPCION,
                                        C_ID = cat.C_ID,
                                        C_DESCRIPCION = cat.C_DESCRIPCION,
                                        IMG_ID = img.IMG_ID,
                                        IMG_DATA = img.IMG_DATA,
                                        IMG_NOMBRE = img.IMG_NOMBRE,
                                        IMG_EXTENSION = img.IMG_EXTENSION
                                    }).ToListAsync();

                    foreach (var item in result)
                    {
                        ClsProducto producto = new ClsProducto();
                        ClsCategoria categoria = new ClsCategoria();
                        ClsProductoImg imagen = new ClsProductoImg();
                        producto.P_ID = item.P_ID;
                        producto.P_NOMBRE = item.P_NOMBRE;
                        producto.P_DESCRIPCION = item.P_DESCRIPCION;

                        categoria.C_ID = item.C_ID;
                        categoria.C_DESCRIPCION = item.C_DESCRIPCION;
                        producto.CATEGORIA = categoria;

                        imagen.IMG_ID = item.IMG_ID;
                        imagen.IMG_DATA = item.IMG_DATA;
                        imagen.IMG_NOMBRE = item.IMG_NOMBRE;
                        imagen.IMG_EXTENSION = item.IMG_EXTENSION;
                        producto.IMAGEN = imagen;
                        resp.Add(producto);
                    }
                }
            }
            catch (Exception ex)
            {
                resp = null;
            }
            return resp;
        }

        public async Task<List<ClsProducto>> ListaProductos_categoria(string categoria, string orden)
        {
            List<ClsProducto> resp = new List<ClsProducto>();
            var result = (dynamic)null;
            try
            {
                using (ProductApiEntities db = new ProductApiEntities())
                {
                    if (orden == "asc")
                    {
                        result = await (from prod in db.PRODUCTO
                                        join cat in db.CATEGORIA on prod.P_CATEGORIA_ID equals cat.C_ID
                                        join img in db.PRODUCTO_IMAGEN on prod.P_IMAGEN_ID equals img.IMG_ID
                                        where cat.C_DESCRIPCION.Contains(categoria)
                                        orderby cat.C_DESCRIPCION ascending
                                        select new
                                        {
                                            P_ID = prod.P_ID,
                                            P_NOMBRE = prod.P_NOMBRE,
                                            P_DESCRIPCION = prod.P_DESCRIPCION,
                                            C_ID = cat.C_ID,
                                            C_DESCRIPCION = cat.C_DESCRIPCION,
                                            IMG_ID = img.IMG_ID,
                                            IMG_DATA = img.IMG_DATA,
                                            IMG_NOMBRE = img.IMG_NOMBRE,
                                            IMG_EXTENSION = img.IMG_EXTENSION
                                        }).ToListAsync();
                    }
                    else
                    {
                        result = await (from prod in db.PRODUCTO
                                        join cat in db.CATEGORIA on prod.P_CATEGORIA_ID equals cat.C_ID
                                        join img in db.PRODUCTO_IMAGEN on prod.P_IMAGEN_ID equals img.IMG_ID
                                        where cat.C_DESCRIPCION.Contains(categoria)
                                        orderby cat.C_DESCRIPCION descending
                                        select new
                                        {
                                            P_ID = prod.P_ID,
                                            P_NOMBRE = prod.P_NOMBRE,
                                            P_DESCRIPCION = prod.P_DESCRIPCION,
                                            C_ID = cat.C_ID,
                                            C_DESCRIPCION = cat.C_DESCRIPCION,
                                            IMG_ID = img.IMG_ID,
                                            IMG_DATA = img.IMG_DATA,
                                            IMG_NOMBRE = img.IMG_NOMBRE,
                                            IMG_EXTENSION = img.IMG_EXTENSION
                                        }).ToListAsync();
                    }
                    foreach (var item in result)
                    {
                        ClsProducto producto = new ClsProducto();
                        ClsCategoria cate = new ClsCategoria();
                        ClsProductoImg imagen = new ClsProductoImg();
                        producto.P_ID = item.P_ID;
                        producto.P_NOMBRE = item.P_NOMBRE;
                        producto.P_DESCRIPCION = item.P_DESCRIPCION;

                        cate.C_ID = item.C_ID;
                        cate.C_DESCRIPCION = item.C_DESCRIPCION;
                        producto.CATEGORIA = cate;

                        imagen.IMG_ID = item.IMG_ID;
                        imagen.IMG_DATA = item.IMG_DATA;
                        imagen.IMG_NOMBRE = item.IMG_NOMBRE;
                        imagen.IMG_EXTENSION = item.IMG_EXTENSION;
                        producto.IMAGEN = imagen;
                        resp.Add(producto);
                    }
                }
            }
            catch (Exception ex)
            {
                resp = null;
            }
            return resp;
        }

        public async Task<string> EliminaProducto(int productoId)
        {
            string resp = "";
            try
            {
                using (ProductApiEntities db = new ProductApiEntities())
                {
                    var oPRODUCTO = db.PRODUCTO.Find(productoId);
                    var oPRODUCTO_IMAGEN = db.PRODUCTO_IMAGEN.Find(oPRODUCTO.P_IMAGEN_ID);
                    db.PRODUCTO_IMAGEN.Remove(oPRODUCTO_IMAGEN);
                    db.PRODUCTO.Remove(oPRODUCTO);
                    db.SaveChanges();
                    resp = "Producto eliminado correctamente";
                }
            }
            catch (Exception ex)
            {
                resp = ex.Message;
            }
            return resp;
        }

        public async Task<string> Crea_Mod_Categoria(ClsCategoria categoria)
        {
            string resp = "";
            try
            {
                using (ProductApiEntities db = new ProductApiEntities())
                {
                    if (categoria.C_ID == 0)
                    {
                        var oCATEGORIA = new CATEGORIA();
                        oCATEGORIA.C_DESCRIPCION = categoria.C_DESCRIPCION;
                        db.CATEGORIA.Add(oCATEGORIA);
                        db.SaveChanges();
                        resp = "Categoria almacenada correctamente";
                    }
                    else
                    {
                        var oCATEGORIA = db.CATEGORIA.Find(categoria.C_ID);
                        oCATEGORIA.C_DESCRIPCION = categoria.C_DESCRIPCION;
                        db.Entry(oCATEGORIA).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        resp = "Categoria modificada correctamente";
                    }
                    
                }
            }
            catch (Exception ex)
            {
                resp = ex.Message;
            }
            return resp;
        }

        public async Task<List<ClsCategoria>> ListaCategorias()
        {
            List<ClsCategoria> resp = new List<ClsCategoria>();
            try
            {
                using (ProductApiEntities db = new ProductApiEntities())
                {
                    resp = await (from cat in db.CATEGORIA
                                  orderby cat.C_DESCRIPCION
                                  select new ClsCategoria
                                  {
                                      C_ID = cat.C_ID,
                                      C_DESCRIPCION = cat.C_DESCRIPCION
                                  }).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                resp = null;
            }
            return resp;
        }

        public async Task<string> EliminaCategoria(int categoriaId)
        {
            string resp = "";
            try
            {
                using (ProductApiEntities db = new ProductApiEntities())
                {
                    var oCATEGORIA = db.CATEGORIA.Find(categoriaId);
                    db.CATEGORIA.Remove(oCATEGORIA);
                    db.SaveChanges();
                    resp = "Categoria eliminada correctamente";
                }
            }
            catch (Exception ex)
            {
                resp = ex.Message;
            }
            return resp;
        }

    }
}