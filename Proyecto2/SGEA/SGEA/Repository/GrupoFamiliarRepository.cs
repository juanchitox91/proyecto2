﻿using Npgsql;
using SGEA.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SGEA.Repository
{
    public class GrupoFamiliarRepository
    {
        public static string connectionString = System.Configuration.ConfigurationManager.
                                     ConnectionStrings["SGEAContext"].ConnectionString;

        public static List<GrupoFamiliar> getGruposFamiliares(string idInstitucion)
        {
            var grupos = new List<GrupoFamiliar>();

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $"select r.id, r.apellidos, r.observacion FROM dbo.grupofamiliar r where r.idinstitucion = {idInstitucion}";
                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    grupos.Add(new GrupoFamiliar
                    {
                        ID = Convert.ToInt64(dataReader.GetValue(0).ToString()),
                        Apellidos = dataReader.GetValue(1).ToString(),
                        Observacion = dataReader.GetValue(2).ToString(),
                        //FechaAlta = dataReader.GetValue(6).ToString()
                    });
                }
                command.Dispose(); cnn.Close();
            }
            catch (Exception)
            {

            }

            return grupos;
        }

        public static string createGrupoFamiliar(GrupoFamiliar grupo)
        {
            string mensaje = string.Empty;

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                sql = $"insert into dbo.grupofamiliar(apellidos, observacion, idinstitucion)" +
                      $"values ('{grupo.Apellidos}', '{grupo.Observacion}', {grupo.InstitucionID})";

                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                mensaje = "OK";
                command.Dispose(); cnn.Close();
            }

            catch (Exception e)
            {
                mensaje = "Ha ocurrido un error al insertar el Grupo Familiar.";
            }

            return mensaje;
        }

        public static string deleteGrupoFamiliar(long id)
        {
            string mensaje = string.Empty;

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                sql = $"delete from dbo.grupofamiliar u where u.id = {id}";

                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                mensaje = "OK";
                command.Dispose(); cnn.Close();
            }

            catch (Exception e)
            {
                mensaje = "Ha ocurrido un error al eliminar el Grupo Familiar.";
            }

            return mensaje;
        }

        public static string updateGrupoFamiliar(GrupoFamiliar grupo)
        {
            string mensaje = string.Empty;

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                sql = $"update dbo.grupofamiliar set apellidos = '{grupo.Apellidos}', observacion = '{grupo.Observacion}' " +
                    $" where id = {grupo.ID}";

                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                mensaje = "OK";
                command.Dispose(); cnn.Close();
            }

            catch (Exception e)
            {
                mensaje = "Ha ocurrido un error al insertar el Grupo Familiar.";
            }

            return mensaje;
        }
    }
}