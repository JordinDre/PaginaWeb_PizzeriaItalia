using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaginaWeb_PizzeriaItalia.Models
{
    public class Datos
    {
        public static List<Tablas.Detalle_pedido> Compras = new List<Tablas.Detalle_pedido>();
        public static List<Tablas.Pizza> Pizzas = new List<Tablas.Pizza>();
        public static double Total = 0;
    }
}
