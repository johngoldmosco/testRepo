using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_EditPopup : System.Web.UI.Page
{
    ODBC objOdbc = new ODBC();
    clsPhoto objphoto = new clsPhoto();
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminID"] == null)
        {
            Response.Redirect("../../Login.aspx");
        }
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();

            ddlShowPopup.SelectedValue = objOdbc.executeScalar_str("SELECT display_status FROM tbl_popup WHERE id=" + Request.QueryString[0] + "");
            ddlPopupType.SelectedValue = objOdbc.executeScalar_str("SELECT popup_type FROM tbl_popup WHERE id=" + Request.QueryString[0] + "");
            ddlPopupType_SelectedIndexChanged(sender, e);

        }
    }
    protected void btnEditPopUp_Click(object sender, EventArgs e)
    {
        string imgPopup = "";
        try
        {
            if (ddlShowPopup.SelectedValue != "0")
            {
                if (ddlShowPopup.SelectedValue == "1")
                {
                    objOdbc.executeNonQuery("UPDATE tbl_popup SET display_status=1 WHERE id=" + Request.QueryString[0] + "");
                }
                else if (ddlShowPopup.SelectedValue == "2")
                {
                    objOdbc.executeNonQuery("UPDATE tbl_popup SET display_status=2 WHERE id=" + Request.QueryString[0] + "");
                }
            }

            if (ddlPopupType.SelectedValue == "2")
            {
                if (flupImage.HasFile)
                {
                    string strImg = objphoto.UploadPhoto(flupImage);
                    flupImage.SaveAs(Server.MapPath("../image/PopUp/") + strImg);
                    imgPopup = "../image/PopUp/" + strImg;
                }
                else
                {
                 //   CommonMessages.ShowAlertMessage_Reload("Please select image first!", "PopupManager.aspx");
                    imgPopup = objOdbc.executeScalar_str("SELECT imgUrl FROM WHERE id=" + Request.QueryString[0] + "");
                }

                objOdbc.executeNonQuery("UPDATE tbl_popup SET popup_type=2, popup_header='" + txtHeader.Text + "', imgUrl='" + imgPopup + "' WHERE id=" + Request.QueryString[0] + "");
                CommonMessages.ShowAlertMessage_Reload("Popup added successfully!", "PopupManager.aspx");
            }
            else if (ddlPopupType.SelectedValue == "1")
            {
                objOdbc.executeNonQuery("UPDATE tbl_popup SET popup_type=1, popup_header='"+ txtHeader.Text +"', content='" + txtContent.Text + "' WHERE id=" + Request.QueryString[0] + "");
                CommonMessages.ShowAlertMessage_Reload("Popup added successfully!", "PopupManager.aspx");
            }
        }
        catch (Exception ex)
        { }
    }


    protected void ddlPopupType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPopupType.SelectedValue != "0")
        {
            if (ddlPopupType.SelectedValue == "1")
            {
                pnlContent.Visible = true;
                pnlImage.Visible = false;
                txtHeader.Text = objOdbc.executeScalar_str("SELECT popup_header FROM tbl_popup WHERE id=" + Request.QueryString[0] + "");
                txtContent.Text = objOdbc.executeScalar_str("SELECT content FROM tbl_popup WHERE id=" + Request.QueryString[0] + "");
            }
            else
            {
                pnlContent.Visible = false;
                pnlImage.Visible = true;
                txtHeader.Text = objOdbc.executeScalar_str("SELECT popup_header FROM tbl_popup WHERE id=" + Request.QueryString[0] + "");
                imgPopup.ImageUrl = objOdbc.executeScalar_str("SELECT imgUrl FROM tbl_popup WHERE id=" + Request.QueryString[0] + "");
            }
        }
    }

}