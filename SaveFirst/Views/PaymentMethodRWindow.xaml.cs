using System;
using System.Windows;
using SaveFirst.Views.UserControls;
using SaveFirst.Models;
using SaveFirst.Repositories;
using System.Text.RegularExpressions;
using SaveFirst.Interfaces;

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

        IRefreshable ParentWindow;
        public PaymentMethodRWindow(Saver saver, IRefreshable parentWindow): base()
        {
            InitializeComponent();
            Saver = saver;
            newPaymentMethod.SaverId = saver.Id;
            newPaymentMethod.RegistrationDate = (DateTime.Now);
            NewPaymentMethodGrid.DataContext = newPaymentMethod;
            ParentWindow = parentWindow;

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

            if (Regex.IsMatch(LimitBox.Text.Trim(), "[^0-9.]+"))
            {
                MessageBox.Show("Use o ponto '.' como separador decimal e digite um número válido");
                LimitBox.Text = LimitBox.Text.Remove(LimitBox.Text.Length - 1);
            }
        }

        private void Process(object sender, RoutedEventArgs e)
        {
            switch ((int)TypeBox.SelectedIndex) {
                case 1:
                    newPaymentMethod.SaverId = Saver.Id;
                    newPaymentMethod.Id = Guid.NewGuid().ToString();

                    if (double.TryParse(LimitBox.Text, out double value))
                        newPaymentMethod.Limit = value;
                    else
                    {
                        MessageBox.Show("Use o ponto '.' como separador decimal e digite um número válido");
                        LimitBox.Text = "";
                    }

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

                    if (creditCard.ClosingDateValidValue(out int closingDay))
                    {
                        newPaymentMethod.InvoiceClosingDate = closingDay;
                    }
                    else
                    {
                        MessageBox.Show("Escolha um valor entre 1 e 31 para o fechamento da fatura");
                        creditCard.ClosingDateBox.Text = "";
                        break;
                    }

                    if (creditCard.DueDateValidValue(out int dueDay))
                    {
                        newPaymentMethod.InvoiceDueDate = dueDay;
                    }
                    else
                    {
                        MessageBox.Show("Escolha um valor entre 1 e 31 para o vencimento da fatura");
                        creditCard.DueDateBox.Text = "";
                        break;
                    }

                    new PaymentMethodRepository().Create(newPaymentMethod);
                    ParentWindow.Refresh();
                    this.Close();

                    break;
                case 2:
                    newPaymentMethod.SaverId = Saver.Id;
                    newPaymentMethod.Id = Guid.NewGuid().ToString();
                    newPaymentMethod.InvoiceDueDate = null;
                    newPaymentMethod.InvoiceClosingDate = null;
                    
                    if (double.TryParse(LimitBox.Text, out double valor))
                        newPaymentMethod.Limit = valor;
                    else
                    {
                        MessageBox.Show("Use o ponto '.' como separador decimal e digite um número válido");
                        LimitBox.Text = "";
                    }

                    new PaymentMethodRepository().Create(newPaymentMethod);
                    ParentWindow.Refresh();
                    this.Close();
                    break;
                default:
                    break;
            }

                    
        }

    }
}

