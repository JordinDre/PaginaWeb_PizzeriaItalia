using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaginaWeb_PizzeriaItalia.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PaginaWeb_PizzeriaItalia.Controllers
{
	public class Administrador : Controller
	{
		// GET: Administrador
		public ActionResult Admin()
		{
			return View();
		}

		// GET: Administrador/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: Administrador/Create
		public ActionResult Create()
		{
			return View();
		}

		public ActionResult cliente()
		{
			String _consulta = "Select * from cliente";
			SqlDataReader Leer = Database.Consulta_Reader(_consulta);
			List<Tablas.Cliente> aux = new List<Tablas.Cliente>();
			while (Leer.Read())
			{
				aux.Add(new Tablas.Cliente((int)Leer[0], (int)Leer[1], (String)Leer[2], (String)Leer[3], (String)Leer[4], (String)Leer[5]));
			}
			return View(aux);
		}

		[HttpPost] 
		public ActionResult cliente(int tipo,String Nombre, String Telefono, String Correo, String Contra)
		{
			//Agregar
			String _consulta = "insert into cliente(tipo,nombre,telefono,correo,contra) values ('" + tipo + "', '" + Nombre + "', '" + Telefono + "', '" + Correo + "', '" + Contra + "')";
			Database.Consulta_Non(_consulta);

			_consulta = "Select * from cliente";
			SqlDataReader Leer = Database.Consulta_Reader(_consulta);
			List<Tablas.Cliente> aux = new List<Tablas.Cliente>();
			while (Leer.Read())
			{
				aux.Add(new Tablas.Cliente((int)Leer[0], (int)Leer[1], (String)Leer[2], (String)Leer[3], (String)Leer[4], (String)Leer[5]));
			}
			return View(aux);
		}

		public ActionResult bodega()
		{
			String _consulta = "Select Bo.cod_bodega, Bo.nombre,TI.cod_tienda,Ti.nombre from bodega BO INNER JOIN Tienda TI on TI.cod_tienda = Bo.cod_tienda";
			SqlDataReader Leer = Database.Consulta_Reader(_consulta);
			List<Admin_aux.Bodega> aux = new List<Admin_aux.Bodega>();
			while (Leer.Read())
			{
				aux.Add(new Admin_aux.Bodega((int)Leer[0], (string)Leer[1], (int)Leer[2], (string)Leer[3]));
			}
			return View(aux);
		}

		[HttpPost]
		public ActionResult bodega(string Codigo_Tienda, string Nombre)
		{
			int _Cod_tienda = 0;
			String _consulta = "Select * from tienda where nombre = '" + Codigo_Tienda + "'";
			SqlDataReader Leer = Database.Consulta_Reader(_consulta);
            while (Leer.Read())
            {
				_Cod_tienda = (int)Leer[0];
            }
			_consulta = "insert into bodega(cod_tienda,nombre) values ('" + _Cod_tienda + "', '" + Nombre + "')";
			Database.Consulta_Non(_consulta);

			_consulta = "Select Bo.cod_bodega, Bo.nombre,TI.cod_tienda,Ti.nombre from bodega BO INNER JOIN Tienda TI on TI.cod_tienda = Bo.cod_tienda";
			Leer = Database.Consulta_Reader(_consulta);
			List<Admin_aux.Bodega> aux = new List<Admin_aux.Bodega>();
			while (Leer.Read())
			{
				aux.Add(new Admin_aux.Bodega((int)Leer[0], (string)Leer[1], (int)Leer[2], (string)Leer[3]));
			}
			return View(aux);
		}
		[HttpPost]
		public JsonResult Obtener_tienda(string nombre)
		{
			String _consulta = "Select * from tienda";
			SqlDataReader Leer = Database.Consulta_Reader(_consulta);
			List<string> aux = new List<string>();
			while (Leer.Read())
			{
				aux.Add((String)Leer[1]);
			}
			return Json(aux);
		}

		public ActionResult bodega_ingrediente()
		{
			string _consulta = "Select BI.cod_bodega_ingrediente as registro, Bi.cod_bodega, B.nombre, BI.cod_ingrediente, I.nombre as ingrediente, BI.cantidad From bodega_ingrediente BI INNER JOIN Bodega B on B.cod_bodega = BI.cod_bodega INNER JOIN ingrediente I on I.cod_ingrediente = BI.cod_ingrediente ";
			SqlDataReader Leer = Database.Consulta_Reader(_consulta);
			List<Admin_aux.Bodega_Ingredientes> aux = new List<Admin_aux.Bodega_Ingredientes>();
			while (Leer.Read())
			{
				aux.Add(new Admin_aux.Bodega_Ingredientes((int)Leer[0], (int)Leer[1], (string)Leer[2], (int)Leer[3], (string)Leer[4], (int)Leer[5]));
			}
			return View(aux);
		}

		[HttpPost]
		public ActionResult bodega_ingrediente(string Codigo_Bodega, string Codigo_Ingrediente, int Cantidad)
		{
			int _Cod_bodega = 0;
			int _Cod_ingrediente = 0;
			string _consulta = "Select * from bodega where nombre = '" + Codigo_Bodega + "'";
			SqlDataReader Leer = Database.Consulta_Reader(_consulta);
			while (Leer.Read())
			{
				_Cod_bodega = (int)Leer[0];
			}

			_consulta = "Select * from ingrediente where nombre = '" + Codigo_Ingrediente + "'";
			Leer = Database.Consulta_Reader(_consulta);
			while (Leer.Read())
			{
				_Cod_ingrediente = (int)Leer[0];
			}

			_consulta = "insert into bodega_ingrediente(cod_bodega,cod_ingrediente,cantidad) values ('" + _Cod_bodega + "', '" + _Cod_ingrediente + "', '" + Cantidad + "')";
			Database.Consulta_Non(_consulta);

			_consulta = "Select BI.cod_bodega_ingrediente as registro, Bi.cod_bodega, B.nombre, BI.cod_ingrediente, I.nombre as ingrediente, BI.cantidad From bodega_ingrediente BI INNER JOIN Bodega B on B.cod_bodega = BI.cod_bodega INNER JOIN ingrediente I on I.cod_ingrediente = BI.cod_ingrediente ";
			Leer = Database.Consulta_Reader(_consulta);
			List<Admin_aux.Bodega_Ingredientes> aux = new List<Admin_aux.Bodega_Ingredientes>();
			while (Leer.Read())
			{
				aux.Add(new Admin_aux.Bodega_Ingredientes((int)Leer[0], (int)Leer[1], (string)Leer[2], (int)Leer[3], (string)Leer[4], (int)Leer[5]));
			}
			return View(aux);
		}
		[HttpPost]
		public JsonResult Obtener_bodega(string nombre)
		{
			string _consulta = "Select * from bodega";
			SqlDataReader Leer = Database.Consulta_Reader(_consulta);
			List<string> aux = new List<string>();
			while (Leer.Read())
			{
				aux.Add((String)Leer[2]);
			}
			return Json(aux);
		}
		[HttpPost]
		public JsonResult Obtener_bodega_ingrediente(string cod_bodega ,string nombre)
		{
			string _consulta = "Select * from ingrediente where cod_ingrediente NOT IN(Select BI.cod_ingrediente From bodega_ingrediente BI INNER JOIN ingrediente I on I.cod_ingrediente = Bi.cod_ingrediente INNER JOIN bodega B on B.cod_bodega = BI.cod_bodega Where B.nombre = '" + cod_bodega + "')";
			SqlDataReader Leer = Database.Consulta_Reader(_consulta);
			List<string> aux = new List<string>();
			while (Leer.Read())
			{
				aux.Add((String)Leer[1]);
			}
			return Json(aux);
		}

		public ActionResult detalle_pedido()
		{
			return RedirectToAction("pedido", "Administrador");
		}
		[HttpPost]
		public ActionResult detalle_pedido(int cod_pedido)
        {
			TempData["Pedido"] = cod_pedido;
			List<Detalles_auxiliar.Detalle_pedido2> Lista_aux = new List<Detalles_auxiliar.Detalle_pedido2>();
			string _consulta = "Select PIZ.cod_pizza,PIZ.foto, PIZ.nombre, DP.cantidad,PIZ.precio From detalle_pedido DP INNER JOIN pizza PIZ on PIZ.cod_pizza = DP.cod_pizza Where DP.cod_pedido = '" + cod_pedido + "'";
			SqlDataReader Leer = Database.Consulta_Reader(_consulta);
			while(Leer.Read())
            {
				Lista_aux.Add(new Detalles_auxiliar.Detalle_pedido2((int)Leer[0], (string)Leer[1], (string)Leer[2], (int)Leer[3], (double)Leer[4]));
            }
            if (Lista_aux.Count>0)
            {
				return View(Lista_aux);
			}else
            {
				return RedirectToAction("pedido", "Administrador");
			}
		}

		public ActionResult ingrediente()
		{
			 
			string _consulta = "Select * from ingrediente";
			SqlDataReader Leer = Database.Consulta_Reader(_consulta);
			List<Tablas.Ingrediente> aux = new List<Tablas.Ingrediente>();
			while (Leer.Read())
			{
				aux.Add(new Tablas.Ingrediente((int)Leer[0], (String)Leer[1]));
			}
			return View(aux);
		}

		[HttpPost]
		public ActionResult ingrediente(String Nombre)
		{
			string _consulta = "insert into ingrediente(nombre) values ('" + Nombre + "')";
			Database.Consulta_Non(_consulta);

			_consulta = "Select * from ingrediente";
			SqlDataReader Leer = Database.Consulta_Reader(_consulta);
			List<Tablas.Ingrediente> aux = new List<Tablas.Ingrediente>();
			while (Leer.Read())
			{
				aux.Add(new Tablas.Ingrediente((int)Leer[0], (String)Leer[1]));
			}
			return View(aux);
		}

		public ActionResult pedido()
		{
			string _consulta = "Select Pe.cod_pedido as orden, (Case When Pe.tipo_pedido = 1 THEN 'Online' When Pe.tipo_pedido >= 2 THEN 'Tienda' END) as tipo_pedido, TI.nombre as tienda, CL.nombre as cliente, Pe.direccion, Pe.fecha, Pe.hora, (Select SUM(detalle_pedido.cantidad * pizza.precio) From detalle_pedido INNER JOIN pizza on pizza.cod_pizza = detalle_pedido.cod_pizza where cod_pedido = PE.cod_pedido) as total, (Case When Pe.estado = 1 THEN 'Preparación' When Pe.estado = 2 THEN 'Enviado' When Pe.estado = 3 THEN 'Entregado' END) as Estado From pedido PE INNER JOIN tienda TI on TI.cod_tienda = PE.cod_tienda INNER JOIN cliente CL on Cl.cod_cliente = Pe.cod_cliente Order by Pe.cod_pedido DESC";
			SqlDataReader Leer = Database.Consulta_Reader(_consulta);
			List<Detalles_auxiliar.Detalle_pedido> aux = new List<Detalles_auxiliar.Detalle_pedido>();
			while (Leer.Read())
			{
				double Total = 0;
                try
                {
					Total = Convert.ToDouble(Leer[7]);
				}
                catch (Exception)
                {
					Total = 0;
                }
				aux.Add(new Detalles_auxiliar.Detalle_pedido((int)Leer[0],(string)Leer[1], (string)Leer[2], (string)Leer[3], (string)Leer[4], (DateTime)Leer[5], (TimeSpan)Leer[6], Total, (string)Leer[8]));
			}
			return View(aux);

		}
		public ActionResult Modificar_pedido(int cod_pedido, String tipo)
		{
			int _tipo = 0;
            switch (tipo)
            {
                case "Preparación":
					_tipo = 1;
                    break;
                case "Enviado":
					_tipo = 2;
                    break;
                case "Entregado":
					_tipo = 3;
					break;
            }
			string _consulta = "UPDATE pedido SET estado = '" + _tipo + "' WHERE cod_pedido = '" + cod_pedido + "'";
			Database.Consulta_Non(_consulta);
			return Content("a");
		}

		public ActionResult pizza()
		{
			string _consulta = "Select * from pizza";
			SqlDataReader Leer = Database.Consulta_Reader(_consulta);
			List<Tablas.Pizza> aux = new List<Tablas.Pizza>();
			while (Leer.Read())
			{
				aux.Add(new Tablas.Pizza((int)Leer[0], (String)Leer[1], (double)Leer[2], (String)Leer[3]));
			}
			return View(aux);
		}

		[HttpPost]
		public ActionResult pizza(String Nombre, double Precio, String Foto)
		{
			string _consulta = "insert into pizza(nombre,precio,foto) values ('" + Nombre + "', '" + Precio + "', '" + Foto + "')";
			Database.Consulta_Non(_consulta);

			_consulta = "Select * from pizza";
			SqlDataReader Leer = Database.Consulta_Reader(_consulta);
			List<Tablas.Pizza> aux = new List<Tablas.Pizza>();
			while (Leer.Read())
			{
				aux.Add(new Tablas.Pizza((int)Leer[0], (String)Leer[1], (double)Leer[2], (String)Leer[3]));
			}
			return View(aux);
		}

		public ActionResult pizza_ingrediente()
		{
			string _consulta = "Select BI.cod_pizza_ingrediente as registro, Bi.cod_pizza, B.nombre, BI.cod_ingrediente, I.nombre as ingrediente, BI.cantidad From pizza_ingrediente BI INNER JOIN pizza B on B.cod_pizza = BI.cod_pizza INNER JOIN ingrediente I on I.cod_ingrediente = BI.cod_ingrediente";
			SqlDataReader Leer = Database.Consulta_Reader(_consulta);
			List<Admin_aux.Pizza_Ingredientes> aux = new List<Admin_aux.Pizza_Ingredientes>();
			while (Leer.Read())
			{
				aux.Add(new Admin_aux.Pizza_Ingredientes((int)Leer[0], (int)Leer[1], (string)Leer[2], (int)Leer[3], (string)Leer[4], (int)Leer[5]));
			}
			return View(aux);
		}

		[HttpPost]
		public ActionResult pizza_ingrediente(int Codigo_Pizza, int Codigo_Ingrediente, int Cantidad)
		{
			string _consulta = "insert into bodega_ingrediente(cod_bodega,cod_ingrediente,cantidad) values ('" + Codigo_Pizza + "', '" + Codigo_Ingrediente + "', '" + Cantidad + "')";
			Database.Consulta_Non(_consulta);

			_consulta = "Select BI.cod_pizza_ingrediente as registro, Bi.cod_pizza, B.nombre, BI.cod_ingrediente, I.nombre as ingrediente, BI.cantidad From pizza_ingrediente BI INNER JOIN pizza B on B.cod_pizza = BI.cod_pizza INNER JOIN ingrediente I on I.cod_ingrediente = BI.cod_ingrediente";
			SqlDataReader Leer = Database.Consulta_Reader(_consulta);
			
			List<Admin_aux.Pizza_Ingredientes> aux = new List<Admin_aux.Pizza_Ingredientes>();
			while (Leer.Read())
			{
				aux.Add(new Admin_aux.Pizza_Ingredientes((int)Leer[0], (int)Leer[1], (string)Leer[2], (int)Leer[3], (string)Leer[4], (int)Leer[5]));
			}
			return View(aux);
		}
		[HttpPost]
		public JsonResult Obtener_pizza(string nombre)
		{
			string _consulta = "Select * from pizza";
			SqlDataReader Leer = Database.Consulta_Reader(_consulta);

			List<string> aux = new List<string>();
			while (Leer.Read())
			{
				aux.Add((String)Leer[1]);
			}
			return Json(aux);
		}
		[HttpPost]
		public JsonResult Obtener_pizza_ingrediente(string cod_pizza, string nombre)
		{
			 
			string _consulta = "Select * from ingrediente where cod_ingrediente NOT IN(Select BI.cod_ingrediente From pizza_ingrediente BI INNER JOIN ingrediente I on I.cod_ingrediente = Bi.cod_ingrediente INNER JOIN pizza B on B.cod_pizza = BI.cod_pizza Where B.nombre = '" + cod_pizza + "')";
			SqlDataReader Leer = Database.Consulta_Reader(_consulta);
			
			List<string> aux = new List<string>();
			while (Leer.Read())
			{
				aux.Add((String)Leer[1]);
			}
			return Json(aux);
		}

		public ActionResult tienda()
		{
			string _consulta = "Select * from tienda";
			SqlDataReader Leer = Database.Consulta_Reader(_consulta);

			List<Tablas.Tienda> aux = new List<Tablas.Tienda>();
			while (Leer.Read())
			{
				aux.Add(new Tablas.Tienda((int)Leer[0], (String)Leer[1], (String)Leer[2], (String)Leer[3], (String)Leer[4]));
			}
			return View(aux);
		}

		[HttpPost]
		public ActionResult tienda(String Nombre, String Direccion, String Ciudad, String Foto)
		{
			string _consulta = "insert into tienda(nombre,direccion,ciudad,foto) values ('" + Nombre + "', '" + Direccion + "', '" + Ciudad + "', '" + Foto + "')";
			Database.Consulta_Non(_consulta);

			_consulta = "Select * from tienda";
			SqlDataReader Leer = Database.Consulta_Reader(_consulta);
			List<Tablas.Tienda> aux = new List<Tablas.Tienda>();
			while (Leer.Read())
			{
				aux.Add(new Tablas.Tienda((int)Leer[0], (String)Leer[1], (String)Leer[2], (String)Leer[3], (String)Leer[4]));
			}
			return View(aux);
		}
	}
}
