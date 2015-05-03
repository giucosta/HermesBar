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
                    var text = new StringBuilder();
                    text.Append("========================RECIBO DE PAGAMENTO========================");
                    text.Append('\n');
                    text.Append("Recebi(emos) de ");
                    text.Append("Hermes Bar e Restaurante ");
                    text.Append("a quantia de R$ 150,00");
                    text.Append(" (Cento e cinquenta reais)");
                    text.Append('\n');
                    text.Append("corresponde a serviços prestados de segurança");
                    text.Append('\n');
                    text.Append("e para clareza firmo(amos) o presente na cidade de Curitiba - PR");
                    text.Append('\n');
                    text.Append("na data de: " + DateTime.Now.ToShortDateString());
                    text.Append('\n');
                    text.Append("Assinatura:___________________________________________________");
                    valor.Write(text.ToString());
                    valor.Close();

                    ConvertAchiveTextToPdf("HMA_Recibo_Pagamento_" + funcionario.Nome + DateTime.Now.ToShortDateString(), "C:/HermesManagementAssistant/ReciboFuncionario/ReciboFuncionario.txt");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void ConvertAchiveTextToPdf(string titulo, string caminhoArquivo)
        {
            string line = null;
            TextReader readFile = new StreamReader(caminhoArquivo);
            int yPoint = 0;

            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = titulo;
            PdfPage pdfPage = pdf.AddPage();
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont font = new XFont("Verdana", 10, XFontStyle.Regular);

            while (true)
            {
                line = readFile.ReadLine();
                if (line != null)
                {
                    graph.DrawString(line, font, XBrushes.Black, new XRect(40, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    yPoint = yPoint + 40;
                }
                else
                    break;
            }

            string pdfFilename = "C:/HermesManagementAssistant/ReciboFuncionario/" + "teste" + ".pdf";
            pdf.Save(pdfFilename);
            readFile.Close();
            readFile = null;
            Process.Start(pdfFilename);
        }
    }
}
