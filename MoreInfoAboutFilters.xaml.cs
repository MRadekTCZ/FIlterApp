using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace FilterApp
{
    /// <summary>
    /// Logika interakcji dla klasy MoreInfoAboutFilters.xaml
    /// </summary>
    public partial class MoreInfoAboutFilters : Window
    {
        public MoreInfoAboutFilters()
        {
            InitializeComponent();
        }

        private void OpenPDF_Click0(object sender, RoutedEventArgs e)
        {
            string pdfFilePath = "filters_lecture.pdf"; // Replace with the actual file name of the PDF

            try
            {
                string fullFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pdfFilePath);
                Process.Start(fullFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while opening the PDF: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void OpenPDF_Click1(object sender, RoutedEventArgs e)
        {
            string pdfFilePath = "TI_LCDesign.pdf"; // Replace with the actual file name of the PDF

            try
            {
                string fullFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pdfFilePath);
                Process.Start(fullFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while opening the PDF: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void OpenPDF_Click2(object sender, RoutedEventArgs e)
        {
            string pdfFilePath = "TI_RLCDesign.pdf"; // Replace with the actual file name of the PDF

            try
            {
                string fullFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pdfFilePath);
                Process.Start(fullFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while opening the PDF: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void OpenPDF_Click3(object sender, RoutedEventArgs e)
        {
            string pdfFilePath = "Ahmed.pdf"; // Replace with the actual file name of the PDF

            try
            {
                string fullFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pdfFilePath);
                Process.Start(fullFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while opening the PDF: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void OpenPDF_Click4(object sender, RoutedEventArgs e)
        {
            string pdfFilePath = "elmech.pdf"; // Replace with the actual file name of the PDF

            try
            {
                string fullFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pdfFilePath);
                Process.Start(fullFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while opening the PDF: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }

}
