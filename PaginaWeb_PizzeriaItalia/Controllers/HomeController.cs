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

		public void Quitar_usuario()
        {
			TempData["Cod_usuario"] = null;
			TempData["Nombre"] = null;
			TempData["Tipo"] = null;
			TempData["Total"] = 0;
			TempData["Compra"] = new List<Tablas.Pizza>();
		}

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
				TempData.Keep();
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

		[HttpPost]
		public JsonResult Agregar_pizza(int cod_pizza, int cantidad, string precio)
		{
			List<Tablas.Detalle_pedido> Tb_detalles = TempData["Compras"] as List<Tablas.Detalle_pedido>;
            if (Tb_detalles == null)
            {
				Tb_detalles = new List<Tablas.Detalle_pedido>();
            }
			Tablas.Detalle_pedido detalle_pedido = new Tablas.Detalle_pedido(Tb_detalles.Count, 1, cod_pizza, cantidad);
			Tb_detalles.Add(detalle_pedido);
			TempData["Compras"] = Tb_detalles;
			double aux = Convert.ToDouble(precio);
			double aux2 = Convert.ToDouble(TempData["Total"].ToString());
			double total = aux2 + (aux * cantidad);
			return Json(total);
		}
		public ActionResult Cerrar()
        {
			Quitar_usuario();
			return RedirectToAction("InicioSesion");
        }
		public IActionResult Privacy()
		{
			return View();
		}
		public IActionResult InicioSesion()
		{
			if (TempData["Nombre"] == null)
			{
				return View();
			}
			else
            {
                if (TempData["Tipo"].ToString().Equals("1"))
                {
					return RedirectToAction("Index");
				}
				else
                {
					//admin
					return View();
                }
            }
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
					TempData["Total"] = 0;
					TempData["Tipo"] = usuario.Tipo;
					TempData["Cod_usuario"] = usuario.Cod_cliente;
					TempData["Nombre"] = usuario.Nombre;
                    if (usuario.Tipo == 1)
					{
						return RedirectToAction("Index");
					}else
                    {
						//admin
						return View();
                    }
				}
            }
        }

		public IActionResult Registrarse()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Registrarse(String nombre, String telefono, String correo, String contra)
        {
			int aux = 0;
			Database.Reiniciar();
			SqlCommand consulta = new SqlCommand("Select * From cliente Where correo = '" + correo + "'", Database.conectar);
			SqlDataReader Leer = consulta.ExecuteReader();
			while (Leer.Read())
			{
				aux++;
			}
            if (aux==0)
            {
				Database.Reiniciar();
				consulta = new SqlCommand("Insert into cliente(tipo, nombre, telefono, correo, contra) Values('1','" + nombre + "','" + telefono + "','" + correo + "','" + contra + "')", Database.conectar);
				consulta.ExecuteNonQuery();
				Database.Reiniciar();
				consulta = new SqlCommand("Select * From cliente Where correo = '" + correo + "'", Database.conectar);
				Leer = consulta.ExecuteReader();
				while (Leer.Read())
				{
					TempData["Cod_usuario"] = (int)Leer[0];
				}
				TempData["Tipo"] = 1;
				TempData["Total"] = 0;
				TempData["Nombre"] = nombre;
				return RedirectToAction("Index");
			}else
            {
				TempData["Error"] = "Correo ingresado";
				return View();
            }
		}

		public ActionResult Admin()
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
