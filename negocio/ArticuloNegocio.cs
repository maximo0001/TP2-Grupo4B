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
        public List<Articulo> listar()
        {
            List<Articulo>lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT a.id, a.codigo, a.Nombre, a.Descripcion, a.Precio, m.descripcion AS Marca, c.descripcion AS Categoria, MIN(i.ImagenUrl) AS ImagenUrl FROM ARTICULOS a inner join MARCAS m ON m.id = a.IdMarca left join CATEGORIAS c ON c.Id = a.IdCategoria inner join IMAGENES i ON i.IdArticulo = a.id GROUP BY a.id, a.codigo, a.Nombre, a.Descripcion, a.Precio, m.descripcion, c.descripcion");
                datos.ejecutarLectura();

                while(datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["id"];
                    aux.Codigo = (string)datos.Lector["codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["a.Descripcion"];
                    aux.Precio = (float)datos.Lector["Precio"];
                    aux.UrlImagen = (string)datos.Lector["a.ImagenUrl"];

                    aux.Marca = new Marca();
                    aux.Categoria = new Categoria();
                    
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    lista.Add(aux);
                }

                datos.cerrarConexion();
                return listar();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
