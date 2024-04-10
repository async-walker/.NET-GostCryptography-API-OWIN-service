using Microsoft.Owin.Hosting;
using Serilog;
using System;
using System.Configuration;

namespace GostCryptographyAPI.TopShelf
{
    public class RestService
    {
        private IDisposable _app;

        public void Start()
        {
            string baseAddress = ConfigurationManager.AppSettings["BaseUrl"];

            _app = WebApp.Start<Startup>(url: baseAddress);

            Log.Information("WebApp is started, listen on {0}", baseAddress);
        }

        public void Stop()
        {
            if (_app != null)
            {
                Log.Information("WebApp is stopping");

                _app.Dispose();
            }
        }
    }
}
