using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Pingi_pingi
{
    internal class Program
    {
        //Mette al centro dello schermo lo spazio
        static void centro(int stringa)
        {
            Console.Clear();
            int grandezza = (Console.WindowWidth / 2);
            int altezza = (Console.WindowHeight / 2) / 2;
            for (int i = 0; i < altezza; i++)
                Console.WriteLine("\n");
            for (int i = 0; i < grandezza - (stringa / 2); i++)
                Console.Write(" ");
        }
        //Mette ha meta dello schermo lo spazio
        static void middle(int stringa)
        {
            int grandezza = (Console.WindowWidth / 2);
            for (int i = 0; i < grandezza - (stringa / 2); i++)
                Console.Write(" ");
        }
        static void Main(string[] args)
        {
            string ipurl;
            //Se il programma ha come argomento un url o ip lo usa se no lo prende in inpu
            try
            {
                ipurl = args[0];
            }
            catch (System.IndexOutOfRangeException)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("!! Nessun ip o url inserita come argomento !!");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Vuoi inserire un ip o url?");
                string yes_no = Console.ReadLine().ToLower();
                if (yes_no == "y" || yes_no == "yes")
                {
                    Console.Clear();
                    Console.WriteLine("Inserire ip o url");
                    ipurl = Console.ReadLine();
                }
                else
                    return;
            }
            string connesione_stringa = "!! No internet !!";
            bool connesione = false; //Se c'è connesione questo è true
            Ping pingSender = new Ping();
            string data = "thisisatesttoseeifihaveinternetl"; //Dati che verrano mandati
            byte[] buffer = Encoding.ASCII.GetBytes(data); //Dati in byte che verrano mandati
            int timeout = 500;
            PingOptions options = new PingOptions(64,true);
            Console.ForegroundColor = ConsoleColor.Red;
            //Finchè non c'è una risposta continua a riprovare
            while (!connesione)
                //Se non c'è internet entra nel catch, se c'è esce dal while
                try
                {
                    PingReply reply = pingSender.Send(ipurl, timeout, buffer, options);
                    connesione = true;
                }
                catch (System.Net.NetworkInformation.PingException)
                {
                    connesione_stringa = "!! No internet !!";
                    centro(connesione_stringa.Length);
                    Console.WriteLine(connesione_stringa);
                    connesione_stringa = $"L'ip o url inserito è {ipurl}";
                    middle(connesione_stringa.Length);
                    Console.Write(connesione_stringa);
                }

            Console.ForegroundColor = ConsoleColor.Green;
            connesione_stringa = "!! Yes internet !!";
            centro(connesione_stringa.Length);
            Console.WriteLine(connesione_stringa);

            Console.ReadKey();
        }
    }
}
