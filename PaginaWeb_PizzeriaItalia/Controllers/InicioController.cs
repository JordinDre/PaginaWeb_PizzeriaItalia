using Microsoft.AspNetCore.Mvc;
using PaginaWeb_PizzeriaItalia.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PaginaWeb_PizzeriaItalia.Controllers
{
    public class InicioController : Controller
    {
        public ActionResult Tienda()
        {
            Database.Abrir();
            Tablas.Tb_Pizza.Clear();
            SqlCommand consulta = new SqlCommand("Select * from pizza", Database.conectar);
            SqlDataReader Leer = consulta.ExecuteReader();
            while (Leer.Read())
            {
                Tablas.Tb_Pizza.Add(new Tablas.Pizza((int)Leer[0],(string)Leer[1],(double)Leer[2],(string)Leer[3]));
            }
            return View(Tablas.Tb_Pizza);
        }
    }
}
