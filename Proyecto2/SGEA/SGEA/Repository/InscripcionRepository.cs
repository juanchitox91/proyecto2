using Helper;
using Npgsql;
using SGEA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SGEA.Repository
{
    public class InscripcionRepository
    {
        public static string connectionString = System.Configuration.ConfigurationManager.
                                     ConnectionStrings["SGEAContext"].ConnectionString;

        public static List<Inscripcion> getInscripciones(string idInstitucion)
        {
            var inscripciones = new List<Inscripcion>();

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $"select i.id, a.id, a.nombre || ' ' || a.apellido as nombrealumno, c.id, c.nombrecurso, ar.id, ar.nombre_arancel, " +
                    $"i.anho, i.mesdesde, i.meshasta, i.fecha_inscripcion, i.estado, i.cantidad_cuotas, i.idinstitucion, i.nrocomprobante, a.cedula " +
                    $"FROM dbo.inscripcion i join dbo.alumno a on i.idalumno= a.id " +
                    $"join dbo.curso c on i.idcurso = c.id join dbo.arancel ar on i.idarancel = ar.id " +
                    $"where i.idinstitucion = {idInstitucion}";
                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    inscripciones.Add(new Inscripcion
                    {
                        ID = Convert.ToInt64(dataReader.GetValue(0).ToString()),
                        AlumnoID = Convert.ToInt64(dataReader.GetValue(1).ToString()),
                        NombreAlumno = dataReader.GetValue(2).ToString(),
                        CursoID = Convert.ToInt64(dataReader.GetValue(3).ToString()),
                        NombreCurso = dataReader.GetValue(4).ToString(),
                        ArancelID = Convert.ToInt64(dataReader.GetValue(5).ToString()),
                        NombreArancel = dataReader.GetValue(6).ToString(),
                        Anho = dataReader.GetValue(7).ToString(),
                        MesDesde = Convert.ToInt16(dataReader.GetValue(8).ToString()),
                        MesHasta = Convert.ToInt16(dataReader.GetValue(9).ToString()),
                        FechaInscripcionString = DateTime.Parse(dataReader.GetValue(10).ToString()).ToString("dd/MM/yyyy"),
                        Estado = (dataReader.GetValue(11).ToString() == "A" ? "ACTIVO" : (dataReader.GetValue(11).ToString() == "C" ? "CONFIRMADO" : "INACTIVO")),
                        CantidadCuotas = Convert.ToInt16(dataReader.GetValue(12).ToString()),
                        InstitucionID = Convert.ToInt64(dataReader.GetValue(13).ToString()),
                        NroComprobante = dataReader.GetValue(14).ToString(),
                        Cedula = Convert.ToInt64(dataReader.GetValue(15).ToString())
                    });
                }
                command.Dispose(); cnn.Close();
            }
            catch (Exception)
            {

            }

            return inscripciones;
        }

        public static Inscripcion getInscripcion(string idInscripcion)
        {
            var inscripcion = new Inscripcion();

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $"select i.id, a.id, a.nombre || ' ' || a.apellido as nombrealumno, c.id, c.nombrecurso, ar.id, ar.nombre_arancel, " +
                    $"i.anho, i.mesdesde, i.meshasta, i.fecha_inscripcion, i.estado, i.cantidad_cuotas, i.idinstitucion, i.nrocomprobante " +
                    $"FROM dbo.inscripcion i join dbo.alumno a on i.idalumno= a.id " +
                    $"join dbo.curso c on i.idcurso = c.id join dbo.arancel ar on i.idarancel = ar.id " +
                    $"where i.id = {idInscripcion}";
                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    inscripcion = new Inscripcion()
                    {
                        ID = Convert.ToInt64(dataReader.GetValue(0).ToString()),
                        AlumnoID = Convert.ToInt64(dataReader.GetValue(1).ToString()),
                        NombreAlumno = dataReader.GetValue(2).ToString(),
                        CursoID = Convert.ToInt64(dataReader.GetValue(3).ToString()),
                        NombreCurso = dataReader.GetValue(4).ToString(),
                        ArancelID = Convert.ToInt64(dataReader.GetValue(5).ToString()),
                        NombreArancel = dataReader.GetValue(6).ToString(),
                        Anho = dataReader.GetValue(7).ToString(),
                        MesDesde = Convert.ToInt16(dataReader.GetValue(8).ToString()),
                        MesHasta = Convert.ToInt16(dataReader.GetValue(9).ToString()),
                        FechaInscripcionString = DateTime.Parse(dataReader.GetValue(10).ToString()).ToString("dd/MM/yyyy"),
                        Estado = dataReader.GetValue(11).ToString(),
                        CantidadCuotas = Convert.ToInt16(dataReader.GetValue(12).ToString()),
                        InstitucionID = Convert.ToInt64(dataReader.GetValue(13).ToString()),
                        NroComprobante = dataReader.GetValue(14).ToString()
                    };
                };
                
                command.Dispose(); cnn.Close();
            }
            catch (Exception)
            {

            }

            return inscripcion;
        }

        public static List<PagareViewModel> getPagares(string idInscripcion)
        {
            var pagares = new List<PagareViewModel>();

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $"select a.id, a.descripcion, a.tipopagare, a.estado, a.monto, a.fechapago, a.fechavencimiento " +
                    $"FROM dbo.pagare a " +
                    $"where a.idinscripcion = {idInscripcion}";

                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    pagares.Add(new PagareViewModel
                    {
                        ID = Convert.ToInt64(dataReader.GetValue(0).ToString()),
                        Descripcion =  dataReader.GetValue(1).ToString(),
                        TipoPagare = dataReader.GetValue(2).ToString(),
                        Estado = dataReader.GetValue(3).ToString(),
                        Monto = Convert.ToDecimal(dataReader.GetValue(4)).ToString("#,###").Replace(",", "."), 
                        FechaPagoString = string.IsNullOrEmpty(dataReader.GetValue(5).ToString()) ? string.Empty : DateTime.Parse(dataReader.GetValue(5).ToString()).ToString("dd/MM/yyyy"),
                        FechaVencimientoString = string.IsNullOrEmpty(dataReader.GetValue(6).ToString()) ? string.Empty : DateTime.Parse(dataReader.GetValue(6).ToString()).ToString("dd/MM/yyyy")
                    });
                }
                command.Dispose(); cnn.Close();
            }
            catch (Exception ex)
            {

            }

            return pagares;
        }

        public static PagareViewModel getPagareById(string idPagare)
        {
            var pagare = new PagareViewModel();

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $"select a.id, a.descripcion, a.tipopagare, a.estado, a.monto, a.fechapago, a.fechavencimiento " +
                    $"FROM dbo.pagare a " +
                    $"where a.id = {idPagare}";

                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    pagare = new PagareViewModel
                    {
                        ID = Convert.ToInt64(dataReader.GetValue(0).ToString()),
                        Descripcion = dataReader.GetValue(1).ToString(),
                        TipoPagare = dataReader.GetValue(2).ToString(),
                        Estado = dataReader.GetValue(3).ToString(),
                        Monto = Convert.ToDecimal(dataReader.GetValue(4)).ToString("#,###").Replace(",", "."),
                        FechaPagoString = string.IsNullOrEmpty(dataReader.GetValue(5).ToString()) ? string.Empty : DateTime.Parse(dataReader.GetValue(5).ToString()).ToString("dd/MM/yyyy"),
                        FechaVencimientoString = string.IsNullOrEmpty(dataReader.GetValue(6).ToString()) ? string.Empty : DateTime.Parse(dataReader.GetValue(6).ToString()).ToString("dd/MM/yyyy")
                    };
                }
                command.Dispose(); cnn.Close();
            }
            catch (Exception ex)
            {

            }

            return pagare;
        }

        public static string createInscripcion(Inscripcion inscripcion)
        {
            string mensaje = "Ha ocurrido un error, favor intentelo nuevamente mas tarde.";
            string anho_lectivo = "";
            try
            {
                NpgsqlConnection cnn;
                NpgsqlDataReader dataReader;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                sql = $"select i.anho_lectivo FROM dbo.arancel i where i.id = {inscripcion.ArancelID}";
                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    anho_lectivo = dataReader.GetValue(0).ToString();
                }
                command.Dispose(); cnn.Close();

                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                sql = $"insert into dbo.inscripcion(idalumno, idcurso, anho, mesdesde, meshasta, estado, cantidad_cuotas, idarancel, idinstitucion, nrocomprobante)" +
                      $"values ({inscripcion.AlumnoID}, {inscripcion.CursoID}, '{anho_lectivo}', {inscripcion.MesDesde}, {inscripcion.MesHasta}, '{inscripcion.Estado}', " +
                      $"{inscripcion.CantidadCuotas}, {inscripcion.ArancelID}, {inscripcion.InstitucionID}, '{inscripcion.NroComprobante}')";

                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                command.Dispose(); cnn.Close();
                mensaje = "OK";
             
            }
            catch (Exception e)
            {
                //mensaje = "Ha ocurrido un error al insertar la inscripcion.";
            }

            return mensaje;
        }

        public static string deleteInscripcion(long id)
        {
            string mensaje = string.Empty;

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                NpgsqlCommand command;
                string sql, Output = string.Empty;
                cnn.Open();

                sql = $"delete from dbo.pagare u where u.idinscripcion = {id}";

                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                command.Dispose(); cnn.Close();

                cnn.Open();

                sql = $"delete from dbo.inscripcion u where u.id = {id}";

                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                mensaje = "OK";
                command.Dispose(); cnn.Close();
            }

            catch (Exception e)
            {
                mensaje = "Ha ocurrido un error al eliminar la inscripcion.";
            }

            return mensaje;
        }

        public static string updateInscripcion(Inscripcion inscripcion)
        {
            string mensaje = string.Empty;
            string anho_lectivo = string.Empty;
            try
            {
                NpgsqlConnection cnn;
                NpgsqlDataReader dataReader;

                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                sql = $"select i.anho_lectivo FROM dbo.arancel i where i.id = {inscripcion.ArancelID}";
                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    anho_lectivo = dataReader.GetValue(0).ToString();
                }
                command.Dispose(); cnn.Close();

                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();
                /*
                      sql = $"insert into dbo.inscripcion(idalumno, idcurso, anho, mesdesde, meshasta, estado, cantidad_cuotas, idarancel, idinstitucion, nrocomprobante)" +
                      $"values ({inscripcion.AlumnoID}, {inscripcion.CursoID}, '{anho_lectivo}', {inscripcion.MesDesde}, {inscripcion.MesHasta}, '{inscripcion.Estado}', " +
                      $"{inscripcion.CantidadCuotas}, {inscripcion.ArancelID}, {inscripcion.InstitucionID}, '{inscripcion.NroComprobante}')";

                 */
                sql = $"update dbo.inscripcion set idcurso = {inscripcion.CursoID}, " +
                      $"anho = '{anho_lectivo}', mesdesde = '{inscripcion.MesDesde}', meshasta = {inscripcion.MesHasta}, " +
                      $"cantidad_cuotas = {inscripcion.CantidadCuotas}, idarancel = {inscripcion.ArancelID}, nrocomprobante = {inscripcion.NroComprobante} " +
                      $"where id = {inscripcion.ID}";

                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();

                mensaje = "OK";
                command.Dispose(); cnn.Close();
            }

            catch (Exception e)
            {
                mensaje = "Ha ocurrido un error al editar el item.";
            }

            return mensaje;
        }

        public static string createPagares(Inscripcion inscripcion)
        {
            string sql, Output = string.Empty;
            NpgsqlConnection cnn;
            cnn = new NpgsqlConnection(connectionString);
            NpgsqlCommand command;

            Arancel arancel = ArancelRepository.getAranceles(inscripcion.InstitucionID.ToString()).Where(x => x.ID == inscripcion.ArancelID).SingleOrDefault();
            decimal montocuota = Convert.ToDecimal(arancel.MatriculaAnual.Replace(".", string.Empty)) / inscripcion.CantidadCuotas;
            
            for (var i = 0; i <= inscripcion.CantidadCuotas; i++ )
            {
                try
                {
                    string titulo = i == 0 ? "INSCRIPCION" : "CUOTA";
                    decimal monto = i == 0 ? Convert.ToInt64(arancel.MontoInscripcion.Replace(".", "")) : montocuota;

                    sql = $"insert into dbo.pagare(idinscripcion, tipopagare, estado, monto,  descripcion) " +
                        $"values ({inscripcion.ID}, '{titulo}',  " +
                        $"'PE', {monto}, '{ titulo + (i == 0 ? "" : $" - {i} / {inscripcion.CantidadCuotas}") }') ";

                    cnn.Open();
                    command = new NpgsqlCommand(sql, cnn);
                    command.ExecuteNonQuery();
                    command.Dispose(); cnn.Close();
                }

                catch (Exception e)
                {
                    
                }
            }

            return "OK";
        }

        //solo se puede actualizar fecha vencimiento, fecha pago y estado
        public static void updatePagares(string id, string fecha)
        {         
                
            NpgsqlConnection cnn;
            cnn = new NpgsqlConnection(connectionString);
            cnn.Open();

            NpgsqlCommand command;
            string sql, Output = string.Empty;

            sql = $"update dbo.pagare set fechavencimiento = TO_DATE('{fecha}', 'DD/MM/YYYY')" +
                    $"where id = {id}";

            command = new NpgsqlCommand(sql, cnn);
            command.ExecuteNonQuery();
            command.Dispose(); cnn.Close();
                
        }

        public static string validarPagares(string idinscripcion)
        {
            NpgsqlConnection cnn;
            cnn = new NpgsqlConnection(connectionString);
            NpgsqlCommand command;
            cnn.Open();

            NpgsqlDataReader dataReader;
            string sql, Output = string.Empty;

                sql = $"select count(*) from dbo.inscripcion ins join dbo.pagare pa on ins.id = pa.idinscripcion " +
                        $"where ins.id = {idinscripcion} and current_date < pa.fechavencimiento ";
            command = new NpgsqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Output = dataReader.GetValue(0).ToString();    
            };
          
            return Output == "0" ?"OK" : "NO-OK";
        }

        public static void confirmarInscripcion(string idinscripcion)
        {
            NpgsqlConnection cnn;
            cnn = new NpgsqlConnection(connectionString);
            cnn.Open();

            NpgsqlCommand command;
            string sql, Output = string.Empty;

            sql = $"update dbo.inscripcion t set t.estado = 'C' where t.id = {idinscripcion}";

            command = new NpgsqlCommand(sql, cnn);
            command.ExecuteNonQuery();
            command.Dispose(); cnn.Close();

        }

        //selectList
        public static List<SelectListItem> getTiposPagoSelect2(string idInstitucion, string id)
        {
            var tiposPago = new List<SelectListItem>();

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $"select  r.id, r.descripcion FROM dbo.tipopago r where r.idinstitucion = {idInstitucion}";
                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                tiposPago.Add(new SelectListItem
                {
                    Value = "",
                    Text = "Seleccione un valor",
                    Selected = id == "0" ? true : false
                });

                while (dataReader.Read())
                {
                    tiposPago.Add(new SelectListItem
                    {
                        Value = dataReader.GetValue(0).ToString(),
                        Text = dataReader.GetValue(1).ToString(),
                        Selected = dataReader.GetValue(0).ToString() == id ? true : false
                    });
                };
                command.Dispose(); cnn.Close(); ;
            }
            catch (Exception)
            {

            };
            return tiposPago;
        }

        public static List<SelectListItem> getTiposDctoSelect2(string idInstitucion, string id)
        {
            var tiposPago = new List<SelectListItem>();

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $"select  r.id, r.nombre_documento FROM dbo.tipo_documento r where r.idinstitucion = {idInstitucion}";
                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                tiposPago.Add(new SelectListItem
                {
                    Value = "",
                    Text = "Seleccione un valor",
                    Selected = id == "0" ? true : false
                });

                while (dataReader.Read())
                {
                    tiposPago.Add(new SelectListItem
                    {
                        Value = dataReader.GetValue(0).ToString(),
                        Text = dataReader.GetValue(1).ToString(),
                        Selected = dataReader.GetValue(0).ToString() == id ? true : false
                    });
                };
                command.Dispose(); cnn.Close(); ;
            }
            catch (Exception)
            {

            };
            return tiposPago;
        }

    }
}