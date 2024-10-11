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
        private const string BusinessObjectDataSource = "Comissao";
        private const string Design = "recibo-comissao.frx";

        private List<Comissao> _comissoes = new List<Comissao>()
        {
            new Comissao()
            {
                Id = 234324,
                DtGeracao = DateTime.Now,
                DtPrevisao = DateTime.Now,
                ComissaoDetalhe = new List<ComissaoDetalhe>()
                {
                    new ComissaoDetalhe()
                    {
                        Produto = "Amil - PME",
                        Corretor = "Renata Maria",
                        DtCadastro = DateTime.Now,
                        DtAssinatura = DateTime.Now,
                        DtVigencia = DateTime.Now,
                        Proposta = 98798,
                        Parcela = 67,
                        VlParcela = 5977,
                        Porcentagem = 2,
                        VlBruto = 103.99m,
                        Taxa = 0,
                        Adm = -10,
                        DescAdm = -10.40m,
                        ISS = 0,
                        Liquido = 93.59m
                    },
                    new ComissaoDetalhe()
                    {
                        Produto = "GNDI - PME",
                        Corretor = "Morales",
                        DtCadastro = DateTime.Now,
                        DtAssinatura = DateTime.Now,
                        DtVigencia = DateTime.Now,
                        Proposta = 337671,
                        Parcela = 48,
                        VlParcela = 1465.08m,
                        Porcentagem = 2,
                        VlBruto = 29.30m,
                        Taxa = 0,
                        Adm = -10,
                        DescAdm = -2.93m,
                        ISS = 0,
                        Liquido = 26.37m
                    }
                }
            }
        };
        public MainWindow()
        {
            InitializeComponent();
            ShowReport();
        }

        private void ShowReport()
        {
            var report = new Report();
            report.Load(Design);

            report.Dictionary.RegisterBusinessObject(_comissoes, BusinessObjectDataSource, 10, true);

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
