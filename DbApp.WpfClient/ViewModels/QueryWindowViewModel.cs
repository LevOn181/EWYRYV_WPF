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
        public RestCollection<IEnumerable<object>> HungarianManagers { get; set; }


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
                HungarianManagers = new RestCollection<IEnumerable<object>>("http://localhost:15885/", "Stat", "hub");
            }
        }
    }
}
