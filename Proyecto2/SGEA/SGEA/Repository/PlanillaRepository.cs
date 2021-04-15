using Npgsql;
using SGEA.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SGEA.Repository
{
    public class PlanillaRepository
    {
        public static string connectionString = System.Configuration.ConfigurationManager.
                                     ConnectionStrings["SGEAContext"].ConnectionString;

        public static List<Planilla> getPlanillas(string idInstitucion)
        {
            var planillas = new List<Planilla>();

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $"select r.id, r.idcurso, r.idmateria, r.iddocente, r.estado, r.anho_lectivo, r.idinstitucion, r.titulo, " +
                    $"r.descripcion, c.nombrecurso, m.nombre_materia, d.apellido || ', ' || d.nombre as nombre_docente " +
                    $" FROM dbo.planilla r join dbo.curso c on r.idcurso = c.id join dbo.materia m on r.idmateria = m.id " +
                    $"join dbo.docente d on r.iddocente = d.id where r.idinstitucion = {idInstitucion}";

                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    planillas.Add(new Planilla
                    {
                        ID = Convert.ToInt64(dataReader.GetValue(0).ToString()),
                        CursoID = Convert.ToInt64(dataReader.GetValue(1).ToString()),
                        MateriaID = Convert.ToInt64(dataReader.GetValue(2).ToString()),
                        DocenteID = Convert.ToInt64(dataReader.GetValue(3).ToString()),
                        Estado = dataReader.GetValue(4).ToString(),
                        AnhoLectivo = Convert.ToInt64(dataReader.GetValue(5).ToString()).ToString("#,###").Replace(",", "."),
                        InstitucionID = Convert.ToInt64(dataReader.GetValue(6).ToString()),
                        Titulo = dataReader.GetValue(7).ToString(),
                        Descripcion = dataReader.GetValue(8).ToString(),
                        NombreCurso = dataReader.GetValue(9).ToString(),
                        NombreMateria = dataReader.GetValue(10).ToString(),
                        NombreDocente = dataReader.GetValue(11).ToString(),

                    });
                }
                command.Dispose(); cnn.Close();
            }
            catch (Exception)
            {

            }

            return planillas;
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

        public static List<ProgramaEstudio> getProgramaEstudio(string idPlanilla)
        {
            var progrEstudio = new List<ProgramaEstudio>();

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                /*
                 select pl.id, pl.titulo as planilla, pl.descripcion as planilla_descripcion, 
                un.id as unidadid, un.titulo as unidad_titulo, su.id as subunidadid,
                su.titulo as subunidad_titulo, it.id as itemid,
                it.titulo as titulo_item, it.descripcion as descr_item, it.puntajemaximo
                    from dbo.planilla pl
                    left join dbo.unidad un on pl.id = un.idplanilla
                    left join dbo.subunidad su on un.id = su.idunidad
                    left join dbo.itemplanilla it on su.id = it.idsubunidad
                    where pl.id = 13
                 */

                sql = $"select pl.id, pl.titulo as planilla, pl.descripcion as planilla_descripcion, " +
                    $" un.id as unidadid, un.titulo as unidad_titulo, su.id as subunidadid, " +
                    $" su.titulo as subunidad_titulo, it.id as itemid, " +
                    $" it.titulo as titulo_item, it.descripcion as descr_item, it.puntajemaximo, cu.id, cu.nombrecurso  " +
                    $" from dbo.planilla pl join dbo.curso cu on pl.idcurso = cu.id left join dbo.unidad un on pl.id = un.idplanilla" +
                    $" left join dbo.subunidad su on un.id = su.idunidad " +
                    $" left join dbo.itemplanilla it on su.id = it.idsubunidad where pl.id = {idPlanilla}";

                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    progrEstudio.Add(new ProgramaEstudio
                    {
                        PlanillaID = Convert.ToInt64(dataReader.GetValue(0).ToString()),
                        TituloPlanilla = dataReader.GetValue(1).ToString(),
                        DescripcionPlanilla = dataReader.GetValue(2).ToString(),
                        UnidadID = Convert.ToInt64(string.IsNullOrEmpty(dataReader.GetValue(3).ToString()) ? "0" : dataReader.GetValue(3).ToString()),
                        NombreUnidad = string.IsNullOrEmpty(dataReader.GetValue(4).ToString()) ? "No existe unidades para esta planilla" : dataReader.GetValue(4).ToString(),
                        SubUnidadID = Convert.ToInt64(string.IsNullOrEmpty(dataReader.GetValue(5).ToString()) ? "0" : dataReader.GetValue(5).ToString()),
                        NombreSubUnidad = string.IsNullOrEmpty(dataReader.GetValue(6).ToString()) ? "No existe subunidades para esta planilla" : dataReader.GetValue(6).ToString(),
                        ItemID = Convert.ToInt64(string.IsNullOrEmpty(dataReader.GetValue(7).ToString()) ? "0" : dataReader.GetValue(7).ToString()),
                        NombreItem = dataReader.GetValue(8).ToString(),
                        DescripcionItem = string.IsNullOrEmpty(dataReader.GetValue(9).ToString()) ? "No existe items para esta planilla" : dataReader.GetValue(9).ToString(),
                        PuntajeMaximo = Convert.ToInt64(string.IsNullOrEmpty(dataReader.GetValue(10).ToString()) ? "0" : dataReader.GetValue(10).ToString()),
                        CursoID = Convert.ToInt64(dataReader.GetValue(11).ToString()),
                        NombreCurso = dataReader.GetValue(12).ToString(),
                    });
                };
                command.Dispose(); cnn.Close();
            }
            catch (Exception ex)
            {

                //return resultado;
            };
            return progrEstudio;
        }


        /*
         select pl.id, pl.titulo as planilla, pl.descripcion as planilla_descripcion, 
        un.id as unidadid, un.titulo as unidad_titulo, su.id as subunidadid,
        su.titulo as subunidad_titulo, it.id as itemid,
        it.titulo as titulo_item, it.descripcion as descr_item, it.puntajemaximo
            from dbo.planilla pl
            left join dbo.unidad un on pl.id = un.idplanilla
            left join dbo.subunidad su on un.id = su.idunidad
            left join dbo.itemplanilla it on su.id = it.idsubunidad
            where pl.id = 13
         */
    }
}