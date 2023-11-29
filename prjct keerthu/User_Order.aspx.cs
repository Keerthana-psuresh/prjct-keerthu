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
    public partial class User_Order : System.Web.UI.Page
    {
        ConnectionClass1 obj = new ConnectionClass1();
        protected void Page_Load(object sender, EventArgs e)
        {
            string s = "select Prdct_Tab.P_Name,Prdct_Tab.P_Image,Order_Tab.Quantity,Order_Tab.Total_Price from Prdct_Tab join Order_Tab on Prdct_Tab.P_Id = Order_Tab.P_Id where Reg_Id = " + Session["UserId"] + "";
            DataSet ds = obj.fn_dataset(s);
            DataList1.DataSource = ds;
            DataList1.DataBind();
            string s1 = "select Total_Amount from Bill_Tab where Bill_Id='" + 1000 + "'";
            string s2 = obj.fun_scalar(s1);
            Label12.Text = s2;
            string s3 = "select * from User_tab where User_Id=" + Session["UserId"] + "";
            SqlDataReader dr = obj.fun_exereader(s3);
            while (dr.Read())
            {
                TextBox1.Text = dr["User_Name"].ToString();
                TextBox2.Text = dr["Email"].ToString();
                TextBox3.Text = dr["Phone"].ToString();
                TextBox4.Text = dr["Address"].ToString();
                TextBox5.Text = dr["Pincode"].ToString();
            }
        }

    



    protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("User_Bill.aspx");

        }
    }
}