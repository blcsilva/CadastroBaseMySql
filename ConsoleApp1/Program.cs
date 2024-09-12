using System;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

class Program
{
    static void Main()
    {
        // Declaração das variáveis
        string nome;
        int idade;
        char sexo;
        string rg;
        string cpf;
        string cidade;
        string cep;
        DateTime dataNascimento;

        // Solicitação e leitura dos dados
        Console.Write("Digite seu nome: ");
        nome = Console.ReadLine();

        idade = LerIdade();
        sexo = LerSexo();
        rg = LerRG();
        cpf = LerCPF();

        Console.Write("Digite sua cidade: ");
        cidade = Console.ReadLine();

        cep = LerCEP();
        dataNascimento = LerDataNascimento();

        // Criação da string concatenada com quebras de linha
        string resultado =
            $"Nome: {nome}\n" +
            $"Idade: {idade}\n" +
            $"Sexo: {sexo}\n" +
            $"RG: {rg}\n" +
            $"CPF: {cpf}\n" +
            $"Cidade: {cidade}\n" +
            $"CEP: {cep}\n" +
            $"Data de Nascimento: {dataNascimento:dd/MM/yyyy}";

        // Exibição do resultado
        Console.WriteLine(resultado);

        // Gravação dos dados no banco de dados
        GravarDadosNoBanco(nome, idade, sexo, rg, cpf, cidade, cep, dataNascimento);
    }

    static int LerIdade()
    {
        while (true)
        {
            Console.Write("Digite sua idade: ");
            string idadeInput = Console.ReadLine();
            if (int.TryParse(idadeInput, out int idade) && idade > 0)
            {
                return idade;
            }
            else
            {
                Console.WriteLine("Por favor, digite um número válido para a idade.");
            }
        }
    }

    static char LerSexo()
    {
        while (true)
        {
            Console.Write("Digite seu sexo (M/F): ");
            string sexoInput = Console.ReadLine();
            if (char.TryParse(sexoInput, out char sexo) && (sexo == 'M' || sexo == 'F'))
            {
                return sexo;
            }
            else
            {
                Console.WriteLine("Por favor, digite 'M' para masculino ou 'F' para feminino.");
            }
        }
    }

    static string LerRG()
    {
        while (true)
        {
            Console.Write("Digite seu RG (somente números): ");
            string rg = Console.ReadLine();
            if (Regex.IsMatch(rg, @"^\d+$"))
            {
                return AplicarMascaraRG(rg);
            }
            else
            {
                Console.WriteLine("RG inválido. Por favor, digite apenas números.");
            }
        }
    }

    static string LerCPF()
    {
        while (true)
        {
            Console.Write("Digite seu CPF (somente números): ");
            string cpf = Console.ReadLine();
            if (Regex.IsMatch(cpf, @"^\d{11}$"))
            {
                return AplicarMascaraCPF(cpf);
            }
            else
            {
                Console.WriteLine("CPF inválido. Por favor, digite exatamente 11 números.");
            }
        }
    }

    static string LerCEP()
    {
        while (true)
        {
            Console.Write("Digite seu CEP (somente números e formato 00000-000): ");
            string cep = Console.ReadLine();
            if (Regex.IsMatch(cep, @"^\d{5}-\d{3}$"))
            {
                return AplicarMascaraCEP(cep);
            }
            else
            {
                Console.WriteLine("CEP inválido. Por favor, use o formato 00000-000.");
            }
        }
    }

    static DateTime LerDataNascimento()
    {
        while (true)
        {
            Console.Write("Digite sua data de nascimento (dd/MM/yyyy): ");
            string dataNascimentoInput = Console.ReadLine();
            if (DateTime.TryParseExact(dataNascimentoInput, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dataNascimento))
            {
                return dataNascimento;
            }
            else
            {
                Console.WriteLine("Data inválida. Por favor, use o formato dd/MM/yyyy.");
            }
        }
    }

    static string AplicarMascaraRG(string rg)
    {
        // Assumindo que o RG pode ter 7 dígitos e opcionalmente um dígito verificador
        return rg.Length == 7 ? $"{rg.Substring(0, 2)}.{rg.Substring(2, 3)}.{rg.Substring(5, 2)}" : rg;
    }

    static string AplicarMascaraCPF(string cpf)
    {
        // Assumindo o formato 000.000.000-00
        return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";
    }

    static string AplicarMascaraCEP(string cep)
    {
        // Assumindo o formato 00000-000
        return $"{cep.Substring(0, 5)}-{cep.Substring(5, 3)}";
    }

    static void GravarDadosNoBanco(string nome, int idade, char sexo, string rg, string cpf, string cidade, string cep, DateTime dataNascimento)
    {
        // String de conexão com o banco de dados
        string connectionString = "Server=localhost;Database=nome_do_banco;User ID=usuario;Password=senha;";

        // Comando SQL para inserir os dados
        string sql = @"INSERT INTO Pessoas (Nome, Idade, Sexo, RG, CPF, Cidade, CEP, DataNascimento) 
                        VALUES (@Nome, @Idade, @Sexo, @RG, @CPF, @Cidade, @CEP, @DataNascimento)";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            MySqlCommand command = new MySqlCommand(sql, connection);

            // Adicionando parâmetros ao comando SQL
            command.Parameters.AddWithValue("@Nome", nome);
            command.Parameters.AddWithValue("@Idade", idade);
            command.Parameters.AddWithValue("@Sexo", sexo);
            command.Parameters.AddWithValue("@RG", rg);
            command.Parameters.AddWithValue("@CPF", cpf);
            command.Parameters.AddWithValue("@Cidade", cidade);
            command.Parameters.AddWithValue("@CEP", cep);
            command.Parameters.AddWithValue("@DataNascimento", dataNascimento);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Dados gravados com sucesso no banco de dados.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao gravar dados: {ex.Message}");
            }
        }
    }
}
