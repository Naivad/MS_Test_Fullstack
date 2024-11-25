using System.Text.RegularExpressions;

namespace MS_Test_Fullstack.Helpers
{
    public class CleanFiels
    {
        public string SanitizeInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return input;

            // Eliminar potenciales scripts o caracteres maliciosos
            var sanitized = Regex.Replace(input, @"<.*?>|['"";]", string.Empty); // Elimina etiquetas HTML y caracteres peligrosos
            return sanitized;
        }
    }
}
