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
            ImagenNegocio negocio = new ImagenNegocio();
            dgvImagenes.DataSource = negocio.Listar(int.Parse(txtIdArticulo.Text));

            
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
    }
}
