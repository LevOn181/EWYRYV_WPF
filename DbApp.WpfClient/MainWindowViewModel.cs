using EWYRYV_HFT_202223.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DbApp.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<Player> Players { get; set; }

        private Player selectedPlayer;
            
        public Player SelectedPlayer
        {
            get { return selectedPlayer; }
            set 
            { 
                if(value != null)
                {
                    selectedPlayer = new Player()
                    {
                        Name = value.Name,
                        PlayerId = value.PlayerId,
                    };
                }
                OnPropertyChanged();
                (DeletePlayerCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }


        public ICommand CreatePlayerCommand { get; set; }
        public ICommand DeletePlayerCommand { get; set; }
        public ICommand UpdatePlayerCommand { get; set; }

        public static bool IsInDesingMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public MainWindowViewModel() 
        {
            
            if (!IsInDesingMode)
            {
                Players = new RestCollection<Player>("http://localhost:15885/", "player", "hub");
                CreatePlayerCommand = new RelayCommand(() =>
                {
                    Players.Add(new Player()
                    {
                        Name = selectedPlayer.Name
                    });
                });

                UpdatePlayerCommand = new RelayCommand(() =>
                {
                    Players.Update(SelectedPlayer);
                });

                DeletePlayerCommand = new RelayCommand(() =>
                {
                    Players.Delete(SelectedPlayer.PlayerId);
                },
                () =>
                {
                    return SelectedPlayer != null;
                });
                SelectedPlayer = new Player();
            }
        }
    }
}
