using System;
using System.Windows;
using SaveFirst.Views.UserControls;
using SaveFirst.Models;
using SaveFirst.Repositories;
using System.Text.RegularExpressions;

namespace SaveFirst.Views
{

    /// <summary>
    /// Lógica interna para PaymentMethodRWindow.xaml
    /// </summary>
    public partial class PaymentMethodRWindow : Window
    {
        Saver Saver;
        PaymentMethod newPaymentMethod = new();
        CreditCard creditCard = new CreditCard();

        public PaymentMethodRWindow(Saver saver): base()
        {
            InitializeComponent();
            Saver = saver;
            newPaymentMethod.SaverId = saver.Id;
            newPaymentMethod.RegistrationDate = DateTime.FromDateTime(DateTime.Now);

        }

        private void ChangeContentForRegister(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;

            if (TypeBox.SelectedIndex == 1)
                NeededData.Content = creditCard;
            else
                NeededData.Content = null;
        }

        private void LimitBoxChecker(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return; 

            if (Regex.IsMatch(LimitBox.Text.Trim(), "[^0-9]+"))
            {
                MessageBox.Show("Digite apenas numeros");
                LimitBox.Text = LimitBox.Text.Remove(LimitBox.Text.Length - 1);
            }
        }

        private void Process(object sender, RoutedEventArgs e)
        {
            switch ((int)TypeBox.SelectedIndex) {
                case 1:

                    if (!creditCard.ExpirationDateFormatCheck())
                    {
                        MessageBox.Show("Siga o formato MM/YYYY para data de validade");
                        creditCard.ExpireDateBox.Text = "";
                        break;
                    }
                    else
                    {
                        int[] values = creditCard.SplitExpirationDate();
                        newPaymentMethod.ExpirationDate = new DateTime(values[1], values[0], 1);
                    }

                    if (creditCard.ClosingDateValidValue(out int day))
                    {
                        newPaymentMethod.InvoiceClosingDate = new(DateTime.Now.Year, DateTime.Now.Month, day);
                    }
                    else
                    {
                        MessageBox.Show("Escolha um valor entre 1 e 31 para o fechamento da fatura");
                        creditCard.ClosingDateBox.Text = "";
                        break;
                    }

                    new PaymentMethodRepository().Create(newPaymentMethod);
                    this.Close();

                    break;
                case 2:
                    new PaymentMethodRepository().Create(newPaymentMethod);
                    this.Close();
                    break;
                default:
                    break;
            }

                    
        }

    }
}

