﻿using EWYRYV_HFT_202223.Models;
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
    internal class PlayerWindowViewModel : ObservableRecipient
    {
        public RestCollection<Player> Players { get; set; }

        private Player selectedPlayer;
        public Player SelectedPlayer
        {
            get { return selectedPlayer; }
            set
            {
                if (value != null)
                {
                    selectedPlayer = new Player()
                    {
                        Name = value.Name,
                        PlayerId = value.PlayerId,
                        BirthDate = value.BirthDate,
                        KitNumber = value.KitNumber,
                        TeamId = value.TeamId,
                        Value = value.Value
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
        public PlayerWindowViewModel()
        {

            if (!IsInDesingMode)
            {
                Players = new RestCollection<Player>("http://localhost:15885/", "player", "hub");
                CreatePlayerCommand = new RelayCommand(() =>
                {
                    Players.Add(new Player()
                    {
                        Name = selectedPlayer.Name,
                        BirthDate = selectedPlayer.BirthDate,
                        KitNumber = selectedPlayer.KitNumber,
                        TeamId = selectedPlayer.TeamId,
                        Value = selectedPlayer.Value
                        
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
