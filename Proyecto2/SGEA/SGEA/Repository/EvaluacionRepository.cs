using Npgsql;
using SGEA.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SGEA.Repository
{
    public class EvaluacionRepository
    {
        public static string connectionString = System.Configuration.ConfigurationManager.
                                     ConnectionStrings["SGEAContext"].ConnectionString;


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

                sql = $"select r.id, r.titulo, 'Año ' || r.anho_lectivo, c.nombrecurso, m.nombre_materia, d.apellido || ', ' || d.nombre as docente" +
                      $"  FROM dbo.planilla r join dbo.curso c on r.idcurso = c.id " +
                      $"join dbo.materia m on r.idmateria = m.id join dbo.docente d on r.iddocente = d.id where r.idinstitucion = {idInstitucion}";
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
                        Text = dataReader.GetValue(1).ToString() + " - " + dataReader.GetValue(2).ToString() + " | " + dataReader.GetValue(3).ToString() + " | " + dataReader.GetValue(4).ToString() + " | " + dataReader.GetValue(5).ToString(),
                        Selected = dataReader.GetValue(0).ToString() == id ? true : false
                    });
                };
                command.Dispose(); cnn.Close(); ;
            }
            catch (Exception)
            {

            };
            return planillas;
        }

        public static List<SelectListItem> getItemsSelect2(string idInstitucion, string idplanilla, string id)
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

                sql = $"select i.id, i.descripcion, u.titulo, s.titulo from dbo.itemplanilla i join dbo.subunidad s on i.idsubunidad = s.id " +
                $"join dbo.unidad u on s.idunidad = u.id join dbo.planilla p on u.idplanilla = p.id " +
                $"where p.id = {idplanilla}";
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
                        Text = dataReader.GetValue(1).ToString() + " | " + dataReader.GetValue(2).ToString() + " | " + dataReader.GetValue(3).ToString(),
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

        public static int getPuntajeMaximo(string idItem)
        {
            int puntajeMaximo = 0;

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $"select i.puntajemaximo from dbo.itemplanilla i " +
                $"where i.id = {idItem}";
                command = new NpgsqlCommand(sql, cnn);


                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    puntajeMaximo = Convert.ToInt32(dataReader.GetValue(0).ToString());
                };
                command.Dispose(); cnn.Close();

            }
            catch (Exception ex)
            {

            };

            return puntajeMaximo;
        }

        public static List<Evaluacion> getAlumnosEvaluar(string iditemplanilla)
        {
            List<Evaluacion> evaluacion = new List<Evaluacion>();

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $"select al.cedula || ' - '  || al.apellido || ', ' || al.nombre alumno " +
                    "from dbo.itemplanilla i join dbo.subunidad s on i.idsubunidad = s.id " +
                    "join dbo.unidad u on s.idunidad = u.id join dbo.planilla p on u.idplanilla = p.id " +
                    "join dbo.curso c on p.idcurso = c.id join dbo.inscripcion ins on c.id = ins.idcurso " +
                    "join dbo.alumno al on ins.idalumno = al.id " +
                    $"where i.id = {iditemplanilla}";

                command = new NpgsqlCommand(sql, cnn);


                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    evaluacion.Add(new Evaluacion
                    {
                        NombreAlumno = dataReader.GetValue(0).ToString(),
                        PuntajeAlcanzado = 0
                    });
                };
                command.Dispose(); cnn.Close();

                return evaluacion;
            }
            catch (Exception ex)
            {

            }

            return evaluacion;
        }
    }
}