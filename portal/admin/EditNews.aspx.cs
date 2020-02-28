using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_EditNews : System.Web.UI.Page
{
    ODBC clsOdbc = new ODBC();
    form_func objFormFun = new form_func();
    ClassOther objOther = new ClassOther();
    clsPhoto objphoto = new clsPhoto();

    static int intID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminID"] == null)
        {
            Response.Redirect("../../Login.aspx");
        }
        if (!IsPostBack)
        {

            string strCurrentUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            string strSubUrl = strCurrentUrl.Substring(strCurrentUrl.LastIndexOf("/") + 15);

            string strDecryptLink = Encryption.ConvertHexToString(strSubUrl, System.Text.Encoding.Unicode); // Decrypt(strSubUrl);

            string strID = strDecryptLink.Substring(4);
            intID = int.Parse(strID);
            FillDetails(sender, e);
        }
    }

    private void FillDetails(object sender, EventArgs e)
    {
        string strQuery = "";
        System.Data.DataSet ds = new System.Data.DataSet();
        System.Data.DataTable dtNominee = new System.Data.DataTable();

        try
        {
            strQuery = "SELECT id,news_title, news_desc, CASE status WHEN 0 THEN 'Hide' WHEN 1 THEN 'SHOW' END AS Status, DATE_FORMAT(created_on, '%Y-%b-%d') AS CreatedOn FROM tbl_news WHERE id=" + intID;

            ds = clsOdbc.getDataSet(strQuery);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtNewsTitle.Text = ds.Tables[0].Rows[0][1].ToString();
                txtDescription.Text = ds.Tables[0].Rows[0][2].ToString();
            }
        }
        catch (Exception ex)
        {
            CommonMessages.ShowAlertMessage(ex.Message);
        }
        finally
        {
            ds.Dispose();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            clsOdbc.executeNonQuery("UPDATE tbl_news SET news_title='" + txtNewsTitle.Text + "', news_desc='" + txtDescription.Text + "' WHERE id=" + intID + "");
            CommonMessages.ShowAlertMessage_Reload("News Edited Successfully!", "NewsManager.aspx");
        }
        catch (Exception ex)
        {

        }
    }
}