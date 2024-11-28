using EXX_CP_FacturacionMasiva.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static EXX_CP_FacturacionMasiva.Common.Utiles.Global;

namespace EXX_CP_FacturacionMasiva.Infrastructure.Data
{
    public delegate T GetDBValues<T>(Dictionary<string, string> prm);
    public static class QueriesManager
    {
        private static Query[] _queries = null;

        static QueriesManager()
        {
            try
            {
                var exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                XmlSerializer serializer = new XmlSerializer(typeof(DBData));
                using (StreamReader reader = new StreamReader(Path.Combine(exePath, "Resources\\QueriesFile.xml")))
                {
                    _queries = ((DBData)serializer.Deserialize(reader)).Queries;
                }
            }
            catch { throw; }
        }

        public static IEnumerable<T> executeQueryAsType<T>(string qryID, GetDBValues<T> getDBValues, params string[] prms)
        {
            Dictionary<string, string> keyValues = null;
            var recSet = (SAPbobsCOM.Recordset)SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            keyValues = new Dictionary<string, string>();
            var qry = getSQLQuery(qryID, prms);
            recSet.DoQuery(qry);
            while (!recSet.EoF)
            {
                recSet.Fields.Cast<SAPbobsCOM.Field>().ToList().ForEach(f => { keyValues[f.Name] = f.Value.ToString(); });
                yield return getDBValues(keyValues);
                recSet.MoveNext();
            }
        }

        public static SAPbobsCOM.Recordset executeQueryAsRecordSet(string qryID, params string[] prms)
        {
            var recSet = (SAPbobsCOM.Recordset)SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            var qry = getSQLQuery(qryID, prms);
            recSet.DoQuery(qry);
            return recSet;
        }

        public static string getSQLQuery(string qryID, params string[] prms)
        {
            var dbType = SBOCompany.DbServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB ? "HANA" : "SQL";
            var query = _queries.ToList().First(q => q.Id.Equals(qryID));
            var script = query.Scripts.FirstOrDefault(s => s.DBType.Equals("MIX")) ?? query.Scripts.First(s => s.DBType.Equals(dbType));
            return string.Format(script.Value, prms);    
        }
    }
}
