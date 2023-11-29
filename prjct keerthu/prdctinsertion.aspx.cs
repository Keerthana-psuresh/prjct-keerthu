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
    public partial class prdctinsertion : System.Web.UI.Page
    {
        ConnectionClass1 obj = new ConnectionClass1();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string str1 = "select Cat_Id,Cat_Name from cat_tab";

                DataSet ds = obj.fn_dataset(str1);
                DropDownList1.DataSource = ds;
                DropDownList1.DataTextField = "Cat_Name";
                DropDownList1.DataValueField = "Cat_Id";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, "-select-");

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string p = "~/Pictures/" + FileUpload1.FileName;
            FileUpload1.SaveAs(MapPath(p));

            string str2 = "insert into Prdct_Tab values('" + DropDownList1.SelectedItem.Value + "','" + TextBox1.Text + "','" + TextBox2.Text + "','" + p + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "')";
            int i = obj.fun_nonquery(str2);
        }

    }
    
}
