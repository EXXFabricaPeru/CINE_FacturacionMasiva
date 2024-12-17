using EXX_CP_FacturacionMasiva.Infrastructure.Data;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXX_CP_FacturacionMaisva.Business.Logic
{
    public class FacturacionMasivaVentasBL
    {
        private static string _queryId = null;

        public static Recordset ObtenerDocBase()
        {
            _queryId = "ObtenerDocBase";
            return QueriesManager.executeQueryAsRecordSet(_queryId);
        }

        public static Recordset ObtenerTipoDocumento()
        {
            _queryId = "ObtenerTipoDocumento";
            return QueriesManager.executeQueryAsRecordSet(_queryId);
        }
    }
}
