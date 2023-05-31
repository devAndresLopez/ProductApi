using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductApi.Models
{
    public class ClsProducto
    {
        public int P_ID { get; set; }
	    public string P_NOMBRE { get; set; }
        public string P_DESCRIPCION { get; set; }
        public ClsCategoria CATEGORIA { get; set; }
        public ClsProductoImg IMAGEN { get; set; }
}
}