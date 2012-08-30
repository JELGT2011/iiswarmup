using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.ServiceProcess;

namespace iiswarmup
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var url = ConfigurationManager.AppSettings["url"];
            using (var lw = new StreamWriter(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "iiswarmup.log"), true))
            {
                try
                {
                    var start = DateTime.Now;
                    using (var rsp = WebRequest.Create(url).GetResponse() as HttpWebResponse)
                    {
                        var tm = DateTime.Now - start;
                        if (rsp == null)
                        {
                            lw.WriteLine("{0} [pid:{1}] Hit {2}; took {3}ms; response is null", DateTime.Now,
                                         Process.GetCurrentProcess().Id, url, tm.TotalMilliseconds);
                        }
                        else
                        {
                            lw.WriteLine("{0} [pid:{1}] Hit {2}; took {3}ms; response code {4}; response length {5}",
                                         DateTime.Now,
                                         Process.GetCurrentProcess().Id, url, tm.TotalMilliseconds, rsp.StatusCode, rsp.ContentLength);
                        }
                    }
                }
                catch (Exception ex)
                {
                    lw.WriteLine("{0} [pid:{1}] Hit {2}; got exception {3}: {4}\nStack: {5}{6}", DateTime.Now,
                        Process.GetCurrentProcess().Id, url, ex.GetType(), ex.Message, ex.StackTrace,
                                 ex.InnerException == null
                                     ? ""
                                     : string.Format("\nInner: {0}: {1}\nInner stack: {2}", ex.InnerException.GetType(),
                                                     ex.InnerException.Message, ex.InnerException.StackTrace));
                }
            }
            Stop();
        }

        protected override void OnStop()
        {
        }
    }
}
