﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;

namespace BLL.Common
{
    public class Utilities
    {
        #region Métodos Públicos

        public static int CalcularDigitoCuit(string cuit)
        {
            var mult = new[] { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
            var nums = cuit.ToCharArray();
            var total = mult.Select((t, i) => int.Parse(nums[i].ToString(CultureInfo.InvariantCulture)) * t).Sum();
            var resto = total % 11;
            return resto == 0 ? 0 : resto == 1 ? 9 : 11 - resto;
        }

        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public static bool EsDiaCorrecto(string validar)
        {
            String diaHoy = DiaSemanaHoy();
            if (diaHoy == validar)
            { return true; }
            else
            { return false; }
        }

        public static String DiaSemanaHoy()
        {
            String dia = DateTime.Now.DayOfWeek.ToString();
            switch (dia)
            {
                case "Monday":
                    {
                        dia = "Lunes";
                        break;
                    }
                case "Tuesday":
                    {
                        dia = "Martes";
                        break;
                    }
                case "Wednesday":
                    {
                        dia = "Miércoles";
                        break;
                    }
                case "Thursday":
                    {
                        dia = "Jueves";
                        break;
                    }
                case "Friday":
                    {
                        dia = "Viernes";
                        break;
                    }
                case "Saturday":
                    {
                        dia = "Sábado";
                        break;
                    }
                case "Sunday":
                    {
                        dia = "Domingo";
                        break;
                    }
            }
            return dia;
        }

        #endregion
    }
}
