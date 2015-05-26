using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
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

public partial class getInvCurrency :System.Web.UI.Page
{
    #region VARIABLES >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    private IClearingHouseService clearingHouseService = (IClearingHouseService)SpringContext.GetObject("ClearingHouseService");

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    	InvCurrencyInfo currInfo = clearingHouseService.GetInvCurrencyInfo(Request["currcode"]);
        if (currInfo == null)
        {
            returnGenError();
        }

        Response.ContentType = "text/xml; charset=utf-8";
        string xml = XmlParserUtil.parseObjectToUTF8Xml<InvCurrencyInfo>(currInfo);
        Response.Write(xml);
        Response.End();
    }

    private void returnGenError()
    {
        InvCurrencyInfo currInfo = new InvCurrencyInfo();
        currInfo.ReturnCode = CstError.GENERAL_ERROR;
        currInfo.CurrInfo = null;
        
        Response.ContentType = "text/xml; charset=utf-8";
        Response.Write(XmlParserUtil.parseObjectToUTF8Xml<InvCurrencyInfo>(currInfo));
        Response.End();
    }
}
