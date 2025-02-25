using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXX_CP_FacturacionMasiva.Domain.Entities
{
    public class LineaDocumentoComprasServ
    {
        public string Slc { get; set; }
        public int ObjType { get; set; }
        public int DocEntry { get; set; }
        public int LineNum { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string CodComplejo { get; set; }
        public string NroFactura { get; set; }
        public string TipoDocumento { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaContable { get; set; }
        public string Glosa { get; set; }
        public string Indicator { get; set; }
        public string GrupoDetraccion { get; set; }
        public string CuentaAsociada { get; set; }
        public double UnitPrice { get; set; }
        public string ItemCode { get; set; }
        public double ImporteBase { get; set; }
    }
}
