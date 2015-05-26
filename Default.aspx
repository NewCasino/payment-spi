<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Untitled Page</title>
    <script type="text/javascript">
		//2.2 Added by SonTran
        function submitCancelTransaction(form) {
            if (!form) {
                alert("Form null");
                return false;
            }
            if (form.stransactionId.value == "")
            {
            	alert("Transaction Id must have value!");
                return false;
            }
            form.action = "http://www.fourierlabdev.com:29090/spi/transaction/" + form.stransactionId.value + "/cancel/";
            form.submit();
        }
		//2.3 Added by SonTran
		function UpdateAdjustmentTransaction(form) {
            if (!form) {
                alert("Form null");
                return false;
            }
            if (form.stransactionId.value == "")
            {
            	alert("Transaction Id must have value!");
                return false;
            }
            form.action = "http://www.fourierlabdev.com:29090/spi/adjustmenttransaction/" + form.stransactionId.value + "/status/";
            form.submit();
        }
        function submitCreateAdjustment(form) {
            if (!form) {
                alert("Form null");
                return false;
            }
            var url;            
            if (document.getElementById("deposit").checked) {
                url = "depositadjust/update"
            } else {
                url = "withdrawaladjust/update"
            }
            form.action = url;
            form.submit();
        }
        
        function submitTransHistory(form) {
            if (!form) {
                alert("Form null");
                return false;
            }
            
            form.action = "http://192.168.1.25:29090/spi/report/" + form.memberCode.value + "/history/";
            

            form.submit();
        }
        
        function submitTransSummary(form) {
            if (!form) {
                alert("Form null");
                return false;
            }
            
            form.action = "http://192.168.1.25:29090/spi/report/" + form.memberCode.value + "/summary/";
            

            form.submit();
        }
        
        function submitLastApproved(form) {
            if (!form) {
                alert("Form null");
                return false;
            }
            
            form.action = "http://192.168.1.52:29090/spi/report/" + form.memberCode.value + "/lastapprovedtransaction/";
            

            form.submit();
        }
        
        function submitMemberVerified2(form) {
            if (!form) {
                alert("Form null");
                return false;
            }
            
            form.action = "http://192.168.1.25:29090/spi/member/" + form.memberCode.value + "/unverified/";
            

            form.submit();
        }
        
        function submitMemberVerified1(form) {
            if (!form) {
                alert("Form null");
                return false;
            }
            
            form.action = "http://192.168.1.25:29090/spi/member/" + form.memberCode.value + "/verified/";            
            form.submit();
        }
		
		function submitUpdateBalance(form) {
            if (!form) {
                alert("Form null");
                return false;
            }
            if (form.memberCode.value == "")
            {
            	alert("MemberCode must have value!");
                return false;
            }
            //form.action = "member/" + form.memberCode.value + "/cashBalance";
            form.action = "http://192.168.1.22/clearinghouse/member/" + form.memberCode.value + "/cashBalance";   
            form.submit();
        }
        function submitVerifyMemberResponse(form) {
            if (!form) {
                alert("Form null");
                return false;
            }
            if (form.memberCode.value == "")
            {
            	alert("MemberCode must have value!");
                return false;
            }
            if (form.ouCode.value == "")
            {
            	alert("OuCode must have value!");
                return false;
            }
            form.action = form.ouCode.value + "/verifyMember/" + form.memberCode.value;
            form.submit();
        }
        function submitMemberUpdate(form){
            if (!form) {
                alert("Form null");
                return false;
            }
            if (form.memberCode.value == "")
            {
            	alert("Please enter member code");
                return false;
            }
            form.action = "http://192.168.1.25:29090/spi/member/update/";
            form.submit();
        }
    </script>
