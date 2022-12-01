﻿using System;
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

namespace SaveFirst.Views
{
    /// <summary>
    /// Interaction logic for PaymentMethodRegister.xaml
    /// </summary>
    public partial class PaymentMethodRegister : Page
    {
        public PaymentMethodRegister()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow next = (MainWindow)sender;
            next.Content = new IncomeResourceRegister();
        }
    }
}
