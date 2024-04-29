using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> Listar()
        {
            List<Articulo>lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {              
                datos.setConsulta("select a.id, a.codigo, a.Nombre, a.Descripcion, a.Precio, m.descripcion Marca, c.descripcion Categoria from ARTICULOS a inner join MARCAS m on m.id = a.IdMarca inner join CATEGORIAS c on c.Id = a.IdCategoria");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["id"];
                    aux.Codigo = (string)datos.Lector["codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (float)(decimal)datos.Lector["Precio"];
                   

                    aux.Marca = new Marca();
                    aux.Categoria = new Categoria();

                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    lista.Add(aux);

                }
                datos.cerrarConexion();
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        public void Agregar(Articulo nuevo/*, Imagen nueva*/)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                //datos.setConsulta("insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio ) values (@codigo, @nombre, @descripcion, @marca, @categoria, @precio) insert into IMAGENES (IdArticulo, ImagenUrl) select MAX(id), @imagen from ARTICULOS");
                datos.setConsulta("insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio ) values (@codigo, @nombre, @descripcion, @marca, @categoria, @precio)");
                datos.setearParametro("@codigo", nuevo.Codigo);
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@descripcion", nuevo.Descripcion);
                datos.setearParametro("@marca", nuevo.Marca.Id);
                datos.setearParametro("@categoria", nuevo.Categoria.Id);
                datos.setearParametro("@precio", nuevo.Precio);

                datos.ejecutarAccion();
                //datos.setearParametro("@imagen", nueva.UrlImagen);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("delete from ARTICULOS where id = @id delete from IMAGENES where IdArticulo = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}
