using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trino.WPF.ProjetoFinal.Data.Repositorios.Interface;

namespace Trino.WPF.ProjetoFinal.Data.Repositorios
{
    public class EstadoRepositorio : IEstadoRepositorio
    {
        private TrinoProjetoFinalDBEntities _context;

        public EstadoRepositorio()
        {
            this._context = new TrinoProjetoFinalDBEntities();
        }

        public IEnumerable<State> Get()
        {
            return this._context.States;
        }
    }
}
