using System;
using System.Threading.Tasks;

namespace AsincroniaPractica
{

    class Program
    {
        
        static async Task Main(string[] args)
        {

            
            int[] busqueda = new int[1000];
            int numMayor = 0;
            int posicion = 0;

            for (int i = 0; i < busqueda.Length; i++)
            {
                busqueda[i] = new Random().Next(45001);
            }

            for (int i = 0; i < busqueda.Length; i++)
            {
                if (busqueda[i] > numMayor)
                {
                    numMayor = busqueda[i];
                    posicion = i;

                }
            }

            Console.WriteLine($"El número mayor en el array es {numMayor} y se encuentra en la posicion {posicion}");

            //proceso asincrónico  

            numMayor = 0;
            posicion = 0;
            Object bloqueador = new Object();

            BuscarAsync(bloqueador, busqueda, 0, (busqueda.Length / 2));
            await BuscarAsync(bloqueador, busqueda, (busqueda.Length / 2), busqueda.Length);

            Console.WriteLine($"El número mayor en el array es {numMayor} y se encuentra en la posicion {posicion}");


            int Buscar(Object bloquear, int[] arr, int inicio, int fin)
            {
                for (int i = inicio; i < fin; i++)
                {
                    lock (bloquear)
                    {
                         //Console.WriteLine($"Buscando en array que inicio en {inicio} , {numMayor} , vuelta {i}");
                        if (arr[i] > numMayor)
                        {
                            numMayor = arr[i];
                            posicion = i;

                        }
                    }
                }

               Console.WriteLine($"Terminó busqueda en mitad de array {inicio}");

                return numMayor;
            }

            async Task<int> BuscarAsync(Object bloquear, int[] arr, int inicio, int fin)
            {

              return await Task<int>.Run(() => Buscar(bloquear, arr, inicio, fin));
               
            }

        }

    }
}

