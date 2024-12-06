using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXX_CP_FacturacionMasiva.Domain.Entities
{
    public class DocumentoVenta
    {
        public string Slc { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoDoc { get; set; }
        public string NroDoc { get; set; }
        public string NroOV { get; set; }
        public string Serie { get; set; }
        public string TipoVenta { get; set; }
        public string Vendedor { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string Moneda { get; set; }
        public double TotalDoc { get; set; }
        public string Anticipo { get; set; }
        public string GenAnti { get; set; }
        public string Comentarios { get; set; }
        public string FactReserva { get; set; }
        public string TipoNC { get; set; }
        public string SutentoNC { get; set; }
        public int CodVendedor { get; set; }
        public int KeyDoc { get; set; }
        public string CodTVenta { get; set; }
        public string CodSerie { get; set; }
        


    }
}
