using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace prjct_keerthu
{
    public partial class catupdation : System.Web.UI.Page
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
            string sel = "select * from cat_tab";
            SqlDataReader dr = obj.fun_exereader(sel);
            DataSet ds = obj.fn_dataset(sel);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            grid_bind();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            grid_bind();

        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int i = e.RowIndex;

            GridViewRow row = (GridViewRow)GridView1.Rows[i];
            TextBox txt1na = (TextBox)row.FindControl("TextBox1");
            FileUpload fu = (FileUpload)row.FindControl("FileUpload1");
            Label lbl1 = (Label)row.FindControl("Label5");
            TextBox txt2descr = (TextBox)row.FindControl("TextBox2");
            TextBox txt3status = (TextBox)row.FindControl("TextBox3");
            Random rand = new Random((int)DateTime.Now.Ticks);
            int numIterations = 0;
            numIterations = rand.Next(1, 1000);

            fu.SaveAs(Server.MapPath("~/Pictures/") + numIterations + Path.GetFileName(fu.FileName));
            string link = "Pictures/" + numIterations + Path.GetFileName(fu.FileName);
            string upd = "update cat_tab set Cat_Name='" + txt1na.Text + "',Cat_Image='" + link + "',Cat_Description='" + txt2descr.Text + "',Cat_Status='" + txt3status.Text + "' where Cat_Id="+lbl1+"";
            int k = obj.fun_nonquery(upd);

            GridView1.EditIndex = -1;
            grid_bind();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int i = e.RowIndex;
            int id = Convert.ToInt32(GridView1.DataKeys[i].Value);
            string del = "delete from cat_tab where Cat_Id= " + id + "";
            int j = obj.fun_nonquery(del);
            grid_bind();
        }
    }
}

    
    


    
    


    
    
