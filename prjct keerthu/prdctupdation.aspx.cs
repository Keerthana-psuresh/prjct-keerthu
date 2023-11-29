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
    public partial class prdctupdation : System.Web.UI.Page
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
            string sel = "select * from Prdct_Tab";
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
            TextBox txt1catna = (TextBox)row.FindControl("TextBox6");
            FileUpload fu = (FileUpload)row.FindControl("FileUpload1");
            Label lbl1 = (Label)row.FindControl("Label8");
            TextBox txt2price = (TextBox)row.FindControl("TextBox8");
            TextBox txt3des = (TextBox)row.FindControl("TextBox7");
            TextBox txt4status= (TextBox)row.FindControl("TextBox9");
            TextBox txt5stock = (TextBox)row.FindControl("TextBox10");
            Random rand = new Random((int)DateTime.Now.Ticks);
            int numIterations = 0;
            numIterations = rand.Next(1, 1000);

            fu.SaveAs(Server.MapPath("~/Pictures/") + numIterations + Path.GetFileName(fu.FileName));
            string link = "Pictures/" + numIterations + Path.GetFileName(fu.FileName);
            string upd = "update Prdct_Tab set P_Name='" + txt1catna.Text + "',P_Image='" + link + "',P_Description='" + txt3des.Text + "',P_Price=" + txt2price.Text + ",P_Status='" + txt4status.Text + "',P_Stock='" + txt5stock.Text + "' where Cat_Id=" + lbl1 + "";
            int k = obj.fun_nonquery(upd);

            GridView1.EditIndex = -1;
            grid_bind();

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int i = e.RowIndex;
            int id = Convert.ToInt32(GridView1.DataKeys[i].Value);
            string del = "delete from Prdct_Tab where P_Id= " + id + "";
            int j = obj.fun_nonquery(del);
            string str4 = "select P_Price from Prdct_Tab where P_Id=" + id + "";
            string Productprice = obj.fun_scalar(str4);
            Session["P_Price"] = Productprice;
            grid_bind();
        }
    }
}
