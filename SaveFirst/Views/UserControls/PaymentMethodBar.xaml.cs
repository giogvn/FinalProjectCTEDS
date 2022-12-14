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
    /// Interação lógica para PaymentMethodBar.xam
    /// </summary>
    public partial class PaymentMethodBar : UserControl
    {
        public PaymentMethodBar(PaymentMethod paymentMethod)
        {
            InitializeComponent();

            NameBox.Text = paymentMethod.Name + " - " + paymentMethod.Limit + " reais";

            var expenses = new PaymentMethodRepository().ExpensesFromPaymentMethod(paymentMethod.Id);
            double percentage = expenses / paymentMethod.Limit;
            Spent.Width = percentage * 200;

            PercentageBox.Text = (percentage * 100) + "%";


        }
    }
}
