using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using PdfSharpCore.Utils;
using System.Diagnostics;


namespace C_Sharp_Basic.PDFGeneration.PDFSharpCore
{
    public class PDFExamples
    {

        public void GenerateUsingPDFSharpCore()
        {
            Console.Write("Generating PDF");
            Stopwatch sw = Stopwatch.StartNew();
            GlobalFontSettings.FontResolver = new FontResolver();

            var document = new PdfDocument();
            var page = document.AddPage();

            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Arial", 20, XFontStyle.Bold);

            var textColor = XBrushes.Black;
            var layout = new XRect(20, 20, page.Width, page.Height);
            var format = XStringFormats.Center;

            gfx.DrawString("Hello World!", font, textColor, layout, format);

            document.Save($"{Constants.PDFSharpCorePath}/testing.pdf");
            sw.Stop();
            Console.WriteLine($"Ellipsed time is {sw.ElapsedMilliseconds}");
            Console.Write("PDF is generated!");
        }
        
    }
}
