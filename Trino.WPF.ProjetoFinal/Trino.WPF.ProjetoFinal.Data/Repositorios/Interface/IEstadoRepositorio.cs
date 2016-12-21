using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trino.WPF.ProjetoFinal.Data.Repositorios.Interface
{
    public interface IEstadoRepositorio
    {
        IEnumerable<State> Get();
    }
}
