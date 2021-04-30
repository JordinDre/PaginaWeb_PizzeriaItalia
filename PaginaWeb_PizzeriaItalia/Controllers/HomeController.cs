using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaginaWeb_PizzeriaItalia.Models;
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
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
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

		public IActionResult Privacy()
		{
			return View();
		}
		public IActionResult InicioSesion()
		{
			return View();
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
