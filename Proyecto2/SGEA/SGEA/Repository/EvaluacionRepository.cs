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

        public static void insertPuntaje(string idItem, string  cedula, string puntaje)
        {
            long idalumno = 0;
            int cantidad = 0;
            try
            {
                NpgsqlConnection cnn;
                NpgsqlDataReader dataReader;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                sql = $"select i.id FROM dbo.alumno i where i.cedula = {cedula}";
                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    idalumno = Convert.ToInt64(dataReader.GetValue(0).ToString());
                }
                command.Dispose(); cnn.Close();

                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                sql = $"select count(*) from dbo.evaluacion where iditem = {idItem} and idalumno = {idalumno} ";
                     

                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    cantidad = Convert.ToInt32(dataReader.GetValue(0).ToString());
                }

                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                if(cantidad == 0)
                {
                    sql = $"insert into dbo.evaluacion(iditem, idalumno, puntajealcanzado) " +
                    $"values ({idItem}, {idalumno}, {puntaje})";
                }
                else
                {
                    sql = $"update dbo.evaluacion set puntaje = {puntaje} where iditem = {idItem} and idalumno = {idalumno}";
                }

                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                command.Dispose(); cnn.Close();

            }
            catch (Exception e)
            {
                //mensaje = "Ha ocurrido un error al insertar la inscripcion.";
            }

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

                sql = $"select a.cedula, a.nombre || ' ' || a.apellido, " +
                    $" COALESCE(e.puntajealcanzado, 0) as puntaje" +
                    $" from dbo.planilla p join dbo.curso c on p.idcurso = c.id " +
                    $" join dbo.inscripcion i on c.id = i.idcurso " +
                    $" join dbo.alumno a on i.idalumno = a.id join dbo.unidad u on p.id = u.idplanilla " +
                    $" join dbo.subunidad su on u.id = su.idunidad join dbo.itemplanilla it on su.id = it.idsubunidad " +
                    $" full join dbo.evaluacion e on it.id = e.iditem and a.id = e.idalumno " +
                    $" where it.id = {iditemplanilla}";

                command = new NpgsqlCommand(sql, cnn);

                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    evaluacion.Add(new Evaluacion
                    {
                        Cedula = dataReader.GetValue(0).ToString(),
                        NombreAlumno = dataReader.GetValue(1).ToString(),
                        PuntajeAlcanzado = Convert.ToInt64(dataReader.GetValue(2).ToString())
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

        public static List<Asistencia> getAlumnosAsistencia(string idplanilla, string fecha)
        {
            List<Asistencia> alumnosAsistencia = new List<Asistencia>();

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                /*
                 select * from asistencia where idplanilla = 12 and fecha = '28/03/2021';
                 */

                sql = $" select tabla.*, coalesce(asis.presente, false) from ( select al.id as idalumno, al.cedula, al.apellido || ', ' || al.nombre as nombre, " +
                    $" pl.id as idplanilla, pl.descripcion from dbo.planilla pl join dbo.curso cur on pl.idcurso = cur.id " +
                    $" join dbo.inscripcion ins on cur.id = ins.idcurso join dbo.alumno al on ins.idalumno = al.id where pl.id = {idplanilla}) tabla" +
                    $" left join dbo.asistencia asis on asis.idalumno = tabla.idalumno and asis.idplanilla = tabla.idplanilla and tabla.idplanilla = {idplanilla} and asis.fecha = '{fecha}' ";

                command = new NpgsqlCommand(sql, cnn);

                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    var input = dataReader.GetValue(5).ToString();
                    alumnosAsistencia.Add(new Asistencia
                    {
                        AlumnoID = Convert.ToInt64(dataReader.GetValue(0).ToString()),
                        Cedula = dataReader.GetValue(1).ToString(),
                        NombreAlumno = dataReader.GetValue(2).ToString(),
                        PlanillaID = Convert.ToInt64(dataReader.GetValue(3).ToString()),
                        NombrePlanilla = dataReader.GetValue(4).ToString(),
                        Presente = Convert.ToBoolean(dataReader.GetValue(5).ToString()),
                        Fecha = fecha
                    });
                };
                command.Dispose(); cnn.Close();

                return alumnosAsistencia;
            }
            catch (Exception ex)
            {

            }

            return alumnosAsistencia;
        }

        public static string insertAsistencia(List<Asistencia> listaAsistencias, string idinstitucion)
        {
            string mensaje = "OK";
            foreach(var item in listaAsistencias)
            {
                string sqlExist = $"select count(*) from dbo.asistencia t where t.idalumno = {item.AlumnoID} and t.idplanilla = {item.PlanillaID}";
                string sqlUpdate = $"update dbo.asistencia set presente = {item.Presente} where idalumno = {item.AlumnoID} and idplanilla = {item.PlanillaID}";
                string sqlInsert = $"insert into dbo.asistencia(idalumno, idplanilla, fecha, presente, idinstitucion) values ({item.AlumnoID}, " +
                    $"{item.PlanillaID}, TO_DATE('{item.Fecha}', 'DD/MM/YYYY'), {item.Presente}, {idinstitucion})";
                bool existe = false;

                try
                {

                    NpgsqlConnection cnn;
                    cnn = new NpgsqlConnection(connectionString);
                    cnn.Open();

                    NpgsqlCommand command;
                    NpgsqlDataReader dataReader;
                    string Output = string.Empty;

                    //vamos a controlar si existe la asistencia en tabla
                    command = new NpgsqlCommand(sqlExist, cnn);

                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        existe = Convert.ToInt32(dataReader.GetValue(0).ToString()) > 0;  
                    };
                    command.Dispose(); cnn.Close();

                    cnn = new NpgsqlConnection(connectionString);
                    cnn.Open();

                    //si existe, hacemos el update correspondiente, si no se inserta en tabla
                    command = new NpgsqlCommand(existe ? sqlUpdate : sqlInsert, cnn);

                    command.ExecuteNonQuery();
                    command.Dispose(); cnn.Close();
                }
                catch (Exception ex)
                {
                    mensaje = "ERROR";
                }
               
            }

            return mensaje;
        }


        /*
         select tabla.*, coalesce(ev.puntajealcanzado, 0) from (
        select pl.titulo as planilla, pl.descripcion as planilla_descripcion, 
        un.titulo as unidad_titulo, su.titulo as subunidad_titulo,
        it.titulo as titulo_item, it.descripcion as descr_item, it.puntajemaximo,
        it.id as iditemplanilla
        from dbo.planilla pl join dbo.unidad un on pl.id = un.idplanilla
        join dbo.subunidad su on un.id = su.idunidad
        join dbo.itemplanilla it on su.id = it.idsubunidad) tabla
        left join dbo.evaluacion ev on tabla.iditemplanilla = ev.iditem
                 */
    }
}