using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Synet.ClearingHouse.Model;
using Synet.ClearingHouse.Constant;
using Synet.ClearingHouse.Service;
using Synet.Common.Factory;
using Synet.Common.Logs;
using Synet.Common.Util;

public partial class getAdmin : System.Web.UI.Page
{
    #region VARIABLES >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    private IClearingHouseService clearingHouseService = (IClearingHouseService)SpringContext.GetObject("ClearingHouseService");
    private string strAdmCode;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
       	strAdmCode = Request["code"];
        AdminInfo admObj = clearingHouseService.GetAdminUserInfo(strAdmCode);
        if (admObj == null)
        {
            returnGenError();
        }
        Response.ContentType = "text/xml; charset=utf-8";
        string xml = XmlParserUtil.parseObjectToUTF8Xml<AdminInfo>(admObj);
        Response.Write(xml);
        Response.End();
    }

    private void returnGenError()
    {
        AdminInfo admObj = new AdminInfo();
        admObj.returnCode = CstError.GENERAL_ERROR;
        admObj.adminname = "";
		admObj.admintypeid = 0;
		
        Response.ContentType = "text/xml; charset=utf-8";
        Response.Write(XmlParserUtil.parseObjectToUTF8Xml<AdminInfo>(admObj));
        Response.End();
    }
}
