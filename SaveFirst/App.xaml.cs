using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SaveFirst.Models;
using SaveFirst.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SaveFirst
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static string[] ConnectionStrings =
        {
            "DataSource = Saver.db",
            "DataSource = Category.db",
            "DataSource = ExpenseCategory.db",
            "DataSource = Expense.db",
            "DataSource = ExpensePaymentMethod.db",
            "DataSource = PaymentMethod.db",
            "DataSource = PaymentMethodIncomeResource.db",
            "DataSource = IncomeResource.db",
            "DataSource = SaverFinancialProduct.db",
            "DataSource = FinancialProduct.db"

        };
        private readonly ServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection services = new();

            foreach (var connectionString in ConnectionStrings) {
                services.AddDbContext<DbContext>(options =>
                {
                    options.UseSqlite(connectionString);
                });
            }
            services.AddSingleton<MainWindow>();
            serviceProvider = services.BuildServiceProvider();
        }

        private void OnStartup(object s, StartupEventArgs e)
        {
            var startWindow = new LoginWindow();
            //create context variables so CRUD repositories can work 

        }
    }
}
