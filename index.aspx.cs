using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class index : System.Web.UI.Page
{
	ODBC objOdbc = new ODBC();
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
        {           
            int intPopupStatus = objOdbc.executeScalar_int("SELECT display_status FROM tbl_popup WHERE id=1");
            if (intPopupStatus == 1)
            {
                int intPopupType = objOdbc.executeScalar_int("SELECT popup_type FROM tbl_popup WHERE id=1");
                if (intPopupType == 1)
                {
                    lblPopupHeader.Text = objOdbc.executeScalar_str("SELECT popup_header FROM tbl_popup WHERE id=1");
                    lblPopupBody.Text = objOdbc.executeScalar_str("SELECT content FROM tbl_popup WHERE id=1");
                    imgPopup.Visible = false;
                }
                else if (intPopupType == 2)
                {
                    lblPopupHeader.Text = objOdbc.executeScalar_str("SELECT popup_header FROM tbl_popup WHERE id=1");
                    string strPhoto = objOdbc.executeScalar_str("SELECT SUBSTR(imgUrl,3) FROM tbl_popup WHERE id=1");
                    imgPopup.ImageUrl = "portal/" + strPhoto;
                    lblPopupBody.Visible = false;
                }
                ModalPopupExtender1.Show();

            }
            if (intPopupStatus == 2)
            {
                ModalPopupExtender1.Hide();
            }
        }
    }
}