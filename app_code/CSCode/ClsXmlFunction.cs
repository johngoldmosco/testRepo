using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Text;
using System.Globalization;


/// <summary>
/// Summary description for xmlFunction
/// </summary>
public class ClsXmlFunction
{
	public ClsXmlFunction()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /******* Create XML File ********/
    public int createXML(string strFilePath, string strRegister)
    {
        XmlWriter writer = XmlWriter.Create(strFilePath);
        //XmlTextWriter writer = new XmlTextWriter(strFilePath , System.Text.Encoding.UTF8);
        writer.WriteStartDocument(true);
       
        writer.WriteStartElement("Register");
        createNode(strRegister, writer ); 
        writer.WriteEndElement();
        writer.WriteEndDocument();
        writer.Close();
        return 1;
    }

    /*********** Create Node ***********/
    private  void createNode(string strLink, XmlWriter writer)
    {
        writer.WriteStartElement("RegisterLink");
        writer.WriteStartElement("Link");
        writer.WriteString(strLink);
        writer.WriteEndElement();
        writer.WriteEndElement();
    }
    /********** Access XML ************/
    public DataSet getXML(string strFilePath)
    {
        XmlReader xmlFile;
        xmlFile = XmlReader.Create(strFilePath, new XmlReaderSettings());
        DataSet ds = new DataSet();
        ds.ReadXml(xmlFile);

        xmlFile.Close();
       
        return ds; 
    }
    public void add(int intUserId, string strMemberId, string strIP, DateTime dtLoginTime)
    {           
        XmlDocument xDoc = new XmlDocument();
        string fileName = HttpContext.Current.Server.MapPath("../../XmlFiles/login.xml");
        xDoc.Load(fileName);

        XmlElement parentelement = xDoc.CreateElement("login");

        XmlElement userid = xDoc.CreateElement("userid");
        XmlElement member_id = xDoc.CreateElement("member_id");
        XmlElement ip_address = xDoc.CreateElement("ip_address");
        XmlElement Login_Date = xDoc.CreateElement("Login_Date");

        userid.InnerText = intUserId.ToString();
        member_id.InnerText = strMemberId.ToString();
        ip_address.InnerText = strIP;
        Login_Date.InnerText = dtLoginTime.ToString();

        parentelement.AppendChild(userid);
        parentelement.AppendChild(member_id);
        parentelement.AppendChild(ip_address);
        parentelement.AppendChild(Login_Date);


        xDoc.DocumentElement.AppendChild(parentelement);
        xDoc.Save(fileName);
        
    }
    public DataTable   Search(ArrayList al,DataTable dt)
    {
        dt.Clear();

        XmlDocument xDoc = new XmlDocument();
        string fileName = HttpContext.Current.Server.MapPath("../../XmlFiles/login.xml");
        xDoc.Load(fileName);

        try
        {
            XmlNodeList nodeList = xDoc.SelectNodes("Login_History/login");
            for (int i = 0; i < 3; i = i + 1)
            {
                foreach (XmlNode node in nodeList)
                {

                    if (al[2 * i + 1] != "")
                    {
                        string str = al[i * 2].ToString().Trim();


                        if (node["" + str + ""].InnerText == al[2 * i + 1].ToString())
                        {
                            DataRow dtrow = dt.NewRow();

                            dtrow["ip_address"] = node["ip_address"].InnerText;
                            dtrow["Login_Date"] = node["Login_Date"].InnerText;
                            dt.Rows.Add(dtrow);
                        }
                    }
                }
            }
            return dt;
        }
        catch (Exception ex)
        {
        }
        finally
        {
            dt.Dispose();
        }
        return dt;
    }

    public void addStatusLog(string intUserId, string name, int old_status , int new_status, string description, DateTime dtLoginTime, string modified_by)
    {
        XmlDocument xDoc = new XmlDocument();
        string fileName = HttpContext.Current.Server.MapPath("~/XmlFiles/statusLog.xml");
        xDoc.Load(fileName);

        XmlElement parentelement = xDoc.CreateElement("status_log");

        XmlElement userid = xDoc.CreateElement("userid");
        XmlElement uname = xDoc.CreateElement("uname");
        XmlElement oldstatus = xDoc.CreateElement("old_status");
        XmlElement newstatus = xDoc.CreateElement("new_status");
        XmlElement descript = xDoc.CreateElement("description");
        XmlElement modifiedby = xDoc.CreateElement("modified_by");
        XmlElement modifiedon = xDoc.CreateElement("modified_on");

        userid.InnerText = intUserId;
        uname.InnerText = name;
        oldstatus.InnerText = old_status.ToString();
        newstatus.InnerText = new_status.ToString();
        descript.InnerText = description;
        modifiedby.InnerText = modified_by.ToString();
        modifiedon.InnerText = dtLoginTime.ToString();

        parentelement.AppendChild(userid);
        parentelement.AppendChild(uname);
        parentelement.AppendChild(oldstatus);
        parentelement.AppendChild(newstatus);
        parentelement.AppendChild(descript);
        parentelement.AppendChild(modifiedby);
        parentelement.AppendChild(modifiedon);

        xDoc.DocumentElement.AppendChild(parentelement);
        xDoc.Save(fileName);

    }

    public void BindGridView(GridView gvReport,string url)
    {
        DataSet dsGridViewXMLEditDeleteUpdate = new DataSet();
        //gvReport.ReadXml(HttpContext.Current.Server.MapPath("~/XmlFiles/'" + url + "'"));
        gvReport.DataSource = gvReport;
        gvReport.DataBind();
        gvReport.ShowFooter = true;
    }


        
}
