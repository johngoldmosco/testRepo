using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_member_WalletDetails : System.Web.UI.Page
{
    form_func obj = new form_func();
    ODBC clsOdbc = new ODBC();
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("../../login.aspx");
        }
        if (!IsPostBack)
        {
            lblWallet1.Text = clsOdbc.executeScalar_str("SELECT wallet1 FROM mlm_my_balance_current WHERE userid='" + Session["UserID"] + "'");
            lblWallet2.Text = clsOdbc.executeScalar_str("SELECT wallet2 FROM mlm_my_balance_current WHERE userid='" + Session["UserID"] + "'");

        //    lblWallet2.Text = clsOdbc.executeScalar_str("SELECT wallet2 FROM mlm_my_balance_current WHERE userid='" + Session["UserID"] + "'");
            //lblWallet3.Text = clsOdbc.executeScalar_str("SELECT wallet3 FROM mlm_my_balance WHERE userid='" + Session["UserID"] + "'");

            //lblWallet11.Text = clsOdbc.executeScalar_str("SELECT wallet1 FROM mlm_my_balance_current WHERE userid='" + Session["UserID"] + "'");
            //lblWallet12.Text = clsOdbc.executeScalar_str("SELECT wallet2 FROM mlm_my_balance_current WHERE userid='" + Session["UserID"] + "'");
            //lblWallet13.Text = clsOdbc.executeScalar_str("SELECT wallet3 FROM mlm_my_balance_current WHERE userid='" + Session["UserID"] + "'");
        }
    }

   
}