using System;
using System.Data;
using System.Web;

using Synet.ClearingHouse.Constant;
using Synet.ClearingHouse.Model;
using Synet.ClearingHouse.Service;
using Synet.ClearingHouse.Util;
using Synet.Common.Exceptions;
using Synet.Common.Factory;
using Synet.Common.Util;

public partial class setMemberUnverified : System.Web.UI.Page
{
    private IClearingHouseService clearingHouseService = (IClearingHouseService)SpringContext.GetObject("ClearingHouseService");
    private string _strOUCode, _strMemberCode, _strData;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        _strOUCode      = Request["oucode"] ==null?"":Request["oucode"];
        _strMemberCode  = Request["code"]   ==null?"":Request["code"] ;
        _strData        = Request["data"]   ==null?"":Request["data"];
        
        if (_strMemberCode == "" || _strData == "")
        {
            this.returnGenError();
        }
        else
        {
            UnverifyMember _unverifyMemObj = null;
            
            try
            {
                _unverifyMemObj = XmlParserUtil.parseXmlToObject<UnverifyMember>(_strData);
                if (_unverifyMemObj == null) 
                {
                    this.returnGenError();
                }
            }
            catch(Exception ex)
            {
                ExceptionManager.ExceptionHandler(ex, CstError.GENERAL_ERROR, "Exception occurred when parsing --- " + Request["data"] + " --- to object ");
                returnGenError();
            }
            
            string _strETrigger = _unverifyMemObj.reason + ". " + _unverifyMemObj.remarks + "";
            
            UnverifyMemberResponse _unverifyMemResObj = clearingHouseService.SetMemberUnverified("SYSTEM", _strMemberCode, _strETrigger);
            if (_unverifyMemResObj == null)
            {
                returnGenError();
            }
            
            Response.ContentType = "text/xml; charset=utf-8";
            string xml = XmlParserUtil.parseObjectToUTF8Xml<UnverifyMemberResponse>(_unverifyMemResObj);
            Response.Write(xml);
            Response.End();
        }
    }
    
    private void returnGenError()
    {
        UnverifyMemberResponse _unVerifyResObj = new UnverifyMemberResponse();
        _unVerifyResObj.returnCode = CstError.GENERAL_ERROR;
        
        Response.ContentType = "text/xml; charset=utf-8";
        Response.Write(XmlParserUtil.parseObjectToUTF8Xml<UnverifyMemberResponse>(_unVerifyResObj));
        Response.End();
    }
}
