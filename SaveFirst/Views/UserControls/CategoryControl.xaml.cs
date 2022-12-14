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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SaveFirst.Views.UserControls
{
    /// <summary>
    /// Interação lógica para CategoryControl.xam
    /// </summary>
    public partial class CategoryControl : UserControl
    {
        ExpenseRWindow ParentWindow;
        Category newCategory = new();
        Saver Saver;
        public CategoryControl(Saver saver, ExpenseRWindow parentWindow)
        {
            InitializeComponent();
            Saver = saver;
            ParentWindow = parentWindow;
        }

        private void RegisterCategory(object sender, RoutedEventArgs e)
        {
            newCategory.SaverId = Saver.Id;
            newCategory.Id = Guid.NewGuid().ToString();
            new CategoryRepository().Create(newCategory);
            ParentWindow.RemoveCategoryControl();

        }
    }
}
