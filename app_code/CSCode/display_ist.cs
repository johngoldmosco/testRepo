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
/// Summary description for display_ist
/// </summary>
public class display_ist
{
    ODBC clsOdbc = new ODBC();
	public display_ist()
	{
		
	}
    public void FillEPINType(DropDownList ddlDropDownList)
    {
        string strQuery = null;
        System.Data.DataSet ds = new System.Data.DataSet();

        strQuery = "SELECT id,pin_type From mlm_epin_type Order By id ASC";
        ddlDropDownList.Items.Clear();

        try
        {
            ds = clsOdbc.getDataSet(strQuery);


            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDropDownList.DataSource = ds;
                ddlDropDownList.DataTextField = "pin_type";
                ddlDropDownList.DataValueField = "id";
                ddlDropDownList.DataBind();
            }
            ddlDropDownList.Items.Insert(0, "Select");
            ddlDropDownList.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
        }
        finally
        {
            ds.Dispose();
        }

    }
}
