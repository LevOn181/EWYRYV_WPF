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

namespace DbApp.WpfClient.ViewModels
{
    internal class TeamWindowViewModel : ObservableRecipient
    {
        public RestCollection<Team> Teams { get; set; }

        private Team selectedTeam;
        public Team SelectedTeam
        {
            get { return selectedTeam; }
            set
            {
                if (value != null)
                {
                    selectedTeam = new Team()
                    {
                        Name = value.Name,
                        TeamId = value.TeamId,
                        FoundationYear = value.FoundationYear,
                        Manager = value.Manager
                    };
                }
                OnPropertyChanged();
                (DeleteTeamCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }


        public ICommand CreateTeamCommand { get; set; }
        public ICommand DeleteTeamCommand { get; set; }
        public ICommand UpdateTeamCommand { get; set; }

        public static bool IsInDesingMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public TeamWindowViewModel()
        {

            if (!IsInDesingMode)
            {
                Teams = new RestCollection<Team>("http://localhost:15885/", "Team", "hub");
                CreateTeamCommand = new RelayCommand(() =>
                {
                    Teams.Add(new Team()
                    {
                        Name = selectedTeam.Name,
                        TeamId = selectedTeam.TeamId,
                        Manager = selectedTeam.Manager,
                        FoundationYear= selectedTeam.FoundationYear,
                        
                    });
                });

                UpdateTeamCommand = new RelayCommand(() =>
                {
                    Teams.Update(SelectedTeam);
                });

                DeleteTeamCommand = new RelayCommand(() =>
                {
                    Teams.Delete(SelectedTeam.TeamId);
                },
                () =>
                {
                    return SelectedTeam != null;
                });
                SelectedTeam = new Team();
            }
        }
    }
}
