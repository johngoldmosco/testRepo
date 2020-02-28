using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_ViewTicket : System.Web.UI.Page
{
    ODBC objOdbc = new ODBC();
    clsWallet objWallet = new clsWallet();
    clsPhoto objPhoto = new clsPhoto();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminID"] == null)
        {
            Response.Redirect("../../Login.aspx");
        }
        if (!IsPostBack)
        {
            if (Request.QueryString[0] != "")
            {
                fillchat();
                FillDetails();
            }
        }
    }
    protected void fillchat()
    {
        DataTable dtChat = new DataTable();
        try
        {
            int intTCount = objOdbc.executeScalar_int("SELECT COUNT(1) FROM tbl_reply WHERE ticket_id =" + Request.QueryString[0]);
            string strLitText = "";
            string strMessage, strName, strPhoto, strAttachLink;
            if (intTCount > 0)
            {
                dtChat = objOdbc.getDataTable("SELECT reply_by, message, reply_on AS reply_on, attachment, id FROM tbl_reply WHERE 	ticket_id=" + Request.QueryString[0] + " ORDER BY id DESC");
                if (dtChat.Rows.Count > 0)
                {
                    for (int i = 0; i < dtChat.Rows.Count; i++)
                    {
                        int intUserType = Int32.Parse(dtChat.Rows[i][0].ToString());
                        strMessage = dtChat.Rows[i][1].ToString();
                        strPhoto = dtChat.Rows[i][3].ToString();

                        if (strPhoto == "")
                        {
                            strAttachLink = "";
                        }
                        else
                        {

                            strAttachLink = " <a href=\"" + strPhoto + "\" runat=\"server\" id=\"lnkAttachment\" target=\"_blank\">Attachment</a> ";
                        }
                        if (intUserType == 1)  // Admin
                        {
                            strName = "staff";

                        }
                        else  //User
                        {
                            strName = objOdbc.executeScalar_str("SELECT username FROM mlm_personal_details WHERE userid='" + intUserType + "'");
                        }

                        strLitText = strLitText + "<div class=\"card card-accent-secondary \" > <div class=\"card-header bg-success text-white  \"><i class=\"fa fa-user\"></i>&nbsp;  " + strName + " &nbsp; " + dtChat.Rows[i][2].ToString() + "  </div>  <div class=\"card-body\">  <p class=\"text-dark\">  " + strMessage + "  </p> <p> " + strAttachLink + " </p>  </div>    </div> ";
                    }

                    litReply.Text = strLitText;
                }
            }
        }
        catch (Exception ex)
        {
            CommonMessages.ShowAlertMessage(ex.Message);
        }
    }

    protected void FillDetails()
    {
        string strPhoto;
        DataTable dtTicket = new DataTable();
        try
        {
            int intTCount = objOdbc.executeScalar_int("SELECT COUNT(1) FROM tbl_ticket WHERE ticket_id =" + Request.QueryString[0]);
            if (intTCount == 1)
            {
                dtTicket = objOdbc.getDataTable("SELECT userid, subject, message, attachment, ticket_on AS reply_on FROM tbl_ticket WHERE 	ticket_id=" + Request.QueryString[0] + " ORDER BY id DESC");

                lblUserName.Text = objOdbc.executeScalar_str("SELECT username FROM mlm_personal_details WHERE userid='" + dtTicket.Rows[0][0].ToString() + "'");
                lblDateTime.Text = dtTicket.Rows[0][4].ToString();
                lblTicketMessage.Text = dtTicket.Rows[0][2].ToString();

                strPhoto = dtTicket.Rows[0][3].ToString();

                if (strPhoto == "")
                {
                    lnkAttachment.Visible = false;
                }
                else
                {
                    lnkAttachment.HRef = strPhoto;
                }
            }
        }
        catch (Exception ex)
        {
            CommonMessages.ShowAlertMessage(ex.Message);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtMessage.Text != null)
        {
            string strFileAttach = null;
            if (flupAttach.HasFile)
            {
                string strImg = objPhoto.UploadPhoto(flupAttach);
                flupAttach.SaveAs(Server.MapPath("../image/Support/") + strImg);
                strFileAttach = "../image/Support/" + strImg;
            }

            objOdbc.executeNonQuery("INSERT INTO `tbl_reply`(`ticket_id`, `reply_by`, `message`,`attachment`, `reply_on`) VALUES ('" + Request.QueryString[0] + "', '" + Session["AdminID"] + "', '" + txtMessage.Text + "', '" + strFileAttach + "' ,'" + objWallet.getCurDateTimeString() + "') ");
            objOdbc.executeNonQuery("UPDATE tbl_ticket SET status=2 WHERE ticket_id='" + Request.QueryString[0] + "'");

            CommonMessages.ShowAlertMessage_Reload("Answer submitted successfully!", "TicketManager.aspx");
        }
        else { CommonMessages.ShowAlertMessage("Kindly enter message reply!"); }
    }
    protected void btnHold_Click(object sender, EventArgs e)
    {
        objOdbc.executeNonQuery("UPDATE tbl_ticket SET status=3 WHERE ticket_id='" + Request.QueryString[0] + "'");
        CommonMessages.ShowAlertMessage_Reload("Ticket Hold Successfully!", "TicketManager.aspx");
    }
    protected void btnClosed_Click(object sender, EventArgs e)
    {
        objOdbc.executeNonQuery("UPDATE tbl_ticket SET status=0 WHERE ticket_id='" + Request.QueryString[0] + "'");
        CommonMessages.ShowAlertMessage_Reload("Ticket Closed Successfully!", "TicketManager.aspx");
    }
}