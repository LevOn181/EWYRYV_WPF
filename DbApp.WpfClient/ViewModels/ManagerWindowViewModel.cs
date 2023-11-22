using EWYRYV_HFT_202223.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Newtonsoft.Json.Linq;

namespace DbApp.WpfClient.ViewModels
{
    internal class ManagerWindowViewModel : ObservableRecipient
    {
        public RestCollection<Manager> Managers { get; set; }

        private Manager selectedManager;
        public Manager SelectedManager{
            get { return selectedManager; }
            set
            {
                if (value != null)
                {
                    selectedManager = new Manager()
                    {
                        Name = value.Name,
                        ManagerId = value.ManagerId,
                        Nationality = value.Nationality,
                        TeamId = value.TeamId,
                    };
                }
                OnPropertyChanged();
                (DeleteManagerCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }


        public ICommand CreateManagerCommand { get; set; }
        public ICommand DeleteManagerCommand { get; set; }
        public ICommand UpdateManagerCommand { get; set; }

        public static bool IsInDesingMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public ManagerWindowViewModel()
        {

            if (!IsInDesingMode)
            {
                Managers = new RestCollection<Manager>("http://localhost:15885/", "Manager", "hub");
                CreateManagerCommand = new RelayCommand(() =>
                {
                    Managers.Add(new Manager()
                    {
                        Name = selectedManager.Name,
                        ManagerId = selectedManager.ManagerId,
                        Nationality = selectedManager.Nationality,
                        TeamId = selectedManager.TeamId
                    });
                });

                UpdateManagerCommand = new RelayCommand(() =>
                {
                    Managers.Update(SelectedManager);
                });

                DeleteManagerCommand = new RelayCommand(() =>
                {
                    Managers.Delete(SelectedManager.ManagerId);
                },
                () =>
                {
                    return SelectedManager != null;
                });
                SelectedManager = new Manager();
            }
        }
    }
}