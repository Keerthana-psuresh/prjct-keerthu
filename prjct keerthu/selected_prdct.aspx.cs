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
    public partial class selected_prdct : System.Web.UI.Page
    {
        ConnectionClass1 obj = new ConnectionClass1();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sel = "select P_Name,P_Image,P_Price,P_Description from Prdct_Tab where P_Id=" + Session["pid"] + "";
                SqlDataReader dr = obj.fun_exereader(sel);
                while (dr.Read())
                {
                    Image1.ImageUrl = dr["P_Image"].ToString();
                    Label1.Text = dr["P_Name"].ToString();
                    Label2.Text = dr["P_Price"].ToString();
                    Label3.Text = dr["P_Description"].ToString();
                }
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string str = "select P_Price from Prdct_Tab where P_Id=" + Session["pid"] + "";
            string s = obj.fun_scalar(str);
            int price = Convert.ToInt32(s);
            int qty = Convert.ToInt32(TextBox1.Text);
            int Tprice = qty * price;

           
            string str1 = "select P_Id from Prdct_Tab where P_Id=" + Session["pid"] + "";
            string i = obj.fun_scalar(str1);
            int pid = Convert.ToInt32(i);

            int uid = Convert.ToInt32(Session["userid"]);


            string str2 = "insert into CARTTAB values(" + pid + "," + uid + ",'" + TextBox1.Text + "'," + Tprice + ")";
            int k = obj.fun_nonquery(str2);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("User_Home.aspx");
        }
    }
}