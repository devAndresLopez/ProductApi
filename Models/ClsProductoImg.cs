using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace ProductApi.Models
{
    public class ClsProductoImg
    {
        public int IMG_ID { get; set; }
        public byte[] IMG_DATA { get; set; }
        public string IMG_NOMBRE { get; set; }
        public string IMG_EXTENSION { get; set; }
    }
}