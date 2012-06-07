using System;
using System.Net;
namespace AthleticsDatabase
{
    /// <summary>Functions for network connections</summary>
    public static class NetworkAccess
    {
        /// <summary>Check for internet access</summary>
        public static bool IsInternetAvailable()
        {
            var webrequest = WebRequest.Create(new Uri("http://www.google.com"));
            //webrequest.Proxy = null; //faster loading
            try
            {
                var response = webrequest.GetResponse();
                response.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
