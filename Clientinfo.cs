using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


namespace DNWS
{
  class Clientinfo : IPlugin
  {
    protected static Dictionary<String, int> statDictionary = null;
    public Clientinfo()
    {
      if (statDictionary == null)
      {
        statDictionary = new Dictionary<String, int>();

      }
    }

    public void PreProcessing(HTTPRequest request)
    {
      if (statDictionary.ContainsKey(request.Url))
      {
        statDictionary[request.Url] = (int)statDictionary[request.Url] + 1;
      }
      else
      {
        statDictionary[request.Url] = 1;
      }
    }
    public HTTPResponse GetResponse(HTTPRequest request)
    {
      String[] IP = Regex.Split(request.getPropertyByKey("RemoteEndPoint") , ":");
      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();
      sb.Append("<html><body>");

      sb.Append("Client IP:");
      sb.Append(IP[0]);
      sb.Append("<br><br>");


      sb.Append("Client Port:");
      sb.Append(IP[1]);
      sb.Append("<br><br>");

      sb.Append("Browser Information:");
      sb.Append(request.getPropertyByKey("User-Agent"));
      sb.Append("<br><br>");

      sb.Append("Accept Language:");
      sb.Append(request.getPropertyByKey("Accept-Language")); 
      sb.Append("<br><br>");

      sb.Append("Accept Encoding:");
      sb.Append(request.getPropertyByKey("Accept-Encoding"));
      sb.Append("<br><br>");

      sb.Append("</body></html>");
      response = new HTTPResponse(200);
      response.body = Encoding.UTF8.GetBytes(sb.ToString());
      return response;
    }

    public HTTPResponse PostProcessing(HTTPResponse response)
    {
      throw new NotImplementedException();
    }
  }
}