using Microsoft.Practices.Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using Trino.WPF.ProjetoFinal.Data;
using Trino.WPF.ProjetoFinal.Data.Repositorios;
using Trino.WPF.ProjetoFinal.Data.Repositorios.Interface;
using Trino.WPF.ProjetoFinal.View;

namespace Trino.WPF.ProjetoFinal.ViewModel
{
    public class MainWindowViewModel
    {
        private IClienteRepositorio _clienteRepositorio;

        public MainWindowViewModel()
        {
            this.InsertCommand = new DelegateCommand<object>(this.Execute_InsertCommand);
            this.EditCommand = new DelegateCommand<object>(this.Execute_EditCommand);
            this.RemoveCommand = new DelegateCommand<object>(this.Execute_RemoveCommand);
            this.RefreshCommand = new DelegateCommand<object>(this.Execute_RefreshCommand);
            this.ExitCommand = new DelegateCommand<Window>(this.Execute_ExitCommand);
            this._clienteRepositorio = new ClienteRepositorio();

            var customers = this._clienteRepositorio.Get();
            this.Customers = new ObservableCollection<Customer>(customers);
        }

        private void Execute_ExitCommand(Window window)
        {
            if (window != null)
                window.Close();
        }

        private void Execute_RefreshCommand(object obj)
        {
            this.RefreshData();
        }

        private void RefreshData()
        {
            this.Customers.Clear();
            foreach (var cliente in this._clienteRepositorio.Get())
                this.Customers.Add(cliente);
        }

        private void Execute_RemoveCommand(object obj)
        {
            if (this.SelectedCustomer == null)
                return;

            this._clienteRepositorio.Remover(this.SelectedCustomer);
            this.RefreshData();
        }

        private void Execute_EditCommand(object obj)
        {
            var clientView = new ClienteView() { DataContext = new ClienteViewModel(this._clienteRepositorio, this.SelectedCustomer) };
            clientView.ShowDialog();
            this.RefreshData();
        }

        private void Execute_InsertCommand(object obj)
        {
            var clienteTela = new ClienteView() { DataContext = new ClienteViewModel(this._clienteRepositorio) };
            clienteTela.ShowDialog();
            this.RefreshData();
        }

        public DelegateCommand<object> InsertCommand { get; set; }
        public DelegateCommand<object> EditCommand { get; set; }
        public DelegateCommand<object> RemoveCommand { get; set; }
        public DelegateCommand<object> RefreshCommand { get; set; }
        public DelegateCommand<Window> ExitCommand { get; set; }

        public Customer SelectedCustomer { get; set; }

        public ObservableCollection<Customer> Customers { get; set; }
    }
}
