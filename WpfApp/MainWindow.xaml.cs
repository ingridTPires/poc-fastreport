using FastReport;
using FastReport.Export.PdfSimple;
using PdfiumViewer;
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
        private readonly List<Users> _users = new List<Users>()
        {
            new Users() { Id = 1, Name = "Ana" },
            new Users() { Id = 2, Name = "Lucas" }
        };
        private const string BusinessObjectDataSource = "Users";
        private const string Design = "designtable.frx";
        public MainWindow()
        {
            InitializeComponent();
            ShowReport();
        }

        private void ShowReport()
        {
            var report = new Report();
            report.Load(Design);

            report.Dictionary.RegisterBusinessObject(_users, BusinessObjectDataSource, 10, true);

            if (report.Prepare())
            {
                var pdfExport = new PDFSimpleExport();
                using (var memoryStream = new MemoryStream())
                {
                    report.Export(pdfExport, memoryStream);
                    memoryStream.Position = 0;

                    var pdfViewer = new PdfViewer() { Document = PdfDocument.Load(memoryStream) };

                    WindowsFormsHost.Child = pdfViewer;
                }
            }
            report.Dispose();
        }

        private void ButtonWeb_Click(object sender, RoutedEventArgs e)
        {
            var report = new Report();
            report.Load(Design);

            report.Dictionary.RegisterBusinessObject(_users, BusinessObjectDataSource, 10, true);

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
