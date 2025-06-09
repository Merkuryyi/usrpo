using Npgsql;
using System;

public class Program
{
    
    public static NpgsqlConnection GetSqlConnection()
    {
        NpgsqlConnection conn =
            new NpgsqlConnection("Host=localhost;Username=postgres;Password=6645;Database=test");
        conn.Open();
        return conn;
    }
    public static void Main(string[] args)
    {
        try
        {
            using (var conn = GetSqlConnection())
            {
                string query = @"SELECT * FROM text";
                using (var command = new NpgsqlCommand(query, conn))
                using (var reader = command.ExecuteReader()) 
                {
                    Console.WriteLine("Содержимое таблицы:");
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write($"{reader.GetName(i),-15}");
                    }
                    Console.WriteLine("\n" + new string('-', 15 * reader.FieldCount));

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write($"{reader[i].ToString(),-15}");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при авторизации пользователя: {ex.Message}");
        }
    }
}