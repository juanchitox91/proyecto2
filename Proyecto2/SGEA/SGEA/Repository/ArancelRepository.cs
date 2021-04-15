using Npgsql;
using SGEA.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SGEA.Repository
{
    public class ArancelRepository
    {
        public static string connectionString = System.Configuration.ConfigurationManager.
                                     ConnectionStrings["SGEAContext"].ConnectionString;

        public static List<Arancel> getAranceles(string idInstitucion)
        {
            var aranceles = new List<Arancel>();

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $"select a.id, a.monto_inscripcion, a.matricula_anual, a.anho_lectivo, a.observacion, a.nombre_arancel " +
                    $"from dbo.arancel a where a.idinstitucion = {idInstitucion}";
                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    aranceles.Add(new Arancel
                    {
                        ID = Convert.ToInt64(dataReader.GetValue(0).ToString()),
                        MontoInscripcion = Convert.ToDecimal( dataReader.GetValue(1)).ToString("#,###").Replace(",", "."),
                        MatriculaAnual = Convert.ToDecimal(dataReader.GetValue(2)).ToString("#,###").Replace(",", "."),
                        AnhoLectivo = Convert.ToDecimal(dataReader.GetValue(3)).ToString("#,###").Replace(",", "."),
                        Observacion = dataReader.GetValue(4).ToString(),
                        NombreArancel = dataReader.GetValue(5).ToString()
                    });
                }

                command.Dispose(); cnn.Close();
            }
            catch (Exception e)
            {

                throw;
            }

            return aranceles;
        }

        public static string createArancel(Arancel arancel)
        {
            string mensaje = string.Empty;
            
            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                decimal montodecimal = Convert.ToDecimal(arancel.MontoInscripcion.Replace(".", string.Empty));
                decimal matriculadecimal = Convert.ToDecimal(arancel.MatriculaAnual.Replace(".", string.Empty));
                int anho = Convert.ToInt32(arancel.AnhoLectivo.Replace(".", string.Empty));
                
                sql = $"insert into dbo.arancel(monto_inscripcion, matricula_anual, anho_lectivo, observacion, nombre_arancel, idinstitucion) " +
                      $"values ({montodecimal}, '{matriculadecimal}', '{anho}', '{arancel.Observacion}', '{arancel.NombreArancel}', '{arancel.InstitucionId}')";

                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                mensaje = "OK";
                command.Dispose(); cnn.Close();
            }

            catch (Exception e)
            {
                mensaje = "Ha ocurrido un error al insertar el arancel.";
            }

            return mensaje;
        }

        public static string deleteArancel(long id)
        {
            string mensaje = string.Empty;

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                sql = $"delete from dbo.arancel u where u.id = {id}";

                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                mensaje = "OK";
                command.Dispose(); cnn.Close();
            }

            catch (Exception e)
            {
                mensaje = "Ha ocurrido un error al eliminar el alumno.";
            }

            return mensaje;
        }

        public static string updateArancel(Arancel arancel)
        {
            string mensaje = string.Empty;

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                decimal montodecimal = Convert.ToDecimal(arancel.MontoInscripcion.Replace(".", string.Empty));
                decimal matriculadecimal = Convert.ToDecimal(arancel.MatriculaAnual.Replace(".", string.Empty));
                int anho = Convert.ToInt32(arancel.AnhoLectivo.Replace(".", string.Empty));

                sql = $"update dbo.arancel set " +
                   $"monto_inscripcion = {montodecimal}, matricula_anual = {matriculadecimal}, anho_lectivo ={anho}, " +
                   $"observacion = '{arancel.Observacion}', nombre_arancel = '{arancel.NombreArancel}',  " +
                   $"idinstitucion = '{arancel.InstitucionId}' " +
                   $"where id = {arancel.ID}";

                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                mensaje = "OK";
                command.Dispose(); cnn.Close();
            }

            catch (Exception e)
            {
                mensaje = "Ha ocurrido un error al insertar el usuario.";
            }

            return mensaje;
        }

        public static List<SelectListItem> getArancelSelect2(string idInstitucion, string id, int anho = 0)
        {
            var aranceles = new List<SelectListItem>();
            int anho_lectivo;
            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $"select  r.id, r.nombre_arancel || ' - ' || r.anho_lectivo, r.anho_lectivo FROM dbo.arancel r where r.idinstitucion = {idInstitucion}";
                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                aranceles.Add(new SelectListItem
                {
                    Value = "",
                    Text = "Seleccione un valor",
                    Selected = id == "0" ? true : false
                });

                while (dataReader.Read())
                {
                    anho_lectivo = Convert.ToInt32(dataReader.GetValue(2).ToString());
                   if ( anho_lectivo == anho ||  anho == 0)
                    {
                        aranceles.Add(new SelectListItem
                        {
                            Value = dataReader.GetValue(0).ToString(),
                            Text = dataReader.GetValue(1).ToString(),
                            Selected = dataReader.GetValue(0).ToString() == id ? true : false
                        });
                    }  
                }

                command.Dispose(); cnn.Close();
            }
            catch (Exception)
            {

                //return resultado;
            };
            return aranceles;
        }

        /*
         * ojo! query para situacion financiera!
         select al.apellido, al.nombre, pa.descripcion, pa.tipopagare, pa.estado, cab.nro_factura,
            det.descripcion, cab.fecha from dbo.inscripcion ins join dbo.alumno al on ins.idalumno = al.id
            join dbo.pagare pa on ins.id = pa.idinscripcion
            left join dbo.facturadetalle det on pa.id = det.id_pagare
            left join dbo.factura_cabecera cab on det.id_facturacabecera = cab.id
            where al.id = 18
         */
    }
}