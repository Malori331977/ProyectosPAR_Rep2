using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Text;

namespace ProyectoPAR.Models
{
    public class Utiles
    {
        public static async Task SetFocus(InputText txt)
        {
            if (txt == null) return;

            if (txt.Element.HasValue)
            {
                await txt.Element.Value.FocusAsync();
            }
        }

        public static async Task SetFocus(InputTextArea txt)
        {
            if (txt == null) return;

            if (txt.Element.HasValue)
            {
                await txt.Element.Value.FocusAsync();
            }
        }

        public static async Task SetFocus(InputNumber<decimal> txt)
        {
            if (txt == null) return;

            if (txt.Element.HasValue)
            {
                await txt.Element.Value.FocusAsync();
            }
        }

        
        /// <summary>
        /// Get substring of specified number of characters on the right.
        /// </summary>
        public static string Right(string value, int length)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            return value.Length <= length ? value : value.Substring(value.Length - length);
        }

        public static string GeneraCodigoSeguridad()
        {
            var characters = "ABCDEFGH!JKLMNOPQRSTUVWXYZabcdefgh?jk&mnopqrstuvwxyz0123456789";

            var Charsarr = new char[8];
            var random = new Random();

            for (int i = 0; i < Charsarr.Length; i++)
            {
                Charsarr[i] = characters[random.Next(characters.Length)];
            }

            var resultString = new String(Charsarr);

            return resultString;
        }

        public static byte[] GetBytes(string text)
        => Encoding.UTF8.GetBytes(text);

        public static string Base64StringToAscii(string token)
        {
            byte[] bytes = Convert.FromBase64String(token);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        public static IEnumerable<int> PageSizeOptions()
        {
            return new int[] { 7, 10, 25, 50, 100, 500 };
        }

    }
}
