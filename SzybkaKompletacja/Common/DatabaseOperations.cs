using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpInfohelp
{
   public  class DatabaseOperations : CrudVMBase, INotifyPropertyChanged
    {

        private string connectionString;

        public DatabaseOperations()
        {

        }
 
        public bool TestConnection2()
        {
           try
            {
 
                if (context.Database.Connection.State == ConnectionState.Open)
                {

                    context.Database.Connection.Close();
                   context.Database.Connection.Open();
                }
                else
                {
                    context.Database.Connection.Open();
                }

            }
            catch (SqlException)
            {
                return false;
            }
            catch (Exception ex)
            {
           
                return false;
            }
       
            return true;
        }

    }
}
