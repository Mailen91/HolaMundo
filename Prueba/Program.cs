using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Prueba
{
    class Program
    {
        static void Main(string[] args)
        {
            string xmlFolderBackup =  @"C:\Carga\Backup\";
            string xmlFolderResult = @"C:\Carga\Result\";

            string[] xmlfilesBackup = Directory.GetFiles(xmlFolderBackup, @"*.xml");
            string[] xmlfilesResult = Directory.GetFiles(xmlFolderResult, @".xml");

            foreach (string xml in xmlfilesBackup)
            {
                string _xmlName = System.IO.Path.GetFileNameWithoutExtension(xml);
                string _xmlExist = xmlFolderResult + _xmlName + @".xml";
                if (!File.Exists(_xmlExist))
                {
                    updateStatus(_xmlName, 2);
                }
            }
        }
        public static void updateStatus(string serviceReference, int newStatus)
        {
            using (OdbcConnection cnnDataset = new OdbcConnection(ObtenerConexion("Aerocargas_Dataset")))
            {
                string tmpSQL = "UPDATE FEEncabezado SET DocStatus = " + newStatus + " WHERE ServiceReference = '" + serviceReference + "'";

                try
                {
                    cnnDataset.Open();

                    OdbcCommand cmd = new OdbcCommand(tmpSQL, cnnDataset);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                  
                }
                catch (Exception oEx)
                {
                   
                }

            }
        }

        public static string ObtenerConexion(string db)
        {

            string ret = "";
            OdbcConnectionStringBuilder _ocsb = new OdbcConnectionStringBuilder();
            _ocsb.Dsn = "sqlserver";
            _ocsb["UID"] = "sa";
            _ocsb["PWD"] = "19091909";
            _ocsb["DATABASE"] = db.ToString();

            ret = _ocsb.ConnectionString;
            return ret;
        }
    }
}
