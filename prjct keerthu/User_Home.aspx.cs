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
    public partial class User_Home : System.Web.UI.Page
    {
        ConnectionClass1 obj = new ConnectionClass1();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                string str = "select * from cat_tab";
                SqlDataReader dr = obj.fun_exereader(str);
                DataSet ds = obj.fn_dataset(str);
                DataList1.DataSource = ds;
                DataList1.DataBind();
            }

        }

      
        

        protected void ImageButton1_Command(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            Session["cid"] = id;
            Response.Redirect("ProductView.aspx");

        }
    }
}
