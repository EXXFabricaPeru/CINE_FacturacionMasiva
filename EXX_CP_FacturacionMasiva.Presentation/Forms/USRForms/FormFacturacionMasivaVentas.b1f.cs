using EXX_CP_FacturacionMaisva.Business.Logic;
using EXX_CP_FacturacionMasiva.Domain.Entities;
using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using static EXX_CP_FacturacionMasiva.Common.Utiles.Global;

namespace EXX_CP_FacturacionMasiva.Presentation.Forms.USRForms
{
    [FormAttribute("EXX_CP_FacturacionMasiva.Presentation.Forms.USRForms.FormFacturacionMasivaVentas", "Forms/USRForms/FormFacturacionMasivaVentas.b1f")]
    class FormFacturacionMasivaVentas : UserFormBase
    {
        public FormFacturacionMasivaVentas()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_0").Specific));
            this.edtFechaIni = ((SAPbouiCOM.EditText)(this.GetItem("Item_1").Specific));
            this.edtFechaFin = ((SAPbouiCOM.EditText)(this.GetItem("Item_2").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_3").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_4").Specific));
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_5").Specific));
            this.lblCliente = ((SAPbouiCOM.StaticText)(this.GetItem("Item_6").Specific));
            this.edtCliente = ((SAPbouiCOM.EditText)(this.GetItem("Item_7").Specific));
            this.edtCliente.ChooseFromListAfter += new SAPbouiCOM._IEditTextEvents_ChooseFromListAfterEventHandler(this.edtCliente_ChooseFromListAfter);
            this.edtVendedor = ((SAPbouiCOM.EditText)(this.GetItem("Item_8").Specific));
            this.edtVendedor.ChooseFromListAfter += new SAPbouiCOM._IEditTextEvents_ChooseFromListAfterEventHandler(this.edtVendedor_ChooseFromListAfter);
            this.lblVendedor = ((SAPbouiCOM.StaticText)(this.GetItem("Item_9").Specific));
            this.mtxDocumentos = ((SAPbouiCOM.Matrix)(this.GetItem("Item_10").Specific));
            this.btnBuscar = ((SAPbouiCOM.Button)(this.GetItem("Item_11").Specific));
            this.btnBuscar.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.btnBuscar_PressedAfter);
            this.StaticText6 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_12").Specific));
            this.StaticText7 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_13").Specific));
            this.cboDocBase = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_14").Specific));
            this.cboTipoDoc = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_15").Specific));
            this.btnGenerarDoc = ((SAPbouiCOM.Button)(this.GetItem("Item_16").Specific));
            this.btnGenerarDoc.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.btnGenerarDoc_PressedAfter);
            this.dttDocumentos = this.UIAPIRawForm.DataSources.DataTables.Item("DT_DOCV");
            this.dttDocumentosDetalle = this.UIAPIRawForm.DataSources.DataTables.Item("DT_DOCD");
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.ResizeAfter += new ResizeAfterHandler(this.Form_ResizeAfter);

        }

        private SAPbouiCOM.StaticText StaticText0;

        private void OnCustomInitialize()
        {

            mtxDocumentos.Columns.Item("Col_14").Visible = false;
            mtxDocumentos.Columns.Item("Col_19").Visible = false;
            mtxDocumentos.Columns.Item("Col_18").Visible = false;
            mtxDocumentos.Columns.Item("Col_20").Visible = false;
            mtxDocumentos.Columns.Item("Col_21").Visible = false;

            edtFechaIni.Value = DateTime.Today.ToString("yyyyMMdd");
            edtFechaFin.Value = DateTime.Today.ToString("yyyyMMdd");

            //Cargar combo de Documentos base
            while (cboDocBase.ValidValues.Count > 0) cboDocBase.ValidValues.Remove(0, SAPbouiCOM.BoSearchKey.psk_Index);
            var docBases = FacturacionMasivaVentasBL.ObtenerDocBase();
            while (!docBases.EoF)
            {
                cboDocBase.ValidValues.Add(docBases.Fields.Item(0).Value.ToString(), docBases.Fields.Item(1).Value.ToString());
                docBases.MoveNext();
            }

            //Cargar combo de tipo Doc
            while (cboTipoDoc.ValidValues.Count > 0) cboTipoDoc.ValidValues.Remove(0, SAPbouiCOM.BoSearchKey.psk_Index);
            var tipoDoc = FacturacionMasivaVentasBL.ObtenerTipoDocumento();
            while (!tipoDoc.EoF)
            {
                cboTipoDoc.ValidValues.Add(tipoDoc.Fields.Item(0).Value.ToString(), tipoDoc.Fields.Item(1).Value.ToString());
                tipoDoc.MoveNext();
            }
        }

        private SAPbouiCOM.EditText edtFechaIni;
        private SAPbouiCOM.EditText edtFechaFin;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.StaticText StaticText3;
        private SAPbouiCOM.StaticText lblCliente;
        private SAPbouiCOM.EditText edtCliente;
        private SAPbouiCOM.EditText edtVendedor;
        private SAPbouiCOM.StaticText lblVendedor;
        private SAPbouiCOM.Matrix mtxDocumentos;
        private SAPbouiCOM.Button btnBuscar;
        private SAPbouiCOM.StaticText StaticText6;
        private SAPbouiCOM.StaticText StaticText7;
        private SAPbouiCOM.ComboBox cboDocBase;
        private SAPbouiCOM.ComboBox cboTipoDoc;
        private SAPbouiCOM.Button btnGenerarDoc;
        private SAPbouiCOM.DataTable dttDocumentos;
        private SAPbouiCOM.DataTable dttDocumentosDetalle;

        private void btnBuscar_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                var fchIni = (DateTime.TryParse(edtFechaIni.Value, out var rslt1) ? rslt1 : DateTime.MinValue).ToString("yyyyMMdd");
                var fchFin = (DateTime.TryParse(edtFechaFin.Value, out var rslt2) ? rslt2 : DateTime.MaxValue).ToString("yyyyMMdd");
                var cliente = string.IsNullOrEmpty(edtCliente.Value)? "" : edtCliente.Value;
                var vendedor = string.IsNullOrEmpty(edtVendedor.Value) ? "" : edtVendedor.Value;
                var docBase = cboDocBase.Selected==null ? "" : cboDocBase.Selected?.Value;
                var tipoDoc = cboTipoDoc.Selected == null  ? "" : cboTipoDoc.Selected?.Value;

                var sqlQry = $"CALL EXX_SP_FM_LISTAR_DOCUMENTOS_FACTURACION_VENTAS('{fchIni}','{fchFin}','{cliente}'," +
            $"'{vendedor}','{docBase}','{tipoDoc}')";


                dttDocumentos.ExecuteQuery(sqlQry);
                mtxDocumentos.LoadFromDataSource();
            }
            catch (Exception ex )
            {

                Application.SBO_Application.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            finally
            {
                mtxDocumentos.AutoResizeColumns();
            }
        }

        private void Form_ResizeAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            mtxDocumentos.AutoResizeColumns();

        }

        private void btnGenerarDoc_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                var rsltIniFact = Application.SBO_Application.MessageBox("Se procederá a gererar los documentos de las líneas seleccionadas " +
                    "\n¿Desea continuar?", 1, "SI", "NO");
                if (rsltIniFact != 1) return;

                IEnumerable<DocumentoSBOVenta> docsSAP = null;

                mtxDocumentos.FlushToDataSource();

                var _xmlSerializer = new XmlSerializer(typeof(XMLDataTable));
                var strXMLDTDocs = dttDocumentos.SerializeAsXML(SAPbouiCOM.BoDataTableXmlSelect.dxs_DataOnly);
                var _dsrXmlDTDocs = (XMLDataTable)_xmlSerializer.Deserialize(new StringReader(strXMLDTDocs));

                List<DocumentoVenta> ltd = new List<DocumentoVenta>();
             
                var lstDocs = _dsrXmlDTDocs.Rows.Where(r => r.Cells.FirstOrDefault(c => c.ColumnUid == "Slc").Value == "Y").Select(s => new DocumentoVenta
                {
                    Slc = s.Cells.FirstOrDefault(c => c.ColumnUid == "Slc").Value,
                    KeyDoc = Convert.ToInt32(s.Cells.FirstOrDefault(c => c.ColumnUid == "KeyDoc").Value),
                    CodVendedor = Convert.ToInt32(s.Cells.FirstOrDefault(c => c.ColumnUid == "CodVendedor").Value),
                    CardCode = s.Cells.FirstOrDefault(c => c.ColumnUid == "RUC").Value,
                    CardName = s.Cells.FirstOrDefault(c => c.ColumnUid == "RazonSocial").Value,
                    TipoVenta = s.Cells.FirstOrDefault(c => c.ColumnUid == "TipoVen").Value,
                    Anticipo = s.Cells.FirstOrDefault(c => c.ColumnUid == "Anticipo").Value,
                    Fecha = DateTime.ParseExact(s.Cells.FirstOrDefault(c => c.ColumnUid == "Fecha").Value, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                    CodSerie = s.Cells.FirstOrDefault(c => c.ColumnUid == "CodSerie").Value,
                    CodTVenta = s.Cells.FirstOrDefault(c => c.ColumnUid == "CodTVenta").Value,
                    TotalDoc = Convert.ToDouble(s.Cells.FirstOrDefault(c => c.ColumnUid == "TotalDoc").Value),
                    Comentarios = s.Cells.FirstOrDefault(c => c.ColumnUid == "Comentarios").Value,
                    FactReserva = s.Cells.FirstOrDefault(c => c.ColumnUid == "FactReserva").Value,
                    Moneda =s.Cells.FirstOrDefault(c => c.ColumnUid == "Moneda").Value,
                    NroDoc = s.Cells.FirstOrDefault(c => c.ColumnUid == "NroDoc").Value,
                    NroOV = s.Cells.FirstOrDefault(c => c.ColumnUid == "NroOV").Value,
                    TipoDoc = s.Cells.FirstOrDefault(c => c.ColumnUid == "TipoDoc").Value,
                    TipoNC = s.Cells.FirstOrDefault(c => c.ColumnUid == "TipoNC").Value,
                    Serie = s.Cells.FirstOrDefault(c => c.ColumnUid == "TipoNC").Value,
                    SutentoNC = s.Cells.FirstOrDefault(c => c.ColumnUid == "SutentoNC").Value,
                    GenAnti = s.Cells.FirstOrDefault(c => c.ColumnUid == "GenAnti").Value,
                    Vendedor = s.Cells.FirstOrDefault(c => c.ColumnUid == "Vendedor").Value
                });

                if (lstDocs.Count() == 0)
                {
                    Application.SBO_Application.StatusBar.SetText("Debe seleccionar al menos una línea", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return;
                }

                var lstDocsAux = lstDocs;
                ////Agrupa por Pelicula
                //var agrupaPorPelicula = false;//



                docsSAP = lstDocsAux.Select(g =>
                    new DocumentoSBOVenta(SBOCompany, SAPbobsCOM.BoObjectTypes.oInvoices)
                    {
                        CardCode = g.CardCode,
                        CardName = g.CardName,
                        TaxDate = g.Fecha,//DateTime.ParseExact(g.Fecha, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                        DocDate = g.Fecha,//DateTime.ParseExact(g.Fecha, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                        Comments = g.Comentarios,
                        U_EXC_TVENTA = g.CodTVenta,
                        //FolioPrefixString = g.NroFactura?.Split('-')[0] ?? "",
                        //FolioNumber = Convert.ToInt32(g.Key.NroFactura?.Split('-').Length > 1 ? g.Key.NroFactura?.Split('-')[1] : "0"),

                        IDs = ObtenerDetalle(g.KeyDoc, g.TipoDoc).Select(s =>
                        {
                            return s.DocEntry + "-" + s.LineNum;
                        })
                    }

                try
                {
                    var recSet = (SAPbobsCOM.Recordset)SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    var sql = string.Empty;
                    var cntErr = 0;
                    foreach (var doc in docsSAP) 
                    {
                        var rslt = doc.Add();
                        if (rslt != 0) 
                        {
                            Application.SBO_Application.StatusBar.SetText(SBOCompany.GetLastErrorDescription(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            cntErr++;
                        }
                        else
                        {
                            foreach (var id in doc.IDs)
                            {
                                sql = string.Format("UPDATE RDR1 SET \"U_EXD_FACTURADO\" = 'Y' WHERE \"DocEntry\" = '{0}' AND \"LineNum\" = '{1}'", id.Split('-'));
                                recSet.DoQuery(sql);
                            }
                        }
                    } 
                    btnBuscar.Item.Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                    if (cntErr == 0)
                        Application.SBO_Application.StatusBar.SetText("Proceso finalizado correctamente", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                }
                catch (Exception ex)
                {
                    Application.SBO_Application.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                }
            }
            catch (Exception ex)
            {
                Application.SBO_Application.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            finally
            {
                mtxDocumentos.AutoResizeColumns();
            }
        }

        private IEnumerable<DocumentoSBOLineVentas> ObtenerDetalle(int keyDoc, string tipoDoc)
        {
            var sqlQry = $"CALL EXX_SP_FM_LISTAR_DOCUMENTOS_FACTURACION_VENTAS_DETALLE('{keyDoc}','{tipoDoc}')";

            dttDocumentosDetalle.ExecuteQuery(sqlQry);
            //mtxDocumentos.LoadFromDataSource();

            var _xmlSerializer = new XmlSerializer(typeof(XMLDataTable));
            var strXMLDTDocs = dttDocumentosDetalle.SerializeAsXML(SAPbouiCOM.BoDataTableXmlSelect.dxs_DataOnly);
            var _dsrXmlDTDocs = (XMLDataTable)_xmlSerializer.Deserialize(new StringReader(strXMLDTDocs));



            var lstDocs = _dsrXmlDTDocs.Rows.Select(s => new DocumentoSBOLineVentas
            {
                CodArea = s.Cells.FirstOrDefault(c => c.ColumnUid == "Area").Value,
                Complejo = s.Cells.FirstOrDefault(c => c.ColumnUid == "Complejo").Value,
                LineaProducto = s.Cells.FirstOrDefault(c => c.ColumnUid == "LineaProducto").Value,
                ItemCode = s.Cells.FirstOrDefault(c => c.ColumnUid == "ItemCode").Value,
                UnitPrice = Convert.ToDouble(s.Cells.FirstOrDefault(c => c.ColumnUid == "Price").Value),
                //WhsCode = s.Cells.FirstOrDefault(c => c.ColumnUid == "CardCode").Value,
                Quantity = Convert.ToDouble(s.Cells.FirstOrDefault(c => c.ColumnUid == "Quantity").Value),
                LineNum = Convert.ToDouble(s.Cells.FirstOrDefault(c => c.ColumnUid == "LineaBase").Value),
                DocEntry = Convert.ToDouble(s.Cells.FirstOrDefault(c => c.ColumnUid == "DocEntryBase").Value),
                FechaFinContrato = DateTime.ParseExact(s.Cells.FirstOrDefault(c => c.ColumnUid == "U_EXX_FFNC").Value, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                FechaInicioContrato = DateTime.ParseExact(s.Cells.FirstOrDefault(c => c.ColumnUid == "U_EXX_FINC").Value, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            }); ;

            
            return lstDocs;
        }

        private void edtVendedor_ChooseFromListAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {



            var cflEvent = (dynamic)pVal;
            if (cflEvent.SelectedObjects is SAPbouiCOM.DataTable dtbl)
            {
                this.edtVendedor.Value = dtbl.GetValue("SlpCode", 0).ToString();
                //this.lblVendedor.Caption =  
            }

        }

        private void edtCliente_ChooseFromListAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            var cflEvent = (dynamic)pVal;
            if (cflEvent.SelectedObjects is SAPbouiCOM.DataTable dtbl)
            {
                this.edtCliente.Value = dtbl.GetValue("CardCode", 0).ToString();
                //this.lblCliente.Caption = dtbl.GetValue("CardName", 0).ToString();
            }

        }
    }
}
