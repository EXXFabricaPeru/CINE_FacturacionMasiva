using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXX_CP_FacturacionMasiva.Domain.Entities
{
    public class DocumentoSBOVenta
    {
        private SAPbobsCOM.Documents _doc = null;

        public DocumentoSBOVenta(SAPbobsCOM.Company company, SAPbobsCOM.BoObjectTypes objType)
        {
            _doc = (SAPbobsCOM.Documents)company.GetBusinessObject(objType);
            //_doc.UserFields.Fields.Item("U_EXC_COSTO").Value = "DO";
            //_doc.UserFields.Fields.Item("U_EXC_TIPCOM").Value = "E";
            //_doc.SalesPersonCode = 21;
        }
        public string CardCode { set => _doc.CardCode = value; }
        public string CardName { set => _doc.CardName = value; }
        public DateTime DocDate { set => _doc.DocDate = value; }
        public DateTime TaxDate { set => _doc.TaxDate = value; }
        public DateTime DocDueDate { set => _doc.DocDueDate = value; }
        public string FolioPrefixString { set => _doc.FolioPrefixString = value; }
        public int FolioNumber { set => _doc.FolioNumber = value; }
        public string Comments { set => _doc.Comments = value; }
        public string U_EXC_TVENTA { set => _doc.UserFields.Fields.Item("U_EXC_TVENTA").Value = value; }
        public IEnumerable<string> IDs { get; set; }
        public IEnumerable<DocumentoSBOLineVentas> Lines
        {
            set
            {
                var currentLine = 0;
                foreach (var item in value)
                {
                    _doc.Lines.Add();
                    _doc.Lines.SetCurrentLine(currentLine);
                    _doc.Lines.ItemCode = item.ItemCode;
                    //_doc.Lines.WarehouseCode = item.WhsCode;
                    _doc.Lines.UnitPrice = item.UnitPrice;
                    _doc.Lines.Quantity = item.Quantity;
                    _doc.Lines.CostingCode = "40";// item.Complejo;
                    _doc.Lines.CostingCode2 = item.CodArea;
                    _doc.Lines.CostingCode3 = item.LineaProducto;
                    _doc.Lines.UserFields.Fields.Item("U_EXX_FFNC").Value = item.FechaFinContrato;
                    _doc.Lines.UserFields.Fields.Item("U_EXX_FINC").Value = item.FechaInicioContrato;

                    currentLine++;
                }
            }
        }

        public int Add() { return _doc.Add(); }
    }

    public class DocumentoSBOLineVentas
    {
        public string ItemCode { get; set; }
        public string WhsCode { get; set; }
        public double UnitPrice { get; set; }
        public string CodArea { get; set; }
        public double Quantity { get; set; }
        public double LineNum { get; set; }
        public double DocEntry { get; set; }
        public string Complejo { get; set; }
        public string LineaProducto { get; set; }
        public DateTime FechaInicioContrato { get; set; }
        public DateTime FechaFinContrato { get; set; }
    }
}
