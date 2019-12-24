using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Configuration;

namespace KpInfohelp
{
    public partial class rptKwit : DevExpress.XtraReports.UI.XtraReport
    {
        public rptKwit()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["WagaDuzaModel"].ConnectionString;
             efDataSource1.Connection.ConnectionString = connectionString;
        }

    }
}
