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

namespace TP_WinForm
{
    public partial class frmMarcas : Form
    {
        private List<Marca> listaMarca;
        public frmMarcas()
        {
            InitializeComponent();
        }
        private void frmMarcas_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            try
            {
                MarcaNegocio negocio = new MarcaNegocio();
                listaMarca = negocio.Listar();
                dgvMarca.DataSource = listaMarca;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

            private void btnEliminar_Click(object sender, EventArgs e)
        {
            Marca seleccionado = new Marca();
            MarcaNegocio negocio = new MarcaNegocio();

            try
            {
                DialogResult respuesta = MessageBox.Show("Una vez eliminada la marca ya no podrás recuperarla. \n¿Estás seguro que deseás eliminarla?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Marca)dgvMarca.CurrentRow.DataBoundItem;
                    negocio.Eliminar(seleccionado.Id);
                    cargar();
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            MarcaNegocio negocio = new MarcaNegocio();
            Marca marca = new Marca();

            try
            {
                marca.Descripcion = txtAgregar.Text;
                negocio.Agregar(marca);
                MessageBox.Show("Agregado con exito");
                cargar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
