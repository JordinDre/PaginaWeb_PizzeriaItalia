using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
			return View();
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
