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
		private readonly ILogger<HomeController> _logger;

		public void Quitar_usuario()
        {
			TempData["Cod_usuario"] = null;
			TempData["Nombre"] = null;
			TempData["Tipo"] = null;
			ViewData["Total"] = null;
			Datos.Compras.Clear();
		}
		public void Calcular_Total()
        {
			Datos.Total = 0;
			foreach (var item in Datos.Compras)
            {
                try
                {
					Database.Reiniciar();
					SqlCommand consulta = new SqlCommand("Select * from pizza where cod_pizza = '" + item.Cod_pizza + "'", Database.conectar);
					SqlDataReader Leer = consulta.ExecuteReader();
					while (Leer.Read())
					{
						Datos.Total += Convert.ToDouble(Leer["Precio"]) * item.Cantidad;
					};
				}
                catch (Exception)
                {

                }
            }
			ViewData["Total"] = Datos.Total;
        }
		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}
		public ActionResult Agregar(String cod_pizza, String cantidad)
        {
			Tablas.Detalle_pedido detalle_pedido = new Tablas.Detalle_pedido(Datos.Compras.Count, 1, Convert.ToInt32(cod_pizza), Convert.ToInt32(cantidad));
			Datos.Compras.Add(detalle_pedido);
			Calcular_Total();
			string aux = Datos.Total.ToString();
            if (aux.Contains('.'))
            {
            }else
            {
				aux += ".00";
            }
			return Content(aux);
		}

		public ActionResult Obtener_total()
        {
			Database.Reiniciar();
			Calcular_Total();
			return Content(Datos.Total.ToString());
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
				Datos.Pizzas.Clear();
				Database.Reiniciar();
				SqlCommand consulta = new SqlCommand("Select * from pizza", Database.conectar);
				SqlDataReader Leer = consulta.ExecuteReader();
				while (Leer.Read())
				{
					String url = (string)Leer[3];
					if (url.Length == 0)
					{
						url = "https://www.cuballama.com/envios/images/imagen-no-encontrada.png";
					}
					Datos.Pizzas.Add(new Tablas.Pizza((int)Leer[0], (string)Leer[1], (double)Leer[2], url));
				}
				return View(Datos.Pizzas);
			}
			
		}
		[HttpPost]
		public JsonResult Obtener_pizza(int cod_pizza, string nombre, string precio, string foto)
		{
			Tablas.Pizza pizza = new Tablas.Pizza();
			Database.Reiniciar();
			SqlCommand consulta = new SqlCommand("Select * from pizza where cod_pizza = '" + cod_pizza + "'", Database.conectar);
			SqlDataReader Leer = consulta.ExecuteReader();
			while (Leer.Read())
			{
				pizza.Cod_pizza = (int)Leer[0];
				pizza.Nombre = (string)Leer[1];
				pizza.Precio = (double)Leer[2];
				pizza.Foto = (string)Leer[3];
			}
            if (pizza.Foto.Length<10)
            {
				pizza.Foto = "https://www.cuballama.com/envios/images/imagen-no-encontrada.png";
			}
			return Json(pizza);
		}
		[HttpPost]
		public JsonResult Obtener_detalle(int cod_pizza, string foto, string nombre, int cantidad, double precio)
		{
			Database.Reiniciar();
			List<Detalles_auxiliar.Detalle_pedido2> Lista_aux = new List<Detalles_auxiliar.Detalle_pedido2>();
            foreach (var item in Datos.Compras)
            {
				Database.Reiniciar();
				SqlCommand consulta = new SqlCommand("Select * from pizza where cod_pizza = '"+item.Cod_pizza+"'", Database.conectar);
				SqlDataReader Leer = consulta.ExecuteReader();
				while(Leer.Read())
                {
					Lista_aux.Add(new Detalles_auxiliar.Detalle_pedido2(item.Cod_pizza, (string)Leer[3], (string)Leer[1],item.Cantidad,(item.Cantidad*(double)Leer[2])));
                }
            }
			return Json(Lista_aux);
		}

		public ActionResult Cerrar()
        {
			Database.Reiniciar();
			Quitar_usuario();
			return RedirectToAction("InicioSesion");
        }
		public IActionResult RastrearPedido()
		{
			Database.Reiniciar();
            if (TempData["Cod_usuario"] != null)
            {
				TempData.Keep();
                if (TempData["Tipo"].ToString().Equals("1"))
                {
					TempData.Keep();
					Database.Reiniciar();
					SqlCommand consulta = new SqlCommand("Select Pe.cod_pedido as orden, (Case When Pe.tipo_pedido = 1 THEN 'Online' When Pe.tipo_pedido >= 2 THEN 'Tienda' END) as tipo_pedido, TI.nombre as tienda, Pe.direccion, Pe.fecha, Pe.hora, Pe.total,(Case When Pe.estado = 1 THEN 'Preparación' When Pe.estado = 2 THEN 'Entregado' END) as Estado From pedido PE INNER JOIN tienda TI on TI.cod_tienda = PE.cod_tienda WHERE Pe.cod_cliente = '" + TempData["Cod_usuario"] + "' ORDER BY PE.fecha DESC", Database.conectar);
					TempData.Keep();
					SqlDataReader Leer = consulta.ExecuteReader();
					List<Detalles_auxiliar.Detalle_pedido> aux = new List<Detalles_auxiliar.Detalle_pedido>();
					while (Leer.Read())
					{
                        try
						{
							aux.Add(new Detalles_auxiliar.Detalle_pedido((int)Leer[0], (string)Leer[1], (string)Leer[2], (string)Leer[3], (DateTime)Leer[4], (TimeSpan)Leer[5], (double)Leer[6], (string)Leer[7]));
						}
						catch (Exception)
                        {
							aux.Add(new Detalles_auxiliar.Detalle_pedido());
						}
					}
					return View(aux);
				}else
                {
					return RedirectToAction("InicioSesion");
				}
			}
			else
            {
				return RedirectToAction("InicioSesion");
			}
		}
		public IActionResult InicioSesion()
		{
			if (TempData["Nombre"] == null)
			{
				return View();
			}
			else
            {
				TempData.Keep();
                if (TempData["Tipo"].ToString().Equals("1"))
                {
					TempData.Keep();
					return RedirectToAction("Index");
				}
				else
                {
					TempData.Keep();
					return RedirectToAction("Admin");
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
				TempData["Tipo"] = usuario.Tipo;
				TempData["Cod_usuario"] = usuario.Cod_cliente;
				TempData["Nombre"] = usuario.Nombre;
				if (usuario.Tipo == 0)
                {
					return RedirectToAction("Admin");
				}
				else
				{
                  	return RedirectToAction("Index");
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
