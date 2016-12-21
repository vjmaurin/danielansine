using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trino.WPF.ProjetoFinal.Data.Repositorios.Interface
{
    public interface IClienteRepositorio
    {
        void Inserir(Customer cliente);
        void Remover(Customer cliente);
        void Atualizar(Customer cliente);
        IEnumerable<Customer> Get();
    }
}
