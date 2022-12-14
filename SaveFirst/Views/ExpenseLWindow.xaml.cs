using SaveFirst.Models;
using SaveFirst.Repositories;
using System.Windows;

namespace SaveFirst.Views
{
    /// <summary>
    /// Lógica interna para ExpenseLWindow.xaml
    /// </summary>
    public partial class ExpenseLWindow : Window
    {
        public ExpenseLWindow(Saver saver)
        {
            InitializeComponent();
            ExpenseDataGrid.ItemsSource = new ExpenseRepository().FindAllFromSaver(saver.Id);

        }
    }
}
