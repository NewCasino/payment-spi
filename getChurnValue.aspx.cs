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

public partial class getChurnValue : System.Web.UI.Page
{
    #region VARIABLES >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    private IClearingHouseService clearingHouseService = (IClearingHouseService)SpringContext.GetObject("ClearingHouseService");
    private string strMemCode;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        strMemCode = Request["code"];
        ChurnResponse churnObj = clearingHouseService.GetMemberChurnObj(strMemCode);
        if (churnObj == null)
        {
            returnGenError();
        }
        Response.ContentType = "text/xml; charset=utf-8";
        string xml = XmlParserUtil.parseObjectToUTF8Xml<ChurnResponse>(churnObj);        
        Response.Write(xml);
        Response.End();
    }

    private void returnGenError()
    {
        ChurnResponse churnObj = new ChurnResponse();
        churnObj.returnCode = CstError.GENERAL_ERROR;

        Response.ContentType = "text/xml; charset=utf-8";
        Response.Write(XmlParserUtil.parseObjectToUTF8Xml<ChurnResponse>(churnObj));
        Response.End();
    }
}
