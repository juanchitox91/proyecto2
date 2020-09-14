using Helper;
using Npgsql;
//using SGEA.DAL;
using SGEA.Models;
using System;
using System.Linq;

namespace SGEA.Repository
{
    public class HomeRepository
    {
        public static string connectionString = System.Configuration.ConfigurationManager.
                             ConnectionStrings["SGEAContext"].ConnectionString;

        public static Usuario getUsuario(string id)
        {
            var user = new Usuario();

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;

                string sql, Output = string.Empty;


                sql = $"select u.id, u.nombre, u.apellido, u.email, u.idinstitucion, u.idrol from dbo.usuario u " +
                    $"where u.id = {id};";

                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    user.ID = Convert.ToInt16(dataReader.GetValue(0).ToString());
                    user.Nombre = dataReader.GetValue(1).ToString();
                    user.Apellido = dataReader.GetValue(2).ToString();
                    user.Email = dataReader.GetValue(3).ToString();
                    user.IDRol = Convert.ToInt64(dataReader.GetValue(5).ToString());
                    user.IDInstitucion = Convert.ToInt64(dataReader.GetValue(4).ToString());
                }
                command.Dispose(); cnn.Close();
            }
            catch (Exception)
            {

            }

            return user;
        }

        public static bool getPermiso(string id, string permiso)
        {
            int suma = 0;
            try
            {
                 
                    NpgsqlConnection cnn;
                    cnn = new NpgsqlConnection(connectionString);
                    cnn.Open();

                    NpgsqlCommand command;
                    NpgsqlDataReader dataReader;

                string sql, Output = string.Empty;
               
                /*
                 select count(*)  from 
                    dbo.accion a join dbo.accionrol b on a.id = b.idaccion
                    join dbo.rol c on b.idrol = c.id
                    join dbo.usuario u on c.id = u.idrol
                    where (a.nombre = 'verUsuarios'
                    and u.id = 10 )
                    or (select count(1) 
	                    from dbo.rol r join dbo.usuario u on r.id = u.idrol
	                    where u.id = 10 and r.esadministrador = 1) > 0
                 */

                sql = $"select count(*) from dbo.accion a join dbo.accionrol b on a.id = b.idaccion join dbo.rol c on b.idrol = c.id " +
                    $"join dbo.usuario u on c.id = u.idrol where (a.nombre = '{permiso}' and u.id = {id}) " +
                    $" or (select count(1) from dbo.rol r join dbo.usuario u on r.id = u.idrol where u.id = {id} and r.esadministrador = 1) > 0";
                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    suma = Convert.ToInt16(dataReader.GetValue(0).ToString());
                }

                command.Dispose(); cnn.Close();

                if(suma > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception e)
            {
                return false;
            }

            
        }
    }
}