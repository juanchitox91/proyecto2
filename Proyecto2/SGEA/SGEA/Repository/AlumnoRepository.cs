﻿using Helper;
using Npgsql;
//using SGEA.DAL;
using SGEA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SGEA.Repository
{
    public class AlumnoRepository
    {
        public static string connectionString = System.Configuration.ConfigurationManager.
                                     ConnectionStrings["SGEAContext"].ConnectionString;

        public static List<Alumno> getAlumnos(string idInstitucion)
        {
            var alumnos = new List<Alumno>();

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $"select a.id, a.cedula, a.nombre, a.apellido, a.telefono,  a.telefono_2, " +
                    $"a.direccion, a.observacion, a.fecha_alta, a.ult_modificacion from dbo.alumno a where a.idinstitucion = {idInstitucion}";
                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    alumnos.Add(new Alumno
                    {
                        ID = Convert.ToInt64(dataReader.GetValue(0).ToString()),
                        Cedula = Convert.ToInt64(dataReader.GetValue(1).ToString()),
                        Nombre = dataReader.GetValue(2).ToString(),
                        Apellido = dataReader.GetValue(3).ToString(),
                        Telefono = dataReader.GetValue(4).ToString(),
                        Telefono2 = dataReader.GetValue(5).ToString(),        
                        Direccion = dataReader.GetValue(6).ToString(),
                        Observacion = dataReader.GetValue(7).ToString(),
                        FechaAlta = dataReader.GetValue(8).ToString(),
                        UltModificacion = dataReader.GetValue(9).ToString(),
                    });
                }
                command.Dispose(); cnn.Close();
                cnn.Close();
            }
            catch (Exception e)
            {

                throw;
            }

            return alumnos;
        }

        public static string createAlumno(Alumno alumno)
        {
            string mensaje = string.Empty;

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                sql = $"insert into dbo.alumno(cedula, nombre, apellido, telefono, telefono_2," +
                      $"direccion, observacion, fecha_alta, ult_modificacion, idinstitucion)" +
                      $"values ({alumno.Cedula}, '{alumno.Nombre}', '{alumno.Apellido}', '{alumno.Telefono}', '{alumno.Telefono2}', " +
                      $"'{alumno.Direccion}', '{alumno.Observacion}', now(), now(), {alumno.InstitucionID})";

                command = new NpgsqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                mensaje = "OK";
                command.Dispose(); cnn.Close();
            }

            catch (Exception e)
            {
                mensaje = "Ha ocurrido un error al insertar el alumno.";
            }

            return mensaje;
        }

        public static string deleteAlumno(long id)
        {
            string mensaje = string.Empty;

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                sql = $"delete from dbo.alumno u where u.id = {id}";

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

        public static string updateAlumno(Alumno alumno)
        {
            string mensaje = string.Empty;

            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                sql = $"update dbo.alumno set " +
                   $"cedula = {alumno.Cedula}, nombre = '{alumno.Nombre}', apellido ='{alumno.Apellido}', telefono = '{alumno.Telefono}',  telefono_2 = '{alumno.Telefono2}', " +
                   $"direccion = '{alumno.Direccion}', observacion = '{alumno.Observacion}', ult_modificacion = now() " +
                   $"where id = {alumno.ID}";


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

        public static List<SelectListItem> getRolesSelect2(string idInstitucion, string id)
        {
            var roles = new List<SelectListItem>();

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $"select  r.id, r.nombre FROM dbo.rol r where r.idinstitucion = {idInstitucion}";
                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                roles.Add(new SelectListItem
                {
                    Value = "",
                    Text = "Seleccione un valor",
                    Selected = id == "0" ? true : false
                });

                while (dataReader.Read())
                {
                    roles.Add(new SelectListItem
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
            return roles;
        }

        public static List<SelectListItem> getAlumnosSelect2(string idInstitucion, string id)
        {
            var alumnos = new List<SelectListItem>();

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $"select  r.id, r.cedula || ' - ' || r.nombre || ' ' || r.apellido  FROM dbo.alumno r where r.idinstitucion = {idInstitucion}";
                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                alumnos.Add(new SelectListItem
                {
                    Value = "",
                    Text = "Seleccione un valor",
                    Selected = id == "0" ? true : false
                });

                while (dataReader.Read())
                {
                    alumnos.Add(new SelectListItem
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
            return alumnos;
        }

        public static List<Alumno> getAlumnosByIdPlanilla(string idCurso)
        {
            var alumnos = new List<Alumno>();

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                sql = $" select al.id, al.apellido, al.nombre, al.cedula from  dbo.curso cu " +
                    $" join dbo.inscripcion ins on cu.id = ins.idcurso join dbo.alumno al on ins.idalumno = al.id where cu.id = {idCurso}";
                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    alumnos.Add(new Alumno
                    {
                        ID = Convert.ToInt64(dataReader.GetValue(0).ToString()),
                        Cedula = Convert.ToInt64(dataReader.GetValue(3).ToString()),
                        Nombre = dataReader.GetValue(2).ToString(),
                        Apellido = dataReader.GetValue(1).ToString()
                    });
                }
                command.Dispose(); cnn.Close();
                cnn.Close();
            }
            catch (Exception e)
            {

            }

            return alumnos;
        }

        public static List<Accion> getAccionesPorRoles(long idusuario)
        {
            var acciones = new List<Accion>();
            var longid = Convert.ToInt64(idusuario);
            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;

                /*
                    select a.nombre, case when
                        (select count(1) from dbo.accionrol ar join dbo.rol r on ar.idrol = r.id join dbo.usuario u on r.id = u.idrol where (ar.idaccion = a.id and 
                        u.id = 10) or 
	                        (select count(1) 
	                                            from dbo.rol r join dbo.usuario u on r.id = u.idrol
	                                            where u.id = 10 and r.esadministrador = 1) > 0
                        )
                         > 0 then 'SI' else 'NO' end ACTIVO
                    from dbo.accion a                 
                 */

                sql = $"select a.id, a.nombre, a.descripcion, case when " +
                    $"(select count(1) from dbo.accionrol ar join dbo.rol r on ar.idrol = r.id where (ar.idaccion = a.id and  r.id ={longid}  and ar.activo) or " +
                    $"(select count(1) from dbo.rol r  where r.id = {longid} and r.esadministrador = 1) > 0) " +
                    $" > 0 then 'SI' else 'NO' end ACTIVO from dbo.accion a where a.parent_id <> 0";

                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    acciones.Add(
                         new Accion {
                         ID = Convert.ToInt64(dataReader.GetValue(0).ToString()),
                         NombreAccion =  dataReader.GetValue(1).ToString(),
                         Descripcion = dataReader.GetValue(2).ToString(),
                         Activo = dataReader.GetValue(3).ToString() == "SI" ? true : false
                         });
                }
                command.Dispose(); cnn.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return acciones;
        }

        public static string GuardarPermisos(List<Accion> lista, string idrol)
        {
            string mensaje = "OK"; 
            foreach(var item in lista)
            {
                //primero verificamos si ya existe la accion relacionada al rol
                var existe = verificarpermiso(item.NombreAccion, idrol);

                if(existe == 0 && item.Activo)
                {
                    //aca insertamos
                    mensaje = agregarPermiso( idrol, item.ID.ToString());
                }
                else
                {
                    //aca modificamos
                    mensaje = actualizarPermiso(idrol, item.ID.ToString(), item.Activo);
                }
            }

            return mensaje;
        }

        private static int verificarpermiso(string nombrepermiso, string idrol)
        {
            int cantidad = 0;

            NpgsqlConnection cnn;
            cnn = new NpgsqlConnection(connectionString);
            cnn.Open();

            NpgsqlCommand command;
            NpgsqlDataReader dataReader;
            string sql, Output = string.Empty;

            /*
                select count(*) from dbo.accion a join dbo.accionrol ar on a.id = ar.idaccion
                join dbo.rol r on ar.idrol = r.id where r.id = 1 
                and (a.nombre = 'verUsuarios'  or r.esadministrador = 1)
            */

            sql = $"select count(*) from dbo.accion a join dbo.accionrol ar on a.id = ar.idaccion " +
                $"join dbo.rol r on ar.idrol = r.id where r.id = {idrol}" +
                $"and (a.nombre = '{nombrepermiso}' or r.esadministrador = 1)";

            command = new NpgsqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();


            while (dataReader.Read())
            {
                cantidad = Convert.ToInt32(dataReader.GetValue(0).ToString());
            }
            command.Dispose(); cnn.Close();
            return cantidad;

        }

        private static string agregarPermiso(string idrol, string idaccion)
        {
            string mensaje = "ERROR";
            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                sql = $"insert into dbo.accionrol(idaccion, idrol)" +
                      $"values ('{idaccion}', '{idrol}')";

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

        private static string actualizarPermiso(string idrol, string idaccion, bool activo)
        {
            string mensaje = "ERROR";
            try
            {
                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                string sql, Output = string.Empty;

                sql = $"update dbo.accionrol set activo = {activo} where idaccion = {idaccion} and idrol = {idrol}";

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

        public static Dictionary<string, string> GetPermisosOperaciones(string id_rol)
        {
            var permisos = new Dictionary<string, string>();

            try
            {

                NpgsqlConnection cnn;
                cnn = new NpgsqlConnection(connectionString);
                cnn.Open();

                NpgsqlCommand command;
                NpgsqlDataReader dataReader;
                string sql, Output = string.Empty;
                /*
                 select ac.id id_padre, ac.nombre nombre_padre,
                    case when 
                    (select count(*) from 
                    (select a.id, a.parent_id, a.nombre, a.descripcion, 
                    case when 
                    (select count(1) from dbo.accionrol ar join dbo.rol r on ar.idrol = r.id where (ar.idaccion = a.id and  r.id =3  and ar.activo) or 
                    (select count(1) from dbo.rol r  where r.id = 3 and r.esadministrador = 1) > 0) 
                     > 0 then 'SI' else 'NO' end ACTIVO from dbo.accion a where a.parent_id <> 0) as tabla where tabla.activo = 'SI' and tabla.parent_id = ac.id) > 0
                     then 'SI' else 'NO' end from 
                    dbo.accion ac where  ac.parent_id = 0
                    group by ac.id, ac.nombre
                 */

                sql = $"select ac.id id_padre, ac.nombre nombre_padre, case when " +
                    $"(select count(*) from (select a.id, a.parent_id, a.nombre, a.descripcion, case when  (select count(1) from dbo.accionrol ar join dbo.rol r " +
                    $"on ar.idrol = r.id where (ar.idaccion = a.id and  r.id ={id_rol}  and ar.activo) or (select count(1) from dbo.rol r  where r.id = {id_rol} and r.esadministrador = 1) > 0) " +
                    $"> 0 then 'SI' else 'NO' end ACTIVO from dbo.accion a where a.parent_id <> 0) as tabla where tabla.activo = 'SI' and " +
                    $"tabla.parent_id = ac.id) > 0 then 'SI' else 'NO' end from dbo.accion ac where  ac.parent_id = 0 group by ac.id, ac.nombre";
                command = new NpgsqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    string valor = dataReader.GetValue(2).ToString();
                    string nombre = dataReader.GetValue(1).ToString();

                    permisos.Add(nombre, valor);
                }
                command.Dispose(); cnn.Close();
            }
            catch (Exception e)
            {

                throw;
            }

            return permisos;
        }
    }
}