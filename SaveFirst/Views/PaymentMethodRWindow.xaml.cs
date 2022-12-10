using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;
using SaveFirst.Views.UserControls;
using SaveFirst.Models;

namespace SaveFirst.Views
{
    
    /// <summary>
    /// Lógica interna para PaymentMethodRWindow.xaml
    /// </summary>
    public partial class PaymentMethodRWindow : Window
    {
        Saver Saver;

        public PaymentMethodRWindow(Saver saver): base()
        {
            InitializeComponent();
            Saver = saver;
            
        }

        private void ChangeContentForRegister(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
            
            if (Choice1.SelectedIndex == 1)
                NeededData.Content = new CreditCard();
            else if (Choice1.SelectedIndex == 2)
                NeededData.Content = new CheckingAccount();
            else
                NeededData.Content = null;
        }

        private void ChangeContentForRegister(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

