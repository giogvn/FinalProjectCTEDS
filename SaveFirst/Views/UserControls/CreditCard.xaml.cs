using SaveFirst.Models;
using System;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows;

namespace SaveFirst.Views.UserControls
{
    /// <summary>
    /// Interação lógica para CreditCard.xam
    /// </summary>
    public partial class CreditCard : UserControl
    {

        public CreditCard()
        {
            InitializeComponent();
        }

        private void DueDateBoxChecker(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;

            if (Regex.IsMatch(DueDateBox.Text.Trim(), "[^0-9]+"))
            {
                MessageBox.Show("Digite apenas numeros");
                DueDateBox.Text = DueDateBox.Text.Remove(DueDateBox.Text.Length - 1);
            }
        }
        private void ClosingDateBoxChecker(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;

            if (Regex.IsMatch(ClosingDateBox.Text.Trim(), "[^0-9]+"))
            {
                MessageBox.Show("Digite apenas numeros");
                ClosingDateBox.Text = ClosingDateBox.Text.Remove(ClosingDateBox.Text.Length - 1);
            }
        }

        public bool ExpirationDateFormatCheck()
        {
            return Regex.IsMatch (ExpireDateBox.Text.Trim(), "[0-9]{2}/[0-9]{4}");
        }
        public bool ClosingDateValidValue (out int day)
        {
            return int.TryParse(ClosingDateBox.Text, out day) && day > 0 && day <=31;
        }

        public bool DueDateValidValue (out int day)
        {
            return int.TryParse(DueDateBox.Text, out day) && day > 0 && day <= 31;
        }
        public int[] SplitExpirationDate()
        {
            int[] result = { int.Parse(ExpireDateBox.Text.Split("/")[0]), int.Parse(ExpireDateBox.Text.Split("/")[1]) };
            return result;
        }

    }
}
