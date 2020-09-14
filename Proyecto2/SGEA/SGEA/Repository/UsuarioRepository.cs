﻿using Helper;
using Npgsql;
using SGEA.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGEA.Repository
{
    public class UsuarioRepository
    {
        public static string connectionString = System.Configuration.ConfigurationManager.
                                     ConnectionStrings["SGEAContext"].ConnectionString;

        public static List<Usuario> getUsuarios(string idInstitucion)
        {
            var user = new List<Usuario>();

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $"select  u.nombre, u.apellido, u.email, r.nombre, u.id, r.id FROM dbo.usuario u left join dbo.rol r on u.idrol = r.id " +
                $"where u.idinstitucion = {idInstitucion}";
                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    user.Add(new Usuario
                    {
                        Nombre = dataReader.GetValue(0).ToString(),
                        Apellido = dataReader.GetValue(1).ToString(),
                        Email = dataReader.GetValue(2).ToString(),
                        NombreRol = dataReader.GetValue(3).ToString(),
                        ID = Convert.ToInt64(dataReader.GetValue(4).ToString()),
                        IDRol = Convert.ToInt64(dataReader.GetValue(5).ToString())

                    });
                }
                command.Dispose(); cnn.Close();
            }
            catch (Exception)
            {

            }

            return user;
        }

        public static string createUsuario(Usuario user)
        {
            string mensaje = string.Empty;

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                sql = $"insert into dbo.usuario(nombre, apellido, email, contrasenha, idinstitucion, idrol)" +
                      $"values ('{user.Nombre}', '{user.Apellido}', '{user.Email}', '{HashHelper.MD5("123456")}', {user.IDInstitucion}, {user.IDRol})";

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

        public static string deleteUsuario(long id)
        {
            string mensaje = string.Empty;

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                sql = $"delete from dbo.usuario u where u.id = {id}";

                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                mensaje = "OK";
                command.Dispose(); cnn.Close();
            }

            catch (Exception e)
            {

                mensaje = "Ha ocurrido un error al eliminar el usuario.";
            }

            return mensaje;
        }

        public static string updateUsuario(Usuario user)
        {
            string mensaje = string.Empty;

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                sql = $"update dbo.usuario set nombre = '{user.Nombre}', apellido = '{user.Apellido}', email = '{user.Email}', idrol =  {user.IDRol} " +
                      $"where id = {user.ID}";

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

    }
}