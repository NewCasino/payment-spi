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

public partial class getDocumentVerifyStatus : System.Web.UI.Page
{
    #region VARIABLES >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    private IClearingHouseService clearingHouseService = (IClearingHouseService) SpringContext.GetObject("ClearingHouseService");
    private string strMemCode;
    private int intDocTypeId;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        strMemCode = Request["code"];        
        try
        {
        	intDocTypeId = int.Parse(Request["docType"].Trim());
        }
        catch(Exception ex)
        {
        	ExceptionManager.ExceptionHandler(ex, CstError.GENERAL_ERROR, "Exception occured when parsing docType ---" + Request["docType"] + "--- to int");
        	returnGenError();
        }
        
        VerifyResponse docObj = clearingHouseService.GetVerifyDocumentStatus(strMemCode, intDocTypeId);
        if (docObj == null)
        {
            returnGenError();
        }
        Response.ContentType = "text/xml; charset=utf-8";
        string xml = XmlParserUtil.parseObjectToUTF8Xml<VerifyResponse>(docObj);
        Response.Write(xml);
        Response.End();
    }

    private void returnGenError()
    {
        VerifyResponse docObj = new VerifyResponse();
        docObj.returnCode = CstError.GENERAL_ERROR;

        Response.ContentType = "text/xml; charset=utf-8";
        Response.Write(XmlParserUtil.parseObjectToUTF8Xml<VerifyResponse>(docObj));
        Response.End();
    }
}
