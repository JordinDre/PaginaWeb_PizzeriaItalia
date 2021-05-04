using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaginaWeb_PizzeriaItalia.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PaginaWeb_PizzeriaItalia.Controllers
{
	public class HomeController : Controller
	{
		public static List<Tablas.Pizza> Compras = new List<Tablas.Pizza>();
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			if (TempData["Nombre"] is null)
			{
				return RedirectToAction("InicioSesion");

			}
			else
			{
				Database.Abrir();
				Datos.Tb_Pizza.Clear();
				SqlCommand consulta = new SqlCommand("Select * from pizza", Database.conectar);
				SqlDataReader Leer = consulta.ExecuteReader();
				while (Leer.Read())
				{
					String url = (string)Leer[3];
					if (url.Length == 0)
					{
						url = "https://www.cuballama.com/envios/images/imagen-no-encontrada.png";
					}
					Datos.Tb_Pizza.Add(new Tablas.Pizza((int)Leer[0], (string)Leer[1], (double)Leer[2], url));
				}
				return View(Datos.Tb_Pizza);
			}
			TempData.Keep();
			
		}
		[HttpPost]
		public JsonResult Obtener_pizza(int cod_pizza, string nombre, string precio, string foto)
		{
			Tablas.Pizza pizza = new Tablas.Pizza();
			Database.Abrir();
			Datos.Tb_Pizza.Clear();
			SqlCommand consulta = new SqlCommand("Select * from pizza where cod_pizza = '" + cod_pizza + "'", Database.conectar);
			SqlDataReader Leer = consulta.ExecuteReader();
			while (Leer.Read())
			{
				pizza.Cod_pizza = (int)Leer[0];
				pizza.Nombre = (string)Leer[1];
				pizza.Precio = (double)Leer[2];
				pizza.Foto = (string)Leer[3];
			}
			return Json(pizza);
		}
		public ActionResult Cerrar()
        {
			TempData["Nombre"] = null;
			return RedirectToAction("InicioSesion");
        }
		public IActionResult Privacy()
		{
			return View();
		}
		public IActionResult InicioSesion()
		{
			return View();
		} 
		[HttpPost]
		public ActionResult InicioSesion(string correo, string contra)
        {
			Tablas.Cliente usuario = null;
			Database.Abrir();
			SqlCommand consulta = new SqlCommand("Select * From cliente Where correo = '"+correo+"' AND contra = '"+contra+"'", Database.conectar);
			SqlDataReader Leer = consulta.ExecuteReader();
			while (Leer.Read())
            {
				usuario = new Tablas.Cliente((int)Leer[0], (int)Leer[1], (string)Leer[2], (string)Leer[3], (string)Leer[4], (string)Leer[5]);
            }
            if (usuario is null)
            {
				return View();
            }else
            {
                if (usuario.Tipo == 0)
                {
					return View();
				}
				else
                {
					TempData["Nombre"] = usuario.Nombre;
					return RedirectToAction("Index");
				}
            }
        }

		public IActionResult Registrarse()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
