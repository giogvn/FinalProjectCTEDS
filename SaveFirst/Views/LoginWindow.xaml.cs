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
using Microsoft.Data.Sqlite;

namespace SaveFirst.Views
{
    /// <summary>
    /// Lógica interna para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        Saver possibleSaver = new();
        public LoginWindow()
        {
            InitializeComponent();
            PossibleSaverGrid.DataContext = possibleSaver;
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            SaverRepository saverRepository = new SaverRepository();

            List<Saver> savers  = saverRepository.findSaver(possibleSaver.Email, possibleSaver.Password);
            if (savers.Count > 0)
            {
                Saver saver = savers[0];
                this.Close();
                MainWindow mainWindow = new MainWindow(saver);
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("Email ou senha incorretos!");
            }        
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            SaverRWindow registerWindow = new();
            registerWindow.Show();
            this.Close();
        }
    }
}
