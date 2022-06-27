using Service_System.Service;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Service_System.View
{
    /// <summary>
    /// Interaction logic for ItemsPage.xaml
    /// </summary>
    public partial class ItemsPage : Page
    {
        private BooksService booksService = new BooksService();
        public ItemsPage()
        {
            InitializeComponent();
        }

        private void ToolBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (booksService.Create(TextBoxAutor.Text(), TextBoxNazwa.Text()))
            {

            }
        }                             
    }
}
