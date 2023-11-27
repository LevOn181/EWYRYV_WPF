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

namespace DbApp.WpfClient.ViewModels
{
    internal class QueryWindowViewModel : ObservableRecipient
    {
        public RestCollection<object> CountPlayers { get; set; }
        public RestCollection<object> TeamValue { get; set; }
        public RestCollection<object> MostValuable { get; set; }
        public RestCollection<object> HungarianManagers { get; set; }
        public RestCollection<object> TopPlayerData { get; set; }


        public static bool IsInDesingMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public QueryWindowViewModel()
        {
            if (!IsInDesingMode)
            {
                CountPlayers = new RestCollection<object>("http://localhost:15885/", "stat/countPlayers", "hub");
                TeamValue = new RestCollection<object>("http://localhost:15885/", "stat/teamValue", "hub");
                MostValuable = new RestCollection<object>("http://localhost:15885/", "stat/mostValuable", "hub");
                HungarianManagers = new RestCollection<object>("http://localhost:15885/", "stat/hungarianManagers", "hub");
                TopPlayerData = new RestCollection<object>("http://localhost:15885/", "stat/topPlayerData", "hub");   
            }
        }
    }
}
