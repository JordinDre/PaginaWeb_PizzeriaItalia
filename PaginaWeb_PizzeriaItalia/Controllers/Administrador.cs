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
			Database.Reiniciar();
			SqlCommand consulta = new SqlCommand("Select * from cliente", Database.conectar);
			SqlDataReader Leer = consulta.ExecuteReader();
			List<Tablas.Cliente> aux = new List<Tablas.Cliente>();
			while (Leer.Read())
			{
				aux.Add(new Tablas.Cliente((int)Leer[0], (int)Leer[1], (String)Leer[2], (String)Leer[3], (String)Leer[4], (String)Leer[5]));
			}
			return View(aux);
		}

		[HttpPost] 
		public ActionResult cliente(String Nombre, String Telefono, String Correo, String Contra)
		{
			Database.Reiniciar();
			SqlCommand consulta = new SqlCommand("insert into cliente(tipo,nombre,telefono,correo,contra) values ('0', '"+Nombre+ "', '" + Telefono + "', '" + Correo + "', '" + Contra + "')", Database.conectar);
			consulta.ExecuteNonQuery();
			Database.Reiniciar();
			consulta = new SqlCommand("Select * from cliente", Database.conectar);
			SqlDataReader Leer = consulta.ExecuteReader();
			List<Tablas.Cliente> aux = new List<Tablas.Cliente>();
			while (Leer.Read())
			{
				aux.Add(new Tablas.Cliente((int)Leer[0], (int)Leer[1], (String)Leer[2], (String)Leer[3], (String)Leer[4], (String)Leer[5]));
			}
			return View(aux);
		}

		public ActionResult bodega()
		{
			return View();
		}

		public ActionResult bodega_ingrediente()
		{
			return View();
		}

		public ActionResult detalle_pedido()
		{
			return View();
		}

		public ActionResult ingrediente()
		{
			return View();
		}

		public ActionResult pedido()
		{
			return View();
		}

		public ActionResult pizza()
		{
			return View();
		}

		public ActionResult pizza_ingrediente()
		{
			return View();
		}

		public ActionResult tienda()
		{
			return View();
		}
	}
}
