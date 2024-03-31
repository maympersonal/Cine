using System;
using System.Text;

namespace Backend.Settings
{
    public class Settings
    {
        public static string GenerarCodigo()
        {
            // Definir los caracteres alfanuméricos permitidos
            const string caracteresPermitidos = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            // Crear un objeto Random para generar números aleatorios
            Random random = new Random();

            // Crear un StringBuilder para construir la cadena
            StringBuilder sb = new StringBuilder();

            // Generar la cadena alfanumérica aleatoria
            for (int i = 0; i < 11; i++)
            {
                // Obtener un índice aleatorio dentro del rango de caracteresPermitidos
                int indiceAleatorio = random.Next(caracteresPermitidos.Length);

                // Agregar el carácter correspondiente al StringBuilder
                sb.Append(caracteresPermitidos[indiceAleatorio]);
            }

            return sb.ToString();;
        }
    }
}