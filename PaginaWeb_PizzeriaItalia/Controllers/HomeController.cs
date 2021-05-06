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
		public bool Comprobar_Usuario()
		{
			if (TempData["Tipo"] is null)
			{
				return false;
			}
			else
			{
				TempData.Keep();
				if (TempData["Tipo"].ToString().Equals("0"))
				{
					TempData.Keep();
					return true;
				}
				else
				{
					TempData.Keep();
					return false;
				}
			}
		}
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
			string _consulta = "";
			Datos.Total = 0;
			List<Tablas.Detalle_pedido> aux2 = new List<Tablas.Detalle_pedido>();
			foreach (var item in Datos.Compras)
			{
				try
				{	
					_consulta ="Select * from pizza where cod_pizza = '" + item.Cod_pizza + "'";
					SqlDataReader Leer = Database.Consulta_Reader(_consulta);
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
		public ActionResult Detalle_pedido()
		{
			string _consulta = "";
			List<Detalles_auxiliar.Detalle_pedido2> Lista_aux = new List<Detalles_auxiliar.Detalle_pedido2>();
			if (Datos.Compras.Count > 0)
			{
				foreach (var item in Datos.Compras)
				{	
					_consulta ="Select * from pizza where cod_pizza = '" + item.Cod_pizza + "'";
					SqlDataReader Leer = Database.Consulta_Reader(_consulta);
					while (Leer.Read())
					{
						Lista_aux.Add(new Detalles_auxiliar.Detalle_pedido2(item.Cod_pizza, (string)Leer[3], (string)Leer[1], item.Cantidad, (double)Leer[2]));
					}
				}
				return View(Lista_aux);
			}
			else
			{
				return RedirectToAction("Index");
			}
		}
		[HttpPost]
		public ActionResult Detalle_pedido(string Codigo_Tienda, string direccion, double total)
        {
			int _Cod_tienda = 0;
			String _consulta = "Select * from tienda where nombre = '" + Codigo_Tienda + "'";
			SqlDataReader Leer = Database.Consulta_Reader(_consulta);
			while (Leer.Read())
			{
				_Cod_tienda = (int)Leer[0];
			}
			//Hacer pedido actualizar para colocar tienda
			int cod_pedido = 0;
			DateTime fecha = DateTime.Now;
			TimeSpan hora = DateTime.Now.TimeOfDay;
			
			_consulta ="Insert into pedido(tipo_pedido,cod_tienda,cod_cliente,direccion,fecha,hora,total,estado) values('1', '"+_Cod_tienda+"', '"+TempData["Cod_usuario"]+"', '"+direccion+"', '"+fecha+"', '"+hora+"','"+total+"', '1')";
			TempData.Keep();
			Database.Consulta_Non(_consulta);
			
			_consulta = "Select * from pedido Where cod_cliente = '"+TempData["Cod_usuario"]+ "' and fecha = '"+fecha+ "' and hora = '"+hora+ "' and total = '"+total+"'";
			TempData.Keep();
			Leer = Database.Consulta_Reader(_consulta);
			while (Leer.Read())
            {
				cod_pedido = (int)Leer[0];
            }
            foreach (var item in Datos.Compras)
            {
				_consulta = "insert into detalle_pedido(cod_pedido,cod_pizza,cantidad) values('"+cod_pedido+"', '"+item.Cod_pizza+"', '"+item.Cantidad+"')";
				Database.Consulta_Non(_consulta);
            }
			Datos.Compras.Clear();
			return RedirectToAction("RastrearPedido");
		}
		[HttpPost]
		public ActionResult Detalle_pedido2(int cod_pizza, int cantidad)
		{
			//Eliminar detalle pedido
			Tablas.Detalle_pedido Aux = new Tablas.Detalle_pedido();
			int comprobar = 0;
			foreach (var item in Datos.Compras)
            {
                if (item.Cantidad == cantidad && item.Cod_pizza == cod_pizza && comprobar == 0)
                {
					comprobar = 1;
					Aux = item;
                }
            }
			Datos.Compras.Remove(Aux);
			return RedirectToAction("Detalle_pedido");
		}

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}
		public ActionResult Agregar(String cod_pizza, String cantidad)
        {
			List<Tablas.Detalle_pedido> aux2 = new List<Tablas.Detalle_pedido>();
			Tablas.Detalle_pedido detalle_pedido = new Tablas.Detalle_pedido(1, 1, Convert.ToInt32(cod_pizza), Convert.ToInt32(cantidad));
			Datos.Compras.Add(detalle_pedido);
			Calcular_Total();
			string aux = Datos.Total.ToString();
            if (Datos.Total%1 == 0)
            {
				aux = aux + ".00";
            }
			return Content(aux);
		}

		public ActionResult Obtener_total()
        {
			
			Calcular_Total();
			return Content(Datos.Total.ToString());
		}
		public IActionResult Index()
		{
			string _consulta = "";
			if (TempData["Nombre"] is null)
			{
				return RedirectToAction("InicioSesion");
			}
			else
			{
				TempData.Keep();
				Datos.Pizzas.Clear();
				
				_consulta ="Select * from pizza";
				SqlDataReader Leer = Database.Consulta_Reader(_consulta);
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
			string _consulta = "";
			Tablas.Pizza pizza = new Tablas.Pizza();
			_consulta ="Select * from pizza where cod_pizza = '" + cod_pizza + "'";
			SqlDataReader Leer = Database.Consulta_Reader(_consulta);
			
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
		public ActionResult Cerrar()
        {
			Quitar_usuario();
			return RedirectToAction("InicioSesion");
        }
		public IActionResult RastrearPedido()
		{
			string _consulta = "";
			if (TempData["Cod_usuario"] != null)
            {
				TempData.Keep();
                if (TempData["Tipo"].ToString().Equals("1"))
                {
					TempData.Keep();
					_consulta ="Select Pe.cod_pedido as orden, (Case When Pe.tipo_pedido = 1 THEN 'Online' When Pe.tipo_pedido >= 2 THEN 'Tienda' END) as tipo_pedido, TI.nombre as tienda, Pe.direccion, Pe.fecha, Pe.hora, (Select SUM(detalle_pedido.cantidad*pizza.precio) From detalle_pedido INNER JOIN pizza on pizza.cod_pizza = detalle_pedido.cod_pizza where cod_pedido = PE.cod_pedido)as total, (Case When Pe.estado = 1 THEN 'Preparación' When Pe.estado = 2 THEN 'Enviado' When Pe.estado = 3 THEN 'Entregado' END) as Estado From pedido PE INNER JOIN tienda TI on TI.cod_tienda = PE.cod_tienda WHERE Pe.cod_cliente = '" + TempData["Cod_usuario"] + "' ORDER BY Pe.cod_pedido DESC";
					TempData.Keep();
					SqlDataReader Leer = Database.Consulta_Reader(_consulta);
					
					List<Detalles_auxiliar.Detalle_pedido> aux = new List<Detalles_auxiliar.Detalle_pedido>();
					while (Leer.Read())
					{
                        try
						{
							aux.Add(new Detalles_auxiliar.Detalle_pedido((int)Leer[0], (string)Leer[1], (string)Leer[2],"", (string)Leer[3], (DateTime)Leer[4], (TimeSpan)Leer[5], (double)Leer[6], (string)Leer[7]));
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
					return RedirectToAction("Admin","Administrador");
				}
            }
		} 
		[HttpPost]
		public ActionResult InicioSesion(string correo, string contra)
        {
			string _consulta = "";
			Tablas.Cliente usuario = null;
			_consulta ="Select * From cliente Where correo = '"+correo+"' AND contra = '"+contra+"'";
			SqlDataReader Leer = Database.Consulta_Reader(_consulta);
			
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
					return RedirectToAction("Admin", "Administrador");
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
			string _consulta = "";
			_consulta ="Select * From cliente Where correo = '" + correo + "'";
			SqlDataReader Leer = Database.Consulta_Reader(_consulta);
			
			while (Leer.Read())
			{
				aux++;
			}
            if (aux==0)
            {
				
				_consulta = "Insert into cliente(tipo, nombre, telefono, correo, contra) Values('1','" + nombre + "','" + telefono + "','" + correo + "','" + contra + "')";
				Database.Consulta_Non(_consulta);
				
				_consulta = "Select * From cliente Where correo = '" + correo + "'";
				Leer = Database.Consulta_Reader(_consulta);
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
