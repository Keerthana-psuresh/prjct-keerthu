using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace prjct_keerthu
{
    public partial class User_Bill : System.Web.UI.Page
    {
        ConnectionClass1 obj = new ConnectionClass1();
        protected void Page_Load(object sender, EventArgs e)
        {
            string s1 = "select Total_Amount from Bill_Tab where Bill_Id='" + Session["billid"] + "'";
            string s2 = obj.fun_scalar(s1);
            TextBox4.Text = s2;
            string s3 = "select * from User_tab where User_Id=" + Session["UserId"] + "";
            SqlDataReader dr = obj.fun_exereader(s3);
            while (dr.Read())
            {
                TextBox1.Text = dr["User_Name"].ToString();
                TextBox2.Text = dr["Email"].ToString();
                TextBox3.Text = dr["Address"].ToString() + " " + dr["District"].ToString() + '\n' + dr["State"].ToString() + " " + dr["Pincode"].ToString();
            }
        }


    

    protected void Button1_Click(object sender, EventArgs e)
        {
            PaymentService.ServiceClient ob = new PaymentService.ServiceClient();
            string bal = ob.balancecheck(TextBox6.Text);
            int T_amount = Convert.ToInt32(TextBox4.Text);
            int balance = Convert.ToInt32(bal);
            if (balance >= T_amount)
            {
                int nwbal = balance - T_amount;
                string newbal = Convert.ToString(nwbal);
                PaymentService.ServiceClient ob1 = new PaymentService.ServiceClient();
                int updatedbal = ob1.balanceupdate(TextBox6.Text, newbal);
                string s1 = "update Order_Tab set Status='paid' where Reg_Id='" + Session["UserId"] + "'";
                int j = obj.fun_nonquery(s1);
                string sel = "select max(Order_Id) from Order_Tab";
                string maxid = obj.fun_scalar(sel);
                int maxcartid = Convert.ToInt32(maxid);
                int reg_id = 0, qnty = 0, prdt_id = 0;
                string status = " ", nwstock = "";
                for (int i = 1; i <= maxcartid; i++)
                {
                    string sel1 = "select * from Order_Tab where Order_Id=" + i + " ";
                    SqlDataReader dr = obj.fun_exereader(sel1);
                    while (dr.Read())
                    {
                        prdt_id = Convert.ToInt32(dr["P_Id"].ToString());
                        reg_id = Convert.ToInt32(dr["Reg_Id"].ToString());
                        qnty = Convert.ToInt32(dr["Quantity"].ToString());
                        status = dr["Status"].ToString();
                    }
                    string r = Session["UserId"].ToString();
                    string u = reg_id.ToString();
                    if (u == r)
                    {
                        if (status == "paid")
                        {
                            string s2 = "select P_Stock from Prdct_Tab where P_Id='" + prdt_id + "'";
                            string s3 = obj.fun_scalar(s2);
                            int k = Convert.ToInt32(s3);
                            if (k > qnty)
                            {
                                int stock = k - qnty;
                                nwstock = stock.ToString();
                            }
                            else
                            {
                                nwstock = "0";
                            }
                            string s4 = "update Prdct_Tab set P_Stock=" + nwstock + " where P_Id='" + prdt_id + "'";
                            int j1 = obj.fun_nonquery(s4);
                        }
                    }
                }
                Response.Redirect("User_Bill_Success.aspx");
            }
            else
            {
                Label9.Visible = true;
            }
        }
    }
}


        
    
