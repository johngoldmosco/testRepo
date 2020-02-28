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
using System.Drawing;
using System.Drawing.Drawing2D;

/// <summary>
/// Summary description for clsProduct
/// </summary>
public class clsProduct
{
    ODBC obj = new ODBC();
	public clsProduct()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int addProduct(string strCategory, string strProductCode, string strProductName, string strImage, string strThumbImg, string strPrice, string intTax, string intMindiscount,string txtAddress, string strqty)
    {
        string strQuery = "INSERT INTO sb_product_details(category_id,product_code,product_name,product_image_thumb,product_image_large,product_price,vat_tax,min_discount,description,qty) VALUES('" + strCategory + "','" + strProductCode + "','" + strProductName + "','" + strThumbImg + "','" + strImage + "','" + strPrice + "','" + intTax + "','" + intMindiscount + "','" + txtAddress + "','" + strqty + "')";
        try
        {
            obj.executeNonQuery(strQuery);
            return 1;
        }
        catch (Exception ex)
        {
            return 0;
        }

       
    }
    public int updateProduct(string strCategory, string strProductCode, string strProductName, string strImage, string strThumbImg, string strPrice, string intTax, string intMindiscount, string txtAddress,string ProductId, string strqty)
    {
        string strQuery = "";
        if (strImage == "" || strThumbImg == "")
        {
            strQuery = "UPDATE sb_product_details SET category_id='" + strCategory + "', product_code='" + strProductCode + "',product_name='" + strProductName + "',product_price='" + strPrice + "',vat_tax = '" + intTax + "',min_discount = '" + intMindiscount + "',description = '" + txtAddress + "',qty='" + strqty + "' WHERE id='" + ProductId + "' ";
        }
        else
        {
            strQuery = "UPDATE sb_product_details SET category_id='" + strCategory + "', product_code='" + strProductCode + "',product_name='" + strProductName + "',product_price='" + strPrice + "', product_image_thumb='" + strThumbImg + "', product_image_large='" + strImage + "',vat_tax = '" + intTax + "',min_discount = '" + intMindiscount + "',description = '" + txtAddress + "',qty='" + strqty + "' WHERE id='" + ProductId + "' ";
        }
        try
        {
            obj.executeNonQuery(strQuery);
            return 1;
        }
        catch (Exception ex)
        {
            return 0;
        }
      
    }

    public string UploadProduct(FileUpload FileUpload1)
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

                        //FileUpload1.SaveAs(.MapPath("~/Files/") + strFileName);
                        // clsOdbc.executeNonQuery("UPDATE personal_setting SET company_logo='logo/" + strFileName + "'");
                        return FileUpload1.FileName;


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
    public void create_thumbnaleImage(System.IO.Stream str, string strfile)
    {
        int newWidth = 285;
        int newHeight = 360;
        System.Drawing.Image image = System.Drawing.Image.FromStream(str);

        // Calculate proportional max width and height.
        int oldWidth = image.Width;
        int oldHeight = image.Height;
        if ((Convert.ToDecimal(oldWidth) / Convert.ToDecimal(oldHeight)) > (Convert.ToDecimal(285) / Convert.ToDecimal(360)))
        {
            decimal ratio = Convert.ToDecimal(285) / oldWidth;
            newHeight = Convert.ToInt32((oldHeight * ratio));
        }
        else
        {
            decimal ratio = Convert.ToDecimal(360) / oldHeight;
            newWidth = Convert.ToInt32((oldWidth * ratio));
        }

        // Create a new bitmap with the same resolution as the original image.
        Bitmap bitmap = new Bitmap(285, 360, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);

        // Create a new graphic.
        Graphics graphics__1 = Graphics.FromImage(bitmap);
        graphics__1.Clear(Color.White);
        graphics__1.InterpolationMode = InterpolationMode.HighQualityBicubic;

        // Create a scaled image based on the original.
        graphics__1.DrawImage(image, new Rectangle(0, 0, 285, 360), new Rectangle(0, 0, oldWidth, oldHeight), GraphicsUnit.Pixel);
        graphics__1.Dispose();

        // Save the scaled image.
        bitmap.Save(HttpContext.Current.Server.MapPath("../../ThumbImages/") + strfile, image.RawFormat);
    }


	public string upload_file(FileUpload FileUpload1)
    {

        if (FileUpload1.HasFile)
        {
            try
            {


                if (FileUpload1.PostedFile.ContentType == "image/jpg" | FileUpload1.PostedFile.ContentType == "image/jpeg" | FileUpload1.PostedFile.ContentType == "image/png" | FileUpload1.PostedFile.ContentType == "image/bmp" | FileUpload1.PostedFile.ContentType == "image/gif")
                {

                    if (FileUpload1.PostedFile.ContentLength < 5242890)
                    {

                        string strFileName = System.IO.Path.GetFileName(FileUpload1.FileName);

                        //FileUpload1.SaveAs(.MapPath("~/Files/") + strFileName);
                        // clsOdbc.executeNonQuery("UPDATE personal_setting SET company_logo='logo/" + strFileName + "'");
                        return FileUpload1.FileName;


                    }
                    else
                    {
                        CommonMessages.ShowAlertMessage("Kindly Upload File less than 5 MB!");
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

	
    public int ProductPurchaseOrder(string strCategory, string strProductCode, string strProductName, string strImage, string strThumbImg, string strPrice, string intTax, string intMindiscount, string txtAddress)
    {
        string strQuery = "INSERT INTO sb_product_details(category_id,product_code,product_name,product_image_thumb,product_image_large,product_price,vat_tax,min_discount,description) VALUES('" + strCategory + "','" + strProductCode + "','" + strProductName + "','" + strThumbImg + "','" + strImage + "','" + strPrice + "','" + intTax + "','" + intMindiscount + "','" + txtAddress + "')";
        try
        {
            obj.executeNonQuery(strQuery);
            return 1;
        }
        catch (Exception ex)
        {
            return 0;
        }


    }
}
