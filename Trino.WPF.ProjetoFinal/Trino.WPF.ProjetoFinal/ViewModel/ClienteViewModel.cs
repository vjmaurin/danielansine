using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Trino.WPF.ProjetoFinal.Data;
using Trino.WPF.ProjetoFinal.Data.Repositorios;
using Trino.WPF.ProjetoFinal.Data.Repositorios.Interface;

namespace Trino.WPF.ProjetoFinal.ViewModel
{
    public class ClienteViewModel : INotifyPropertyChanged
    {
        public ClienteViewModel(IClienteRepositorio clienteRepositorio)
            : this(clienteRepositorio, new Customer())
        {

        }

        public ClienteViewModel(IClienteRepositorio clienteRepositorio, Customer customer)
        {
            this._clienteRepositorio = clienteRepositorio;
            this.Customer = customer;

            this.SaveCommand = new DelegateCommand<Window>(this.Execute_SaveCommand);
            this.CancelCommand = new DelegateCommand<Window>(this.Execute_CancelCommand);
            this._estadoRepositorio = new EstadoRepositorio();

            var states = this._estadoRepositorio.Get().OrderBy(s => s.Name);
            this.States = new ObservableCollection<State>(states);

            if (this.Customer.City != null)
            {
                this.SelectedState = this.States.FirstOrDefault(s => s.Id.Equals(this.Customer.City.StateId));
                this.SelectedCity = this.SelectedState.Cities.FirstOrDefault(c => c.Id.Equals(this.Customer.CityId));
            }
        }

        private IClienteRepositorio _clienteRepositorio;
        public Customer Customer { get; set; }

        private State _selectedState;
        public State SelectedState
        {
            get { return this._selectedState; }
            set
            {
                if (this._selectedState == value)
                    return;

                this._selectedState = value;
                this.RaisePropertyChanged("SelectedState");
            }
        }
        
        private City _selectedCity;
        public City SelectedCity
        {
            get { return this._selectedCity; }
            set
            {
                if (this._selectedCity == value)
                    return;

                this._selectedCity = value;
                this.RaisePropertyChanged("SelectedCity");
            }
        }

        private IEstadoRepositorio _estadoRepositorio;

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Execute_CancelCommand(Window window)
        {
            if (window != null)
                window.Close();
        }

        private void Execute_SaveCommand(Window window)
        {
            if (this.SelectedCity != null)
                this.Customer.CityId = this.SelectedCity.Id;

            if (this.Customer.Id == Guid.Empty)
                this._clienteRepositorio.Inserir(this.Customer);
            else
                this._clienteRepositorio.Atualizar(this.Customer);

            if (window != null)
                window.Close();
        }

        public DelegateCommand<Window> SaveCommand { get; set; }
        public DelegateCommand<Window> CancelCommand { get; set; }
        public ObservableCollection<State> States { get; set; }
    }
}
