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
			Database.Reiniciar();
			SqlCommand consulta = new SqlCommand("Select Pe.cod_pedido as orden, (Case When Pe.tipo_pedido = 1 THEN 'Online' When Pe.tipo_pedido >= 2 THEN 'Tienda' END) as tipo_pedido, TI.nombre as tienda, CL.nombre as cliente, Pe.direccion, Pe.fecha, Pe.hora, (Select SUM(detalle_pedido.cantidad * pizza.precio) From detalle_pedido INNER JOIN pizza on pizza.cod_pizza = detalle_pedido.cod_pizza where cod_pedido = PE.cod_pedido) as total, (Case When Pe.estado = 1 THEN 'Preparación' When Pe.estado = 2 THEN 'Entregado' END) as Estado From pedido PE INNER JOIN tienda TI on TI.cod_tienda = PE.cod_tienda INNER JOIN cliente CL on Cl.cod_cliente = Pe.cod_cliente ORDER BY PE.fecha DESC", Database.conectar);
			SqlDataReader Leer = consulta.ExecuteReader();
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
