using MySql.Data.MySqlClient;

public static class BancoDeDados
{
    private static readonly string ConnectionString = "Server=localhost;Database=cadastro;User ID=administrador;Password=6SPst7um-UdbDzk[;";

    public static void ConfigurarBancoDeDados()
    {
        string sql = @"
            CREATE TABLE IF NOT EXISTS Pessoas (
                Id INT AUTO_INCREMENT PRIMARY KEY,
                Nome VARCHAR(100) NOT NULL,
                Idade INT NOT NULL,
                Sexo CHAR(1) NOT NULL,
                RG VARCHAR(20) NOT NULL,
                CPF VARCHAR(14) NOT NULL,
                Cidade VARCHAR(100) NOT NULL,
                CEP VARCHAR(10) NOT NULL,
                DataNascimento DATE NOT NULL,
                Email VARCHAR(100) NOT NULL,
                Telefone VARCHAR(15) NOT NULL,
                Estado VARCHAR(50) NOT NULL,
                Pais VARCHAR(50) NOT NULL
            );";

        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            MySqlCommand command = new MySqlCommand(sql, connection);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Tabela configurada com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao configurar o banco de dados: {ex.Message}");
            }
        }
    }

    public static void GravarDados(string nome, int idade, char sexo, string rg, string cpf, string cidade, string cep, DateTime dataNascimento, string email, string telefone, string estado, string pais)
    {
        string sql = @"INSERT INTO Pessoas (Nome, Idade, Sexo, RG, CPF, Cidade, CEP, DataNascimento, Email, Telefone, Estado, Pais) 
                        VALUES (@Nome, @Idade, @Sexo, @RG, @CPF, @Cidade, @CEP, @DataNascimento, @Email, @Telefone, @Estado, @Pais)";

        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
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
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Telefone", telefone);
            command.Parameters.AddWithValue("@Estado", estado);
            command.Parameters.AddWithValue("@Pais", pais);

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
