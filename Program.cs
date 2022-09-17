using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSVUtility;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Oracle.DataAccess.Client;
using System.Configuration;
using System.Collections.Specialized;

namespace OracleMigrator
{
    class Program
    {
        static void Main(string[] args)
        {

            //string MSSQLtableName = "[ORACLE].[dbo].[DOC_NACH]";

            //string sAttr;
            
            
            
            //string ORACLEtableName = "DOC_PAY";
            //string startDate = "01.01.19";
            //string endDate = "31.12.19";

            string ORACLECONSTR = ConfigurationManager.AppSettings.Get("ORACLECONSTR");
            string ORACLEtableName = ConfigurationManager.AppSettings.Get("ORACLEtableName");

            string cmdstr = "SELECT * from "+ ORACLEtableName + " where 1=1";

            string CSVPath = @ConfigurationManager.AppSettings.Get("CSVPath");

            // Create the adapter with the selectCommand txt and the
            // connection string
            Console.WriteLine("Подключаемся к ДБ ORACLE.");
            OracleDataAdapter adapter = new OracleDataAdapter(cmdstr, ORACLECONSTR);

            // Create the builder for the adapter to automatically generate
            // the Command when needed
            OracleCommandBuilder builder = new OracleCommandBuilder(adapter);


            Console.WriteLine("Получаем данные из таблицы "+ ORACLEtableName);
            // Create and fill the DataSet
            DataSet dataset = new DataSet();
            adapter.Fill(dataset, ORACLEtableName);

            // Get the table from the dataset
            DataTable table = dataset.Tables[ORACLEtableName];

            Console.WriteLine("Записываем данные в файл" + CSVPath + ORACLEtableName + ".csv");
            CSVUtility.CSVUtility.ToCSV(table, CSVPath + ORACLEtableName+".csv");
            Console.WriteLine("Готово!");
            
            


            Console.WriteLine();
            Console.ReadKey();
            //CSVUtility.InsertDataIntoSQLServerUsingSQLBulkCopy(CSVtable, MSSQLtableName, csbuilder.ConnectionString);
        }
    }
}