using SaveFirst.Models;
using SaveFirst.Repositories;
using System.Collections.Generic;
using System.Windows;
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


            List<Category> categories = new CategoryRepository().FindAllFromSaver(saver.Id);
            List<string> names = new();
            names.Add("Criar uma nova categoria");
            foreach (var category in categories)
                if (category.Name != null)
                    names.Add(category.Name);
            
            CategoryBox.ItemsSource = names;
        }
    }
}
