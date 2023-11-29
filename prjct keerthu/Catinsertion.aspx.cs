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
    public partial class Catinsertion : System.Web.UI.Page
    {
        ConnectionClass1 obj = new ConnectionClass1();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string p = "~/Pictures/" + FileUpload1.FileName;
            FileUpload1.SaveAs(MapPath(p));

            string str = "insert into cat_tab values('" + TextBox10.Text + "','" + p + "','" + TextBox11.Text + "','" + TextBox12.Text + "')";
            int s = obj.fun_nonquery(str);

        }
    }

}
    
