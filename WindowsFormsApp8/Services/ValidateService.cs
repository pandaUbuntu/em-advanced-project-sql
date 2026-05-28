using System;
using System.Windows.Forms;

namespace WindowsFormsApp8.Services
{
    public class ValidateService
    {
        public static string validateTextBox(TextBox textBox, int minLength, string fieldName)
        {
            string text = textBox.Text.Trim();

            if (text.Length < minLength)
                throw new Exception($"Недостатня довжина тексту в полі {fieldName}!");

            return text;
        }
    }
}
