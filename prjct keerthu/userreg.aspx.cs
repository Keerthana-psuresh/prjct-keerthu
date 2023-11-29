using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace prjct_keerthu
{
    public partial class userreg : System.Web.UI.Page
    {
        ConnectionClass1 obj = new ConnectionClass1();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

     

        protected void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            string sel = "select max(Reg_Id) From Log_Tab";
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
            string ins = "insert into User_tab values(" + reg_id + ",'" + TextBox9.Text + "','" + TextBox10.Text + "','" + DropDownList1.SelectedItem.Text + "','" + TextBox11.Text + "','" + TextBox12.Text + "','" + TextBox13.Text + "','" + TextBox14.Text + "')";
            int i = obj.fun_nonquery(ins);
            string inslog = "insert into Log_Tab values(" + reg_id + ",'" + TextBox15.Text + "','" + TextBox16.Text + "','User','active')";
            int j = obj.fun_nonquery(inslog);
        }

        protected void TextBox14_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox13_TextChanged(object sender, EventArgs e)
        {

        }
    }
}   


        