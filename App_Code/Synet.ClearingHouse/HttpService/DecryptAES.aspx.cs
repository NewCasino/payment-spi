using System;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

public partial class DecryptAES : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XmlDocument xml = new XmlDocument();
        XmlElement root;
        XmlElement decryptedTextElement;
        XmlElement errors;

        // retreive the encrypted and key
        String encrypted = Server.UrlDecode(Request["strEncrypted"]);
        String key = Server.UrlDecode(Request["strKey"]);
        String iv = Server.UrlDecode(Request["iv"]);
        encrypted = encrypted.Replace(' ', '+');

        // decrypt data
        /*String encrypted = "P8EbKgD7HdOvr08YD4RsXpZNOcQL26iUtbW+WRrJlVzNFD8sJVFgAu/io3/9SK8rJn+lPhumHeTfb2+SU+upkmnn2dhnUncG+q6+GfJvU8oXkn2NYzIvnGJTnWrYjIA+pLxa4vQ+dXR2e1MU1IWOnrHM6ygYvuSkzZYjwKz3EYqnysWjEy+P/eUKB4jSRsWPRZ5HKLRXGuVdGMXdNNOkxicovqkv3mdcgjX++L04AiEFNv9MmS9RR7Fn3ncEHICUIIe/nSyEnCwR+5nDOiUjdPXpj9t/8bgFt1mej9jYn7yV2aF5BIIAzQnrRQJDI6DITWDM6Jdpkbz47Lqptu+nlPkIFcU0blGVs/EWNSu6EZI=";
        String key = "8946f5c0d4d9a3ef";
        String iv = "AA3401400BC76CFA";*/
        String decryptedText = decrypt(encrypted, key, iv);
        
        // write xml to response
        Response.ContentType = "text/xml charset=utf-8";
        xml.CreateXmlDeclaration("1.0", "UTF-8", "yes");
        root = xml.CreateElement("Response");
        xml.AppendChild(root);

        errors = xml.CreateElement("Errors");
        decryptedTextElement = xml.CreateElement("Decrypted_Text");
        if (decryptedText == null)
        {
            errors.InnerText = "Error while decrypt your input. Please check your key and input";
        }
        else
        {
            decryptedTextElement.InnerText = decryptedText;
        }

        root.AppendChild(decryptedTextElement);
        root.AppendChild(errors);

        XmlTextWriter xmlOut = new XmlTextWriter(Response.Output);
        xml.WriteContentTo(xmlOut);
        Response.End();
    }

    public string encrypt(string text, string password, string iv) 
	{
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        rijndaelCipher.Mode = CipherMode.CBC;
        rijndaelCipher.Padding = PaddingMode.PKCS7;
        rijndaelCipher.KeySize = 128;
        rijndaelCipher.BlockSize = 128;

        byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(password);
        byte[] keyBytes = new byte[16];
        int len = pwdBytes.Length;
        if (len > keyBytes.Length) len = keyBytes.Length;
        System.Array.Copy(pwdBytes, keyBytes, len);

        rijndaelCipher.Key = keyBytes;

        byte[] ivBytes1 = System.Text.Encoding.UTF8.GetBytes(iv);
        byte[] keyBytes1 = new byte[16];

        int len1 = ivBytes1.Length;
        if (len1 > keyBytes1.Length) len1 = keyBytes1.Length;
        System.Array.Copy(ivBytes1, keyBytes1, len1);
        rijndaelCipher.IV = ivBytes1;

        ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
        byte[] plainText = Encoding.UTF8.GetBytes(text);
        byte[] cipherBytes = transform.TransformFinalBlock(plainText, 0, plainText.Length);
        return Convert.ToBase64String(cipherBytes);
    }

    public string decrypt(string text, string password, string iv) 
	{
        try
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;

            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;

            byte[] encryptedData = Convert.FromBase64String(text);
            byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] keyBytes = new byte[16];

            int len = pwdBytes.Length;
            if (len > keyBytes.Length) len = keyBytes.Length;
            System.Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;

            byte[] ivBytes1 = System.Text.Encoding.UTF8.GetBytes(iv);
            byte[] keyBytes1 = new byte[16];

            int len1 = ivBytes1.Length;
            if (len1 > keyBytes1.Length) len1 = keyBytes1.Length;
            System.Array.Copy(ivBytes1, keyBytes1, len1);
            rijndaelCipher.IV = keyBytes1;

            ICryptoTransform transform = rijndaelCipher.CreateDecryptor();

            byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);

            return Encoding.UTF8.GetString(plainText);
        } 
        catch (Exception ex)
        {
        	ex.StackTrace.ToString();
            return null;
        }
	}
}
