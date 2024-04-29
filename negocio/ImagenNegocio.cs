using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ImagenNegocio
    {
        public List<Categoria> Listar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Categoria> lista = new List<Categoria>();

            try
            {
                datos.setConsulta("select id, idArticulo, ImagenUrl from IMAGENES where IdArticulo = @idArt");
                datos.setearParametro("@idArt", id);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = (int)datos.Lector["id"];
                    aux.Id = (int)datos.Lector["idArticulo"];
                    aux.Descripcion = (string)datos.Lector["ImagenUrl"];
                    lista.Add(aux);
                }
                return lista;
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
