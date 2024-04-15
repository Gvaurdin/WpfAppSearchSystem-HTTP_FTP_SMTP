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
            string query = QueryTextBox.Text;
            List<string> selectedSearchEngines = GetSelectedSearchEngines();

            if (!string.IsNullOrWhiteSpace(query) && selectedSearchEngines.Count > 0)
            {
                foreach (string searchEngine in selectedSearchEngines)
                {
                    string searchUrl = GetSearchUrl(searchEngine, query);
                    try
                    {
                        System.Diagnostics.Process.Start(searchUrl);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при запуске браузера: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите хотя бы одну поисковую систему и введите текст для поиска.");
            }
        }

        private List<string> GetSelectedSearchEngines()
        {
            List<string> selectedEngines = new List<string>();

            if (GoogleCheckBox.IsChecked == true)
            {
                selectedEngines.Add("Google");
            }

            if (BingCheckBox.IsChecked == true)
            {
                selectedEngines.Add("Bing");
            }

            // Добавьте другие поисковые системы здесь по аналогии

            return selectedEngines;
        }

        private string GetSearchUrl(string searchEngine, string query)
        {
            switch (searchEngine)
            {
                case "Google":
                    return $"https://www.google.com/search?q={WebUtility.UrlEncode(query)}";
                case "Bing":
                    return $"https://www.bing.com/search?q={WebUtility.UrlEncode(query)}";
                // Добавьте другие поисковые системы здесь по аналогии
                default:
                    throw new ArgumentException("Неподдерживаемая поисковая система.");
            }
        }
    }
}
