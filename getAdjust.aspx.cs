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
using Synet.Common.Exceptions;

public partial class getAdjust : System.Web.UI.Page
{
	#region VARIABLES >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    private IClearingHouseService clearingHouseService = (IClearingHouseService)SpringContext.GetObject("ClearingHouseService");
    private string _type;
    private int refNo;
    private int _transMode;

    #endregion
    
    protected void Page_Load(object sender, EventArgs e)
    {
        _type = Request["type"];
        _transMode = (_type == "withdrawal") ? Constants.WITHDRAWAL_ADJUSTMENT : Constants.DEPOSIT_ADJUSTMENT;
        getAdjustment();
    }

    private void getAdjustment()
    {
    	if (Request["refNo"] != null && Request["refNo"] != "")
    	{
    		try
    		{
    			refNo = int.Parse(Request["refNo"].Trim());
    		}
    		catch(Exception ex)
    		{
    			ExceptionManager.ExceptionHandler(ex, CstError.GENERAL_ERROR, "Exception occurred when parsing refNo ---" + Request["refNo"] + "--- to int");
    			returnGenError();
    		}
    		
	    	AdjustmentRetrieval adjRetrieval = clearingHouseService.GetDepWithAdjustment(refNo);        
	    	
	        Response.ContentType = "text/xml; charset=utf-8";
	        string xml = XmlParserUtil.parseObjectToUTF8Xml<AdjustmentRetrieval>(adjRetrieval);        
	        Response.Write(xml);
	        Response.End();
    	}    	
    }
    
    private void returnGenError()
    {
        AdjustmentRetrieval adjRetrieval = new AdjustmentRetrieval();        
    	adjRetrieval.returnCode = CstError.GENERAL_ERROR;
    	adjRetrieval.transID = 0;
    	adjRetrieval.refNo = 0;
    	adjRetrieval.adminCode = "";
    	adjRetrieval.memberCode = "";
    	adjRetrieval.creditDebit = 0;
    	adjRetrieval.amount = 0;
    	adjRetrieval.description = "";
    	adjRetrieval.status = 0;
	
        Response.ContentType = "text/xml; charset=utf-8";
        Response.Write(XmlParserUtil.parseObjectToUTF8Xml<AdjustmentRetrieval>(adjRetrieval));
        Response.End();
    }
}
