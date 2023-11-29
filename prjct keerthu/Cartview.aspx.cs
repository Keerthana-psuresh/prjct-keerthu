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
    public partial class Cartview : System.Web.UI.Page
    {
        ConnectionClass1 obj = new ConnectionClass1();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grid_bind();
            }
        }

        public void grid_bind()
        {
            string st = "select count(Cart_Id) from CARTTAB where Reg_Id=" + Session["UserId"] + "";
            string count_id = obj.fun_scalar(st);

            if (count_id == "0")
            {

                Label9.Visible = true;
                Label9.Text = "Your Cart Is Empty!";
                Label7.Visible = false;
                Label8.Visible = false;
                Button1.Visible = false;
                Button2.Visible = false;
                GridView1.Visible = false;


            }
            else
            {
                string str = "select CARTTAB.*,Prdct_Tab.P_Name,Prdct_Tab.P_Image from CARTTAB join Prdct_Tab on CARTTAB.P_Id = Prdct_Tab.P_Id where Reg_Id=" + Session["UserId"] + "";
                SqlDataReader dr = obj.fun_exereader(str);
                DataSet ds = obj.fn_dataset(str);
                GridView1.DataSource = ds;
                GridView1.DataBind();
                string st1 = "select sum(Total_Price) from Carttab where Reg_Id=" + Session["UserId"] + "";
                string Gtotal = obj.fun_scalar(st1);
                if (Gtotal != "0")
                {
                    Label8.Visible = true;
                    Button1.Visible = true;
                    Button2.Visible = true;
                    Label8.Text = Gtotal;
                    Session["G_Total"] = Gtotal;
                }
            }
        }

    

    protected void LinkButton1_Command(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            string del = "delete from CARTTAB where Cart_Id=" + id + "";
            int i = obj.fun_nonquery(del);
            grid_bind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int sum = 0;
            string sel = "select max(Cart_Id) from CARTTAB";
            string maxid = obj.fun_scalar(sel);
            int maxcartid = Convert.ToInt32(maxid);
            int cart_id = 0, prdt_id = 0, reg_id = 0, qnty = 0, tprice = 0;
            for (int i = 1; i <= maxcartid; i++)
            {
                string sel1 = "select * from CARTTAB where Cart_Id=" + i + " ";
                SqlDataReader dr = obj.fun_exereader(sel1);
                while (dr.Read())
                {
                    cart_id = Convert.ToInt32(dr["Cart_Id"].ToString());
                    prdt_id = Convert.ToInt32(dr["P_Id"].ToString());
                    reg_id = Convert.ToInt32(dr["Reg_Id"].ToString());
                    qnty = Convert.ToInt32(dr["Quantity"].ToString());
                    tprice = Convert.ToInt32(dr["Total_Price"].ToString());
                }
                string r = Session["UserId"].ToString();
                string u = reg_id.ToString();
                if (u == r)
                {
                    string s = "select max(Order_Id) from Order_Tab";
                    string s1 = obj.fun_scalar(s);
                    int ord_id = 0;
                    if (s1 == "")
                    {
                        ord_id = 1;
                    }
                    else
                    {
                        int newregid = Convert.ToInt32(s1);
                        ord_id = newregid + 1;
                    }
                    string oin = "insert into Order_Tab values(" + ord_id + "," + cart_id + "," + prdt_id +"," + reg_id + "," + qnty + "," + tprice + ",'Active')";
                    int s2 = obj.fun_nonquery(oin);
                    if (s2 != 0)
                    {
                        string del1 = "delete from CARTTAB where Cart_Id=" + cart_id + "";
                        int j = obj.fun_nonquery(del1);
                    }
                }
            }
            string s3 = "select sum(Total_Price) from Order_Tab where Reg_Id=" + Session["UserId"] + "";
            string t = obj.fun_scalar(s3);
            sum = Convert.ToInt32(t);
            string s4 = "select max(Bill_Id) from Bill_Tab";
            string s5 = obj.fun_scalar(s4);
            int bills_id = 0;
            if (s5 == "")
            {
                bills_id = 1000;
            }
            else
            {
                int newregid = Convert.ToInt32(s5);
                bills_id = newregid + 1;
            }
            Session["billid"] = bills_id;
            var date1 = DateTime.Now.ToShortDateString();
            string newdate = Convert.ToDateTime(date1).ToString("yyyy-MM-dd");
            string bin = "insert into Bill_Tab values(" + bills_id + "," + reg_id + ",'" + newdate + "'," + Session["G_Total"] + ",'processing')";
            int s6 = obj.fun_nonquery(bin);
            Response.Redirect("User_Order.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("User_Home.aspx");
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            grid_bind();
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs
        e)
        {
            GridView1.EditIndex = -1;
            grid_bind();
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int index = e.RowIndex;
            int id = Convert.ToInt32(GridView1.DataKeys[index].Value);
            TextBox texqty = (TextBox)GridView1.Rows[index].Cells[2].Controls[0];
            //string s = "update Cart_Tab set Qnty='" + texqty.Text + "' where Cart_Id=" + id + "";
            int i = Convert.ToInt32(Session["P_Price"]);
            int j = Convert.ToInt32(texqty.Text);
            int k = i * j;
            string s = "update CARTTAB set Quantity='" + texqty.Text + "',Total_Price=" + k + "where Cart_Id = " + id + "";
            int m = obj.fun_nonquery(s);
            GridView1.EditIndex = -1;
            grid_bind();
        }
    }
}
    

    

