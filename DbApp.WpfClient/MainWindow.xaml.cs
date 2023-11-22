using DbApp.WpfClient.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DbApp.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void bt_managerWindow_Click(object sender, RoutedEventArgs e)
        {
            ManagerWindow managerWindow = new ManagerWindow();
            managerWindow.Show();
        }

        private void bt_teamWindow_Click(object sender, RoutedEventArgs e)
        {
            TeamWindow teamWindow = new TeamWindow();
            teamWindow.Show();
        }

        private void bt_playerWindow_Click(object sender, RoutedEventArgs e)
        {
            PlayerWindow playerWindow = new PlayerWindow();
            playerWindow.Show();
        }

        private void bt_queryWindow_click(object sender, RoutedEventArgs e)
        {
            QueryWindow queryWindow = new QueryWindow();
            queryWindow.Show();
        }
    }
}
