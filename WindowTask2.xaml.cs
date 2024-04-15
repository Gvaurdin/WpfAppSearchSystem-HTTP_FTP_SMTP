using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using System.Windows.Shapes;

namespace WpfAppSearchSystem_HTTP_FTP_SMTP
{
    /// <summary>
    /// Логика взаимодействия для WindowTask2.xaml
    /// </summary>
    public partial class WindowTask2 : Window
    {
        public WindowTask2()
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
                        string html = new WebClient().DownloadString(searchUrl);
                        List<string> imageUrls = ExtractImageUrls(html);

                        if (imageUrls.Count > 0)
                        {
                            ShowImages(imageUrls);
                        }
                        else
                        {
                            MessageBox.Show($"No results for search system: {searchEngine}");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error working search system {searchEngine}: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Change to search system.");
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



            return selectedEngines;
        }

        private string GetSearchUrl(string searchEngine, string query)
        {
            switch (searchEngine)
            {
                case "Google":
                    return $"https://www.google.com/search?q={WebUtility.UrlEncode(query)}&tbm=isch";
                case "Bing":
                    return $"https://www.bing.com/images/search?q={WebUtility.UrlEncode(query)}";
                default:
                    throw new ArgumentException("Search system no support.");
            }
        }

        private List<string> ExtractImageUrls(string html)
        {
            List<string> imageUrls = new List<string>();

            // регулярное выражение для поиска URL изображений
            string pattern = @"(?<=<img\s+[^>]*?src=(['""]))[^'""\s>]+";

            foreach (Match match in Regex.Matches(html, pattern, RegexOptions.IgnoreCase))
            {
                string imageUrl = match.Value;
                imageUrls.Add(imageUrl);
            }

            return imageUrls;
        }

        private void ShowImages(List<string> imageUrls)
        {
            ImagePanel.Children.Clear();

            foreach (string imageUrl in imageUrls)
            {
                try
                {
                    BitmapImage image = new BitmapImage(new Uri(imageUrl));
                    Image imgControl = new Image
                    {
                        Source = image,
                        Width = 200,
                        Height = 150,
                        Margin = new Thickness(5)
                    };
                    ImagePanel.Children.Add(imgControl);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
    }
}
