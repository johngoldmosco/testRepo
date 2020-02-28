using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_LegalUpload : System.Web.UI.Page
{
    ODBC objOdbc = new ODBC();
    clsWallet objWallet = new clsWallet();
    clsPhoto objphoto = new clsPhoto();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminID"] == null)
        {
            Response.Redirect("../../Login.aspx");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strPhoto = "";

        try
        {
            if (flupPhoto.HasFile)
            {
                string strImg = objphoto.UploadPhoto(flupPhoto);
                flupPhoto.SaveAs(Server.MapPath("../image/legal/") + strImg);
                strPhoto = "../image/legal/" + strImg;
            }
            else
            {
                CommonMessages.ShowAlertMessage("Please kindly upload photo!");
                flupPhoto.Focus();
            }

            objOdbc.executeNonQuery("INSERT INTO `tbl_legal`(`imgUrl`, `status`, `created_on`) VALUES ('" + strPhoto + "',1,'" + objWallet.getCurDateTimeString() + "')");

            CommonMessages.ShowAlertMessage_Reload("Certificate Added Successfully!", "LegalManager.aspx");
        }
        catch (Exception ex)
        {

        }
    }
}