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
using System.Xml;

using Synet.ClearingHouse.Model;
using Synet.ClearingHouse.Constant;
using Synet.ClearingHouse.Service;
using Synet.Common.Factory;
using Synet.Common.Logs;
using Synet.Common.Util;
using Synet.Common.Exceptions;

public partial class changeBalance : System.Web.UI.Page
{
    #region VARIABLES >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    private IClearingHouseService clearingHouseService = (IClearingHouseService)SpringContext.GetObject("ClearingHouseService");
    private string strMemCode;
    private string strCasBalanceXml;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        strMemCode = Request["code"];
        strCasBalanceXml = Request["data"];
        CashBalanceInput casBalanceObjIn = null;
        try
        {
            casBalanceObjIn = XmlParserUtil.parseXmlToObject<CashBalanceInput>(strCasBalanceXml);
            if (casBalanceObjIn == null)
            {
            	returnGenError();
            }            
        }
        catch(Exception ex)
        {
        	ExceptionManager.ExceptionHandler(ex, CstError.GENERAL_ERROR, "Exception occurred when parsing ---" + Request["data"] + "--- to object ");
            returnGenError();
        }

        CashbalanceOutPut casBalanceObjOut = clearingHouseService.UpdateMemberCashBalance(strMemCode, casBalanceObjIn);
        if (casBalanceObjOut == null)
        {
            returnGenError();
        }
        Response.ContentType = "text/xml; charset=utf-8";
        string xml = XmlParserUtil.parseObjectToUTF8Xml<CashbalanceOutPut>(casBalanceObjOut);
        Response.Write(xml);
        Response.End();
    }

    private void returnGenError()
    {
        CashbalanceOutPut casBalanceObjOut = new CashbalanceOutPut();
        casBalanceObjOut.returnCode = CstError.GENERAL_ERROR;

        Response.ContentType = "text/xml; charset=utf-8";
        Response.Write(XmlParserUtil.parseObjectToUTF8Xml<CashbalanceOutPut>(casBalanceObjOut));
        Response.End();
    }
}

