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
using System.Windows.Shapes;

namespace SaveFirst.Views
{
    /// <summary>
    /// Lógica interna para SaverRWindow.xaml
    /// </summary>
    public partial class SaverRWindow : Window
    {
        Saver newSaver = new();
        public SaverRWindow()
        {
            InitializeComponent();
            NewSaverGrid.DataContext = newSaver;
            Birthday.SelectedDate = DateTime.Now;

        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            if (Birthday.SelectedDate == null)
            {
                MessageBox.Show("Escolha um aniversário");
                return;
            }

            newSaver.Birthday = DateTime.FromDateTime((DateTime)Birthday.SelectedDate);
            SaverRepository saverRepository = new SaverRepository();
            saverRepository.Create(newSaver);

            new LoginWindow().Show();
            this.Close();

        }
    }
}
