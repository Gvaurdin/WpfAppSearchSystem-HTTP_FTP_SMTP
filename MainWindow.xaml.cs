using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
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
            string query = SearchBox.Text;
            if (!string.IsNullOrWhiteSpace(query))
            {
                string searchResults = string.Empty;
                if (YandexRadioButton.IsChecked == true)
                {
                    searchResults = SearchInYandex(query);
                }
                else if (MailruRadioButton.IsChecked == true)
                {
                    searchResults = SearchInMailRu(query);
                }
                DisplayResults(searchResults);
            }
            else
            {
                MessageBox.Show("Введите текст для поиска.");
            }
        }

        private string SearchInYandex(string query)
        {
            string searchUrl = $"https://yandex.ru/search/?text={Uri.EscapeDataString(query)}";
            return GetSearchResults(searchUrl);
        }

        private string SearchInMailRu(string query)
        {
            string searchUrl = $"https://go.mail.ru/search?q={Uri.EscapeDataString(query)}";
            return GetSearchResults(searchUrl);
        }

        private string GetSearchResults(string url)
        {
            WebRequest request = WebRequest.Create(url);
            using (WebResponse response = request.GetResponse())
            using (Stream dataStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(dataStream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        private void DisplayResults(string results)
        {
            ResultsTextBox.Text = results;
        }
    }
}
