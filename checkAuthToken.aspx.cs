using System;
using System.Data;
using System.Text;
using System.Xml;

using System.Configuration;
using System.Collections;
using System.Web;

using Synet.ClearingHouse.Model;
using Synet.ClearingHouse.Constant;
using Synet.ClearingHouse.Util;
using Synet.ClearingHouse.Service;
using Synet.Common.Factory;
using Synet.Common.Logs;
using Synet.Common.Util;


public partial class checkAuthToken : System.Web.UI.Page
{
    #region VARIABLES >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    private IClearingHouseService clearingHouseService = (IClearingHouseService)SpringContext.GetObject("ClearingHouseService");
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    	string userRole = Request["userrole"];
        string txtKey = Request["txtkey"];
        string ipAddress = Request["ipaddress"];
        string ouCode = Request["oucode"];
        string memberCode = Request["membercode"];
        
        string xml = null;
        
        Response.ContentType = "text/xml; charset=utf-8";
        //AuthToken token = null; 
        if ( "admin".Equals(userRole)) {
        	AdminAuthToken token = clearingHouseService.VerifyAdminToken(memberCode, txtKey, ipAddress, ouCode);
        	xml = XmlParserUtil.parseObjectToUTF8Xml<AdminAuthToken>(token);
        	//xml = toXml(token, "adminauthinfo");
        } else {
        	AuthToken token = clearingHouseService.VerifyAuthenticationToken(memberCode, txtKey, ipAddress, ouCode);
        	xml = XmlParserUtil.parseObjectToUTF8Xml<AuthToken>(token);
        	//xml = toXml(token, "memberauthinfo");
        }
        
    	Response.Write(xml);
    	Response.End();
    }

    private void returnGenError()
    {
        AuthToken auTokenObj = new AuthToken();
        auTokenObj.ReturnCode = CstError.GENERAL_ERROR;

        Response.ContentType = "text/xml; charset=utf-8";
        Response.Write(XmlParserUtil.parseObjectToUTF8Xml<AuthToken>(auTokenObj));
        Response.End();
    }
    
    private static string toXml(AuthToken token, string name) {
    	
    	return "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
				"<" + name + ">" + 
    				"<returncode>" + token.ReturnCode + "</returncode>" +
    				"<authenticated>" + token.Passed + "</authenticated>" + 
				"</" + name + ">";
    }
}

