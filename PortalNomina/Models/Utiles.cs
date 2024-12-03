using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace PortalNomina.Models
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

    }
}
