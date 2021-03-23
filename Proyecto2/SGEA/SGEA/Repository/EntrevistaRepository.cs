using Npgsql;
using SGEA.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SGEA.Repository
{
    public class EntrevistaRepository
    {
        public static string connectionString = System.Configuration.ConfigurationManager.
                                     ConnectionStrings["SGEAContext"].ConnectionString;

        public static List<Entrevista> getEntrevistas(string idCurso)
        {
            var entrevistas = new List<Entrevista>();

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $"select al.cedula, al.nombre || ' ' || al.apellido as nombre, e.motivo, e.fecha, e.id, cu.id, i.id, e.acuerdo, e.sugerencia  " +
                    $" from dbo.entrevista e join dbo.inscripcion i on e.idinscripcion = i.id join dbo.curso cu on i.idcurso = cu.id " +
                    $" join dbo.alumno al on i.idalumno = al.id where cu.id = {idCurso}"; 

                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    entrevistas.Add(new Entrevista
                    {
                        ID = Convert.ToInt64(dataReader.GetValue(4).ToString()),
                        Cedula = Convert.ToInt64(dataReader.GetValue(0).ToString()),
                        NombreAlumno = dataReader.GetValue(1).ToString(),
                        Motivo = dataReader.GetValue(2).ToString(),
                        FechaEntrevistaString = Convert.ToDateTime(dataReader.GetValue(3).ToString()).ToString("dd/MM/yyyy"),
                        CursoID = Convert.ToInt64(dataReader.GetValue(5).ToString()),
                        InscripcionID = Convert.ToInt64(dataReader.GetValue(6).ToString()),
                        Acuerdo = dataReader.GetValue(7).ToString(),
                        Sugerencia = dataReader.GetValue(8).ToString()
                    });
                }
                command.Dispose(); cnn.Close();
            }
            catch (Exception ex)
            {

            }

            return entrevistas;
        }

        public static List<Entrevista> getEntrevistasReporte(string idAlumno)
        {
            var entrevistas = new List<Entrevista>();

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $"select al.cedula, al.nombre || ' ' || al.apellido as nombre, e.motivo, e.fecha, e.id, cu.id, i.id, e.acuerdo, e.sugerencia  " +
                    $" from dbo.entrevista e join dbo.inscripcion i on e.idinscripcion = i.id join dbo.curso cu on i.idcurso = cu.id " +
                    $" join dbo.alumno al on i.idalumno = al.id where al.id = {idAlumno}";

                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    entrevistas.Add(new Entrevista
                    {
                        ID = Convert.ToInt64(dataReader.GetValue(4).ToString()),
                        Cedula = Convert.ToInt64(dataReader.GetValue(0).ToString()),
                        NombreAlumno = dataReader.GetValue(1).ToString(),
                        Motivo = dataReader.GetValue(2).ToString(),
                        FechaEntrevistaString = Convert.ToDateTime(dataReader.GetValue(3).ToString()).ToString("dd/MM/yyyy"),
                        CursoID = Convert.ToInt64(dataReader.GetValue(5).ToString()),
                        InscripcionID = Convert.ToInt64(dataReader.GetValue(6).ToString()),
                        Acuerdo = dataReader.GetValue(7).ToString(),
                        Sugerencia = dataReader.GetValue(8).ToString()
                    });
                }
                command.Dispose(); cnn.Close();
            }
            catch (Exception ex)
            {

            }

            return entrevistas;
        }

        public static List<SelectListItem> getAlumnosSelect2(string idInstitucion, string idcurso, string id)
        {
            var items = new List<SelectListItem>();

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                /*
                 select i.id, a.apellido || ', ' || a.nombre as nombre  from dbo.inscripcion i
                    join dbo.alumno a on i.idalumno = a.id
                    join dbo.curso c on i.idcurso = c.id
                    where c.id = 1;
                 
                 */
                sql = $"select i.id, a.apellido || ', ' || a.nombre as nombre  from dbo.inscripcion i " +
                $"join dbo.alumno a on i.idalumno = a.id " +
                $"join dbo.curso c on i.idcurso = c.id where c.id = {idcurso}";

                command = new NpgsqlCommand(sql, cnn);

                dataReader = command.ExecuteReader();
                items.Add(new SelectListItem
                {
                    Value = "",
                    Text = "Seleccione un valor",
                    Selected = id == "0" ? true : false
                });

                while (dataReader.Read())
                {
                    items.Add(new SelectListItem
                    {
                        Value = dataReader.GetValue(0).ToString(),
                        Text = dataReader.GetValue(1).ToString(),
                        Selected = dataReader.GetValue(0).ToString() == id ? true : false
                    });
                };
                command.Dispose(); cnn.Close();

            }
            catch (Exception ex)
            {

            };
            return items;
        }

        public static List<Alumno> getAlumnosReporteEntrevistas(string idcurso)
        {
            var items = new List<Alumno>();

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                /*
                 select i.id, a.apellido || ', ' || a.nombre as nombre  from dbo.inscripcion i
                    join dbo.alumno a on i.idalumno = a.id
                    join dbo.curso c on i.idcurso = c.id
                    where c.id = 1;
                 
                 */
                sql = $"select a.id,  a.cedula, a.apellido || ', ' || a.nombre as nombre  from dbo.inscripcion i " +
                $"join dbo.alumno a on i.idalumno = a.id " +
                $"join dbo.curso c on i.idcurso = c.id where c.id = {idcurso}";

                command = new NpgsqlCommand(sql, cnn);

                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    items.Add(new Alumno
                    {
                        ID = Convert.ToInt64(dataReader.GetValue(0).ToString()),
                        Cedula = Convert.ToInt64(dataReader.GetValue(1).ToString()),
                        Nombre = dataReader.GetValue(2).ToString() 
                    });
                };
                command.Dispose(); cnn.Close();
            }
            catch (Exception ex)
            {

            };
            return items;
        }

        public static string createEntrevista(Entrevista entrevista)
        {
            string mensaje = string.Empty;

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;


                sql = $"insert into dbo.entrevista(idinscripcion, motivo, acuerdo, sugerencia) " +
                      $"values ({entrevista.InscripcionID}, '{entrevista.Motivo}', '{entrevista.Acuerdo}', '{entrevista.Sugerencia}')";
                      

                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                mensaje = "OK";
                command.Dispose(); cnn.Close();
            }

            catch (Exception e)
            {
                mensaje = "Ha ocurrido un error al insertar la entrevista.";
            }

            return mensaje;
        }

        public static string deletePlanilla(long id)
        {
            string mensaje = string.Empty;

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                sql = $"delete from dbo.planilla u where u.id = {id}";

                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                mensaje = "OK";
                command.Dispose(); cnn.Close();
            }

            catch (Exception e)
            {
                mensaje = "Ha ocurrido un error al eliminar la planilla.";
            }

            return mensaje;
        }

        public static string updateEntrevista(Entrevista entrevista)
        {
            string mensaje = string.Empty;

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                sql = $"update dbo.entrevista set motivo = '{entrevista.Motivo}', acuerdo = '{entrevista.Acuerdo}', sugerencia = '{entrevista.Sugerencia}' " +
                    $" where id = {entrevista.ID}";

                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                mensaje = "OK";
                command.Dispose(); cnn.Close();
            }

            catch (Exception e)
            {
                mensaje = "Ha ocurrido un error al editar la entrevista.";
            }

            return mensaje;
        }

        public static List<SelectListItem> getPlanillasSelect2(string idInstitucion, string id)
        {
            var planillas = new List<SelectListItem>();

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $"select  r.id, r.titulo FROM dbo.planilla r where r.idinstitucion = {idInstitucion}";
                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                planillas.Add(new SelectListItem
                {
                    Value = "",
                    Text = "Seleccione un valor",
                    Selected = id == "0" ? true : false
                });

                while (dataReader.Read())
                {
                    planillas.Add(new SelectListItem
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

                //return resultado;
            };
            return planillas;
        }
    }
}