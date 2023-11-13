// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography;
using System.Text;

class programaFuerzaBruta
{
    static void Main()
    {
        List<String> allLines = new List<String>(File.ReadAllLines("./contraseñas.txt"));
        Random random = new Random();
        int numeroRandom = random.Next(0, 2151219);

        string contraseñaEncriptada = CalcularSha256(allLines[numeroRandom]);
        
        for (int i = 0; i < allLines.Count; i++)
        {
            string hashActual = (CalcularSha256(allLines[i]));
            if (hashActual == contraseñaEncriptada)
            {
                Console.WriteLine("Contraseña encriptada: " + contraseñaEncriptada);
                Console.WriteLine("Contraseña desencriptada: " + allLines[i]);
                break;
            }
           
        }
        
        
        static string CalcularSha256(string input)
        { ;
            StringBuilder StringSha256 = new StringBuilder();
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytesDeEntrada = Encoding.UTF8.GetBytes(input);
                byte[] valorHash = sha256.ComputeHash(bytesDeEntrada);
                foreach (byte b in valorHash)
                {
                    StringSha256.Append(b.ToString("x2"));
                }
            }

            return StringSha256.ToString();
        }
    }
}

// 6ce37d755c717e4e494226715b373a84e14bcd4bb5a489881d995cde6da2099f