using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    ClassOther objother = new ClassOther();
    ODBC clsOdbc = new ODBC();

    protected void Page_Load(object sender, EventArgs e)
    {
		//Response.Redirect("index.aspx");
        if (!IsPostBack)
        {
            if (Session["UserID"] != null)
            {
                Response.Redirect("portal/member/overview.aspx");
            }
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string strQuery1 = null;
        string strQuery2 = null;
        string strUserID = null;
        string strUserTypeID = null;

        string strDateTime = string.Empty;
        clsEpin objEpin = new clsEpin();
        clsWallet objWallet = new clsWallet();
        strDateTime = objWallet.getCurDateTimeString();

        // if (ctrlGoogleReCaptcha.Validate())
        // {
        strQuery1 = "SELECT count(1) From mlm_login where status = 1 and my_sponsar_id='" + clsOdbc.GetStringForSQL(txtUserID.Text) + "' and password='" + clsOdbc.GetStringForSQL(txtPwd.Text) + "'";

        int intCount = clsOdbc.executeScalar_int(strQuery1);


        if (intCount > 0)
        {
            System.Data.DataSet ds = new System.Data.DataSet();

            strQuery2 = "SELECT a.userid,a.UserTypeId From mlm_login a Where a.status = 1 and a.my_sponsar_id='" + clsOdbc.GetStringForSQL(txtUserID.Text) + "' and a.password='" + clsOdbc.GetStringForSQL(txtPwd.Text) + "'";

            try
            {
                ds = clsOdbc.getDataSet(strQuery2);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    strUserID = ds.Tables[0].Rows[0][0].ToString();
                    strUserTypeID = ds.Tables[0].Rows[0][1].ToString();

                    string strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    objother.AddLoginHistory(Int32.Parse(strUserID), strIPAddress);


                    if (strUserTypeID == "1")
                    {
                        Session["AdminID"] = strUserID;
                        Response.Redirect("portal/admin/dashboard.aspx");
                    }
                    else if (strUserTypeID == "2")
                    {
                        Session["UserID"] = strUserID;
                        Response.Redirect("portal/member/overview.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                ds.Dispose();
            }
        }
        else
        {
            CommonMessages.ShowAlertMessage("Entered username or Password is Wrong!");
        }
        /*  }
          else
          {
              CommonMessages.ShowAlertMessage("Select Captcha!");
          }*/
    }
}