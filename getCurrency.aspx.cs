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

public partial class getCurrency :System.Web.UI.Page
{
    #region VARIABLES >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    private IClearingHouseService clearingHouseService = (IClearingHouseService)SpringContext.GetObject("ClearingHouseService");

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrencyInfoList currLstObj = clearingHouseService.GetCurrencyInfoList();
        if (currLstObj == null)
        {
            returnGenError();
        }
        
        Response.ContentType = "text/xml; charset=utf-8";
        string xml = XmlParserUtil.parseObjectToUTF8Xml<CurrencyInfoList>(currLstObj);
        Response.Write(xml);
        Response.End();
    }

    private void returnGenError()
    {
        CurrencyInfoList currLstObj = new CurrencyInfoList();
        currLstObj.returnCode = CstError.GENERAL_ERROR;
        currLstObj.currencyList = new List<CurrencyInfo>();
        
        Response.ContentType = "text/xml; charset=utf-8";
        Response.Write(XmlParserUtil.parseObjectToUTF8Xml<CurrencyInfoList>(currLstObj));
        Response.End();
    }
}
