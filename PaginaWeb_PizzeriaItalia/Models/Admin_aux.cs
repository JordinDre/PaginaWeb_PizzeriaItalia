using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaginaWeb_PizzeriaItalia.Models
{
    public class Admin_aux
    {
        public class Bodega
        {
            public int Cod_bodega { get; set; }
            public String Nombre { get; set; }
            public int Cod_tienda { get; set; }
            public String Tienda { get; set; }
            public Bodega() { }
            public Bodega(int _Cod_bodega, string _Nombre, int _Cod_tienda, string _Tienda)
            {
                Cod_bodega = _Cod_bodega;
                Nombre = _Nombre;
                Cod_tienda = _Cod_tienda;
                Tienda = _Tienda;
            }
        }
        public class Bodega_Ingredientes
        {
            public int Registro { get; set; }
            public int Cod_bodega { get; set; }
            public String Nombre { get; set; }
            public int Cod_ingrediente { get; set; }
            public String Ingrediente { get; set; }
            public int Cantidad { get; set; }
            public Bodega_Ingredientes() { }
            public Bodega_Ingredientes(int _Registro, int _Cod_bodega, String _Nombre, int _Cod_ingrediente, String _Ingrediente, int _Cantidad)
            {
                Registro = _Registro;
                Cod_bodega = _Cod_bodega;
                Nombre = _Nombre;
                Cod_ingrediente = _Cod_ingrediente;
                Ingrediente = _Ingrediente;
                Cantidad = _Cantidad;
            }
        }
    }
}
