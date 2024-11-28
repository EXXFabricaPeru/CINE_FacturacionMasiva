using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXX_CP_FacturacionMasiva.Domain.Entities
{
    public class DocumentoSBO
    {
        private SAPbobsCOM.Documents _doc = null;

        public DocumentoSBO(SAPbobsCOM.Company company, SAPbobsCOM.BoObjectTypes objType)
        {
            _doc = (SAPbobsCOM.Documents)company.GetBusinessObject(objType);
            _doc.UserFields.Fields.Item("U_EXC_COSTO").Value = "DO";
            _doc.UserFields.Fields.Item("U_EXC_TIPCOM").Value = "E";
            _doc.SalesPersonCode = 21;
        }
        public string CardCode { set => _doc.CardCode = value; }
        public string CardName { set => _doc.CardName = value; }
        public DateTime DocDate { set => _doc.DocDate = value; }
        public DateTime TaxDate { set => _doc.TaxDate = value; }
        public DateTime DocDueDate { set => _doc.DocDueDate = value; }
        public string FolioPrefixString { set => _doc.FolioPrefixString = value; }
        public int FolioNumber { set => _doc.FolioNumber = value; }
        public string Comments { set => _doc.Comments = value; }
        public IEnumerable<string> IDs { get; set; }
        public IEnumerable<DocumentoSBOLine> Lines
        {
            set
            {
                var currentLine = 0;
                foreach (var item in value)
                {
                    _doc.Lines.Add();
                    _doc.Lines.SetCurrentLine(currentLine);
                    _doc.Lines.ItemCode = item.ItemCode;
                    _doc.Lines.WarehouseCode = item.WhsCode;
                    _doc.Lines.UnitPrice = item.UnitPrice;
                    _doc.Lines.CostingCode = "56";
                    _doc.Lines.CostingCode2 = item.CodArea;
                    currentLine++;
                }
            }
        }

        public int Add() { return _doc.Add(); }
    }

    public class DocumentoSBOLine
    {
        public string ItemCode { get; set; }
        public string WhsCode { get; set; }
        public double UnitPrice { get; set; }
        public string CodArea { get; set; }
    }
}
