using Helper;
using Npgsql;
//using SGEA.DAL;
using SGEA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SGEA.Repository
{
    public class GeneralRepository
    {

        public static string connectionString = System.Configuration.ConfigurationManager.
                             ConnectionStrings["SGEAContext"].ConnectionString;

        public static Dictionary<int, string> Meses = new Dictionary<int, string>()
        {
            [01] = "Enero",
            [02] = "Febrero",
            [03] = "Marzo",
            [04] = "Abril",
            [05] = "Mayo",
            [06] = "Junio",
            [07] = "Julio",
            [08] = "Agosto",
            [09] = "Setiembre",
            [10] = "Octubre",
            [11] = "Noviembre",
            [12] = "Diciembre"
        };

        public static string obtenerMensajeExcepcion(Exception ex, string dato,string accion)
        {
            string mensaje = ex.ToString(); // string.Empty;

            switch (ex.GetHashCode()) {
                case 23505:
                    mensaje = "";
                    break;

                default:
                    mensaje = $"Ha ocurrido un error al {accion} el dato {dato}.";
                    break;
            }
            


            return mensaje;
        }

        public static List<SelectListItem> getMeses(int desde, int hasta, int selected)
        {
            List<SelectListItem> meses = new List<SelectListItem>();
            foreach(var item in Meses)
            {
                if(item.Key >= desde && item.Key <= hasta)
                {
                    meses.Add(new SelectListItem
                    { //key = item.Key, item.Value
                        Value = item.Key.ToString(),
                        Text = item.Value,
                        Selected =  item.Key == selected ? true : false
                    });
                }
            }

            return meses;
        }

        public static List<SelectListItem> getCantidadCuotas(int selected)
        {
            List<SelectListItem> cuotas = new List<SelectListItem>();

            for(var i = 1; i < 11; i++ )
            {
                    cuotas.Add(new SelectListItem
                    { //key = item.Key, item.Value
                        Value = i.ToString(),
                        Text = i.ToString(),
                        Selected = i == selected ? true : false
                    });
            }

            return cuotas;
        }
    }
}