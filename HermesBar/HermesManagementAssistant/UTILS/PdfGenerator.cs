using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class PdfGenerator
    {
        public static BaseColor collumHeaderColor
        {
            get
            {
                return WebColors.GetRGBColor("#999999");
            }
        }
        public static BaseColor tableHeaderColor
        {
            get
            {
                return WebColors.GetRGBColor("#12508b");
            }
        }
        public static Font arial
        {
            get
            {
                return FontFactory.GetFont("Arial", 5);
            }
        }
        public static Font headerFont
        {
            get
            {
                return FontFactory.GetFont("Arial", 12, WebColors.GetRGBColor("#FFFFFF"));
            }
        }

        public static void CreateCell(ref PdfPTable table, object obj)
        {
            try
            {
                Chunk value = new Chunk(obj.ToString(), arial);
                PdfPCell cell = new PdfPCell(new Phrase(value));
                cell.HorizontalAlignment = 1;

                table.AddCell(cell);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void CreateHeader(ref PdfPTable table, object model)
        {
            BaseColor collumnHeaderColor = WebColors.GetRGBColor("#999999");
            object[] attribute;
            foreach (PropertyInfo prop in model.GetType().GetProperties())
            {
                attribute = prop.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                if (attribute.Count() != 0)
                {
                    var displayName = (attribute[0] as DisplayNameAttribute).DisplayName;
                    Chunk value = new Chunk(displayName, arial);
                    PdfPCell celula = new PdfPCell(new Phrase(value));
                    celula.BackgroundColor = collumnHeaderColor;
                    celula.HorizontalAlignment = 1;
                    table.AddCell(celula);
                }
            }
        }
        public static void CreateTitle(ref PdfPTable table, string title, int colspan)
        {
            Chunk header = new Chunk(title, headerFont);
            PdfPCell cell = new PdfPCell(new Phrase(header));
            cell.Colspan = colspan;
            cell.HorizontalAlignment = 1;   //0=Left, 1=Center, 2=Right
            cell.VerticalAlignment = 1;     //0=Left, 1=Center, 2=Right
            cell.BackgroundColor = tableHeaderColor;

            table.AddCell(cell);
        }
    }
}
