using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using System.Configuration;

namespace KpInfohelp.Reports
{
    public partial class KwitUsluga : DevExpress.XtraReports.UI.XtraReport
    {
        public KwitUsluga()
        {
            InitializeComponent();
             string connectionString =  ConfigurationManager.ConnectionStrings["WagaDuzaModel"].ConnectionString;
              efDataSource1.Connection.ConnectionString = connectionString;
        }

    }
}
