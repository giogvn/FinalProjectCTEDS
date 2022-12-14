using System;
using System.Collections.Generic;
using System.Windows;
using SaveFirst.Models;
using SaveFirst.Views.UserControls;
using SaveFirst.Repositories;
using SaveFirst.Views;

namespace SaveFirst
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Saver Saver;

        List<PaymentMethod> PaymentMethods;
        List<Category> Categories;

        int currentLastPositionPM;
        int currentStartPositionPM;

        int currentLastPositionC;
        int currentStartPositionC;

        private void FillPMs(int startPosition, int howMany)
        {
            int k = startPosition;
            currentStartPositionPM = k;

            for (int i = 0; i < 5; i++)
            {
                if (i == 0)
                    if (howMany >= 1)
                    {
                        PM1.Content = new PaymentMethodBar(PaymentMethods[k]);
                        currentLastPositionPM = k;
                    }
                    else
                    {
                        PM1.Content = null;
                        currentLastPositionPM = k;
                    }
                else if (i == 1)
                    if (howMany >= 2)
                    {
                        PM2.Content = new PaymentMethodBar(PaymentMethods[k + 1]);
                        currentLastPositionPM = k + 1;
                    }
                    else
                        PM2.Content = null;
                else if (i == 2)
                    if (howMany >= 3)
                    {
                        PM3.Content = new PaymentMethodBar(PaymentMethods[k + 2]);
                        currentLastPositionPM = k + 2;
                    }
                    else
                        PM3.Content = null;
                else if (i == 3)
                    if (howMany >= 4)
                    {
                        PM4.Content = new PaymentMethodBar(PaymentMethods[k + 3]);
                        currentLastPositionPM = k + 3;
                    }
                    else
                        PM4.Content = null;
                else if (i == 4)
                    if (howMany >= 5)
                    {
                        PM5.Content = new PaymentMethodBar(PaymentMethods[k + 4]);
                        currentLastPositionPM = k + 4;
                    }
                    else
                        PM5.Content = null;
            }

        }

        private void FillCs (int startPosition, int howMany)
        {
            int k = startPosition;
            currentStartPositionC = k;

            for (int i = 0; i < 5; i++)
            {
                if (i == 0)
                    if (howMany >= 1)
                    {
                        C1.Content = new CategoryBar(Categories[k]);
                        currentLastPositionC = k;
                    }
                    else
                    {
                        C1.Content = null;
                        currentLastPositionC = k;
                    }
                else if (i == 1)
                    if (howMany >= 2)
                    {
                        C2.Content = new CategoryBar(Categories[k + 1]);
                        currentLastPositionC = k + 1;
                    }
                    else
                        C2.Content = null;
                else if (i == 2)
                    if (howMany >= 3)
                    {
                        C3.Content = new CategoryBar(Categories[k + 2]);
                        currentLastPositionC = k + 2;
                    }
                    else
                        C3.Content = null;
                else if (i == 3)
                    if (howMany >= 4)
                    {
                        C4.Content = new CategoryBar(Categories[k + 3]);
                        currentLastPositionC = k + 3;
                    }
                    else
                        C4.Content = null;
                else if (i == 4)
                    if (howMany >= 5)
                    {
                        C4.Content = new CategoryBar(Categories[k + 4]);
                        currentLastPositionC = k + 4;
                    }
                    else
                        C4.Content = null;
            }
        }

        public MainWindow(Saver saver): base()
        {

            InitializeComponent();
            Saver = saver;

            PaymentMethods = new PaymentMethodRepository().FindAllFromSaver(saver.Id);
            if (PaymentMethods.Count <= 5)
                FillPMs(0, PaymentMethods.Count);
            else
                FillPMs(0, 5);

            Categories = new CategoryRepository().FindAllFromSaver(saver.Id);
            if (Categories.Count <= 5)
                FillCs(0, Categories.Count);
            else
                FillCs(0, 5);

            float total = new ExpenseRepository().CalculateTotalExpenses(Saver.Id, new(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)));

            TotalBox.Text = "Total: " + total + " reais";
        }

        private void PreviousPM(object sender, RoutedEventArgs e)
        {
            if (currentStartPositionPM == 0)
                return;
            else
                FillPMs(currentStartPositionC - 5, 5);
        }

        private void NextPM(object sender, RoutedEventArgs e)
        {
            if (currentLastPositionPM == PaymentMethods.Count - 1)
                return;
            else
            {
                if (currentLastPositionPM + 5 > PaymentMethods.Count - 1)
                    FillPMs(currentLastPositionPM + 1, PaymentMethods.Count - (currentLastPositionPM + 1));
                else
                    FillPMs(currentLastPositionPM + 1, 5);
            }
                
        }

        private void PreviousC(object sender, RoutedEventArgs e)
        {
            if (currentStartPositionC == 0)
                return;
            else
                FillCs(currentStartPositionC - 5, 5);
        }

        private void NextC(object sender, RoutedEventArgs e)
        {
            if (currentLastPositionC == Categories.Count - 1)
                return;
            else
            {
                if (currentLastPositionC + 5 > Categories.Count - 1)
                    FillPMs(currentLastPositionC + 1, Categories.Count - (currentLastPositionC + 1));
                else
                    FillPMs(currentLastPositionC + 1, 5);
            }
        }

        private void RegisterExpense(object sender, RoutedEventArgs e)
        {
            new ExpenseRWindow(Saver).Show();
        }

        private void RegisterPM(object sender, RoutedEventArgs e)
        {
            new PaymentMethodRWindow(Saver).Show();
        }
    }
}
