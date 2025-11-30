using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

public static class BD
{
    public static string cadena = "server=localhost; database=turnera; uid=root; pwd=;";

    public static void Ejecutar(string sql)
    {
        MySqlConnection con = new MySqlConnection(cadena);
        con.Open();

        MySqlCommand cmd = new MySqlCommand(sql, con);
        cmd.ExecuteNonQuery();

        con.Close();
    }

    public static DataTable Consultar(string sql)
    {
        MySqlConnection con = new MySqlConnection(cadena);
        con.Open();

        MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        con.Close();
        return dt;
    }
}
