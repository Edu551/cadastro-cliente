using ExercicioCadastroCliente.JsonPersistencia.Atributos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExercicioCadastroCliente.Models
{
    public class Cliente
    {
        public Cliente() 
        { 
            this.Id = Guid.NewGuid().ToString();
        }

        [Identidade(NomeNoBancoDeDados = "Id")]
        public string Id { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }
    }
}
