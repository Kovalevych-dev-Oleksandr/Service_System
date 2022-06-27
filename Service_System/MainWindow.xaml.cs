using Service_System.Service;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Service_System
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AdminService adminService = new AdminService();

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

                if (adminService.LogIn(TextBoxLogin.Text, PassBox.Password))
                {
                    MessageBox.Show("Witamy w systemie");
                    new ProgView().Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Wprowadzono nie poprawne dane");
                }
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
