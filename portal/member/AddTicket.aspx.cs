using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_member_AddTicket : System.Web.UI.Page
{
    ODBC objOdbc = new ODBC();
    clsWallet objWallet = new clsWallet();
    clsPhoto objPhoto = new clsPhoto();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            Random rnd = new Random();
            int intTicketID = rnd.Next(1, 99999);

            int intCount = objOdbc.executeScalar_int("SELECT COUNT(1) FROM tbl_ticket WHERE ticket_id = " + intTicketID + "");

            while (intCount != 0)
            {
                intTicketID = rnd.Next(1, 99999);
                intCount = objOdbc.executeScalar_int("SELECT COUNT(1) FROM tbl_ticket WHERE ticket_id = " + intTicketID + "");
            }

            string strFileAttach = null;
            if (flupAttach.HasFile)
            {
                string strImg = objPhoto.UploadPhoto(flupAttach);
                flupAttach.SaveAs(Server.MapPath("../image/Support/") + strImg);
                strFileAttach = "../image/Support/" + strImg;
            }

            objOdbc.executeNonQuery("INSERT INTO `tbl_ticket`( `userid`, `ticket_id`, `subject`, `department`, `priority`, `message`, `attachment`, `ticket_on`, `status`, `Active`) VALUES ('" + Session["UserID"] + "', " + intTicketID + ", '" + txtSubject.Text + "', '" + ddlCategory.SelectedValue + "', '" + ddlPriority.SelectedValue + "', '" + txtMessage.Text + "', '" + strFileAttach + "', '" + objWallet.getCurDateTimeString() + "',1,1)");
            CommonMessages.ShowAlertMessage_Reload("Ticket submitted successfully!", "TicketManager.aspx");


        }
        catch (Exception ex)
        { }
    }
}