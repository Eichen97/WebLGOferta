using System;
using System.Text;
using System.Security.Cryptography; 

namespace ConsoleSha1
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Encriptar();

        }

        private void Encriptar()
        {
            Console.WriteLine(EncriptarSHA1("admin"));
            Console.WriteLine(EncriptarSHA1("admin"));
            Console.ReadKey();

        }

        private string EncriptarSHA1(string cadena)
        {

            UTF8Encoding enc = new UTF8Encoding();
            byte[] data = enc.GetBytes(cadena);
            byte[] result;

            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();

            result = sha.ComputeHash(data);


            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {

                // Convertimos los valores en hexadecimal
                // cuando tiene una cifra hay que rellenarlo con cero
                // para que siempre ocupen dos dígitos.
                if (result[i] < 16)
                {
                    sb.Append("0");
                }
                sb.Append(result[i].ToString("x"));
            }

            //Devolvemos la cadena con el hash en mayúsculas para que quede más chuli 🙂
            return sb.ToString().ToUpper();
        }
    }
}
