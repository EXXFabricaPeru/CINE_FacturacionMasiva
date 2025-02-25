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
        private SAPbobsCOM.Company _company = null;

        public DocumentoSBO(SAPbobsCOM.Company company, SAPbobsCOM.BoObjectTypes objType)
        {
            _company = company;
            _doc = (SAPbobsCOM.Documents)company.GetBusinessObject(objType);
            _doc.UserFields.Fields.Item("U_EXC_COSTO").Value = "DO";
            _doc.UserFields.Fields.Item("U_EXC_TIPCOM").Value = "E";
            _doc.UserFields.Fields.Item("U_EXX_TIPOOPER").Value = "02";
            _doc.SalesPersonCode = 21;
        }
        public string CardCode { set => _doc.CardCode = value; }
        public string CardName { set => _doc.CardName = value; }
        public string DocCurrency { set => _doc.DocCurrency = value; }
        public DateTime DocDate { set => _doc.DocDate = value; }
        public DateTime TaxDate { set => _doc.TaxDate = value; }
        public DateTime DocDueDate { set => _doc.DocDueDate = value; }
        public string FolioPrefixString { set => _doc.FolioPrefixString = value; }
        public int FolioNumber { set => _doc.FolioNumber = value; }
        public string Comments { set => _doc.Comments = value; }
        public string JournalMemo { set => _doc.JournalMemo = value; }
        public string NumAtCard { set => _doc.NumAtCard = value; }
        public string Indicator { set => _doc.Indicator = value; }

        public IEnumerable<string> IDs { get; set; }
        public string CodDetraccion { get; set; }
        public double TotalDocumento { get; set; }
        public IEnumerable<DocumentoSBOLine> Lines
        {
            set
            {
                var currentLine = 0;
                foreach (var item in value)
                {
                    _doc.Lines.Add();
                    _doc.Lines.SetCurrentLine(currentLine);
                    _doc.Lines.BaseType = item.BaseType;
                    _doc.Lines.BaseEntry = item.BaseEntry;
                    _doc.Lines.BaseLine = item.BaseLine;
                    //_doc.Lines.ItemCode = item.ItemCode;
                    _doc.Lines.WarehouseCode = item.WhsCode;
                    //_doc.Lines.UnitPrice = item.UnitPrice;

                    if (currentLine == 0)
                    {
                        _doc.Lines.Expenses.SetCurrentLine(0);
                        _doc.Lines.Expenses.ExpenseCode = 3;
                        _doc.Lines.Expenses.LineTotal = item.Ajuste;
                        _doc.Lines.Expenses.TaxCode = "IGV";
                    }

                    _doc.Lines.CostingCode = item.CodComplejo;
                    _doc.Lines.CostingCode2 = item.CodArea;
                    _doc.Lines.CostingCode3 = item.CodLineaProducto;
                    _doc.Lines.CostingCode4 = item.CodTipoGasto;
                    //_doc.Lines.TaxLiable = SAPbobsCOM.BoYesNoEnum.tNO;
                    //_doc.Lines.TaxCode = "IGV";
                    if (!string.IsNullOrWhiteSpace(item.U_EXX_GRUPODET))
                    {
                        CodDetraccion = item.U_EXX_GRUPODET;
                        _doc.Lines.UserFields.Fields.Item("U_EXX_GRUPODET").Value = item.U_EXX_GRUPODET;
                    }
                    currentLine++;
                }
            }
        }

        public void QuitarRetencion()
        {
            for (int i = 0; i < _doc.Lines.Count - 1; i++)
            {
                _doc.Lines.SetCurrentLine(i);
                _doc.Lines.TaxLiable = SAPbobsCOM.BoYesNoEnum.tNO;
            }
        }

        // Detraccion
        public void AplicarDetraccion(string codDet)
        {
            var sqlQry = $"select U_EXX_PORDET from \"@EXX_GRUDET\" where \"Code\" = '{codDet}'";
            var recSet = (SAPbobsCOM.Recordset)_company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            recSet.DoQuery(sqlQry);
            var porDet = Convert.ToDouble(recSet.Fields.Item(0).Value) / 100;

            _doc.Installments.SetCurrentLine(0);
            _doc.Installments.DueDate = DateTime.Today;

            //si es M.E.
            if (_doc.DocCurrency != "SOL")
            {
                _doc.Installments.Percentage = porDet;
            }
            else
            {
                _doc.Installments.Percentage = (Math.Round(TotalDocumento * 1.18 * porDet) * 100) / (TotalDocumento * 1.18);
            }
            _doc.Installments.UserFields.Fields.Item("U_EXX_CONFTIPODET").Value = "Si";

            _doc.Installments.Add();
            _doc.Installments.SetCurrentLine(1);
            _doc.Installments.DueDate = _doc.DocDueDate;
            _doc.Installments.Percentage = 100 - (Math.Round(TotalDocumento * 1.18 * porDet) * 100) / (TotalDocumento * 1.18);
            //_doc.Installments.Total = TotalDocumento - Math.Round(TotalDocumento * (porDet / 100));
            _doc.Installments.UserFields.Fields.Item("U_EXX_CONFTIPODET").Value = "No";
        }


        public int Add() { return _doc.Add(); }
    }

    public class DocumentoSBOLine
    {
        public int BaseType { get; set; }
        public int BaseEntry { get; set; }
        public int BaseLine { get; set; }
        public string ItemCode { get; set; }
        public string WhsCode { get; set; }
        public double UnitPrice { get; set; }
        public string CodComplejo { get; set; }
        public string CodArea { get; set; }
        public string CodLineaProducto { get; set; }
        public string CodTipoGasto { get; set; }
        public string U_EXX_GRUPODET { get; set; }
        public double Ajuste { get; set; }
    }
}
