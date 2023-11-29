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
    public partial class login : System.Web.UI.Page
    {
        ConnectionClass1 obj = new ConnectionClass1();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string str = "select count(Reg_Id) from Log_Tab where Username='" + TextBox1.Text + "'and Password='" + TextBox2.Text + "'";
            string cid = obj.fun_scalar(str);
            int cid1 = Convert.ToInt32(cid);
            if (cid1 == 1)
            {
                string str1 = "select Reg_Id from Log_Tab where Username='" + TextBox1.Text + "'and Password='" + TextBox2.Text + "'";
                string regid = obj.fun_scalar(str1);
                Session["UserId"] = regid;
                string str2 = "select Log_Type from Log_Tab where Username='" + TextBox1.Text + "'and Password='" + TextBox2.Text + "'";
                string logtype = obj.fun_scalar(str2);
                if (logtype == "Admin")
                {
                    Response.Redirect("Admin_Homee.aspx");
                }
                else if (logtype == "User")
                {
                    Response.Redirect("User_Home.aspx");
                }
            }
            else
            {
                Label1.Text = "invalid username and password";
            }

        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

        

        