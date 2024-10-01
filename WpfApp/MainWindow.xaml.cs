using FastReport;
using FastReport.Export.PdfSimple;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void ButtonWeb_Click(object sender, RoutedEventArgs e)
        {
            var report = new Report();
            report.Load("designtable.frx");

            var users = new List<Users>()
            {
                new Users() { Id = 1, Name = "Ana" },
                new Users() { Id = 2, Name = "Lucas" }
            };

            report.Dictionary.RegisterBusinessObject(users, "Users", 10, true);

            if (report.Prepare())
            {
                var pdfExport = new PDFSimpleExport();
                using (var memoryStream = new MemoryStream())
                {
                    report.Export(pdfExport, memoryStream);

                    var tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
                    File.WriteAllBytes(tempFilePath, memoryStream.ToArray());

                    try
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = tempFilePath,
                            UseShellExecute = true
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao abrir arquivo PDF: {ex.Message} ");
                    }
                }
            }
        }
    }
}
