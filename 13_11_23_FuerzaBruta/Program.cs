
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

class programaFuerzaBruta
{
    static void Main()
    {
        //crea numero random
        int numeroRandom = fnNumeroRandom();
        // lee todas las lineas del archivo txt
        List<String> allLines = new List<String>(File.ReadAllLines("./contraseñas.txt"));
        //encripta una contraseña random
        string contraseñaEncriptada = CalcularSha256(allLines[numeroRandom]);
        //bool para saber si se ha encontrado la contraseña encriptada
        bool contraseñaEncontrada = false;

        // hecho con 4 hilos:
        int listaThread1 = allLines.Count / 4;
        Thread hilo1 = new Thread(() => threadLeer(allLines, contraseñaEncriptada, 0, listaThread1, 1,contraseñaEncontrada));
        Thread hilo2 = new Thread(() => threadLeer(allLines, contraseñaEncriptada, listaThread1, listaThread1*2, 2,contraseñaEncontrada));
        Thread hilo3 = new Thread(() => threadLeer(allLines, contraseñaEncriptada, listaThread1*2, listaThread1*3, 3,contraseñaEncontrada));
        Thread hilo4 = new Thread(() => threadLeer(allLines, contraseñaEncriptada, listaThread1*3, listaThread1*4, 4,contraseñaEncontrada));

        hilo1.Start();
        hilo2.Start();
        hilo3.Start();
        hilo4.Start();

        




    }
    /**
     * Funcion para crear numero random (usado para encontrar una contraseña en el .txt que encriptar
     * @return : numeroRandom
     */
    static int fnNumeroRandom()
    {
        Random random = new Random();
        int numeroRandom = random.Next(0, 2151219);
        return numeroRandom;
    }
    
    /**
     * Función que calcula el sha256 de una string que pasamos como parámetro.
     * @return: StringSha256 -> string del sha256 encriptada.
     */
    public static string CalcularSha256(string input)
    {
        ;
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

    /**
     * Función para que cada hilo vaya leyendo de la lista
     * @param : allLines -> todas las lineas del .txt
     * @param : contraseñaEncriptada -> contraseña random del .txt que hemos encriptado y vamos comparando
     * @param : start,end -> principio y final de la lista que va a leer
     * @param : hilo -> número del hilo que esta ejecutando la función
     * @param : contraseñaEncontrada -> booleano para saber si la contraseña ha sido encontrada
     */
    static void threadLeer(
        List<String> allLines,
        string contraseñaEncriptada,
        int start,
        int end,
        int hilo,
        bool contraseñaEncontrada
    )
    {
        //creamos reloj para controlar tiempo
        Stopwatch reloj = new Stopwatch();

        
        //empezamos el reloj
            reloj.Start();
            //recorremos la lista, calculando el sha256 de cada linea para comparar con la contraseña encriptada
            for (int i = start; i < end; i++)
            {
                string hashActual = (CalcularSha256(allLines[i]));
                if (hashActual == contraseñaEncriptada && !contraseñaEncontrada)
                {
                    contraseñaEncontrada = true;
                    Console.WriteLine("Contraseña encriptada: " + contraseñaEncriptada);
                    Console.WriteLine("Contraseña desencriptada: " + allLines[i]);
                    Console.WriteLine("Hilo: " + hilo);
                    break;
                }

            }
            //paramos reloj
            reloj.Stop();
            Console.WriteLine("Tiempo hilo " + hilo + ": "  + reloj.ElapsedMilliseconds);
        }
    }



 

// 6ce37d755c717e4e494226715b373a84e14bcd4bb5a489881d995cde6da2099f