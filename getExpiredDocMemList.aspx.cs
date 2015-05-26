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

using Synet.ClearingHouse.Constant;
using Synet.ClearingHouse.Exceptions;
using Synet.ClearingHouse.Model;
using Synet.ClearingHouse.Service;
using Synet.Common.Exceptions;
using Synet.Common.Factory;
using Synet.Common.Util;

public partial class getExpiredDocMemList : System.Web.UI.Page
{
    private IClearingHouseService clearingHouseService = (IClearingHouseService)SpringContext.GetObject("ClearingHouseService");
    private string _strOUCode, _strDate;

    protected void Page_Load(object sender, EventArgs e)
    {
        _strOUCode = Request["oucode"]; 
        _strDate   = Request["date"];
        DateTime dateObj = new DateTime();
        try
        {
            dateObj = DateTime.ParseExact(_strDate, "ddMMyyyy", null);
        }
        catch (Exception ex)
        {
            ExceptionManager.ExceptionHandler(ex, CstError.GENERAL_ERROR, "Exception occurred when parsing date ---" + Request["month"] + "--- to MMyyyy");
            returnGenError();
        }
        _strDate = _strDate.Substring(0, 2) + "-" + _strDate.Substring(2, 2) + "-" + _strDate.Substring(4, 4);
        ExDocMemberList _objExDocMemList = clearingHouseService.GetExpiredDocMemberList(_strDate);
        if (_objExDocMemList == null)
        {
            returnGenError();
        }

        Response.ContentType = "text/xml; charset=utf-8";
        string xml = XmlParserUtil.parseObjectToUTF8Xml<ExDocMemberList>(_objExDocMemList);
        Response.Write(xml);
        Response.End();
    }

    private void returnGenError()
    {
        ExDocMemberList _objExDocMemList = new ExDocMemberList();
        _objExDocMemList.returnCode = CstError.GENERAL_ERROR;

        Response.ContentType = "text/xml; charset=utf-8";
        Response.Write(XmlParserUtil.parseObjectToUTF8Xml<ExDocMemberList>(_objExDocMemList));
        Response.End();
    }
}
