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
        public string CodPelicula { get; set; }
        public string CodComplejo { get; set; }
        public string Sala { get; set; }
        public DateTime FechaFuncion { get; set; }
        public string NroFactura { get; set; }
        public double Ajuste { get; set; }
        public string Glosa { get; set; }
        public string Area { get; set; }
        public double UnitPrice { get; set; }
        public string ItemCode { get; set; }
    }
}
