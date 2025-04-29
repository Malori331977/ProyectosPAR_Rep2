using iTextSharp.text;
using System.Globalization;

namespace ProyectoPAR.Pages.Reportes
{
    
    public class ReporteBase
    {

        static Font TextFormat = FontFactory.GetFont(FontFactory.HELVETICA, 24f, new BaseColor(99, 99, 99));

        public static float InverseY(float Y)
        {
            // PDF uses a coordinate system which starts in the left corner at the BOTTOM of the page, not at the Top
            return PageSize.Letter.Height - Y;
        }

        public static Phrase Text(string text)
        {
            var fontStylePhrase = FontFactory.GetFont(FontFactory.HELVETICA, 16f, new BaseColor(99, 99, 99));

            return new Phrase(text, fontStylePhrase);
        }

        public static Phrase Text(string text, System.Single fontSize)
        {
            var fontStylePhrase = FontFactory.GetFont(FontFactory.HELVETICA, fontSize, new BaseColor(99, 99, 99));

            return new Phrase(text, fontStylePhrase);
        }

        public static string GetNombreMes(DateTime fecha)
        {
            CultureInfo ci = new CultureInfo("Es-Es");
            return ci.DateTimeFormat.GetMonthName(fecha.Month);
        }


    }
}
