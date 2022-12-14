using SaveFirst.Models;
using SaveFirst.Repositories;
using SaveFirst.Views.UserControls;
using System;
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
        Expense newExpense = new();

        List<PaymentMethod> PaymentMethods;
        List<Category> Categories;

        public ExpenseRWindow(Saver saver)
        {
            InitializeComponent();
            Saver = saver;


            Categories = new CategoryRepository().FindAllFromSaver(saver.Id);
            List<string> categoryNames = new();
            categoryNames.Add("Criar uma nova categoria");
            //
            foreach (var category in Categories)
                if (category.Name != null)
                    categoryNames.Add(category.Name); 
            //*/

            PaymentMethods = new PaymentMethodRepository().FindAllFromSaver(saver.Id);
            List<string> paymentMethodNames = new();
            //
            foreach(var paymentMethod in PaymentMethods)  
                if(paymentMethod.Name != null)
                    paymentMethodNames.Add(paymentMethod.Name);
            //*/

            paymentMethodNames.Add("Cadastrar um novo método de pagamento");

            PaymentMethodBox.ItemsSource = paymentMethodNames;

            CategoryBox.ItemsSource = categoryNames;


            NewExpenseGrid.DataContext = newExpense;

        }

        private void CategoryBoxChanged(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
            if (CategoryBox.SelectedIndex == 0)
            {
                CategoryRegister.Content = new CategoryControl(Saver, this);
                CategoryBox.SelectedIndex = -1;
            }
        }
        public void RemoveCategoryControl()
        {
            CategoryRegister.Content = null;
        }
        private void PaymentMethodBoxChanged(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
            if (PaymentMethodBox.SelectedIndex == 0)
            {
                new PaymentMethodRWindow(Saver).Show();
                PaymentMethodBox.SelectedIndex = -1;
            }
        }
        private void TypeBoxChanged(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
            if (ExpenseTypeBox.SelectedIndex == 1)
                newExpense.Type = "Esporádico";
            else if (ExpenseTypeBox.SelectedIndex == 2)
                newExpense.Type = "Recorrente";
            else
                MessageBox.Show("Escolha uma opção válida");
        }

        private void ValueBoxChecker(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;

            if (Regex.IsMatch(ValueBox.Text.Trim(), "[^0-9.]"))
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

        private void RegisterNewExpense(object sender, RoutedEventArgs e)
        {
            newExpense.Status = "active";

            if (float.TryParse(ValueBox.Text, out float value))
            {
                MessageBox.Show("Use o ponto '.' como separador decimal e digite um número válido");
                ValueBox.Text = "";
                return;
            } else
            {
                newExpense.Value = value;
            }
            
            newExpense.NumberOfInstallments = int.Parse(InstallmentsBox.Text);
            if (newExpense.NumberOfInstallments <= 0)
            {
                MessageBox.Show("Digite um número válido");
                InstallmentsBox.Text = "";
                return;
            }


            if (ExpenseDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Escolha uma data para o gasto");
                return;
            }
            else
            {
                newExpense.Date = ((DateTime)ExpenseDatePicker.SelectedDate);
            }
                

            if (PaymentMethodBox.SelectedIndex == -1 || PaymentMethodBox.SelectedIndex == 0)
            {
                MessageBox.Show("Escolha um método de pagamento valido primeiro");
                return;
            }

            if (CategoryBox.SelectedIndex == -1 || CategoryBox.SelectedIndex == 0)
            {
                MessageBox.Show("Escolha uma categoria válida primeiro");
                return;
            }

            PaymentMethod selectedPM = PaymentMethods[PaymentMethodBox.SelectedIndex - 1];
            Category selectedC = Categories[CategoryBox.SelectedIndex - 1];

            CalculatorRepository creator = new();

            newExpense.InstallmentValue = creator.CalculateInstallmentValue(newExpense.NumberOfInstallments, newExpense.Value);

            if (selectedPM.InvoiceDueDate == null && newExpense.NumberOfInstallments != 1)
            {
                MessageBox.Show("Contas correntes não podem ser usados por gastos parcelados");
                PaymentMethodBox.SelectedIndex = -1;
                return;
            }
            else if (selectedPM.InvoiceDueDate != null)
            {
                newExpense.DueDate = creator.CalculateDueDate(newExpense.Date, newExpense.NumberOfInstallments, (int)selectedPM.InvoiceDueDate);
                newExpense.InstallmentsLeft = creator.CalculateInstallmentsLeft(newExpense.DueDate, (int)selectedPM.InvoiceDueDate);
            }

            newExpense.Id = Guid.NewGuid().ToString();

            ExpenseRepository expenseRepository = new();
            expenseRepository.Create(newExpense);

            string connectionString = " ";
            IntermediateRepository repository1 = new(connectionString, "ExpenseCategory", ExpenseCategory.Labels);
            IntermediateRepository repository2 = new(connectionString, "ExpensePaymentMethod", ExpensePaymentMethod.Labels);

            repository1.Create(new ExpenseCategory()
            {
                ForeignKey1 = newExpense.Id,
                ForeignKey2 = selectedC.Id,
                SaverId = Saver.Id
            });
            repository2.Create(new ExpensePaymentMethod()
            {
                ForeignKey1 = newExpense.Id,
                ForeignKey2 = selectedPM.Id,
                SaverId = Saver.Id
            });

            this.Close();
                
            
            


        }
    }
}
