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
			Database.Reiniciar();
			SqlCommand consulta = new SqlCommand("Select * from bodega", Database.conectar);
			SqlDataReader Leer = consulta.ExecuteReader();
			List<Tablas.Bodega> aux = new List<Tablas.Bodega>();
			while (Leer.Read())
			{
				aux.Add(new Tablas.Bodega((int)Leer[0], (int)Leer[1], (String)Leer[2]));
			}
			return View(aux);
		}

		[HttpPost]
		public ActionResult bodega(int Codigo_Tienda, String Nombre)
		{
			Database.Reiniciar();
			SqlCommand consulta = new SqlCommand("insert into bodega(cod_tienda,nombre) values ('" + Codigo_Tienda + "', '" + Nombre + "')", Database.conectar);
			consulta.ExecuteNonQuery();
			Database.Reiniciar();
			consulta = new SqlCommand("Select * from bodega", Database.conectar);
			SqlDataReader Leer = consulta.ExecuteReader();
			List<Tablas.Bodega> aux = new List<Tablas.Bodega>();
			while (Leer.Read())
			{
				aux.Add(new Tablas.Bodega((int)Leer[0], (int)Leer[1], (String)Leer[2]));
			}
			return View(aux);
		}

		public ActionResult bodega_ingrediente()
		{
			Database.Reiniciar();
			SqlCommand consulta = new SqlCommand("Select * from bodega_ingrediente", Database.conectar);
			SqlDataReader Leer = consulta.ExecuteReader();
			List<Tablas.Bodega_ingrediente> aux = new List<Tablas.Bodega_ingrediente>();
			while (Leer.Read())
			{
				aux.Add(new Tablas.Bodega_ingrediente((int)Leer[0], (int)Leer[1], (int)Leer[2], (int)Leer[3]));
			}
			return View(aux);
		}

		[HttpPost]
		public ActionResult bodega_ingrediente(int Codigo_Bodega, int Codigo_Ingrediente, int Cantidad)
		{
			Database.Reiniciar();
			SqlCommand consulta = new SqlCommand("insert into bodega_ingrediente(cod_bodega,cod_ingrediente,cantidad) values ('" + Codigo_Bodega + "', '" + Codigo_Ingrediente + "', '" + Cantidad + "')", Database.conectar);
			consulta.ExecuteNonQuery();
			Database.Reiniciar();
			consulta = new SqlCommand("Select * from bodega_ingrediente", Database.conectar);
			SqlDataReader Leer = consulta.ExecuteReader();
			List<Tablas.Bodega_ingrediente> aux = new List<Tablas.Bodega_ingrediente>();
			while (Leer.Read())
			{
				aux.Add(new Tablas.Bodega_ingrediente((int)Leer[0], (int)Leer[1], (int)Leer[2], (int)Leer[3]));
			}
			return View(aux);
		}

		public ActionResult detalle_pedido()
		{
			return View();
		}

		public ActionResult ingrediente()
		{
			Database.Reiniciar();
			SqlCommand consulta = new SqlCommand("Select * from ingrediente", Database.conectar);
			SqlDataReader Leer = consulta.ExecuteReader();
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
			Database.Reiniciar();
			SqlCommand consulta = new SqlCommand("insert into ingrediente(nombre) values ('" + Nombre + "')", Database.conectar);
			consulta.ExecuteNonQuery();
			Database.Reiniciar();
			consulta = new SqlCommand("Select * from ingrediente", Database.conectar);
			SqlDataReader Leer = consulta.ExecuteReader();
			List<Tablas.Ingrediente> aux = new List<Tablas.Ingrediente>();
			while (Leer.Read())
			{
				aux.Add(new Tablas.Ingrediente((int)Leer[0], (String)Leer[1]));
			}
			return View(aux);
		}

		public ActionResult pedido()
		{
			return View();
		}

		public ActionResult pizza()
		{
			Database.Reiniciar();
			SqlCommand consulta = new SqlCommand("Select * from pizza", Database.conectar);
			SqlDataReader Leer = consulta.ExecuteReader();
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
			Database.Reiniciar();
			SqlCommand consulta = new SqlCommand("insert into pizza(nombre,precio,foto) values ('" + Nombre + "', '" + Precio + "', '" + Foto + "')", Database.conectar);
			consulta.ExecuteNonQuery();
			Database.Reiniciar();
			consulta = new SqlCommand("Select * from pizza", Database.conectar);
			SqlDataReader Leer = consulta.ExecuteReader();
			List<Tablas.Pizza> aux = new List<Tablas.Pizza>();
			while (Leer.Read())
			{
				aux.Add(new Tablas.Pizza((int)Leer[0], (String)Leer[1], (double)Leer[2], (String)Leer[3]));
			}
			return View(aux);
		}

		public ActionResult pizza_ingrediente()
		{
			return View();
		}

		public ActionResult tienda()
		{
			Database.Reiniciar();
			SqlCommand consulta = new SqlCommand("Select * from tienda", Database.conectar);
			SqlDataReader Leer = consulta.ExecuteReader();
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
			Database.Reiniciar();
			SqlCommand consulta = new SqlCommand("insert into tienda(nombre,direccion,ciudad,foto) values ('" + Nombre + "', '" + Direccion + "', '" + Ciudad + "', '" + Foto + "')", Database.conectar);
			consulta.ExecuteNonQuery();
			Database.Reiniciar();
			consulta = new SqlCommand("Select * from tienda", Database.conectar);
			SqlDataReader Leer = consulta.ExecuteReader();
			List<Tablas.Tienda> aux = new List<Tablas.Tienda>();
			while (Leer.Read())
			{
				aux.Add(new Tablas.Tienda((int)Leer[0], (String)Leer[1], (String)Leer[2], (String)Leer[3], (String)Leer[4]));
			}
			return View(aux);
		}
	}
}
