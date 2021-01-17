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

                sql = $"select al.cedula, al.nombre || ' ' || al.apellido as nombre, e.motivo, e.fecha from dbo.entrevista e " +
                    $" join dbo.inscripcion i on e.idinscripcion = i.id join dbo.curso cu on i.idcurso = cu.id " +
                    $" join dbo.alumno al on i.idalumno = al.id where cu.id = {idCurso}"; 

                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    entrevistas.Add(new Entrevista
                    {
                        Cedula = Convert.ToInt64(dataReader.GetValue(0).ToString()),
                        NombreAlumno = dataReader.GetValue(1).ToString(),
                        Motivo = dataReader.GetValue(2).ToString(),
                        FechaEntrevistaString = dataReader.GetValue(3).ToString()
                    });
                }
                command.Dispose(); cnn.Close();
            }
            catch (Exception)
            {

            }

            return entrevistas;
        }

        public static string createPlanilla(Planilla planilla)
        {
            string mensaje = string.Empty;

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;
                planilla.Estado = "A";
                int anho = Convert.ToInt32(planilla.AnhoLectivo.Replace(".", string.Empty));

                sql = $"insert into dbo.planilla(idcurso, idmateria, iddocente, estado, anho_lectivo, idinstitucion, titulo, descripcion)" +
                      $"values ({planilla.CursoID}, {planilla.MateriaID}, {planilla.DocenteID}, '{planilla.Estado}', " +
                      $"{anho}, {planilla.InstitucionID}, '{planilla.Titulo}', '{planilla.Descripcion}')";

                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                mensaje = "OK";
                command.Dispose(); cnn.Close();
            }

            catch (Exception e)
            {
                mensaje = "Ha ocurrido un error al insertar la planilla.";
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

        public static string updatePlanilla(Planilla planilla)
        {
            string mensaje = string.Empty;

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;
                int anho = Convert.ToInt32(planilla.AnhoLectivo.Replace(".", string.Empty));
                planilla.Estado = planilla.Estado != "I" || planilla.Estado != "A" ? "A" : planilla.Estado;

                sql = $"update dbo.planilla set idcurso = {planilla.CursoID}, idmateria = {planilla.MateriaID}, iddocente = {planilla.DocenteID}, " +
                    $"estado = '{planilla.Estado}', anho_lectivo = {anho}, idinstitucion = {planilla.InstitucionID}, " +
                    $"titulo = '{planilla.Titulo}', descripcion = '{planilla.Descripcion}' where id = {planilla.ID}";

                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                mensaje = "OK";
                command.Dispose(); cnn.Close();
            }

            catch (Exception e)
            {
                mensaje = "Ha ocurrido un error al editar la planilla.";
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