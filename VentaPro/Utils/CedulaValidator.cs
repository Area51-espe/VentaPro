using System;

namespace VentaPro.Utils
{
    public static class CedulaValidator
    {
        public static bool EsCedulaValida(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula) || cedula.Length != 10 || !long.TryParse(cedula, out _))
                return false;

            // Validar código de provincia
            int provincia = int.Parse(cedula.Substring(0, 2));
            if (!(provincia >= 1 && provincia <= 24 || provincia == 30))
                return false;

            // Validar tercer dígito
            if (int.Parse(cedula[2].ToString()) > 5)
                return false;

            int[] digitos = new int[10];
            for (int i = 0; i < 10; i++)
                digitos[i] = int.Parse(cedula[i].ToString());

            int suma = 0;
            for (int i = 0; i < 9; i++)
            {
                if (i % 2 == 0) // posiciones impares (0,2,4,6,8)
                {
                    int valor = digitos[i] * 2;
                    if (valor > 9)
                        valor -= 9;
                    suma += valor;
                }
                else // posiciones pares
                {
                    suma += digitos[i];
                }
            }

            int residuo = suma % 10;
            int digitoVerificador = residuo == 0 ? 0 : 10 - residuo;

            return digitoVerificador == digitos[9];
        }
    }
}
