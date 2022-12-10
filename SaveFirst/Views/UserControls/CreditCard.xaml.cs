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
    /// Interação lógica para CreditCard.xam
    /// </summary>
    public partial class CreditCard : UserControl
    {
        Saver Saver;
        public CreditCard(Saver saver)

        {
            InitializeComponent();
            Saver = saver;
            this.DataContext = this;

            CategoryRepository categoryRepository = new CategoryRepository();

            List<Category> categories = categoryRepository.ReadAll(""); // see what to do later
            List<string> names = new();
            foreach (var category in categories)
                names.Add(category.Name);
            IncomeResourceBox.ItemsSource = names;
        }
    }
}
