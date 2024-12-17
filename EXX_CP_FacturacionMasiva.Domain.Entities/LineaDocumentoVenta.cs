using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXX_CP_FacturacionMasiva.Domain.Entities
{
    public class LineaDocumentoVenta
    {
        public int LineNum { get; set; }
        public double UnitPrice { get; set; }
        public string ItemCode { get; set; }
        public double Quantity { get; set; }
    }
}
