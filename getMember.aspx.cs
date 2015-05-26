using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Synet.ClearingHouse.Model;
using Synet.ClearingHouse.Constant;
using Synet.ClearingHouse.Service;
using Synet.Common.Factory;
using Synet.Common.Logs;
using Synet.Common.Util;

public partial class getMember :System.Web.UI.Page
{
    #region VARIABLES >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    private IClearingHouseService clearingHouseService = (IClearingHouseService)SpringContext.GetObject("ClearingHouseService");
    private string strMemCode;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
       	strMemCode = Request["code"];
        MemberInfo memObj = clearingHouseService.GetMemberInfoPersonal(strMemCode);
        if (memObj == null)
        {
            returnGenError();
        }
        Response.ContentType = "text/xml; charset=utf-8";
        string xml = XmlParserUtil.parseObjectToUTF8Xml<MemberInfo>(memObj);
        Response.Write(xml);
        Response.End();
    }

    private void returnGenError()
    {
        MemberInfo memObj = new MemberInfo();
        memObj.returnCode = CstError.GENERAL_ERROR;
        memObj.memberCode = "";
		memObj.userID = 0;
		memObj.currCode = "";
		memObj.email = "";
		memObj.websiteName = "";
		memObj.balance = 0;
		memObj.vendorID = "";
		memObj.country = "";
		memObj.firstname = "";
		memObj.lastname = "";
		memObj.verifyID = 0;
		memObj.dtupdatedmember = "";
		memObj.dtupdatedadmin = "";

        Response.ContentType = "text/xml; charset=utf-8";
        Response.Write(XmlParserUtil.parseObjectToUTF8Xml<MemberInfo>(memObj));
        Response.End();
    }
}
