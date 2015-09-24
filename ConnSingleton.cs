using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace logNImageConsole
{
    /// <summary>
    /// Singleton design pattern is used to get a connection to the DB
    /// Singleton can be used where there is a need to create only one instance of a given class
    /// </summary>
    
    class ConnSingleton
    {

        
        private static String constr = null; //holds tnsnames.ora statemnt
     


        private static ConnSingleton dbInstance;
        private OracleConnection conn;

        private ConnSingleton()
        {
        }
    

        public static ConnSingleton getDbInstance()
        {
            if (dbInstance == null)
            {

                dbInstance = new ConnSingleton();
            }
            return dbInstance;
        }

        /// <summary>
        /// this returns an oracle connection created using the connection string
        /// </summary>
        /// <returns>oracle connection</returns>
        public OracleConnection GetDBConnection()
        {
            try
            {
                createConnectionstring();
                conn = new OracleConnection(constr);
                conn.Open();
                Console.WriteLine("Connected");
            }
            catch (OracleException e)
            {
                Console.WriteLine("Not connected : " + e.ToString());
                Console.ReadLine();
            }
           
            return conn;
        }

        /// <summary>
        /// this closes the created database connection
        /// </summary>
        public void closeDBConnection()
        {
            try
            {
                conn.Close();
                Console.WriteLine("Connection closed");
            }
            catch (OracleException e)
            {
                Console.WriteLine("Connection closed failed : " + e.ToString());
                
            }
            finally
            {
                Console.WriteLine("End..");
                
            }
           
        }

       /// <summary>
       /// Read the configurations.xml in order to create the connection string
       /// </summary>
        public static void createConnectionstring()
        {

            try
            {
                Console.WriteLine("Reading configurations.....");
                String serverName, databaseName, username, password;

     		//give values to these variables

                constr = "Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = " + serverName + ".corpnet.ifsworld.com)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = " + databaseName + ") ));user id= " + username + ";password=" + password;
                Console.WriteLine("ConstrVariable created ");

            }
            catch (Exception ConstrError)
            {
                Console.WriteLine(ConstrError.Message);
            }





        }
    }
}