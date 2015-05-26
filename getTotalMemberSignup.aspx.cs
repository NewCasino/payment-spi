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

public partial class getTotalMemberSignup : System.Web.UI.Page
{
    #region VARIABLES >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    private IClearingHouseService clearingHouseService = (IClearingHouseService)SpringContext.GetObject("ClearingHouseService");
    string strMonth = "";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        strMonth = Request["month"];
        DateTime dateObj = new DateTime();
        try
        {
        	dateObj = DateTime.ParseExact(strMonth, "MMyyyy", null);
        }
        catch (Exception ex)
        {
        	ExceptionManager.ExceptionHandler(ex, CstError.GENERAL_ERROR, "Exception occurred when parsing month ---" + Request["month"] + "--- to MMyyyy");
        	returnGenError();
        }
               
        TotalMemberSignup totalMems = clearingHouseService.GetTotalMemberSignup(dateObj.Month, dateObj.Year);
        if (totalMems == null)
        {
            returnGenError();
        }

        Response.ContentType = "text/xml; charset=utf-8";
        string xml = XmlParserUtil.parseObjectToUTF8Xml<TotalMemberSignup>(totalMems);
        Response.Write(xml);
        Response.End();
    }

    private void returnGenError()
    {
        TotalMemberSignup totalMems = new TotalMemberSignup();
        totalMems.returnCode = CstError.GENERAL_ERROR;

        Response.ContentType = "text/xml; charset=utf-8";
        Response.Write(XmlParserUtil.parseObjectToUTF8Xml<TotalMemberSignup>(totalMems));
        Response.End();
    }
}

