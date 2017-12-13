using System;
using System.IO;

namespace Exercício_3
{
    class Program
    {
        static void Main(string[] args)
        {
            string dadosArquivo1, dadosArquivo2, dados;
            string[] arrDados;

            StreamReader sr1, sr2;
            StreamWriter sw;

            sr1 = new StreamReader(@"arquivo1.txt");
            sr2 = new StreamReader(@"arquivo2.txt");

            // false -> para o parâmetro append
            sw = new StreamWriter(@"arquivo3.txt", false);

            dadosArquivo1 = sr1.ReadToEnd();
            dadosArquivo2 = sr2.ReadToEnd();

            dados = dadosArquivo1 + " " + dadosArquivo2;

            char[] sep = new char[] { ' ', '\n' };

            arrDados = dados.Split(sep);

            // Método de ordenação
            InsertionSort(arrDados.Length, arrDados);

            // Imprimir no console
            foreach (string line in arrDados)
            {
                Console.WriteLine(line);
                
                // Escrever em arquivo
                sw.WriteLine(line);
            }       

            sr1.Close();
            sr2.Close();
            sw.Close();
            
            Sair();
            
        }

        public static void InsertionSort(int n, string[] s)
        {
            int i, j;
            string temp;

            for (i = 1; i < n; i++)
            {
                j = i;
                while (j > 0 && (String.Compare(s[j], s[j - 1]) < 0))
                {
                    temp = s[j];
                    s[j] = s[j - 1];
                    s[j - 1] = temp;
                    j--;
                }
            }
        }

        public static void Sair()
        {
            Console.WriteLine("Pressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}
