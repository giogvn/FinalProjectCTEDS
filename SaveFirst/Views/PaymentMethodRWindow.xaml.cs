using System;
using System.Windows;
using SaveFirst.Views.UserControls;
using SaveFirst.Models;
using SaveFirst.Repositories;

namespace SaveFirst.Views
{

    /// <summary>
    /// Lógica interna para PaymentMethodRWindow.xaml
    /// </summary>
    public partial class PaymentMethodRWindow : Window
    {
        Saver Saver;
        PaymentMethod newPaymentMethod = new();
        CreditCard CreditCardOptions;
        CheckingAccount CheckingAccountOptions;

        public PaymentMethodRWindow(Saver saver): base()
        {
            InitializeComponent();
            Saver = saver;
            CreditCardOptions = new(newPaymentMethod);
            CheckingAccountOptions = new(newPaymentMethod);
            newPaymentMethod.SaverId = saver.Id;
            
        }

        private void ChangeContentForRegister(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
            
            if (Choice1.SelectedIndex == 1)
                NeededData.Content = CreditCardOptions;
            else if (Choice1.SelectedIndex == 2)
                NeededData.Content = CheckingAccountOptions;
            else
                NeededData.Content = null;
        }

        private void Process(object sender, RoutedEventArgs e)
        {
            switch ((int)Choice1.SelectedIndex) {
                case 1:

                    if (!CreditCardOptions.ExpirationDateFormatCheck())
                    {
                        MessageBox.Show("Siga o formato MM/YYYY para data de validade");
                        break;
                    }
                    else
                    {
                        int[] values = CreditCardOptions.SplitExpirationDate();
                        newPaymentMethod.ExpirationDate = new DateOnly(values[1], values[0], 1);
                    }

                    if (CreditCardOptions.ClosingDateValidValue(out int day))
                    {
                        newPaymentMethod.InvoiceClosingDate = new(DateTime.Now.Year, DateTime.Now.Month, day);
                    }
                    else
                    {
                        MessageBox.Show("Escolha um valor entre 1 e 31 para o fechamento da fatura");
                        CreditCardOptions.ClosingDateBox.Text = "";
                        break;
                    }

                    new PaymentMethodRepository().Create(newPaymentMethod);

                    break;
                case 2:
                    new PaymentMethodRepository().Create(newPaymentMethod);
                    break;
                default:
                    break;
            }

                    
        }

    }
}

