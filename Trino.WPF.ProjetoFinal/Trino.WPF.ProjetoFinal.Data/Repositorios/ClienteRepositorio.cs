using System;
using System.Collections.Generic;
using Trino.WPF.ProjetoFinal.Data.Repositorios.Interface;

namespace Trino.WPF.ProjetoFinal.Data.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        
        private TrinoProjetoFinalDBEntities _context;

        public ClienteRepositorio()
        {
            this._context = new TrinoProjetoFinalDBEntities();
        }

        public void Atualizar(Customer cliente)
        {
            this._context.SaveChanges();
        }

        public IEnumerable<Customer> Get()
        {
            return this._context.Customers.Include("City");
        }

        public void Inserir(Customer cliente)
        {
            if (cliente == null)
                return;

            if (cliente.Id == Guid.Empty)
                cliente.Id = Guid.NewGuid();


            this._context.Customers.Add(cliente);
            this._context.SaveChanges();
        }

        public void Remover(Customer cliente)
        {
            this._context.Customers.Remove(cliente);
            this._context.SaveChanges();
        }


    }
}
