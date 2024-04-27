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
    }
}
