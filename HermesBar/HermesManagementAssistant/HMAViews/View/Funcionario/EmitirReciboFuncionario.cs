using MODEL;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMAViews.View.Funcionario
{
    public class EmitirReciboFuncionario
    {
        public void emitirRecibos(FuncionarioModel funcionario)
        {
            try
            {
                FileInfo aFile = new FileInfo("C:/HermesManagementAssistant/ReciboFuncionario/ReciboFuncionario.txt");
                if (aFile.Exists)
                {
                    StreamWriter valor = new StreamWriter("C:/HermesManagementAssistant/ReciboFuncionario/ReciboFuncionario.txt", true, Encoding.ASCII);
                    valor.Write("Recibo de Pagamento");
                    valor.WriteLine();
                    valor.Write("Giuliano Henrique Costa");
                    valor.Close();

                    DoText();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void DoText()
        {
            string line = null;
            TextReader readFile = new StreamReader("C:/HermesManagementAssistant/ReciboFuncionario/ReciboFuncionario.txt");
            int yPoint = 0;

            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = "TXT to PDF";
            PdfPage pdfPage = pdf.AddPage();
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont font = new XFont("Verdana", 20, XFontStyle.Regular);

            while (true)
            {
                line = readFile.ReadLine();
                if (line == null)
                {
                    break; // TODO: might not be correct. Was : Exit While
                }
                else
                {
                    graph.DrawString(line, font, XBrushes.Black, new XRect(40, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    yPoint = yPoint + 40;
                }
            }

            string pdfFilename = "C:/HermesManagementAssistant/ReciboFuncionario/ReciboFuncionario.pdf";
            pdf.Save(pdfFilename);
            readFile.Close();
            readFile = null;
            Process.Start(pdfFilename);
        }
    }
}
