using System;

namespace DIO.Cadastro_Serie_Filmes
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static int entradaGenero, entradaAno;
        static string entradaTitulo, entradaDescricao;
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nOpção Inválida!!!");
                        Console.ResetColor();
                        break;
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }
        }

        private static void VisualizarSerie()
        {
            Console.WriteLine("VISUALIZAR SÉRIE: ");
            var serie = repositorio.RetornaPorId(IndiceSerie());

            Console.WriteLine(serie);
        }

        private static void AtualizarSerie()
        {
            Console.WriteLine("ATUALIZAR SÉRIE: ");
            int indiceSerie = IndiceSerie();
            InserirAtributos();

            Serie atualizarSerie = new Serie(id: indiceSerie,
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorio.Atualizar(indiceSerie, atualizarSerie);
        }

        private static void ExcluirSerie()
        {
            Console.WriteLine("EXCLUIR SÉRIE: ");
            int indiceSerie = IndiceSerie();

            Console.WriteLine("Deseja mesmo excluir a série? ");
            Console.WriteLine("1- SIM ");
            string confirmacao = Console.ReadLine();

            if (confirmacao == "1")
            {
                repositorio.Excluir(indiceSerie);
            }
            else
            {
                Console.WriteLine("Operação Cancelada");
            }
        }

        private static void InserirSerie()
        {
            Console.WriteLine("INSERIR NOVA SÉRIE");

            InserirAtributos();

            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorio.Insere(novaSerie);
        }

        private static void ListarSeries()
        {
            Console.WriteLine("Listar Séries");

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada.");
                Console.Clear();
                return;
            }

            foreach (var serie in lista)
            {
                if (!serie.retornaExcluido() == true)
                {
                    Console.WriteLine("#ID {0}: - {1}", serie.retornaId(), serie.retornaTitulo());
                }
            }
        }

        private static string ObterOpcaoUsuario()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Cadastro de Séries e Filmes!!!");
            Console.WriteLine("Informe a opção desejada:");
            Console.WriteLine();
            Console.WriteLine("1- Listar séries");
            Console.WriteLine("2- Inserir nova série");
            Console.WriteLine("3- Atualizar série");
            Console.WriteLine("4- Excluir série");
            Console.WriteLine("5- Vizualizar série");
            Console.WriteLine("C- Limpar Tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
            Console.ResetColor();

            Console.Write("Digite a opção desejada: ");
            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            Console.WriteLine("---------------------------------------");
            return opcaoUsuario;
        }

        private static void InserirAtributos()
        {
            Console.WriteLine();
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("---------------------------------------\n");
            Console.Write("Digite o gênero entre as opções acima: ");
            entradaGenero = int.Parse(Console.ReadLine());

            if (entradaGenero > 0 && entradaGenero < 14)
            {
                Console.Write("Digite o Título da Série: ");
                entradaTitulo = Console.ReadLine();

                Console.Write("Digite o Ano de Início da Série: ");
                entradaAno = int.Parse(Console.ReadLine());

                if (entradaAno > 1900)
                {
                    Console.Write("Digite a Descrição da Série: ");
                    entradaDescricao = Console.ReadLine();
                }
            }
            
            Console.WriteLine("---------------------------------------");
            Console.ResetColor();
        }

        private static int IndiceSerie()
        {
            Console.Write("Digite o id da serie: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            return indiceSerie;
        }
    }
}
