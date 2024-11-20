using GalaSoft.MvvmLight.Command;
using Grpc.Core;
using Shared;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Notify;
using WPF.Services;

namespace WPF.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {


        public MainWindowViewModel(IEnumerable<Worker> workers)
        {
            Workers = new ObservableCollection<Worker>(workers);
            SelectedWorker = Workers.FirstOrDefault();
            Initialize();
            FillEnums();
        }

        private void Initialize()
        {
            DeleteCommand = new RelayCommand(Delete, CanDelete);
            UpdateCommand = new RelayCommand(Update, CanUpdate);
            CreateCommand = new RelayCommand(Create, CanCreate);

            Sexes = new ObservableCollection<Sex>();
        }

        private void FillEnums()
        {
            foreach (Sex item in Enum.GetValues(typeof(Sex)))
            {
                Sexes.Add(item);
            }
        }

        private ObservableCollection<Worker> _workers;
        public ObservableCollection<Worker> Workers
        {
            get { return _workers; }
            set
            {
                _workers = value;
                OnPropertyChanged();
            }
        }

        private Worker _selectedWorker;
        public Worker SelectedWorker
        {
            get { return _selectedWorker; }
            set
            {
                _selectedWorker = value;
                if(_selectedWorker != null) FillInfoWorker();
                OnPropertyChanged();
            }
        }

       

        private Worker _infoWorker;
        public Worker InfoWorker
        {
            get { return _infoWorker; }
            set
            {
                _infoWorker = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Sex> _sexes;
        public ObservableCollection<Sex> Sexes
        {
            get { return _sexes; }
            set
            {
                _sexes = value;
                OnPropertyChanged();
            }
        }

        public ICommand DeleteCommand { get; set; }
        private bool CanDelete() => SelectedWorker != null;
        private async void Delete()
        {
            try
            {
                await WorkerServiceClient.ChangeWorker(SelectedWorker, Shared.Action.Delete);
                Workers.Remove(SelectedWorker);
            } catch (Exception ex)
            {
                MessageBoxCreater.ShowError(ex.Message);
            }
        }

        public ICommand UpdateCommand { get; set; }
        private bool CanUpdate() => ValidInfoWorker() && InfoWorker.ID != 0;
        private async void Update()
        {
            try
            {
                var newWorker = await WorkerServiceClient.ChangeWorker(InfoWorker, Shared.Action.Update);
                var worker = Workers.FirstOrDefault(x=>x.ID == InfoWorker.ID);
                if(worker != null)
                {
                    UpdateWorker(worker, newWorker);
                }
            }
            catch (Exception ex)
            {
                MessageBoxCreater.ShowError(ex.Message);
            }
        }

        public ICommand CreateCommand { get; set; }
        private bool CanCreate() => ValidInfoWorker();
        private async void Create()
        {
            try
            {
                var newWorker = await WorkerServiceClient.ChangeWorker(InfoWorker, Shared.Action.Create);
                Workers.Add(newWorker);
            }
            catch (Exception ex)
            {
                MessageBoxCreater.ShowError(ex.Message);
            }
        }

        private void FillInfoWorker()
        {
            InfoWorker = new Worker()
            {
                ID = SelectedWorker.ID,
                FirstName = SelectedWorker.FirstName,
                LastName = SelectedWorker.LastName,
                MiddleName = SelectedWorker.MiddleName,
                Birthday = SelectedWorker.Birthday,
                Sex = SelectedWorker.Sex,
                HaveChildren = SelectedWorker.HaveChildren
            };
        }

        private void UpdateWorker(Worker replaced,Worker toReplace)
        {
            replaced.FirstName = toReplace.FirstName;
            replaced.LastName = toReplace.LastName;
            replaced.MiddleName = toReplace.MiddleName;
            replaced.Birthday = toReplace.Birthday;
            replaced.Sex = toReplace.Sex;
            replaced.HaveChildren = toReplace.HaveChildren;
        }

        private bool ValidInfoWorker()
        {
            return InfoWorker != null && InfoWorker.Birthday != null
                && !string.IsNullOrWhiteSpace(InfoWorker.FirstName) 
                && !string.IsNullOrWhiteSpace(InfoWorker.LastName)
                && !string.IsNullOrWhiteSpace(InfoWorker.MiddleName);
        }


    }
}
