using ExercicioCadastroCliente.JsonPersistencia;
using ExercicioCadastroCliente.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExercicioCadastroCliente
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private Cliente ClienteSelecionado()
        {
            if (dataGridViewClientes.SelectedRows.Count == 0)
                return null;

            Cliente clienteSelecionado = new Cliente();

            DataGridViewRow row = this.dataGridViewClientes.SelectedRows[0];

            clienteSelecionado.Id = row.Cells["Id"].Value.ToString();
            clienteSelecionado.Nome = row.Cells["Nome"].Value.ToString();
            clienteSelecionado.Telefone = row.Cells["Telefone"].Value.ToString();

            return clienteSelecionado;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            carregarDadosGrid();
        }

        private void carregarDadosGrid()
        {
            var listaClientes = new PersistenciaJson<Cliente>();

            BindingSource bindingSource = new BindingSource();

            bindingSource.DataSource = listaClientes.Buscar();

            dataGridViewClientes.DataSource = bindingSource;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {

            var persistencia = new PersistenciaJson<Cliente>();
            var cliente = new Cliente();


            cliente.Nome = txtName.Text;
            cliente.Telefone = txtPhoneNumber.Text;

            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            {
                MessageBox.Show("Preencha todos os campos!");
                return;
            }

            persistencia.Incluir(cliente);

            txtName.Text = string.Empty;
            txtPhoneNumber.Text = string.Empty;

            MessageBox.Show("Cliente cadastrado com sucesso!");

            Log.Information($"Cliente {cliente.Nome} cadastrado as {DateTime.Now}");

            carregarDadosGrid();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewClientes.SelectedRows.Count != 0)
            {       
                txtNameEdit.Text = ClienteSelecionado().Nome;
                txtPhoneNumberEdit.Text = ClienteSelecionado().Telefone;

                if (!string.IsNullOrWhiteSpace(txtNameEdit.Text) && !string.IsNullOrWhiteSpace(txtPhoneNumberEdit.Text))
                    btnSaveEdit.Enabled = true;
            }
        }

        private void btnSaveEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNameEdit.Text) || string.IsNullOrWhiteSpace(txtPhoneNumberEdit.Text))
            {
                MessageBox.Show("Preencha todos os campos!");
                return;
            }

            var persistencia = new PersistenciaJson<Cliente>();
            var clienteEditado = new Cliente();

            var nomeAntigo = ClienteSelecionado().Nome;

            clienteEditado.Id = ClienteSelecionado().Id;
            clienteEditado.Nome = txtNameEdit.Text;
            clienteEditado.Telefone = txtPhoneNumberEdit.Text;

            persistencia.Atualizar(clienteEditado);

            Log.Information($"Cliente {clienteEditado.Nome} editado, nome antigo: {nomeAntigo}.");

            txtNameEdit.Text = string.Empty;
            txtPhoneNumberEdit.Text = string.Empty;

            btnSaveEdit.Enabled = false;
            MessageBox.Show("Cliente atualizado com sucesso!");


            carregarDadosGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewClientes.SelectedRows.Count != 0)
            {
                var persistencia = new PersistenciaJson<Cliente>();
                var apagarCliente = new Cliente();

                apagarCliente.Id = ClienteSelecionado().Id;

                Log.Information($"Cliente {ClienteSelecionado().Nome} deletado.");

                persistencia.Apagar(apagarCliente);

                MessageBox.Show("Cliente deletado com sucesso!");
                carregarDadosGrid();
            }
        }
    }
}
