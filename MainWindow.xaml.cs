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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Win32;
using System.Windows.Forms.DataVisualization.Charting;
using System.Numerics;
using System.Data;
using System.Windows.Controls.Primitives;
using System.IO;

namespace FilterApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Chart chart01;
        //-----------------------------------------------------
        private double U1;
        private double fmin;
        private double fmax;
        private double R1;
        private double R2;
        private double L1;
        private double C1;
        private double[] Results_1;
        private double[] Results_2;
        private double[] Results_3;
        private int size;
        private string fileContent;

        private ContextMenu circuitContextMenu = null;
        
        public MainWindow()
        {
            InitializeComponent();

            this.U1 = 10;
            this.fmin = 0;
            this.fmax = 1000;
            this.R1 = 0.5;
            this.R2 = 0.5;
            this.L1 = 0.0001;
            this.C1 = 0.0005;
            this.size = 2000;

            this.txtMagnitude.Text = U1.ToString();
            this.txtFreqMax.Text = fmax.ToString();
            this.txtFreqMin.Text = fmin.ToString(); 
            this.txtResistance.Text = R1.ToString();
            this.txtResistance2.Text = R2.ToString();
            this.txtInductance.Text = L1.ToString();
            this.txtCapacitance.Text = C1.ToString();

            MenuItem paramMenuItem = new MenuItem();
            paramMenuItem.Header = "Parameters...";
            paramMenuItem.Click += ParamMenuItem_Click;

            MenuItem clearMenuItem = new MenuItem();
            clearMenuItem.Header = "Clear Waveforms...";
            clearMenuItem.Click += ClearWaveforms_Click;

            circuitContextMenu = new ContextMenu();
            circuitContextMenu.Items.Add(paramMenuItem);
            circuitContextMenu.Items.Add(clearMenuItem);

            this.circuitImage.ContextMenu = circuitContextMenu;
        }

        private void ClearWaveforms_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
            chart01.Series.Clear();
            chart01.Titles.Clear();
        }

        private void ParamMenuItem_Click(object sender, RoutedEventArgs e)
        {
           // throw new NotImplementedException();
           filterParams_Click(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            chart01 = new Chart();
            host.Child = chart01;
            chart01.ChartAreas.Add(new ChartArea("Magnitude"));
            chart01.ChartAreas.Add(new ChartArea("Phase"));
            chart01.MouseDown += Chart01_MouseDown;
            chart01.MouseMove += Chart01_MouseMove;
        }

        private void Chart01_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //throw new NotImplementedException();
            System.Drawing.PointF mousePt = new System.Drawing.PointF(e.X, e.Y);
            chart01.ChartAreas["Magnitude"].CursorX.SetCursorPixelPosition(mousePt, true);
            chart01.ChartAreas["Magnitude"].CursorY.SetCursorPixelPosition(mousePt, true);

            chart01.ChartAreas["Magnitude"].AxisX.LabelStyle.Format = "{#0.0}";
            chart01.ChartAreas["Magnitude"].AxisY.LabelStyle.Format = "{#0.0}";
        }

        private void Chart01_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //throw new NotImplementedException();
            double ptx = chart01.ChartAreas["Magnitude"].AxisX.PixelPositionToValue(e.X);
            double pty = chart01.ChartAreas["Magnitude"].AxisX.PixelPositionToValue(e.Y);
            txtStatus.Text = "X=" + ptx.ToString("#.0") + ";" + "Y=" + pty.ToString("#.0");

        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
        }

        private void filterParams_Click(object sender, RoutedEventArgs e)
        {
            ParamWindow paramDialog = new ParamWindow();
            paramDialog.txtPoints.Text = size.ToString();
            paramDialog.txtResistance.Text = this.txtResistance.Text;
            paramDialog.txtResistance2.Text = this.txtResistance2.Text;
            paramDialog.txtInductance.Text = this.txtInductance.Text;
            paramDialog.txtCapacitance.Text = this.txtCapacitance.Text;
            paramDialog.txtMagnitude.Text = this.txtInductance.Text;
            paramDialog.txtFreqMin.Text = this.txtFreqMin.Text;
            paramDialog.txtFreqMax.Text = this.txtFreqMax.Text;

            bool? dialogResult = paramDialog.ShowDialog();

            if(dialogResult == true)
            {

                if(!Int32.TryParse(paramDialog.txtPoints.Text, out size))
                {
                    MessageBox.Show("Błędna wartość rozmiaru tablicy", "Parametry",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (!Double.TryParse(paramDialog.txtResistance.Text, out R1))
                {
                    MessageBox.Show("Błędna wartość rozmiaru tablicy", "Parametry",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (!Double.TryParse(paramDialog.txtResistance2.Text, out R2))
                {
                    MessageBox.Show("Błędna wartość rozmiaru tablicy", "Parametry",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                this.txtResistance.Text = R1.ToString();
                if (!Double.TryParse(paramDialog.txtInductance.Text, out L1))
                {
                    MessageBox.Show("Błędna wartość rozmiaru tablicy", "Parametry",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                this.txtInductance.Text = L1.ToString();
                if (!Double.TryParse(paramDialog.txtCapacitance.Text, out C1))
                {
                    MessageBox.Show("Błędna wartość rozmiaru tablicy", "Parametry",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                this.txtCapacitance.Text = C1.ToString();
                if (!Double.TryParse(paramDialog.txtMagnitude.Text, out U1))
                {
                    MessageBox.Show("Błędna wartość rozmiaru tablicy", "Parametry",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                this.txtMagnitude.Text = U1.ToString();
                if (!Double.TryParse(paramDialog.txtFreqMax.Text, out fmax))
                {
                    MessageBox.Show("Błędna wartość rozmiaru tablicy", "Parametry",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                this.txtFreqMax.Text = fmax.ToString();
                if (!Double.TryParse(paramDialog.txtFreqMin.Text, out fmin))
                {
                    MessageBox.Show("Błędna wartość rozmiaru tablicy", "Parametry",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                this.txtFreqMin.Text = fmin.ToString();
            }
        }

        private void aboutWindow_Click(object sender, RoutedEventArgs e)
        {
            About aboutWindow = new About();
            aboutWindow.ShowDialog();
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            if(!Double.TryParse(txtMagnitude.Text, out U1))
            {
                MessageBox.Show("Błędny format danych", "Parametry", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Double.TryParse(txtFreqMin.Text, out fmin))
            {
                MessageBox.Show("Błędny format danych", "Parametry", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Double.TryParse(txtFreqMax.Text, out fmax))
            {
                MessageBox.Show("Błędny format danych", "Parametry", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Double.TryParse(txtResistance.Text, out R1))
            {
                MessageBox.Show("Błędny format danych", "Parametry", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Double.TryParse(txtResistance2.Text, out R2))
            {
                MessageBox.Show("Błędny format danych", "Parametry", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Double.TryParse(txtInductance.Text, out L1))
            {
                MessageBox.Show("Błędny format danych", "Parametry", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Double.TryParse(txtCapacitance.Text, out C1))
            {
                MessageBox.Show("Błędny format danych", "Parametry", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Transmittance();
            DrawWaveforms();

        }
        private void Transmittance()
        {
            this.Results_1 = new double[size];
            this.Results_2 = new double[size];
            this.Results_3 = new double[size];
            Complex Z1;
            Complex Z2;
            Complex Z3;
            Complex c_R1;
            Complex c_R2;
            Complex c_L;
            Complex c_C;
            Complex c_I1;
            Complex c_U1;
            Complex c_U2;
            double f = fmin;
            double df = (fmax - fmin) / (size - 1);
            double omega = 0;
            //---

            //---
            for (int i = 0; i < size; i++)
            {
                omega = 2 * Math.PI * f;
                Z1 = new Complex(U1, 0);
                Z2 = new Complex((1 - omega * omega * L1 * C1), (omega * R1 * C1));
                Z3 = Z1 / Z2;
                c_R1 = new Complex(R1, 0);
                c_R2 = new Complex(R2, 0);
                c_C = new Complex(0, (-1 / omega / C1));
                c_L = new Complex(0, (omega * L1));
                Z1 = c_R1 + 1 / (c_C + c_R2 + c_L);
                c_U1 = new Complex(U1, 0);
                c_I1 = c_U1 / Z1;
                c_U2 = c_L * c_I1;
                Z3 = c_L / Z1;
                Results_1[i] = f;
                Results_2[i] = Z3.Magnitude;
                Results_3[i] = Z3.Phase;
                f += df;
            }
            //---
           
        }
        public void DrawWaveforms()
        {
            DataTable dTable;
            DataView dView;
            dTable = new DataTable();

            DataColumn column;
            DataRow row;

            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "Frequency";
            dTable.Columns.Add(column);
            
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "Transmittance";
            dTable.Columns.Add(column);
            
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "PhaseSpectrum";
            dTable.Columns.Add(column);

            for (int i = 0; i < size; i++)
            {
                row = dTable.NewRow();
                row["Frequency"] = Results_1[i];
                row["Transmittance"] = Results_2[i];
                row["PhaseSpectrum"] = Results_3[i];

                dTable.Rows.Add(row);
            }
            
            
            dView = new DataView(dTable);

            chart01.Series.Clear();
            chart01.Titles.Clear();
            chart01.Titles.Add("U2/U1");
            chart01.DataBindTable(dView, "Frequency");
            //---
            chart01.Series["Transmittance"].ChartType = SeriesChartType.Line;
            chart01.Series["PhaseSpectrum"].ChartType = SeriesChartType.Line;

            chart01.Series["Transmittance"].ChartArea = "Magnitude";
            chart01.Series["PhaseSpectrum"].ChartArea = "Phase";

            chart01.ChartAreas["Magnitude"].AxisX.LabelStyle.Format = "{#0.0}";
            chart01.ChartAreas["Magnitude"].AxisX.Minimum = 0;

            chart01.ChartAreas["Phase"].AxisX.LabelStyle.Format = "{#0.0}";
            chart01.ChartAreas["Phase"].AxisX.Minimum = 0;
            chart01.Series["Transmittance"].Color = System.Drawing.Color.Red;
            chart01.Series["PhaseSpectrum"].Color = System.Drawing.Color.Blue;

            chart01.Titles[0].Font =
                new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            chart01.ChartAreas[0].AxisX.TitleFont =
                new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            chart01.ChartAreas[0].AxisY.TitleFont =
                new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            chart01.ChartAreas[1].AxisY.TitleFont =
                new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);

            chart01.ChartAreas["Magnitude"].CursorX.IsUserEnabled = true;
            chart01.ChartAreas["Magnitude"].CursorY.IsUserEnabled = true;
            chart01.ChartAreas["Magnitude"].CursorX.IsUserSelectionEnabled = true;
            chart01.ChartAreas["Magnitude"].CursorY.IsUserSelectionEnabled = true;
            chart01.ChartAreas["Magnitude"].CursorX.Interval = 0.0001;
            chart01.ChartAreas["Magnitude"].CursorY.Interval = 0.0001;
            chart01.ChartAreas["Magnitude"].CursorX.LineColor = System.Drawing.Color.BlueViolet;
            chart01.ChartAreas["Magnitude"].CursorY.LineColor = System.Drawing.Color.BlueViolet;
            chart01.ChartAreas["Magnitude"].CursorX.LineDashStyle = ChartDashStyle.Dash;
            chart01.ChartAreas["Magnitude"].CursorY.LineDashStyle = ChartDashStyle.Dash;
            chart01.ChartAreas["Magnitude"].AxisX.ScaleView.Zoomable = true;
            chart01.ChartAreas["Magnitude"].AxisY.ScaleView.Zoomable = true;
            chart01.ChartAreas["Magnitude"].AxisX.Interval = 50;
            chart01.ChartAreas["Magnitude"].AxisY.Interval = 0.1;
            chart01.ChartAreas["Magnitude"].AxisY.ScaleView.SmallScrollMinSize = 0.1;

        }

        private void resetValue_Click(object sender, RoutedEventArgs e)
        {
            this.txtMagnitude.Text = "10";
            this.U1 = 10;
        }

        private void clearWaveforms_Click_1(object sender, RoutedEventArgs e)
        {
            chart01.Series.Clear();
            chart01.Titles.Clear();
        }

        private void MoreInfoFilters(object sender, RoutedEventArgs e)
        {
            MoreInfoAboutFilters MoreInfoWindow = new MoreInfoAboutFilters();
            MoreInfoWindow.ShowDialog();
        }

        private void Author_Click(object sender, RoutedEventArgs e)
        {
            Author AuthorWindow = new Author();
            AuthorWindow.ShowDialog();
        }
        private void openFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt"; // Specify the file filter if needed

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                fileContent = File.ReadAllText(filePath);
            }

            

            string[] splitString = fileContent.Split(';');

            

            foreach (string item in splitString)
            {
                string[] keyValue = item.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string key = keyValue[0].Trim();
                string value = keyValue[1].Trim();

                switch (key)
                {
                    case "U1":
                        U1 = float.Parse(value);
                        break;
                    case "fmax":
                        fmax = float.Parse(value);
                        break;
                    case "fmin":
                        fmin = float.Parse(value);
                        break;
                    case "R1":
                        R1 = float.Parse(value);
                        break;
                    case "R2":
                        R2 = float.Parse(value);
                        break;
                    case "L1":
                        L1 = float.Parse(value);
                        break;
                    case "C1":
                        C1 = float.Parse(value);
                        break;
                }
                this.txtMagnitude.Text = U1.ToString();
                this.txtFreqMax.Text = fmax.ToString();
                this.txtFreqMin.Text = fmin.ToString();
                this.txtResistance.Text = R1.ToString();
                this.txtResistance2.Text = R2.ToString();
                this.txtInductance.Text = L1.ToString();
                this.txtCapacitance.Text = C1.ToString();

            }
        }

        private void SaveFilter_Click(object sender, RoutedEventArgs e)
        {
            string FilterToSave;
            FilterToSave = "U1 " + U1.ToString() + " ; " + "fmax " + fmax.ToString() + " ; " + "fmin " + fmin.ToString() + " ; " + "R1 " + R1.ToString() + " ; " + "R2 " + R2.ToString() + " ; " + "L1 " + L1.ToString() + " ; " + "C1 " + C1.ToString();
   
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                string contentToSave = FilterToSave;

                File.WriteAllText(filePath, contentToSave);
            }
        }

        private void SaveWaweformImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            // Prompt the user to choose the file path and name
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG Files (*.png)|*.png|All Files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                  
                    // Save the chart as an image file
                    chart01.Width = 2000;
                    chart01.Height = 1500;
                    chart01.SaveImage(saveFileDialog.FileName, ChartImageFormat.Jpeg);
                    chart01 = new Chart();
                    host.Child = chart01;
                    chart01.ChartAreas.Add(new ChartArea("Magnitude"));
                    chart01.ChartAreas.Add(new ChartArea("Phase"));
                    chart01.MouseDown += Chart01_MouseDown;
                    chart01.MouseMove += Chart01_MouseMove;
                    DrawWaveforms();
                    MessageBox.Show("Chart saved successfully.");
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the chart as an image: {ex.Message}");
            }
         }

        private void SaveWaveformCSV_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create a SaveFileDialog instance
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "TXT Files (*.txt)|*.txt";
                saveFileDialog.Title = "Save TXT File";

                // Show the SaveFileDialog and get the selected file path
                if (saveFileDialog.ShowDialog() == true)
                {
                    string filePath = saveFileDialog.FileName;

                    // Create a StringBuilder to build the CSV content
                    StringBuilder csvContent = new StringBuilder();

                    // Append headers with tab separators
                    csvContent.AppendLine("Array1\tArray2\tArray3");

                    // Append array data with tab separators
                    for (int i = 0; i < Results_1.Length; i++)
                    {
                        csvContent.AppendLine($"{Results_1[i]}\t{Results_2[i]}\t{Results_3[i]}");
                    }

                    // Write the CSV content to the file
                    File.WriteAllText(filePath, csvContent.ToString());

                    MessageBox.Show("TXT file saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the TXT file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
