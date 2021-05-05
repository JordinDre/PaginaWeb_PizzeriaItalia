using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaginaWeb_PizzeriaItalia.Models
{
    public class Detalles_auxiliar
    {
        public class Detalle_pedido
        {
            public int Orden { get; set; }
            public String Tipo_pedido { get; set; }
            public String Tienda { get; set; }
            public String Direccion { get; set; }
            public DateTime Fecha { get; set; }
            public TimeSpan Hora { get; set; }
            public double Total { get; set; }
            public String Estado { get; set; }
            public Detalle_pedido() { }
            public Detalle_pedido(int _Orden, String _Tipo_pedido, String _Tienda, String _Direccion, DateTime _Fecha, TimeSpan _Hora, double _Total, String _Estado)
            {
                Orden = _Orden;
                Tipo_pedido = _Tipo_pedido;
                Tienda = _Tienda;
                Direccion = _Direccion;
                Fecha = _Fecha;
                Hora = _Hora;
                Total = _Total;
                Estado = _Estado;
            }
        }
        public class Detalle_pedido2
        {
            public int Cod_pizza { get; set; }
            public String Foto { get; set; }
            public String Nombre { get; set; }
            public int Cantidad { get; set; }
            public double Precio { get; set; }
            public Detalle_pedido2() { }
            public Detalle_pedido2(int _Cod_pizza, String _Foto, String _Nombre, int _Cantidad, double _Precio)
            {
                Cod_pizza = _Cod_pizza;
                Foto = _Foto;
                Nombre = _Nombre;
                Cantidad = _Cantidad;
                Precio = _Precio;
            }
        }
    }
}
