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
    /// Interação lógica para CategoryBar.xam
    /// </summary>
    public partial class CategoryBar : UserControl
    {
        public CategoryBar(Category category)
        {
            InitializeComponent();
            NameBox.Text = category.Name;
            double total = 0;
            foreach (var expense in new ExpenseRepository().getCategoryExpenses(category.Id))
                total += expense.Value;
            TotalExpenseBox.Text = total + " reais";
        }
    }
}
