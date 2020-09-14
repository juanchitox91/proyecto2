using Helper;
using Npgsql;
//using SGEA.DAL;
using SGEA.Models;
using System;
using System.Linq;

namespace SGEA.Repository
{
    public class LoginRepository
    {
        public static string connectionString = System.Configuration.ConfigurationManager.
                             ConnectionStrings["SGEAContext"].ConnectionString;

        public static Usuario Acceder(string Email, string Password)
        {
            var user = new Usuario();
            Password = HashHelper.MD5(Password);

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $"Select d.id, d.idinstitucion, d.nombre, d.apellido, d.idrol from dbo.usuario d where d.email = '{Email}' and d.contrasenha = '{Password}'";
                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read()) 
                {
                    string idusuario = dataReader.GetValue(0).ToString();
                    string institucion = dataReader.GetValue(1).ToString();
                    string nombre = dataReader.GetValue(2).ToString();
                    string apellido = dataReader.GetValue(3).ToString();
                    string rol = dataReader.GetValue(4).ToString();

                    user.ID = long.Parse(idusuario);
                    user.IDInstitucion = long.Parse(institucion);
                    user.Nombre = nombre;
                    user.Apellido = apellido;
                    user.IDRol = long.Parse(rol);

                    SessionHelper.AddUserToSession(user, institucion);

                }
                command.Dispose(); cnn.Close();
            }
            catch (Exception e)
            {

            }

            return user;
        }
    }
}