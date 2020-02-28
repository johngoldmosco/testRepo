using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_Add_Promotion : System.Web.UI.Page
{
    ODBC clsodbc = new ODBC();
    clsPhoto objphoto = new clsPhoto();
    clsWallet objWallet = new clsWallet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminID"] == null)
        {
            Response.Redirect("../../login.aspx");
        }
        if (!IsPostBack)
        {
            // portal/images/promotion
        }
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlType.SelectedValue!= "Select")
            {
                if (ddlType.SelectedValue == "1")
                { // Image
                    divImage.Visible = true;
                    divYoutube.Visible = false;
                    divDocument.Visible = false;
                }
                if (ddlType.SelectedValue == "2")
                { // Youtube Link
                    divImage.Visible = false ;
                    divYoutube.Visible = true ;
                    divDocument.Visible = false;
                }
                if (ddlType.SelectedValue == "3")
                {
                    // Document
                    divImage.Visible = false ;
                    divYoutube.Visible = false;
                    divDocument.Visible = true ;
                }
            }
        }
        catch (Exception ex) { }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlType.SelectedValue != "Select")
            {
                if (ddlType.SelectedValue == "1")
                { // Image
                    string strPhoto = "";
                    if (fileImage.HasFile)
                    {
                        string strImg = objphoto.UploadPhoto(fileImage);
                        fileImage.SaveAs(Server.MapPath("../images/promotion/") + strImg);
                        strPhoto = "../images/promotion/" + strImg;

                        clsodbc.executeNonQuery("INSERT INTO mlm_promotion(title, p_type, image_path, status, created_on) VALUES('" + txtTitle.Text + "', 1,'" + strPhoto + "', 1, '" + objWallet.getCurDateTimeString() + "')");

                        CommonMessages.ShowAlertMessage_Reload("Image Uploaded", "Add_Promotion.aspx");
                    }
                    else
                    {
                        CommonMessages.ShowAlertMessage("Please kindly upload photo!");
                        fileImage.Focus();
                    }
                }
                if (ddlType.SelectedValue == "2")
                { // Youtube Link
                    if (txtYoutubeLink.Text.Contains("https://www.youtube.com/watch?v="))
                    {
                        clsodbc.executeNonQuery("INSERT INTO mlm_promotion(title, p_type, youtube_link, status, created_on) VALUES('"+ txtTitle.Text +"',2,'" + txtYoutubeLink.Text + "', 1, '" + objWallet.getCurDateTimeString() + "')");

                        CommonMessages.ShowAlertMessage_Reload("Youtube Link Uploaded", "Add_Promotion.aspx");
                    }
                    else
                    {
                        CommonMessages.ShowAlertMessage("Kindly Enter Proper youtube link!");
                        txtYoutubeLink.Text = "";
                        txtYoutubeLink.Focus();
                    }
                }
                if (ddlType.SelectedValue == "3")
                {
                    // Document
                    string[] validFileTypes = { "pdf", "ppt", "pptx", "doc", "docx", "xls" };

                    string ext = System.IO.Path.GetExtension(fileDoc.PostedFile.FileName);

                    bool isValidFile = false;

                    for (int i = 0; i < validFileTypes.Length; i++)
                    {
                        if (ext == "." + validFileTypes[i])
                        {
                            isValidFile = true;
                            break;
                        }
                    }
                    if (!isValidFile)
                    {
                        CommonMessages.ShowAlertMessage("Invalid File. Please upload a File like pdf, ppt, docx ");  
                    }
                    else
                    {
                        string strDoc = "";
                        fileDoc.SaveAs(Server.MapPath("../images/promotion/") + fileDoc.PostedFile.FileName);
                        strDoc = "../images/promotion/" + fileDoc.PostedFile.FileName;

                        clsodbc.executeNonQuery("INSERT INTO mlm_promotion(title, p_type, doc_link, status, created_on) VALUES('"+ txtTitle.Text +"', 3,'" + strDoc + "', 1, '" + objWallet.getCurDateTimeString() + "')");

                        CommonMessages.ShowAlertMessage_Reload("Document Uploaded", "Add_Promotion.aspx");
                    }
                }
            }
            else
            {
                CommonMessages.ShowAlertMessage("Please select Promotion Type!");
                ddlType.Focus();
            }
        }
        catch (Exception ex) { }
    }
}