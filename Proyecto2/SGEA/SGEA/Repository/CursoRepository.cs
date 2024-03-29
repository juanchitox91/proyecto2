﻿using Npgsql;
using SGEA.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SGEA.Repository
{
    public class CursoRepository
    {
        public static string connectionString = System.Configuration.ConfigurationManager.
                                     ConnectionStrings["SGEAContext"].ConnectionString;

        public static List<Curso> getCursos(string idInstitucion)
        {
            var cursos = new List<Curso>();

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $"select r.id, r.curso, r.seccion, r.turno, r.nombrecurso, r.observacion FROM dbo.curso r join dbo.institucion u on r.idinstitucion = u.id where r.idinstitucion = {idInstitucion}";
                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    cursos.Add(new Curso
                    {
                        ID = Convert.ToInt64(dataReader.GetValue(0).ToString()),
                        _Curso = Convert.ToInt16(dataReader.GetValue(1).ToString()),
                        Seccion = dataReader.GetValue(2).ToString(),
                        //Enum.Parse(typeof(TestAppAreana.MovieList.Movies), KeyVal)
                        Turno = (Curso.Turnos)Enum.Parse(typeof(Curso.Turnos), dataReader.GetValue(3).ToString()),
                        NombreCurso = dataReader.GetValue(4).ToString(),
                        Observacion = dataReader.GetValue(5).ToString()
                        //FechaAlta = dataReader.GetValue(6).ToString()
                    });
                }
                command.Dispose(); cnn.Close();;
            }
            catch (Exception)
            {

                throw;
            }

            return cursos;
        }

        public static string createCurso(Curso curso)
        {
            string mensaje = string.Empty;

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                sql = $"insert into dbo.curso(curso, seccion, turno, nombrecurso, observacion, idinstitucion)" +
                      $"values ({curso._Curso}, '{curso.Seccion}', '{curso.Turno}', '{curso.NombreCurso}', '{curso.Observacion}', {curso.InstitucionID})";

                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                mensaje = "OK";
                command.Dispose(); cnn.Close();;
            }

            catch (Exception e)
            {
                mensaje = "Ha ocurrido un error al insertar el curso.";
            }

            return mensaje;
        }

        public static string deleteCurso(long id)
        {
            string mensaje = string.Empty;

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                sql = $"delete from dbo.curso u where u.id = {id}";

                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                mensaje = "OK";
                command.Dispose(); cnn.Close();;
            }

            catch (Exception e)
            {
                mensaje = "Ha ocurrido un error al eliminar el curso.";
            }

            return mensaje;
        }

        public static string updateCurso(Curso curso)
        {
            string mensaje = string.Empty;

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                sql = $"update dbo.curso set curso = {curso._Curso}, seccion = '{curso.Seccion}', turno = '{curso.Turno}', nombrecurso = '{curso.NombreCurso}', " +
                    $"observacion = '{curso.Observacion}' where id = {curso.ID}";

                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                mensaje = "OK";
                command.Dispose(); cnn.Close();;
            }

            catch (Exception e)
            {
                mensaje = "Ha ocurrido un error al insertar el curso.";
            }

            return mensaje;
        }

        public static List<SelectListItem> getCursosSelect2(string idInstitucion, string id)
        {
            var cursos = new List<SelectListItem>();

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $"select  r.id, r.nombrecurso FROM dbo.curso r where r.idinstitucion = {idInstitucion}";
                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                cursos.Add(new SelectListItem
                {
                    Value = "",
                    Text = "Seleccione un valor",
                    Selected = id == "0" ? true : false
                });

                while (dataReader.Read())
                {
                    cursos.Add(new SelectListItem
                    {
                        Value = dataReader.GetValue(0).ToString(),
                        Text = dataReader.GetValue(1).ToString(),
                        Selected = dataReader.GetValue(0).ToString() == id ? true : false
                    });
                };
                command.Dispose(); cnn.Close();;
            }
            catch (Exception)
            {

            };
            return cursos;
        }

        public static List<SelectListItem> getCursosSelect2WithAll(string idInstitucion)
        {
            var cursos = new List<SelectListItem>();

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $"select  r.id, r.nombrecurso FROM dbo.curso r where r.idinstitucion = {idInstitucion}";
                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                cursos.Add(new SelectListItem
                {
                    Value = "",
                    Text = "Seleccione un valor",
                    Selected = true 
                });

                cursos.Add(new SelectListItem
                {
                    Value = "0",
                    Text = "Todos los cursos",
                });

                while (dataReader.Read())
                {
                    cursos.Add(new SelectListItem
                    {
                        Value = dataReader.GetValue(0).ToString(),
                        Text = dataReader.GetValue(1).ToString(),
                    });
                };
                command.Dispose(); cnn.Close(); ;
            }
            catch (Exception)
            {

            };
            return cursos;
        }

        public static List<SelectListItem> getTurnosSelect2(string idInstitucion, string id)
        {
            var items = new List<SelectListItem>();

            try
            {
                items.Add(new SelectListItem
                {
                    Value = "",
                    Text = "Seleccione un valor",
                    Selected = id == "0" ? true : false
                });

                Array values = Enum.GetValues(typeof(Curso.Turnos));
               
                foreach (var i in values)
                {
                    items.Add(new SelectListItem
                    {
                        Value = ((int)i).ToString(),
                        Text = Enum.GetName(typeof(Curso.Turnos), i),
                        Selected = ((int)i).ToString() == id ? true : false
                    });
                     
                }                      
            }
            catch (Exception)
            {

            };
            return items;
        }
    }
}