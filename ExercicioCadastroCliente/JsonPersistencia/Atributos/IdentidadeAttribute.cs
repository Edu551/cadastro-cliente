using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExercicioCadastroCliente.JsonPersistencia.Atributos
{
    public class IdentidadeAttribute : Attribute
    {
        public string NomeNoBancoDeDados { get; set; } = default;
    }
}
