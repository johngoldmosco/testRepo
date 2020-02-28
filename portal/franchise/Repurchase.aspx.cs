using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_franchise_Repurchase : System.Web.UI.Page
{
    ODBC objOdbc = new ODBC();
    ClassOther objother = new ClassOther();
    clsWallet objWallet = new clsWallet();
    string strProdilist = "";

    double s;

    static int intTotalQuantity = 0;
    static int intFinalQty = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FransID"] == null)
        {
            Response.Redirect("../../Login.aspx");
        }

        if (!IsPostBack)
        {

            DataTable dt2 = new DataTable(); 

            dt2 = objOdbc.getDataTable("SELECT id, pin_type FROM mlm_epin_type WHERE prod_category=2");
            CheckBoxList1.DataSource = dt2;
            CheckBoxList1.DataTextField = "pin_type";
            CheckBoxList1.DataValueField = "id";
            CheckBoxList1.DataBind();
        }
    }

    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtProducts = new DataTable();
        try
        {
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected)
                {
                    strProdilist = strProdilist + CheckBoxList1.Items[i].Value + ",";
                }
            }
            strProdilist = strProdilist.TrimEnd(',');

            dtProducts = objOdbc.getDataTable("SELECT id, pin_type as prod_name, description as prod_desc, epin_cost as prod_price, bv as pv, 10 AS AvlQty, 1 AS SelQty, created_on FROM mlm_epin_type WHERE Active=1 AND FIND_IN_SET(id ,'" + strProdilist + "')");
            gvProducts.DataSource = dtProducts;
            gvProducts.DataBind();

            DataTable dt = new DataTable();

            dt = objOdbc.getDataTable("SELECT SUM(epin_cost) AS price , SUM(bv) AS pv, SUM(qty) as qty FROM  mlm_epin_type WHERE 1  AND FIND_IN_SET(id ,'" + strProdilist + "')");
            if (dt.Rows.Count > 0)
            {
                txtTotalAmount.Text = dt.Rows[0][0].ToString();
                txtTotalBV.Text = dt.Rows[0][1].ToString();
                intTotalQuantity = Convert.ToInt32(dt.Rows[0][2].ToString());
                intFinalQty = intTotalQuantity;
            }
        }
        catch (Exception ex) { }
        finally
        {
            dtProducts.Dispose();
        }
    }

    protected void TextBoxQty_TextChanged(object sender, EventArgs e)
    {
        TextBox txtbox = (TextBox)sender;
        double dblGrandTotal = 0.00;
        double dblTotalBv = 0.00;
        intFinalQty = 0;
        //GridViewRow Grow = (GridViewRow)txtbox.NamingContainer;
        foreach (GridViewRow Grow in gvProducts.Rows)
        {
            if (txtbox.Text != null)
            {
                Label Price = (Label)Grow.FindControl("prod_price");
                TextBox Quantity = (TextBox)Grow.FindControl("TextBoxQty");
                double s1 = Convert.ToDouble(Grow.Cells[2].Text);
                double s2 = Convert.ToDouble(Quantity.Text);
                double s3 = s1 * s2;

                double s4 = Convert.ToDouble(Grow.Cells[3].Text);
                double s5 = s4 * s2;
                Grow.Cells[5].Text = s3.ToString();
                dblGrandTotal = dblGrandTotal + s3;
                dblTotalBv = dblTotalBv + s5;
                int s6 = Convert.ToInt32(Quantity.Text);
                intTotalQuantity = s6;
                txtTotalAmount.Text = dblGrandTotal.ToString();
                txtTotalBV.Text = dblTotalBv.ToString();
                intFinalQty = intFinalQty + intTotalQuantity;
            }
            CheckStock();
        }
    }

    /* Get data from gridview */
    private void getGridInfo(int intUserID, int intorderID)
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new System.Data.DataColumn("ID", typeof(String)));
        dt.Columns.Add(new System.Data.DataColumn("ProductCode", typeof(String)));
        dt.Columns.Add(new System.Data.DataColumn("ProductName", typeof(String)));
        dt.Columns.Add(new System.Data.DataColumn("Price", typeof(String)));
        dt.Columns.Add(new System.Data.DataColumn("BV", typeof(String)));
        dt.Columns.Add(new System.Data.DataColumn("Quantity", typeof(int)));
        dt.Columns.Add(new System.Data.DataColumn("NetPrice", typeof(String)));

        foreach (GridViewRow row in gvProducts.Rows)
        {
            Label ProdCode = (Label)row.FindControl("id");
            Label ProdName = (Label)row.FindControl("prod_name");
            Label Price = (Label)row.FindControl("prod_price");
            TextBox Quantity = (TextBox)row.FindControl("TextBoxQty");
            Label TotalPrice = (Label)row.FindControl("lbltotalPrice");
            Label GrandPrice = (Label)row.FindControl("lblgrandtotal");
            dr = dt.NewRow();
            dr[0] = row.Cells[6].Text;                      //ID
            dr[1] = row.Cells[0].Text;                     //code
            dr[2] = row.Cells[1].Text;                     //Prod Name
            dr[3] = row.Cells[2].Text;                    //Price
            dr[4] = row.Cells[3].Text;                     //BV
            dr[5] = Convert.ToInt16(Quantity.Text);       // row.Cells[4].Text;
            dr[6] = row.Cells[5].Text;                     //NetPrice         

            objOdbc.executeNonQuery("INSERT INTO mlm_order_product(order_id, product_id, product_name, product_qty, product_bv, price, total_bv, total_price,created_on)VALUES(" + intorderID + ", " + row.Cells[6].Text + ", '" + row.Cells[1].Text + "', " + Convert.ToInt16(Quantity.Text) + ", " + row.Cells[3].Text + ", " + row.Cells[2].Text + ", '" + txtTotalBV.Text + "', '" + txtTotalAmount.Text + "' , '" + objWallet.getCurDateTimeString() + "')");
        }

    }

    protected void btnRepurchase_Click(object sender, EventArgs e)
    {
        CheckStock();
    
        int intUserID = objOdbc.executeScalar_int("SELECT userid FROM mlm_login WHERE my_sponsar_id='" + txtUserID.Text + "'");

        double dblUserRepWallet = objOdbc.executeScalar_dbl("SELECT wallet3 FROM mlm_my_balance_current WHERE userid=" + intUserID + "");

        if (txtUserID.Text != "")
        {
            if (double.Parse(txtTotalAmount.Text) >0)
            {
                for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                {
                    if (CheckBoxList1.Items[i].Selected)
                    {
                        strProdilist = strProdilist + CheckBoxList1.Items[i].Value + ",";
                    }
                }
                strProdilist = strProdilist.TrimEnd(',');

                Random rnd = new Random();
                int transno = rnd.Next(1, 999999); // creates a number between 1 and 12

                int intTransCount = objOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_repurchase WHERE transID='" + transno + "'");
                while (intTransCount != 0)
                {
                    transno = rnd.Next(1, 999999);
                    intTransCount = objOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_repurchase WHERE transID='" + transno + "'");
                }

                string strQuery = "CALL repurchase(" + intUserID + ",'" + transno + "','" + strProdilist + "', " + intFinalQty + ", " + txtTotalAmount.Text + "," + txtTotalBV.Text + ",'' )";
                 
                int intorderID = objOdbc.executeScalar_int(strQuery);
                objOdbc.executeNonQuery("UPDATE mlm_repurchase set franchise_id='" + Session["FransID"] + "' WHERE id='" + intorderID + "'");
                getGridInfo(intUserID, intorderID); 
                 
                UpdateStock();
                CommonMessages.ShowAlertMessage_Reload("Repurchase successful", "Dashboard.aspx");
            }
            else
            {
                CommonMessages.ShowAlertMessage("You have insufficient balance! Please contact to administrator.");
            }
        }
        else
        {
            CommonMessages.ShowAlertMessage("Please enter userid");
        }
    }

    protected void gvProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
        e.Row.Cells[6].Visible = false;
    }

    protected void txtUserID_TextChanged(object sender, EventArgs e)
    {
        int intCount = objOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_login WHERE my_sponsar_id='" + txtUserID.Text + "'");

        if (intCount == 1)
        {
            int intUserID = objOdbc.executeScalar_int("SELECT userid FROM mlm_login WHERE my_sponsar_id='" + txtUserID.Text + "'");
            txtUserName.Text = objOdbc.executeScalar_str("SELECT username FROM mlm_personal_details WHERE userid=" + intUserID + "");          
        }
        else
        {
            txtUserID.Text = "";
            txtUserName.Text = "";
            txtUserID.BorderColor = System.Drawing.Color.Red;
        }
    }

    protected void CheckStock()
    {
        foreach (GridViewRow Grow in gvProducts.Rows)
        {
            Label ID = (Label)Grow.FindControl("product_id");
            Label ProdName = (Label)Grow.FindControl("product_name");
            TextBox Quantity = (TextBox)Grow.FindControl("TextBoxQty");

            int intID = Int32.Parse(Grow.Cells[0].Text);

            int intStock = objOdbc.executeScalar_int("SELECT stock FROM mlm_franchise_stock WHERE franchise_id='" + Session["FransID"] + "' AND product_id='" + intID + "'");

            if (intStock < int.Parse(Quantity.Text))
            {
                //  CommonMessages.ShowAlertMessage_Reload("You have insufficient stock for selected product!", "Repurchase.aspx");
                lblError1.Text = "You have Not sufficient stock for selected product!";
                btnRepurchase.Enabled = false;

            }
            else if (intStock == 0)
            {
                //    CommonMessages.ShowAlertMessage_Reload("You have insufficient stock for selected product!", "Repurchase.aspx");
                lblError1.Text = "You have Not sufficient stock for selected product!";
                btnRepurchase.Enabled = false;
            }
            else
            {
                //   CommonMessages.ShowAlertMessage("You have sufficient stock for selected product!");
                lblError1.Text = "";
                btnRepurchase.Enabled = true;
            }
        }
    }
    protected void UpdateStock()
    {
        foreach (GridViewRow Grow in gvProducts.Rows)
        {
            Label ID = (Label)Grow.FindControl("product_id");
            Label ProdName = (Label)Grow.FindControl("product_name");
            TextBox Quantity = (TextBox)Grow.FindControl("TextBoxQty");

            int intID = Int32.Parse(Grow.Cells[0].Text);

            int intStock = objOdbc.executeScalar_int("SELECT stock FROM mlm_franchise_stock WHERE franchise_id='" + Session["FransID"] + "' AND product_id='" + intID + "'");

            if (intStock < 1)
            {
                CommonMessages.ShowAlertMessage_Reload("You have insufficient stock for selected product!", "Repurchase.aspx");
            }
            else
            {
                objOdbc.executeNonQuery("UPDATE mlm_franchise_stock SET  stock= stock-" + int.Parse(Quantity.Text) + " WHERE franchise_id='" + Session["FransID"] + "' AND product_id='" + intID + "'");

                /*     int intDeliveryStatus = objOdbc.executeScalar_int("SELECT delivery_status FROM mlm_order_product WHERE order_id='" + intOrderID + "'");
                     if (intDeliveryStatus == 0)
                     {
                         objOdbc.executeNonQuery("UPDATE mlm_franchise_stock SET  stock= stock-"+ int.Parse(Quantity.Text)+ " WHERE franchise_id='" + Session["FransID"] + "' AND product_id='" + intID + "'");
                     }
                     else
                     {
                         CommonMessages.ShowAlertMessage_Reload("Product is already delivered!", "ProductDelivery.aspx");
                     }
                 */
            }
            //if (intStock == 0)
            //{
            //    CommonMessages.ShowAlertMessage_Reload("You have insufficient stock for selected product!", "Repurchase.aspx");
            //}
        }
    }
}