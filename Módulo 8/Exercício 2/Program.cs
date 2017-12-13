using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercício_2
{
    class Program
    {
        private static string pathDefault;

        static void Main(string[] args)
        {
			// Endereço padrão para armazenar os dados dos contatos
            pathDefault = @"C:\dados.txt";

            CriaMenu();
        }

        public static void CriaMenu()
        {
            int opcao;
            Console.Clear();

            Console.WriteLine("******* Agenda Telefônica *******");
            Console.WriteLine("Escolha as opções abaixo:");
            Console.WriteLine("1 - Adicionar contatos");
            Console.WriteLine("2 - Listar contatos");
            Console.WriteLine("3 - Apagar todos os contatos");
            Console.WriteLine("0 - Sair");
            Console.Write("Opção: ");
            opcao = Convert.ToInt16(Console.ReadLine());

            switch (opcao)
            {
                case 0:
                    Sair();
                    break;

                case 1:
                    AddContato();
                    break;

                case 2:
                    ListContato();
                    break;

                case 3:
                    AskDel();
                    break;

                default:
                    Sair();
                    break;
            }
        }

        public static void AddContato()
        {
            int numPessoas;

            Console.Clear();

            Console.Write("Digite o número de pessoas que deseja cadastrar: ");
            numPessoas = Convert.ToInt16(Console.ReadLine());

            Pessoa[] pessoas = new Pessoa[numPessoas];

            for (int i = 0; i < numPessoas; i++)
            {
                Pessoa p = new Pessoa();

                Console.Write("Nome: ");
                p.Nome = Console.ReadLine();

                Console.Write("Telefone: ");
                p.Telefone = Console.ReadLine();

                pessoas[i] = p;
            }

            Program.SaveContatos(pessoas);

            CriaMenu();
        }

        public static void ListContato()
        {
            string dadosDoArquivo;
            StreamReader sr;

            sr = new StreamReader(pathDefault, true);

            dadosDoArquivo = sr.ReadToEnd();

            sr.Close();

            Console.Clear();
            Console.WriteLine("******* Agenda Telefônica *******");

            if (dadosDoArquivo.Length <= 0)
            {
                Console.WriteLine("Nenhum contato adicionado ainda.");
            }
            else
            {
                Console.WriteLine("Contatos:");
                Console.WriteLine("NOME - TELEFONE");
                Console.Write(dadosDoArquivo);
            }

            Console.WriteLine();
            Sair();
            CriaMenu();
        }

        public static void SaveContatos(Pessoa[] pessoas)
        {
            string dado;
            try
            {
                StreamWriter sw = new StreamWriter(pathDefault, true);

                // Ordena o array Pessoas pelo nome
                InsertionSort(pessoas.Length, pessoas);

                foreach (Pessoa p in pessoas)
                {
                    dado = p.Nome + " - " + p.Telefone;
                    sw.WriteLine(dado);
                }

                sw.Close();

            }
            catch(Exception ex)
            {
                Console.WriteLine("Não foi possível salvar os contatos.");
                //Sair();

                throw ex;
            }

        }

        public static void AskDel()
        {
            string opcao;
            Console.Clear();

            Console.WriteLine("Deseja realmente apagar todos os contatos? S (Sim) ou N (Não)");
            Console.Write("Opção: ");
            opcao = Console.ReadLine();

            switch (opcao.ToLower())
            {
                case "s":
                    DelAllContatos();
                    break;

                case "n":
                    CriaMenu();
                    break;

                default:
                    AskDel();
                    break;
            }

        }

        public static void DelAllContatos()
        {
            try
            {
                StreamWriter sw = new StreamWriter(pathDefault, false);

                sw.Write("");
                sw.Close();

                Console.WriteLine("Todos os contatos foram apagados!");
                Sair();
                CriaMenu();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Não foi possível apagar todos os contatos.");
                throw ex;
            }

        }

        public static void InsertionSort(int n, Pessoa[] p)
        {
            int i, j;
            Pessoa temp;

            for (i = 1; i < n; i++)
            {
                j = i;
                while (j > 0 && (String.Compare(p[j].Nome, p[j - 1].Nome) < 0))
                {
                    temp = p[j];
                    p[j] = p[j - 1];
                    p[j - 1] = temp;
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
