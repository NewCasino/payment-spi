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

public partial class createAdjust : System.Web.UI.Page
{
	#region VARIABLES >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    private IClearingHouseService clearingHouseService = (IClearingHouseService)SpringContext.GetObject("ClearingHouseService");
    private string _type;    
    private int _transMode;

    #endregion
    
    protected void Page_Load(object sender, EventArgs e)
    {
        _type = Request["type"];
        _transMode = (_type == "withdrawal") ? Constants.WITHDRAWAL_ADJUSTMENT : Constants.DEPOSIT_ADJUSTMENT;
        addAdjustment(_transMode);
    }

    private void addAdjustment(int _transMode)
    {
    	string xml_input = Request["data"];
        AdjustmentInfo adjInfo = new AdjustmentInfo();
        AdjustmentResult adjResult;
        try
        {
            adjInfo = XmlParserUtil.parseXmlToObject<AdjustmentInfo>(xml_input);
            if (adjInfo == null)
            {
            	returnGenError();
            }            
        }
        catch(Exception ex)
        {
        	ExceptionManager.ExceptionHandler(ex, CstError.GENERAL_ERROR, "Exception occurred when parsing --- " + Request["data"] + " --- to object ");
            returnGenError();
        }        

        adjResult = clearingHouseService.AddUptAdjust(adjInfo, _transMode);
        if (adjResult == null)
        {
            returnGenError();
        }
        Response.ContentType = "text/xml; charset=utf-8";
        string xml = XmlParserUtil.parseObjectToUTF8Xml<AdjustmentResult>(adjResult);        
        Response.Write(xml);
        Response.End();
    }
    
    private void returnGenError() 
    {
        AdjustmentResult adjResult = new AdjustmentResult();
        adjResult.returnCode = CstError.GENERAL_ERROR;
        adjResult.transID = 0;
	
        Response.ContentType = "text/xml; charset=utf-8";
        Response.Write(XmlParserUtil.parseObjectToUTF8Xml<AdjustmentResult>(adjResult));
        Response.End();
    }
}
