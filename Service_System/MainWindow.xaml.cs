using Service_System.Entity;
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
using System.Windows.Media.Animation;

namespace Service_System
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxLogin.Text.Trim().Length < 5)
            {
                TextBoxLogin.ToolTip = "Pole musi zawierać min 5 znaków";
                TextBoxLogin.Background = Brushes.IndianRed;
            }
            else if (PassBox.Password.Trim().Length < 6)
            {
                PassBox.ToolTip = "Pole musi zawierać min 6 znaków";
                PassBox.Background = Brushes.IndianRed;
            }
            else
            {
                TextBoxLogin.ToolTip = "";
                TextBoxLogin.Background = Brushes.Transparent;
                PassBox.ToolTip = "";
                PassBox.Background = Brushes.Transparent;
                // DB.GetInstance().FindAdmin(TextBoxLogin.Text.Trim(), PassBox.Password.Trim());
                MessageBox.Show("Witamy w systemie");
                ProgView progView = new ProgView();
                progView.Show();
                Close();
            }
        }

        private void Button_Reg_Win_Click(object sender, RoutedEventArgs e)
        {
            RegWindow regWindow = new RegWindow();
            regWindow.Show();
            Close();
        }

        private void ToolBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
