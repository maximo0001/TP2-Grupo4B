using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace TP_WinForm
{
    public partial class frmImagenes : Form
    {
        
        public frmImagenes()
        {
            InitializeComponent();
        }

        private void frmImagenes_Load(object sender, EventArgs e)
        {
            cargar();

            
        }
        private void cargar()
        {
            try
            {
                ImagenNegocio negocio = new ImagenNegocio();
                dgvImagenes.DataSource = negocio.Listar(int.Parse(txtIdArticulo.Text));
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvImagenes_SelectionChanged(object sender, EventArgs e)
        {
            Imagen seleccionado = (Imagen)dgvImagenes.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.UrlImagen);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxImagen.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxImagen.Load("https://static.wikia.nocookie.net/207f8103-c56c-4b65-8ee6-658436d05e6e/scale-to-width/755");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ImagenNegocio negocio = new ImagenNegocio();
            Imagen imagen = new Imagen();

            try
            {
                imagen.UrlImagen = txtAgregar.Text;
                imagen.IdArticulo = int.Parse(txtIdArticulo.Text);
                negocio.Agregar(imagen);
                txtAgregar.Text = "";
                cargar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Imagen seleccionado = new Imagen();
            ImagenNegocio negocio = new ImagenNegocio();

            try
            {
                DialogResult respuesta = MessageBox.Show("Una vez eliminada la Imagen ya no podrás recuperarla. \n¿Estás seguro que deseás eliminarla?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Imagen)dgvImagenes.CurrentRow.DataBoundItem;
                    negocio.Eliminar(seleccionado.Id);
                    cargar();
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
