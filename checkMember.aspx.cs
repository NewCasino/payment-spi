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
using Synet.Common.Exceptions;

public partial class checkMember : System.Web.UI.Page
{
    #region VARIABLES >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    private IClearingHouseService clearingHouseService = (IClearingHouseService)SpringContext.GetObject("ClearingHouseService");
    string strMemCode = "";
    string strOuCode = "";
    string strData = ""; //credential
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        strMemCode = Request["membercode"];
        strOuCode = Request["oucode"];
        strData = Request["data"];

        VerifyMember veriMemObj = null;
        try
        {
            veriMemObj = XmlParserUtil.parseXmlToObject<VerifyMember>(strData);
            if (veriMemObj == null)
            {
                returnGenError();
            }
        }
        catch (Exception ex)
        {
        	ExceptionManager.ExceptionHandler(ex, CstError.GENERAL_ERROR, "Exception occurred when parsing --- " + Request["data"] + " --- to object ");
            returnGenError();
        }

        VerifyMemberResponse veriMemResObj = clearingHouseService.VerifyMemberCredentials(strMemCode, strOuCode,  veriMemObj.credential);
        if (veriMemResObj == null)
        {
            returnGenError();
        }
        Response.ContentType = "text/xml; charset=utf-8";
        string xml = XmlParserUtil.parseObjectToUTF8Xml<VerifyMemberResponse>(veriMemResObj);
        Response.Write(xml);
        Response.End();
    }

    private void returnGenError()
    {
        VerifyMemberResponse veriMemResObj = new VerifyMemberResponse();
        veriMemResObj.returnCode = CstError.GENERAL_ERROR;

        Response.ContentType = "text/xml; charset=utf-8";
        Response.Write(XmlParserUtil.parseObjectToUTF8Xml<VerifyMemberResponse>(veriMemResObj));
        Response.End();
    }
}

