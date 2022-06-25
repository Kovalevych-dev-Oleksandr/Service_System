using Service_System.Service;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Service_System
{
    /// <summary>
    /// Interaction logic for RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        private AdminService adminService = new AdminService();

        public RegWindow()
        {
            InitializeComponent();

            DoubleAnimation btnanimation = new DoubleAnimation();
            btnanimation.From = 0;
            btnanimation.To = 250;
            btnanimation.Duration = TimeSpan.FromSeconds(3);
            RegButton.BeginAnimation(Button.WidthProperty, btnanimation);
        }

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
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
            else if (PassBox2.Password.Trim() != PassBox.Password.Trim())
            {
                PassBox2.ToolTip = "Wprowadzone hasła różnią się";
                PassBox2.Background = Brushes.IndianRed;
            }
            else if (TextBoxEmail.Text.Trim().ToLower().Length < 10 || !TextBoxEmail.Text.Trim().ToLower().Contains("@") || !TextBoxEmail.Text.Trim().ToLower().Contains("."))
            {
                TextBoxEmail.ToolTip = "Pole ma nie odpowiedni format albo jest za krutkie";
                TextBoxEmail.Background = Brushes.IndianRed;
            }
            else
            {
                TextBoxLogin.ToolTip = "";
                TextBoxLogin.Background = Brushes.Transparent;
                PassBox.ToolTip = "";
                PassBox.Background = Brushes.Transparent;
                PassBox2.ToolTip = "";
                PassBox2.Background = Brushes.Transparent;
                TextBoxEmail.ToolTip = "";
                TextBoxEmail.Background = Brushes.Transparent;
                MessageBox.Show("Konto założonę");
                adminService.Create(TextBoxLogin.Text.Trim(), PassBox.Password.Trim());
                // Dao.GetInstance().CreateAdmin(TextBoxLogin.Text.Trim(), PassBox.Password.Trim());
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
        }

        private void Button_Auth_Win_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ToolBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void TextBoxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
