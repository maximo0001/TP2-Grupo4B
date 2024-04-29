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
                datos.setConsulta("select a.id, a.codigo, a.Nombre, a.Descripcion, a.Precio, m.descripcion Marca, c.descripcion Categoria, c.Id idCat, m.Id idMarca from ARTICULOS a inner join MARCAS m on m.id = a.IdMarca inner join CATEGORIAS c on c.Id = a.IdCategoria");
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

                    aux.Marca.Id = (int)datos.Lector["idMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria.Id = (int)datos.Lector["idCat"];
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

        public void Modificar(Articulo art)
        {
            AccesoDatos datos = new AccesoDatos();
            
            try
            {

                datos.setConsulta("UPDATE ARTICULOS SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMar, IdCategoria = @IdCat, Precio = @Precio WHERE Id = @Id");
                datos.setearParametro("@Codigo", art.Codigo);
                datos.setearParametro("@Nombre", art.Nombre);
                datos.setearParametro("@Descripcion", art.Descripcion);
                datos.setearParametro("@IdMar", art.Marca.Id);
                datos.setearParametro("@IdCat", art.Categoria.Id);
                datos.setearParametro("@Precio", art.Precio);
                datos.setearParametro("@Id", art.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion();}

        }

        public List<Articulo> Filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "select a.id, a.codigo, a.Nombre, a.Descripcion, a.Precio, m.descripcion Marca, c.descripcion Categoria, c.Id idCat, m.Id idMarca from ARTICULOS a inner join MARCAS m on m.id = a.IdMarca inner join CATEGORIAS c on c.Id = a.IdCategoria WHERE ";
                if (campo == "Id")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "a.id > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "a.id < " + filtro;
                            break;
                        default:
                            consulta += "a.id = " + filtro;
                            break;
                    }
                }
                else if (campo == "Codigo")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "a.codigo LIKE '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "a.codigo LIKE '%" + filtro + "'";
                            break;
                        default:
                            consulta += "a.codigo LIKE '%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "a.Nombre LIKE '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "a.Nombre LIKE '%" + filtro + "'";
                            break;
                        default:
                            consulta += "a.Nombre LIKE '%" + filtro + "%'";
                            break;
                    }
                }
                datos.setConsulta(consulta);

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

                    aux.Marca.Id = (int)datos.Lector["idMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria.Id = (int)datos.Lector["idCat"];
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
    }
}