</head>
<body>
	
	<form id="CancelTransactionForm" method="POST">
        <asp:Label ID="Label1" runat="server" Text="Transaction Id"></asp:Label>
        <input id="stransactionId" type="text" /><br />
        <input id="btnSubmit" type="submit" onclick="submitCancelTransaction(this.form)" value="Cancel Transaction (SPI 2.2 Fourier)" />
        <br />
        &nbsp;&nbsp;&nbsp;</form>
        
        
	<form id="AdjustmentTransactionForm" method="POST">    Transaction Id
        <input id="stransactionId" type=text name="memberCode" />
        <br />
        <textarea name="input" style="width: 462px; height: 180px"></textarea>
        <br />
        <input id="btnSubmit1" type="submit" onclick="UpdateAdjustmentTransaction(this.form)" value="Adjustment Transaction (SPI 2.3 Fourier)" />
    </form>
	
    <form id="CreateAdjustmentFrom" method="POST">
        <textarea name="data" style="width: 462px; height: 180px"></textarea>
        <br />
        <input id="btnSubmit" type="submit" onclick="submitCreateAdjustment(this.form)" value="Adjust (SPI 2.2)" />
        &nbsp;&nbsp;&nbsp;
        <input id="deposit" name="transMode" type="radio" value="deposit" checked="CHECKED"/>&nbsp; deposit
        &nbsp;&nbsp;&nbsp;
        <input id="withdrawal" name="transMode" type="radio" value="withdrawal" />&nbsp; withdrawal        
    </form>
	<br/><br/>
	<form id="BalanceInputForm" method="POST">    
        Member Code <input id="memberCode" type=text name="memberCode" />
        <br />
        <textarea name="data" style="width: 462px; height: 180px"></textarea>
        <br />
        <input id="btnSubmit1" type="submit" onclick="submitUpdateBalance(this.form)" value="ChangeBalance (SPI 2.7)" />
    </form>
    <br/><br/>
    <form id="VerifyMemberForm" method="POST">  
        MemberCode
        <input id="memberCode" type=text name="memberCode" /><br />
        OuCode
        <input id="ouCode" type=text name="ouCode" /><br />
        <textarea name="data" style="width: 462px; height: 180px"></textarea><br />
        <input id="btnSubmit2" type="submit" onclick="submitVerifyMemberResponse(this.form)" value="Verify Member Response (SPI 2.14)" />
    </form> 
    <form id="TransactionHistoryFrom" method="POST">    
        Member Code <input id="memberCode" type=text name="memberCode" />
        <br />
        <textarea name="input" style="width: 462px; height: 180px"></textarea>
        <br />
        <input id="btnSubmit1" type="submit" onclick="submitTransHistory(this.form)" value="Transaction History (SPI 2.5 Fourier)" />
    </form>
    <form id="TransactionSummaryFrom" method="POST">    
        Member Code <input id="memberCode" type=text name="memberCode" />
        <br />
        <textarea name="input" style="width: 462px; height: 180px"></textarea>
        <br />
        <input id="btnSubmit1" type="submit" onclick="submitTransSummary(this.form)" value="Transaction Summary (SPI 2.6 Fourier)" />
    </form>
    <form id="LastApprovedFrom" method="POST">    
        Member Code <input id="memberCode" type=text name="memberCode" />
        <br />
        <textarea name="input" style="width: 462px; height: 180px"></textarea>
        <br />
        <input id="btnSubmit1" type="submit" onclick="submitLastApproved(this.form)" value="Last Approved (SPI 2.7 Fourier)" />
    </form>
    <form id="MemberVerified2From" method="POST">    
        Member Code <input id="memberCode" type=text name="memberCode" />
        <br />
        <textarea name="input" style="width: 462px; height: 180px"></textarea>
        <br />
        <input id="btnSubmit1" type="submit" onclick="submitMemberVerified2(this.form)" value="Member Verified (SPI 2.8 Fourier)" />
    </form>
    <form id="MemberVerified1From" method="POST">    
        Member Code <input id="memberCode" type=text name="memberCode" />
        <br />
        <textarea name="input" style="width: 462px; height: 180px"></textarea>
        <br />
        <input id="btnSubmit1" type="submit" onclick="submitMemberVerified1(this.form)" value="Member Verified (SPI 2.4 Fourier)" />
    </form>
    <form id="MemberUpdateInfoForm" method="POST">
         Member Code <input id="memberCode" type=text name="memberCode" />
        <br />
        <textarea name="input" style="width: 462px; height: 180px"></textarea>
        <br />
        <input id="btnSubmit1" type="submit" onclick="submitMemberUpdate(this.form)" value="Update Member Info(FL SPI 2.9)" />
    </form>
</body>
</html>
