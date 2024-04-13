using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace WpfAppSearchSystem_HTTP_FTP_SMTP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = SearchTextBox.Text;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string searchUrl = $"https://www.google.com/search?q={WebUtility.UrlEncode(searchTerm)}";
                Browser.Navigate(new Uri(searchUrl));
            }
            else
            {
                MessageBox.Show("Введите текст для поиска.");
            }
        }
    }
}
