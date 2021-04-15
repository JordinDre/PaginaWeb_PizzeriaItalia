using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PaginaWeb_PizzeriaItalia.Models
{
    public class Database
    {
        //Conexion Camilo
        public static string conexion = ("Data source = 192.168.1.30; Initial Catalog = db_pizza; Integrated Security = False; User Id = sa; Password=1234 ;MultipleActiveResultSets=True");
        public static SqlConnection conectar = new SqlConnection(conexion);
        public static void Abrir()
        {
            if (conectar.State == ConnectionState.Closed)
            {
                conectar.Open();
            }
        }
        public static void Cerrar()
        {
            if (conectar.State == ConnectionState.Open)
            {
                conectar.Close();
                //conectar.Dispose();
            }
        }
        public static void Reiniciar()
        {
            conectar.Close();
            conectar.Open();
        }
    }
}
