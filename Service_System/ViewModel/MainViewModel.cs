using GalaSoft.MvvmLight.Command;
using Service_System.View;
using System.Windows.Controls;
using System.Windows.Input;

namespace Service_System.ViewModel
{
    internal class MainViewModel : ViewModelBase
    {
        private Page Account = new AccountPage();
        private Page Delivery = new DeliveryPage();
        private Page Items = new ItemsPage();
        private Page Order = new OrderPage();
        private Page Settings = new SettingsPage();
        private Page _CurPage = new AccountPage();

        public Page CurPage
        {
            get => _CurPage;
            set => Set(ref _CurPage, value);
        }

        public ICommand OpenAccountPage
        {
            get
            {
                return new RelayCommand(() => CurPage = Account);
            }
        }

        public ICommand OpenDeliveryPage
        {
            get
            {
                return new RelayCommand(() => CurPage = Delivery);
            }
        }

        public ICommand OpenItemsPage
        {
            get
            {
                return new RelayCommand(() => CurPage = Items);
            }
        }

        public ICommand OpenOrderPage
        {
            get
            {
                return new RelayCommand(() => CurPage = Order);
            }
        }

        public ICommand OpenSettingsPage
        {
            get
            {
                return new RelayCommand(() => CurPage = Settings);
            }
        }
    }
}
