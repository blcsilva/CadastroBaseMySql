using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        // Configurar o banco de dados (criar tabelas se não existirem)
        BancoDeDados.ConfigurarBancoDeDados();

        // Declaração das variáveis
        string nome;
        int idade;
        char sexo;
        string rg;
        string cpf;
        string cidade;
        string cep;
        DateTime dataNascimento;
        string email;
        string telefone;
        string estado;
        string pais;

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
        email = LerEmail();
        telefone = LerTelefone();
        estado = LerEstado();
        pais = LerPais();

        // Criação da string concatenada com quebras de linha
        string resultado =
            $"Nome: {nome}\n" +
            $"Idade: {idade}\n" +
            $"Sexo: {sexo}\n" +
            $"RG: {rg}\n" +
            $"CPF: {cpf}\n" +
            $"Cidade: {cidade}\n" +
            $"CEP: {cep}\n" +
            $"Data de Nascimento: {dataNascimento:dd/MM/yyyy}\n" +
            $"Email: {email}\n" +
            $"Telefone: {telefone}\n" +
            $"Estado: {estado}\n" +
            $"Pais: {pais}";

        // Exibição do resultado
        Console.WriteLine(resultado);

        // Gravação dos dados no banco de dados
        BancoDeDados.GravarDados(nome, idade, sexo, rg, cpf, cidade, cep, dataNascimento, email, telefone, estado, pais);
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

    static string LerEmail()
    {
        while (true)
        {
            Console.Write("Digite seu e-mail: ");
            string email = Console.ReadLine();
            if (Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return email;
            }
            else
            {
                Console.WriteLine("E-mail inválido. Por favor, insira um e-mail válido.");
            }
        }
    }

    static string LerTelefone()
    {
        while (true)
        {
            Console.Write("Digite seu telefone (somente números e formato (00) 00000-0000): ");
            string telefone = Console.ReadLine();
            if (Regex.IsMatch(telefone, @"^\d{2} \d{5}-\d{4}$"))
            {
                return AplicarMascaraTelefone(telefone);
            }
            else
            {
                Console.WriteLine("Telefone inválido. Por favor, use o formato (00) 00000-0000.");
            }
        }
    }

    static string LerEstado()
    {
        while (true)
        {
            Console.Write("Digite seu estado: ");
            string estado = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(estado))
            {
                return estado;
            }
            else
            {
                Console.WriteLine("Estado inválido. Por favor, insira um estado válido.");
            }
        }
    }

    static string LerPais()
    {
        while (true)
        {
            Console.Write("Digite seu país: ");
            string pais = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(pais))
            {
                return pais;
            }
            else
            {
                Console.WriteLine("País inválido. Por favor, insira um país válido.");
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

    static string AplicarMascaraTelefone(string telefone)
    {
        // Assumindo o formato (00) 00000-0000
        return $"({telefone.Substring(0, 2)}) {telefone.Substring(2, 5)}-{telefone.Substring(7, 4)}";
    }
}
