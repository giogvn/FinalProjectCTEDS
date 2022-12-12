using SaveFirst.Models;
using SaveFirst.Repositories;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace SaveFirst.Views
{
    /// <summary>
    /// Lógica interna para ExpenseRWindow.xaml
    /// </summary>
    public partial class ExpenseRWindow : Window
    {
        Saver Saver;
        public ExpenseRWindow(Saver saver)
        {
            InitializeComponent();
            Saver = saver;


            //List<Category> categories = new CategoryRepository().FindAllFromSaver(saver.Id);
            List<string> names = new();
            names.Add("Criar uma nova categoria");
            /*//
            foreach (var category in categories)
                if (category.Name != null)
                    names.Add(category.Name); 
            //*/
            
            CategoryBox.ItemsSource = names;
        }
        private void ValueBoxChecker(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;

            if (Regex.IsMatch(ValueBox.Text.Trim(), "[^0-9]+"))
            {
                MessageBox.Show("Digite apenas numeros");
                ValueBox.Text = ValueBox.Text.Remove(ValueBox.Text.Length - 1);
            }
        }
        private void InstallmentsBoxChecker(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;

            if (Regex.IsMatch(InstallmentsBox.Text.Trim(), "[^0-9]+"))
            {
                MessageBox.Show("Digite apenas numeros");
                InstallmentsBox.Text = InstallmentsBox.Text.Remove(InstallmentsBox.Text.Length - 1);
            }
        }
    }
}
