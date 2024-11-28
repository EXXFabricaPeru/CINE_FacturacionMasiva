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
    [FormAttribute("FormFacturacionMasivaCompras", "Forms/USRForms/FormFacturacionMasivaCompras.b1f")]
    class FormFacturacionMasivaCompras : UserFormBase
    {
        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.StaticText StaticText3;
        private SAPbouiCOM.StaticText StaticText4;
        private SAPbouiCOM.EditText EditText2;
        private SAPbouiCOM.ComboBox cmbComplejo;
        private SAPbouiCOM.ComboBox cmbPeliculas;
        private SAPbouiCOM.StaticText StaticText5;
        private SAPbouiCOM.EditText EditText3;
        private SAPbouiCOM.StaticText StaticText6;
        private SAPbouiCOM.EditText EditText4;
        private SAPbouiCOM.StaticText StaticText7;
        private SAPbouiCOM.Button btnBuscar;
        private SAPbouiCOM.CheckBox CheckBox0;
        private SAPbouiCOM.CheckBox CheckBox1;
        private SAPbouiCOM.CheckBox CheckBox2;
        private SAPbouiCOM.StaticText StaticText8;
        private SAPbouiCOM.Button btnGenDocs;
        private SAPbouiCOM.Matrix mtxDocs;
        private SAPbouiCOM.Matrix mtxDocsServ;

        private SAPbouiCOM.EditText EditText5;
        private SAPbouiCOM.StaticText StaticText9;
        private SAPbouiCOM.EditText EditText6;

        private SAPbouiCOM.DataTable dttDocumentos;
        private SAPbouiCOM.DataTable dttDocumentosSrv;
        private SAPbouiCOM.UserDataSource udsCodProveedor = null;
        private SAPbouiCOM.UserDataSource udsFechaIniFunc = null;
        private SAPbouiCOM.UserDataSource udsFechaFinFunc = null;
        private SAPbouiCOM.UserDataSource udsFechaIniCont = null;
        private SAPbouiCOM.UserDataSource udsFechaFinCont = null;
        private SAPbouiCOM.UserDataSource udsCodComplejo = null;
        private SAPbouiCOM.UserDataSource udsCodSala = null;
        private SAPbouiCOM.UserDataSource udsCodPelicula = null;
        private SAPbouiCOM.UserDataSource udsSlcPelicula = null;
        private SAPbouiCOM.UserDataSource udsSlcComplejo = null;
        private SAPbouiCOM.UserDataSource udsSlcSala = null;
        private SAPbouiCOM.UserDataSource udsFechaFacturacion = null;

        private string _tipoFacturacion = null;
        public FormFacturacionMasivaCompras(string tipoFacturacion)
        {
            _tipoFacturacion = tipoFacturacion;
            this.UIAPIRawForm.Title = $"Facturación Masiva - {(_tipoFacturacion == "S" ? "Dulcería y Servicios" : "Distribución")}";
            this.MostrarControlesPorTipo(tipoFacturacion);
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_0").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_1").Specific));
            this.EditText0.ChooseFromListAfter += new SAPbouiCOM._IEditTextEvents_ChooseFromListAfterEventHandler(this.EditText0_ChooseFromListAfter);
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_2").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_3").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_4").Specific));
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_5").Specific));
            this.StaticText4 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_6").Specific));
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("Item_7").Specific));
            this.cmbComplejo = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_8").Specific));
            this.cmbPeliculas = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_9").Specific));
            this.StaticText5 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_10").Specific));
            this.EditText3 = ((SAPbouiCOM.EditText)(this.GetItem("Item_11").Specific));
            this.StaticText6 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_12").Specific));
            this.EditText4 = ((SAPbouiCOM.EditText)(this.GetItem("Item_13").Specific));
            this.StaticText7 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_14").Specific));
            this.btnBuscar = ((SAPbouiCOM.Button)(this.GetItem("Item_16").Specific));
            this.btnBuscar.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.btnBuscar_PressedAfter);
            this.CheckBox0 = ((SAPbouiCOM.CheckBox)(this.GetItem("Item_17").Specific));
            this.CheckBox1 = ((SAPbouiCOM.CheckBox)(this.GetItem("Item_18").Specific));
            this.CheckBox2 = ((SAPbouiCOM.CheckBox)(this.GetItem("Item_19").Specific));
            this.StaticText8 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_20").Specific));
            this.btnGenDocs = ((SAPbouiCOM.Button)(this.GetItem("Item_22").Specific));
            this.btnGenDocs.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.btnGenDocs_PressedAfter);
            this.dttDocumentos = this.UIAPIRawForm.DataSources.DataTables.Item("DT_DOCS");
            this.dttDocumentosSrv = this.UIAPIRawForm.DataSources.DataTables.Item("DT_DOCS_S");
            this.udsCodProveedor = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_CODPRV");
            this.udsFechaIniFunc = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_FINIFN");
            this.udsFechaFinFunc = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_FFINFN");
            this.udsFechaIniCont = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_FINICT");
            this.udsFechaFinCont = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_FFINCT");
            this.udsCodComplejo = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_CODCMP");
            this.udsCodPelicula = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_CODPEL");
            this.udsCodSala = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_CODSLA");
            this.udsSlcComplejo = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_SLCCMP");
            this.udsSlcPelicula = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_SLCPEL");
            this.udsSlcSala = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_SLCSLA");
            this.udsFechaFacturacion = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_FCHFAC");
            this.mtxDocs = ((SAPbouiCOM.Matrix)(this.GetItem("Item_21").Specific));
            this.EditText5 = ((SAPbouiCOM.EditText)(this.GetItem("Item_15").Specific));
            this.StaticText9 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_23").Specific));
            this.EditText6 = ((SAPbouiCOM.EditText)(this.GetItem("Item_24").Specific));
            this.mtxDocsServ = ((SAPbouiCOM.Matrix)(this.GetItem("Item_25").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private void OnCustomInitialize()
        {
            // Oculto columnas innecesarias
            mtxDocs.Columns.Item("Col_14").Visible = false;
            mtxDocs.Columns.Item("Col_15").Visible = false;
            mtxDocs.Columns.Item("Col_16").Visible = false;
            mtxDocs.Columns.Item("Col_17").Visible = false;
            mtxDocs.Columns.Item("Col_19").Visible = false;
            mtxDocs.Columns.Item("Col_20").Visible = false;

            udsFechaFacturacion.ValueEx = DateTime.Today.ToString("yyyyMMdd");

            //Cargar combo de almacenes
            while (cmbComplejo.ValidValues.Count > 0) cmbComplejo.ValidValues.Remove(0, SAPbouiCOM.BoSearchKey.psk_Index);
            var almacenes = FacturacionMasivaProveedoresBL.ObtenerAlmacenes();
            while (!almacenes.EoF)
            {
                cmbComplejo.ValidValues.Add(almacenes.Fields.Item(0).Value.ToString(), almacenes.Fields.Item(1).Value.ToString());
                almacenes.MoveNext();
            }

            //Cargar combo de peliculas
            while (cmbPeliculas.ValidValues.Count > 0) cmbPeliculas.ValidValues.Remove(0, SAPbouiCOM.BoSearchKey.psk_Index);
            var peliculas = FacturacionMasivaProveedoresBL.ObtenerPeliculas();
            while (!peliculas.EoF)
            {
                cmbPeliculas.ValidValues.Add(peliculas.Fields.Item(0).Value.ToString(), peliculas.Fields.Item(1).Value.ToString());
                peliculas.MoveNext();
            }
        }

        private void btnBuscar_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            //dttDocumentos.Rows.Clear();
            var codProv = string.IsNullOrWhiteSpace(this.udsCodProveedor.Value) ? "null" : $"'{this.udsCodProveedor.Value}'";
            var fchIniFunc = (DateTime.TryParse(udsFechaIniFunc.Value, out var rslt1) ? rslt1 : DateTime.MinValue).ToString("yyyyMMdd");
            var fchFinFunc = (DateTime.TryParse(udsFechaFinFunc.Value, out var rslt2) ? rslt2 : DateTime.MaxValue).ToString("yyyyMMdd");
            var fchIniCont = (DateTime.TryParse(udsFechaFinCont.Value, out var rslt3) ? rslt3 : DateTime.MinValue).ToString("yyyyMMdd");
            var fchFinCont = (DateTime.TryParse(udsFechaFinCont.Value, out var rslt4) ? rslt4 : DateTime.MaxValue).ToString("yyyyMMdd");
            var codComplejo = string.IsNullOrWhiteSpace(this.udsCodComplejo.Value) ? "null" : $"'{this.udsCodComplejo.Value}'";
            var codSala = string.IsNullOrWhiteSpace(this.udsCodSala.Value) ? "null" : $"'{this.udsCodSala.Value}'";
            var codPelicula = string.IsNullOrWhiteSpace(this.udsCodPelicula.Value) ? "null" : $"'{this.udsCodPelicula.Value}'";
            //Agrupa por Pelicula
            var agrupaPorPelicula = udsSlcPelicula.ValueEx;
            //Agrupa por complejo
            var agrupaPorComplejo = udsSlcComplejo.ValueEx;
            //Agrupa por sala
            var agrupaPorSala = udsSlcSala.ValueEx;

            var sqlQry = $"CALL EXX_SP_FM_LISTAR_DOCUMENTOS_FACTURACION_COMPRAS({codProv},'{fchIniFunc}','{fchFinFunc}'," +
                $"'{fchIniCont}','{fchFinCont}',{codComplejo},{codSala},{codPelicula},'{agrupaPorPelicula}','{agrupaPorComplejo}','{agrupaPorSala}','{_tipoFacturacion}')";


            if (_tipoFacturacion == "D")
            {
                dttDocumentos.ExecuteQuery(sqlQry);
                mtxDocs.LoadFromDataSource();
            }
            else
            {
                dttDocumentosSrv.ExecuteQuery(sqlQry);
                mtxDocsServ.LoadFromDataSource();
            }
        }

        private void EditText0_ChooseFromListAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            var cflEvent = (dynamic)pVal;
            if (cflEvent.SelectedObjects is SAPbouiCOM.DataTable dtbl)
            {
                this.udsCodProveedor.Value = dtbl.GetValue(0, 0).ToString();
            }
        }

        private void MostrarControlesPorTipo(string tipoFacturacion)
        {
            switch (tipoFacturacion)
            {
                case "D":
                    this.UIAPIRawForm.Items.Item("Item_3").Visible = true;
                    this.UIAPIRawForm.Items.Item("Item_2").Visible = true;
                    this.UIAPIRawForm.Items.Item("Item_6").Visible = true;
                    this.UIAPIRawForm.Items.Item("Item_9").Visible = true;
                    this.UIAPIRawForm.Items.Item("Item_10").Visible = true;
                    this.UIAPIRawForm.Items.Item("Item_11").Visible = true;
                    this.UIAPIRawForm.Items.Item("Item_14").Visible = true;
                    this.UIAPIRawForm.Items.Item("Item_15").Visible = true;
                    this.UIAPIRawForm.Items.Item("Item_17").Visible = true;
                    this.UIAPIRawForm.Items.Item("Item_18").Visible = true;
                    this.UIAPIRawForm.Items.Item("Item_19").Visible = true;
                    this.UIAPIRawForm.Items.Item("Item_20").Visible = true;
                    this.mtxDocs.Item.Visible = true;
                    this.mtxDocsServ.Item.Visible = false;

                    ((SAPbouiCOM.StaticText)this.UIAPIRawForm.Items.Item("Item_0").Specific).Caption = "Distribuidor";
                    break;
                case "S":
                    this.UIAPIRawForm.Items.Item("Item_3").Visible = false;
                    this.UIAPIRawForm.Items.Item("Item_2").Visible = false;
                    this.UIAPIRawForm.Items.Item("Item_6").Visible = false;
                    this.UIAPIRawForm.Items.Item("Item_9").Visible = false;
                    this.UIAPIRawForm.Items.Item("Item_10").Visible = false;
                    this.UIAPIRawForm.Items.Item("Item_11").Visible = false;
                    this.UIAPIRawForm.Items.Item("Item_14").Visible = false;
                    this.UIAPIRawForm.Items.Item("Item_15").Visible = false;
                    this.UIAPIRawForm.Items.Item("Item_17").Visible = false;
                    this.UIAPIRawForm.Items.Item("Item_18").Visible = false;
                    this.UIAPIRawForm.Items.Item("Item_19").Visible = false;
                    this.UIAPIRawForm.Items.Item("Item_20").Visible = false;
                    this.mtxDocs.Item.Visible = false;
                    this.mtxDocsServ.Item.Visible = true;

                    ((SAPbouiCOM.StaticText)this.UIAPIRawForm.Items.Item("Item_0").Specific).Caption = "Proveedor";
                    this.UIAPIRawForm.Items.Item("Item_5").Top = this.UIAPIRawForm.Items.Item("Item_4").Top;
                    this.UIAPIRawForm.Items.Item("Item_8").Top = this.UIAPIRawForm.Items.Item("Item_7").Top;
                    this.UIAPIRawForm.Items.Item("Item_23").Top = this.UIAPIRawForm.Items.Item("Item_12").Top;
                    this.UIAPIRawForm.Items.Item("Item_24").Top = this.UIAPIRawForm.Items.Item("Item_13").Top;
                    this.UIAPIRawForm.Items.Item("Item_4").Top = this.UIAPIRawForm.Items.Item("Item_3").Top;
                    this.UIAPIRawForm.Items.Item("Item_7").Top = this.UIAPIRawForm.Items.Item("Item_2").Top;
                    this.UIAPIRawForm.Items.Item("Item_12").Top = this.UIAPIRawForm.Items.Item("Item_10").Top;
                    this.UIAPIRawForm.Items.Item("Item_13").Top = this.UIAPIRawForm.Items.Item("Item_11").Top;
                    break;
                default:
                    break;
            }
        }

        private void btnGenDocs_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            var rsltIniFact = Application.SBO_Application.MessageBox("Se procederá a facturar las lineas seleccionadas " +
                 "\n¿Desea continuar?", 1, "SI", "NO");
            if (rsltIniFact != 1) return;

            IEnumerable<DocumentoSBO> docsSAP = null;

            if (_tipoFacturacion == "D")
            {
                mtxDocs.FlushToDataSource();
                var _xmlSerializer = new XmlSerializer(typeof(XMLDataTable));
                var strXMLDTDocs = dttDocumentos.SerializeAsXML(SAPbouiCOM.BoDataTableXmlSelect.dxs_DataOnly);
                var _dsrXmlDTDocs = (XMLDataTable)_xmlSerializer.Deserialize(new StringReader(strXMLDTDocs));
                var lstDocs = _dsrXmlDTDocs.Rows.Where(r => r.Cells.FirstOrDefault(c => c.ColumnUid == "Slc").Value == "Y").Select(s => new LineaDocumentoCompras
                {
                    Slc = s.Cells.FirstOrDefault(c => c.ColumnUid == "Slc").Value,
                    ObjType = Convert.ToInt32(s.Cells.FirstOrDefault(c => c.ColumnUid == "TipoDoc").Value),
                    DocEntry = Convert.ToInt32(s.Cells.FirstOrDefault(c => c.ColumnUid == "KeyDoc").Value),
                    LineNum = Convert.ToInt32(s.Cells.FirstOrDefault(c => c.ColumnUid == "LineaDoc").Value),
                    CardCode = s.Cells.FirstOrDefault(c => c.ColumnUid == "CardCode").Value,
                    CardName = s.Cells.FirstOrDefault(c => c.ColumnUid == "CardName").Value,
                    CodPelicula = s.Cells.FirstOrDefault(c => c.ColumnUid == "CodPelicula").Value,
                    CodComplejo = s.Cells.FirstOrDefault(c => c.ColumnUid == "CodComplejo").Value,
                    FechaFuncion = DateTime.ParseExact(s.Cells.FirstOrDefault(c => c.ColumnUid == "FechaFuncion").Value, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                    Sala = s.Cells.FirstOrDefault(c => c.ColumnUid == "Sala").Value,
                    NroFactura = s.Cells.FirstOrDefault(c => c.ColumnUid == "NroFactura").Value,
                    Ajuste = Convert.ToDouble(s.Cells.FirstOrDefault(c => c.ColumnUid == "Ajuste").Value),
                    Area = s.Cells.FirstOrDefault(c => c.ColumnUid == "Area").Value,
                    Glosa = s.Cells.FirstOrDefault(c => c.ColumnUid == "Glosa").Value,
                    UnitPrice = Convert.ToDouble(s.Cells.FirstOrDefault(c => c.ColumnUid == "PrecioUnitario").Value),
                    ItemCode = s.Cells.FirstOrDefault(c => c.ColumnUid == "CodArticulo").Value
                });

                if (lstDocs.Count() == 0)
                {
                    Application.SBO_Application.StatusBar.SetText("Debe seleccionar al menos una linea", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return;
                }

                var lstDocsAux = lstDocs;
                //Agrupa por Pelicula
                var agrupaPorPelicula = false;//udsSlcPelicula.ValueEx.Equals("Y");
                                              //Agrupa por complejo
                var agrupaPorComplejo = false;//udsSlcComplejo.ValueEx.Equals("Y");
                                              //Agrupa por sala
                var agrupaPorSala = false;//udsSlcSala.ValueEx.Equals("Y");

                if (agrupaPorSala)
                {
                    lstDocsAux = lstDocs.GroupBy(d => new
                    {
                        d.CardCode,
                        d.CardName,
                        d.CodPelicula,
                        d.CodComplejo,
                        d.Sala
                    }).Select(g => new LineaDocumentoCompras
                    {
                        CardCode = g.Key.CardCode,
                        CardName = g.Key.CardName,
                        CodComplejo = g.Key.CodComplejo,
                        CodPelicula = g.Key.CodPelicula,
                        Sala = g.Key.Sala,
                        Ajuste = g.Sum(d => d.Ajuste),
                        Area = g.FirstOrDefault().Area,
                        FechaFuncion = g.FirstOrDefault().FechaFuncion,
                        Glosa = g.FirstOrDefault().Glosa,
                        NroFactura = g.FirstOrDefault().NroFactura,
                        ItemCode = g.FirstOrDefault().ItemCode,
                        UnitPrice = g.FirstOrDefault().UnitPrice
                    });
                }
                if (agrupaPorComplejo)
                {
                    lstDocsAux = lstDocs.GroupBy(d => new
                    {
                        d.CardCode,
                        d.CardName,
                        d.CodPelicula,
                        d.CodComplejo,
                    }).Select(g => new LineaDocumentoCompras
                    {
                        CardCode = g.Key.CardCode,
                        CardName = g.Key.CardName,
                        CodComplejo = g.Key.CodComplejo,
                        CodPelicula = g.Key.CodPelicula,
                        Sala = g.FirstOrDefault().Sala,
                        Ajuste = g.Sum(d => d.Ajuste),
                        Area = g.FirstOrDefault().Area,
                        FechaFuncion = g.FirstOrDefault().FechaFuncion,
                        Glosa = g.FirstOrDefault().Glosa,
                        NroFactura = g.FirstOrDefault().NroFactura,
                        ItemCode = g.FirstOrDefault().ItemCode,
                        UnitPrice = g.FirstOrDefault().UnitPrice
                    });
                }

                if (agrupaPorPelicula)
                {
                    lstDocsAux = lstDocs.GroupBy(d => new
                    {
                        d.CardCode,
                        d.CardName,
                        d.CodPelicula,
                    }).Select(g => new LineaDocumentoCompras
                    {
                        CardCode = g.Key.CardCode,
                        CardName = g.Key.CardName,
                        CodComplejo = "100100",
                        CodPelicula = g.Key.CodPelicula,
                        Sala = g.FirstOrDefault().Sala,
                        Ajuste = g.Sum(d => d.Ajuste),
                        Area = g.FirstOrDefault().Area,
                        FechaFuncion = g.FirstOrDefault().FechaFuncion,
                        Glosa = g.FirstOrDefault().Glosa,
                        NroFactura = g.FirstOrDefault().NroFactura,
                        ItemCode = g.FirstOrDefault().ItemCode,
                        UnitPrice = g.FirstOrDefault().UnitPrice
                    });
                }

                docsSAP = lstDocsAux.GroupBy(d => new
                {
                    CardCode = d.CardCode,
                    CardName = d.CardName,
                    NroFactura = d.NroFactura,
                    Glosa = d.Glosa
                }).Select(g =>
                    new DocumentoSBO(SBOCompany, SAPbobsCOM.BoObjectTypes.oPurchaseInvoices)
                    {
                        CardCode = g.Key.CardCode,
                        CardName = g.Key.CardName,
                        TaxDate = DateTime.ParseExact(udsFechaFacturacion.ValueEx, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                        DocDate = DateTime.ParseExact(udsFechaFacturacion.ValueEx, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                        Comments = g.Key.Glosa,
                        FolioPrefixString = g.Key.NroFactura?.Split('-')[0] ?? "",
                        FolioNumber = Convert.ToInt32(g.Key.NroFactura?.Split('-').Length > 1 ? g.Key.NroFactura?.Split('-')[1] : "0"),
                        Lines = g.Select(s => new DocumentoSBOLine
                        {
                            ItemCode = g.FirstOrDefault()?.ItemCode,
                            WhsCode = g.FirstOrDefault()?.CodComplejo,
                            UnitPrice = g.FirstOrDefault().UnitPrice,
                            CodArea = g.FirstOrDefault()?.Area
                        }),
                        IDs = g.Select(s =>
                        {
                            return s.DocEntry + "-" + s.LineNum;
                        })
                    }
               );
            }
            else 
            {
                mtxDocsServ.FlushToDataSource();
                var _xmlSerializer = new XmlSerializer(typeof(XMLDataTable));
                var strXMLDTDocs = dttDocumentosSrv.SerializeAsXML(SAPbouiCOM.BoDataTableXmlSelect.dxs_DataOnly);
                var _dsrXmlDTDocs = (XMLDataTable)_xmlSerializer.Deserialize(new StringReader(strXMLDTDocs));
                var lstDocs = _dsrXmlDTDocs.Rows.Where(r => r.Cells.FirstOrDefault(c => c.ColumnUid == "Slc").Value == "Y").Select(s => new LineaDocumentoComprasServ
                {
                    Slc = s.Cells.FirstOrDefault(c => c.ColumnUid == "Slc").Value,
                    ObjType = Convert.ToInt32(s.Cells.FirstOrDefault(c => c.ColumnUid == "TipoDoc").Value),
                    DocEntry = Convert.ToInt32(s.Cells.FirstOrDefault(c => c.ColumnUid == "KeyDoc").Value),
                    LineNum = Convert.ToInt32(s.Cells.FirstOrDefault(c => c.ColumnUid == "LineaDoc").Value),
                    CardCode = s.Cells.FirstOrDefault(c => c.ColumnUid == "CardCode").Value,
                    CardName = s.Cells.FirstOrDefault(c => c.ColumnUid == "CardName").Value,               
                    CodComplejo = s.Cells.FirstOrDefault(c => c.ColumnUid == "CodComplejo").Value,
                    FechaFuncion = DateTime.ParseExact(s.Cells.FirstOrDefault(c => c.ColumnUid == "FechaFuncion").Value, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                    Sala = s.Cells.FirstOrDefault(c => c.ColumnUid == "Sala").Value,
                    NroFactura = s.Cells.FirstOrDefault(c => c.ColumnUid == "NroFactura").Value,
                    Ajuste = Convert.ToDouble(s.Cells.FirstOrDefault(c => c.ColumnUid == "Ajuste").Value),
                    Area = s.Cells.FirstOrDefault(c => c.ColumnUid == "Area").Value,
                    Glosa = s.Cells.FirstOrDefault(c => c.ColumnUid == "Glosa").Value,
                    UnitPrice = Convert.ToDouble(s.Cells.FirstOrDefault(c => c.ColumnUid == "PrecioUnitario").Value),
                    ItemCode = s.Cells.FirstOrDefault(c => c.ColumnUid == "CodArticulo").Value
                });

                if (lstDocs.Count() == 0)
                {
                    Application.SBO_Application.StatusBar.SetText("Debe seleccionar al menos una linea", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return;
                }
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
                            sql = string.Format("UPDATE PDN1 SET \"U_EXD_FACTURADO\" = 'Y' WHERE \"DocEntry\" = '{0}' AND \"LineNum\" = '{1}'", id.Split('-'));
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
    }
}
