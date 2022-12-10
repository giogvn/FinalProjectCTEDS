using SaveFirst.Models;
using SaveFirst.Repositories;
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
using System.Windows.Shapes;

namespace SaveFirst.Views
{
    /// <summary>
    /// Lógica interna para IncomeResourceRWindow.xaml
    /// </summary>
    public partial class IncomeResourceRWindow : Window
    {
        Saver Saver;

        public IncomeResourceRWindow(Saver saver): base()
        {
            InitializeComponent();
            Saver = saver;
        }

        private void AddIncome(object sender, RoutedEventArgs e)
        {
            
            if (int.TryParse(DateBox.Text, out int payday) && payday > 31 )
            {
                MessageBox.Show("Insira um valor entre 1 e 31");
                DateBox.Text = "";
                return;
            }
            if (float.TryParse(ValueBox.Text, out float amount) && amount < 0)
            {
                MessageBox.Show("Insira um valor decimal válido");
                ValueBox.Text = "";
                return;
            }
            string? recurrence;

            if (RecurrenceBox.SelectedIndex == 2)
                recurrence = "Semanalmente";
            else if (RecurrenceBox.SelectedIndex == 3)
                recurrence = "Mensalmente";
            else if (RecurrenceBox.SelectedIndex == 4)
                recurrence = "Semestralmente";
            else
                recurrence = null;

            IncomeResource newSource = new()
            {
                SaverId = Saver.Id,
                Name = NameBox.Text,
                Value = amount,
                PayDay = payday,
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                Recurrence = recurrence
            };

            IncomeResourceRepository register = new();
            register.Create(newSource);

            
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
            new MainWindow(Saver).Show();
        }

        private void DateBoxChanged(object sender, RoutedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(DateBox.Text, "[^0-9]"))
            {
                    MessageBox.Show("Digite apenas numeros");
                    DateBox.Text = DateBox.Text.Remove(DateBox.Text.Length - 1);
            }
        }

        private void ValueBoxChanged(object sender, RoutedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(ValueBox.Text, "[^0-9]+^.?[^0-9]*"))
            {
                MessageBox.Show("Digite um numero usando o ponto como separador decimal");
                ValueBox.Text = ValueBox.Text.Remove(ValueBox.Text.Length - 1);
            }
        }


    }
}
