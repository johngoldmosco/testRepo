using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_Tree : System.Web.UI.Page
{
    clsBinaryTree objBinaryTree = new clsBinaryTree();
    ODBC clsOdbc = new ODBC();
    EncryptTest.Encryption Ec = new EncryptTest.Encryption();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminID"] == null)
        {
            Response.Redirect("../../login.aspx");
        }

        if (!IsPostBack)
        {
            int intUserID = 0;

            string strReq = Request.RawUrl;
            string strQueryString = Ec.Decrypt(strReq, "VbFM45Lt");
            intUserID = Convert.ToInt32(strQueryString);

            if (intUserID == 0)
            {
                intUserID = 2;
            }

            string str = objBinaryTree.FillBinaryLiteral(intUserID);
            litPopup.Text = str.Replace("sessionID", "2");
            objBinaryTree.get_Recursive_Popup(intUserID, litPopup);
        }
    }
	protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtUserId.Text != "")
        {
            int intCount = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_login WHERE my_sponsar_id = '" + txtUserId.Text + "' AND Active=1 and status=1");
            if (intCount == 1)
            {
                int intUserID = clsOdbc.executeScalar_int("SELECT userid FROM mlm_login WHERE my_sponsar_id = '" + txtUserId.Text + "' AND Active=1 and status=1");
                Response.Redirect("Tree.aspx?" + Ec.EncryptQueryString(String.Format("userid={0}", intUserID.ToString()), "VbFM45Lt"));
            }
        }
        else
            txtUserId.Focus();
    }
}