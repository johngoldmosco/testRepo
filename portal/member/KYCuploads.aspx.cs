using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_member_KYCuploads : System.Web.UI.Page
{
    ODBC clsodbc = new ODBC();
    clsPhoto objphoto = new clsPhoto();
    clsWallet objwallet = new clsWallet();

    protected void Page_Load(object sender, EventArgs e)
    
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("../../Login.aspx");
        }
        else
        {
            try
            {
                string str = "SELECT pan_card, aadhar_card, photo, cheque, kyc_status FROM mlm_kyc_documents WHERE userid=" + Session["UserID"];

                DataTable dt = new DataTable();

                dt = clsodbc.getDataTable(str);

                if (dt.Rows.Count > 0)
                {
                    imgPAN.ImageUrl = dt.Rows[0][0].ToString();
                    imgAdhar.ImageUrl = dt.Rows[0][1].ToString();
                    imgPhoto.ImageUrl = dt.Rows[0][2].ToString();
                    imgCheque.ImageUrl = dt.Rows[0][3].ToString();
		//			imgAdharBack.ImageUrl = dt.Rows[0][5].ToString();
                }

                int intKYCstatus= int.Parse(dt.Rows[0][4].ToString());
                if (intKYCstatus==1)
                {
                    CommonMessages.ShowAlertMessage_Reload("Congratulations! Your account is KYC verified.","Overview.aspx");
                }
            }
                
            catch (Exception ex)
            {
    //            CommonMessages.ShowAlertMessage(ex.Message);
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string strphoto = "", strpancard = "", straadharcard = "", straadharcard2="", strCheque = "", strPassbook = "", strImg = "";
        try
        {
            if (fu_photo.HasFile)
            {
                strImg = objphoto.UploadPhoto(fu_photo);
                fu_photo.SaveAs(Server.MapPath("../image/kyc_docs/") + strImg);
                strphoto = "../image/kyc_docs/" + strImg;
            }
            else
                strphoto = imgPhoto.ImageUrl;

            if (fu_pan_card.HasFile)
            {
                string strImg1 = objphoto.UploadPhoto(fu_pan_card);
                fu_pan_card.SaveAs(Server.MapPath("../image/kyc_docs/") + strImg1);
                strpancard = "../image/kyc_docs/" + strImg1;
            }
            else
                strpancard = imgPAN.ImageUrl;

            if (fu_aadhar_card.HasFile)
            {
                string strImg2 = objphoto.UploadPhoto(fu_aadhar_card);
                fu_aadhar_card.SaveAs(Server.MapPath("../image/kyc_docs/") + strImg2);
                straadharcard = "../image/kyc_docs/" + strImg2;
            }
            else
                straadharcard = imgAdhar.ImageUrl;
			
            //if (fu_aadhar_cardBack.HasFile)
            //{
            //    string strImgB = objphoto.UploadPhoto(fu_aadhar_cardBack);
            //    fu_aadhar_cardBack.SaveAs(Server.MapPath("../image/kyc_docs/") + strImgB);
            //    straadharcard2 = "../image/kyc_docs/" + strImgB;
            //}
            //else
            //    straadharcard2 = imgAdharBack.ImageUrl;

            if (fileCheque.HasFile)
            {
                string strImg3 = objphoto.UploadPhoto(fileCheque);
                fileCheque.SaveAs(Server.MapPath("../image/kyc_docs/") + strImg3);
                strCheque = "../image/kyc_docs/" + strImg3;
            }
            else
                strCheque = imgCheque.ImageUrl;

            int intCount = clsodbc.executeScalar_int("SELECT COUNT(1) FROM mlm_kyc_documents WHERE userid = " + Session["UserID"] + "");

            if (intCount == 0)
            {
                clsodbc.executeNonQuery("Insert into mlm_kyc_documents(userid, pan_card, aadhar_card, photo, cheque, kyc_status, kyc_on) values ( " + Session["UserID"] + ",'" + strpancard + "','" + straadharcard + "','" + strphoto + "','" + strCheque + "', 0,'" + objwallet.getCurDateTimeString() + "' )");
            }
            else
            {
                clsodbc.executeNonQuery("UPDATE mlm_kyc_documents SET pan_card = '" + strpancard + "', aadhar_card = '" + straadharcard + "', photo = '" + strphoto + "', cheque ='" + strCheque + "', kyc_status =0, kyc_on = '" + objwallet.getCurDateTimeString() + "' WHERE userid=" + Session["UserID"] + "");
            }

            clsodbc.executeNonQuery("update mlm_login set kyc_status=0 where userid= " + Session["UserID"] + "");

            CommonMessages.ShowAlertMessage_Reload("KYC Document Upload Successfully", "overview.aspx");

        }
        catch (Exception es)
        {
            CommonMessages.ShowAlertMessage(es.Message);
        }

    }
}