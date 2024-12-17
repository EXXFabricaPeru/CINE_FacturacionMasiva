using EXX_CP_FacturacionMasiva.Domain.Entities;
using EXX_CP_FacturacionMasiva.Infrastructure.Data;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXX_CP_FacturacionMaisva.Business.Logic
{
    public class FacturacionMasivaProveedoresBL
    {
        private static string _queryId = null;
        public static Recordset ObtenerAlmacenes()
        {
            _queryId = "ObtenerAlmacenes";
            return QueriesManager.executeQueryAsRecordSet(_queryId);
        }

        public static Recordset ObtenerPeliculas()
        {
            _queryId = "ObtenerPeliculas";
            return QueriesManager.executeQueryAsRecordSet(_queryId);
        }

        public static Recordset ObtenerTipoDocumentoSUNAT()
        {
            _queryId = "ObtenerTipoDocumentoSUNAT";
            return QueriesManager.executeQueryAsRecordSet(_queryId);
        }

        public static Recordset ObtenerGruposDetraccion()
        {
            _queryId = "ObtenerGruposDetraccion";
            return QueriesManager.executeQueryAsRecordSet(_queryId);
        }

        public void GenerarDocumentosEnSBO(IEnumerable<LineaDocumentoCompras> lstDocumentos)
        { 


        }
    }
}
