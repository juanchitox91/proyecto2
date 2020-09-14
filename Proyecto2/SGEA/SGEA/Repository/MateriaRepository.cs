using Npgsql;
using SGEA.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SGEA.Repository
{
    public class MateriaRepository
    {
        public static string connectionString = System.Configuration.ConfigurationManager.
                                     ConnectionStrings["SGEAContext"].ConnectionString;

        public static List<Materia> getMaterias(string idInstitucion)
        {
            var materias = new List<Materia>();

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $"select r.id, r.nombre_materia, r.observacion, r.idinstitucion FROM dbo.materia r  where r.idinstitucion = {idInstitucion}";
                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    materias.Add(new Materia
                    {
                        ID = Convert.ToInt64(dataReader.GetValue(0).ToString()),
                        Nombre = dataReader.GetValue(1).ToString(),
                        Observacion = dataReader.GetValue(2).ToString(),
                        InstitucionID = Convert.ToInt64(dataReader.GetValue(3).ToString())
                        //FechaAlta = dataReader.GetValue(6).ToString()
                    });
                }
                command.Dispose(); cnn.Close();
            }
            catch (Exception)
            {

            }

            return materias;
        }

        public static string createMateria(Materia materia)
        {
            string mensaje = string.Empty;

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                sql = $"insert into dbo.materia(nombre_materia, observacion, idinstitucion)" +
                      $"values ('{materia.Nombre}', '{materia.Observacion}', {materia.InstitucionID})";

                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                mensaje = "OK";
                command.Dispose(); cnn.Close();
            }

            catch (Exception e)
            {
                mensaje = "Ha ocurrido un error al insertar la materia.";
            }

            return mensaje;
        }

        public static string deleteMateria(long id)
        {
            string mensaje = string.Empty;

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                sql = $"delete from dbo.materia u where u.id = {id}";

                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                mensaje = "OK";
                command.Dispose(); cnn.Close();
            }

            catch (Exception e)
            {
                mensaje = "Ha ocurrido un error al eliminar la materia.";
            }

            return mensaje;
        }

        public static string updateMateria(Materia materia)
        {
            string mensaje = string.Empty;

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                sql = $"update dbo.materia set nombre_materia = '{materia.Nombre}', observacion = '{materia.Observacion}' "+
                    $"where id = {materia.ID}";

                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                mensaje = "OK";
                command.Dispose(); cnn.Close();
            }

            catch (Exception e)
            {
                mensaje = "Ha ocurrido un error al eliminar la materia.";
            }

            return mensaje;
        }

        public static List<SelectListItem> getMateriaSelect2(string idInstitucion, string id)
        {
            var materias = new List<SelectListItem>();

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $"select  r.id, r.nombre_materia FROM dbo.materia r where r.idinstitucion = {idInstitucion}";
                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                materias.Add(new SelectListItem
                {
                    Value = "",
                    Text = "Seleccione un valor",
                    Selected = id == "0" ? true : false
                });

                while (dataReader.Read())
                {
                    materias.Add(new SelectListItem
                    {
                        Value = dataReader.GetValue(0).ToString(),
                        Text = dataReader.GetValue(1).ToString(),
                        Selected = dataReader.GetValue(0).ToString() == id ? true : false
                    });
                };
                command.Dispose(); cnn.Close();
            }
            catch (Exception)
            {

            };
            return materias;
        }
    }
}