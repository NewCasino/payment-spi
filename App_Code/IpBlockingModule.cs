/*
 * Created by SharpDevelop.
 * User: Xuan
 * Date: 10/22/2007
 * Time: 5:04 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Web;
using System.Configuration;
using System.Collections.Specialized;

namespace ClearingHouse
{
	/// <summary>
	/// Description of IpBlockingModule.
	/// </summary>
	public class IpBlockingModule: IHttpModule
	{
		private static StringCollection _IpAdresses = FillAllowedlps();
		
		void IHttpModule.Dispose()
   		{
			//Nothing to dispose;
		}

	   	void IHttpModule.Init(HttpApplication context)
	   	{ 
	   		context.BeginRequest += new EventHandler(context_BeginRequest);
	   	}
	   	
	   	private void context_BeginRequest(object sender, EventArgs e)
		{
	   		string ip = HttpContext.Current.Request.UserHostAddress;
		   	if(_IpAdresses.Count > 0 && !_IpAdresses.Contains(ip))
		   	{ 
		   		HttpContext.Current.Response.StatusCode = 403;
		   		HttpContext.Current.Response.Write("Your IP " + ip + " was blocked!");
		     	HttpContext.Current.Response.End();
		   	}
	   	}
		   
		private static StringCollection FillAllowedlps()
		{
			StringCollection col = new StringCollection();
		   	string raw = ConfigurationSettings.AppSettings.Get("AllowIP");
		   	raw = raw.Replace(",", ";");
		   	raw = raw.Replace(" ", ";");
		   	foreach(string ip in raw.Split(';'))
		   	{ 
		   		if (ip.Trim() != "")
                {
                    col.Add(ip.Trim());
                }
		   	}
		   	return col;
		}
	}
}
