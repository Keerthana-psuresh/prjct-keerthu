﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace prjct_keerthu
{
    public partial class adminreg : System.Web.UI.Page
    {
        ConnectionClass1 obj = new ConnectionClass1();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sel = "select max(Reg_Id) from Log_Tab";
            string regid = obj.fun_scalar(sel);
            int reg_id = 0;
            if (regid == "")
            {
                reg_id = 1;
            }
            else
            {
                int newregid = Convert.ToInt32(regid);
                reg_id = newregid + 1;
            }
            string ins = "insert into Admin_tab values(" + reg_id + ",'" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "')";
            int i = obj.fun_nonquery(ins);
            if (i != 0)
            {
                string inslog = "insert into Log_Tab values(" + reg_id + ",'" + TextBox5.Text + "','" + TextBox6.Text + "','Admin','active')";
                int j = obj.fun_nonquery(inslog);

            }    
        }
    }
}