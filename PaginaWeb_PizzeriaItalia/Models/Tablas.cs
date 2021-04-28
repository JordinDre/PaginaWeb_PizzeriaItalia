using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaginaWeb_PizzeriaItalia.Models
{
    public class Tablas
    {
        public class Tienda
        {
            public int Cod_tienda { get; set; }
            public String Nombre { get; set; }
            public String Direccion { get; set; }
            public String Ciudad { get; set; }
            public String Foto { get; set; }
            public Tienda() { }
            public Tienda(int _Cod_tienda, String _Nombre, String _Direccion, String _Ciudad, String _Foto)
            {
                Cod_tienda = _Cod_tienda;
                Nombre = _Nombre;
                Direccion = _Direccion;
                Ciudad = _Ciudad;
                Foto = _Foto;
            }
        }
        public class Pizza
        {
            public int Cod_pizza { get; set; }
            public String Nombre { get; set; }
            public float Precio { get; set; }
            public String Foto { get; set; }
            public Pizza() { }
            public Pizza(int _Cod_pizza, String _Nombre, float _Precio, String _Foto)
            {
                Cod_pizza = _Cod_pizza;
                Nombre = _Nombre;
                Precio = _Precio;
                Foto = _Foto;
            }
        }
        public class Ingrediente
        {
            public int Cod_ingrediente { get; set; }
            public String Nombre { get; set; }
            public Ingrediente() { }
            public Ingrediente(int _Cod_ingrediente, String _Nombre)
            {
                Cod_ingrediente = _Cod_ingrediente;
                Nombre = _Nombre;
            }
        }
        public class Bodega
        {
            public int Cod_bodega { get; set; }
            public int Cod_tienda { get; set; }
            public String Nombre { get; set; }
            public Bodega() { }
            public Bodega(int _Cod_bodega, int _Cod_tienda, String _Nombre)
            {
                Cod_bodega = _Cod_bodega;
                Cod_tienda = _Cod_tienda;
                Nombre = _Nombre;
            }
        }
        public class Bodega_ingrediente
        {
            public int Cod_bodega_ingrediente { get; set; }
            public int Cod_bodega { get; set; }
            public int Cod_ingrediente { get; set; }
            public int Cantidad { get; set; }
            public Bodega_ingrediente() { }
            public Bodega_ingrediente(int _Cod_bodega_ingrediente, int _Cod_bodega, int _Cod_ingrediente, int _Cantidad)
            {
                Cod_bodega_ingrediente = _Cod_bodega_ingrediente;
                Cod_bodega = _Cod_bodega;
                Cod_ingrediente = _Cod_ingrediente;
                Cantidad = _Cantidad;
            }
        }
        public class Pizza_ingrediente
        {
            public int Cod_pizza_ingrediente { get; set; }
            public int Cod_pizza { get; set; }
            public int Cod_ingrediente { get; set; }
            public int Cantidad { get; set; }
            public Pizza_ingrediente() { }
            public Pizza_ingrediente(int _Cod_pizza_ingrediente, int _Cod_pizza, int _Cod_ingrediente, int _Cantidad)
            {
                Cod_pizza_ingrediente = _Cod_pizza_ingrediente;
                Cod_pizza = _Cod_pizza;
                Cod_ingrediente = _Cod_ingrediente;
                Cantidad = _Cantidad;
            }
        }
        public class Cliente
        {
            public int Cod_cliente { get; set; }
            public int Tipo { get; set; }
            public String Nombre { get; set; }
            public String Telefono { get; set; }
            public String Correo { get; set; }
            public String Contra { get; set; }
            public Cliente() { }
            public Cliente(int _Cod_cliente, int _Tipo, String _Nombre, String _Telefono, String _Correo, String _Contra)
            {
                Cod_cliente = _Cod_cliente;
                Tipo = _Tipo;
                Nombre = _Nombre;
                Telefono = _Telefono;
                Correo = _Correo;
                Contra = _Contra;
            }
        }
        public class Pedido
        {
            public int Cod_pedido { get; set; }
            public int Tipo_pedido { get; set; }
            public int Cod_tienda { get; set; }
            public int Cod_cliente { get; set; }
            public String Direccion { get; set; }
            public DateTime Fecha { get; set; }
            public TimeSpan Hora { get; set; }
            public float Total { get; set; }
            public int Estado { get; set; }
            public Pedido() { }
            public Pedido(int _Cod_pedido, int _Tipo_pedido, int _Cod_tienda, int _Cod_cliente, String _Direccion, DateTime _Fecha, TimeSpan _Hora, float _Total, int _Estado)
            {
                Cod_pedido = _Cod_pedido;
                Tipo_pedido = _Tipo_pedido;
                Cod_tienda = _Cod_tienda;
                Cod_cliente = _Cod_cliente;
                Direccion = _Direccion;
                Fecha = _Fecha;
                Hora = _Hora;
                Total = _Total;
                Estado = _Estado;
            }
        }
        public class Detalle_pedido
        {
            public int Cod_detalle_pedido { get; set; }
            public int Cod_pedido { get; set; }
            public int Cod_pizza { get; set; }
            public int Cantidad { get; set; }
            public Detalle_pedido() { }
            public Detalle_pedido(int _Cod_detalle_pedido, int _Cod_pedido, int _Cod_pizza, int _Cantidad)
            {
                Cod_detalle_pedido = _Cod_detalle_pedido;
                Cod_pedido = _Cod_pedido;
                Cod_pizza = _Cod_pizza;
                Cantidad = _Cantidad;
            }
        }
    }
}
