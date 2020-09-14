using Npgsql;
using SGEA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SGEA.Repository
{
    public class TutorRepository
    {
        public static string connectionString = System.Configuration.ConfigurationManager.
                                     ConnectionStrings["SGEAContext"].ConnectionString;

        public static List<Tutor> getTutores(string idInstitucion)
        {
            var Tutores = new List<Tutor>();

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $"select a.id, a.cedula, a.nombre, a.apellido, a.telefono,  a.telefono_2, " +
                    $"a.direccion, a.observacion, a.fecha_alta, a.ult_modificacion from dbo.Tutor a where a.idinstitucion = {idInstitucion}";
                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Tutores.Add(new Tutor
                    {
                        ID = Convert.ToInt64(dataReader.GetValue(0).ToString()),
                        Cedula = Convert.ToInt64(dataReader.GetValue(1).ToString()),
                        Nombre = dataReader.GetValue(2).ToString(),
                        Apellido = dataReader.GetValue(3).ToString(),
                        Telefono = dataReader.GetValue(4).ToString(),
                        Telefono2 = dataReader.GetValue(5).ToString(),        
                        Direccion = dataReader.GetValue(6).ToString(),
                        Observacion = dataReader.GetValue(7).ToString(),
                        FechaAlta = dataReader.GetValue(8).ToString(),
                        UltModificacion = dataReader.GetValue(9).ToString(),
                    });
                }
                command.Dispose(); cnn.Close();
                cnn.Close();
            }
            catch (Exception e)
            {
                throw;
            }

            return Tutores;
        }

        public static string createTutor(Tutor Tutor)
        {
            string mensaje = string.Empty;

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                sql = $"insert into dbo.Tutor(cedula, nombre, apellido, telefono, telefono_2," +
                      $"direccion, observacion, fecha_alta, ult_modificacion, idinstitucion)" +
                      $"values ({Tutor.Cedula}, '{Tutor.Nombre}', '{Tutor.Apellido}', '{Tutor.Telefono}', '{Tutor.Telefono2}', " +
                      $"'{Tutor.Direccion}', '{Tutor.Observacion}', now(), now(), {Tutor.InstitucionID})";

                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                mensaje = "OK";
                command.Dispose(); cnn.Close();
            }

            catch (Exception e)
            {

                mensaje = GeneralRepository.obtenerMensajeExcepcion(e, "tutor", "insertar"); //"Ha ocurrido un error al insertar el Tutor.";
            }

            return mensaje;
        }

        public static string deleteTutor(long id)
        {
            string mensaje = string.Empty;

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                sql = $"delete from dbo.Tutor u where u.id = {id}";

                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                mensaje = "OK";
                command.Dispose(); cnn.Close();
            }

            catch (Exception e)
            {
                mensaje = "Ha ocurrido un error al eliminar el Tutor.";
            }

            return mensaje;
        }

        public static string updateTutor(Tutor Tutor)
        {
            string mensaje = string.Empty;

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                sql = $"update dbo.Tutor set " +
                   $"cedula = {Tutor.Cedula}, nombre = '{Tutor.Nombre}', apellido ='{Tutor.Apellido}', telefono = '{Tutor.Telefono}',  telefono_2 = '{Tutor.Telefono2}', " +
                   $"direccion = '{Tutor.Direccion}', observacion = '{Tutor.Observacion}', ult_modificacion = now() " +
                   $"where id = {Tutor.ID}";


                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                mensaje = "OK";
                command.Dispose(); cnn.Close();
            }

            catch (Exception e)
            {
                mensaje = "Ha ocurrido un error al actualizar el tutor.";
            }

            return mensaje;
        }

    }
}