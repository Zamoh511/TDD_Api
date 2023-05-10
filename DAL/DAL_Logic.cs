using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TDD_Api.DAL
{
    public class DAL_Logic
    {

        private List<SqlParameter> GenerateSQLParameters(object model)
        {
            var paramList = new List<SqlParameter>();
            Type modelType = model.GetType();
            var properties = modelType.GetProperties();
            foreach (var property in properties)
            {
                if (property.GetValue(model) == null)
                {
                    paramList.Add(new SqlParameter(property.Name, DBNull.Value));
                }
                else
                {
                    paramList.Add(new SqlParameter(property.Name, property.GetValue(model)));
                }
            }
            return paramList;

        }
        public void ExecuteStoredProcedure(string procedureName, object model)
        {
            var parameters = GenerateSQLParameters(model);
            SqlConnection sqlConnObj = new SqlConnection(ConfigurationManager.ConnectionStrings["TDD_ApiContext"].ConnectionString);

            SqlCommand sqlCmd = new SqlCommand(procedureName, sqlConnObj);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            foreach (var param in parameters)
            {
                sqlCmd.Parameters.Add(param);
            }

            sqlConnObj.Open();
            sqlCmd.ExecuteNonQuery();
            sqlConnObj.Close();
          
        }

        public dynamic getProcedureData (string procedureName)
        {
            SqlConnection sqlConnObj = new SqlConnection(ConfigurationManager.ConnectionStrings["TDD_ApiContext"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand(procedureName, sqlConnObj);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = sqlCmd.ExecuteReader();

            List<dynamic> dataObject=new List<dynamic>();
            while (reader.Read())
            {
                dataObject.Add(reader);
            }
            reader.Close();
            return dataObject;
        }
    }
}