using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for clsPhoto
/// </summary>
public class clsPhoto
{
	public clsPhoto()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string UploadPhoto(FileUpload FileUpload1)
    {
        if (FileUpload1.HasFile)
        {
            try
            {


                if (FileUpload1.PostedFile.ContentType == "image/jpg" | FileUpload1.PostedFile.ContentType == "image/jpeg" | FileUpload1.PostedFile.ContentType == "image/png" | FileUpload1.PostedFile.ContentType == "image/bmp" | FileUpload1.PostedFile.ContentType == "image/gif")
                {

                    if (FileUpload1.PostedFile.ContentLength < 3145728)
                    {

                        string strFileName = System.IO.Path.GetFileName(FileUpload1.FileName);
                        DateTime dt = DateTime.Now;
                        string strDate = dt.ToString("dd-MM-yyyy");


                        strFileName = strFileName.Replace(" ", "_");

                        string[] strDateSplit = strDate.Split(' ');
                        strDate = strDateSplit[0];

                        string[] strImgSplit = strFileName.Split('.');
                        strFileName = strImgSplit[0].ToString() + "_" + strDate + "." + strImgSplit[1];
                        //FileUpload1.SaveAs(.MapPath("~/Files/") + strFileName);
                        // clsOdbc.executeNonQuery("UPDATE personal_setting SET company_logo='logo/" + strFileName + "'");
                        //return FileUpload1.FileName;
                        return strFileName;

                    }
                    else
                    {
                        CommonMessages.ShowAlertMessage("Kindly Upload File less than 3 MB!");
                        return null;
                    }
                    return FileUpload1.FileName;

                }
                else
                {
                    CommonMessages.ShowAlertMessage("Kindly Upload (.jpg,.jpeg,.png,.bmp,gif) File Only!");
                    return null;
                }


            }
            catch (Exception ex)
            {
                CommonMessages.ShowAlertMessage(ex.Message);
            }
            return FileUpload1.FileName;

        }
        else
        {
            CommonMessages.ShowAlertMessage("Kindly Select File!");
            return null;
        }
    }
    public int CalculateDate(string ddlMonth, string ddlDate, string ddlYear)
    {
        try
        {
            string s = ddlMonth + "/" + ddlDate + "/" + ddlYear;
            DateTime dob = Convert.ToDateTime(s);
            DateTime currentdate = Convert.ToDateTime(DateTime.Now);
            TimeSpan time = currentdate.Subtract(dob);
            int total = (time.Days) / 365;
            return total;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }



}
