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
    }
}
