using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Gallery : System.Web.UI.Page
{
    ODBC objOdbc = new ODBC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillPhoto();
        }
    }
    private void fillPhoto()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = objOdbc.getDataTable("SELECT `id`, SUBSTR(imgUrl,3), `created_on` FROM `tbl_gallery` WHERE status=1");
            string strGallery = "", strPhoto = "", strClear = "", strID = "";
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strID = dt.Rows[i][0].ToString();
                    strPhoto = "portal/" + dt.Rows[i][1].ToString();
                    //if (i % 4 == 0)
                    //{
                    //    strClear = "<div class=\"clear_fix\"></div>";
                    //}
                    //else
                    //{
                    //    strClear = "";
                    //}
                    strGallery = strGallery + "" + strClear + " <div class=\"col-lg-4 col-md-4 col-sm-6\">  <div class=\"our-team-box aos-item\" data-aos=\"fade-up\" data-aos-duration=\"1000\">  <div class=\"team-img\">  <a href=\"#team-" + strID + "\" class=\"membername\">  <img src=\"" + strPhoto + "\" alt=\"\" />  <div class=\"filter-title\">  </div>  </a>  </div>  </div>  </div> <div style=\"display: none\">  <div class=\"teamdetail\" id=\"team-" + strID + "\">  <div class=\"innerImg\" style=\"background-image: url('" + strPhoto + "'); height: 1000px;\"></div>  </div>  </div> ";
                    lit1.Text = strGallery;
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            dt.Dispose();
        }
    }
}