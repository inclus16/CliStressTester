using StressCLI.src.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StressCLI.Tests.Faker
{
    class TestServer:IDisposable
    {
        private readonly HttpListener Server;

        public string Address { get; set; }

        public TestServer()
        {
            Server = new HttpListener();
            SetAddress();
            Server.Prefixes.Add(Address);
        }

        private void SetAddress()
        {
            Random random = new Random();
            Address = "http://127.0.0.1:" + random.Next(10000, 20000).ToString()+"/";
        }

        public void Dispose()
        {
            Server.Close();
        }

        public async Task Start(StopSignal stopSignal)
        {
            Server.Start();
            await Task.Run(() =>
            {
                while (Server.IsListening)
                {

                    HttpListenerContext context = Server.GetContext();
                    HttpListenerRequest request = context.Request;
                    HttpListenerResponse response = context.Response;
                    response.StatusCode = (int)GetCode(stopSignal);
                    byte[] responseMessage = Encoding.UTF8.GetBytes("Test response");
                    response.OutputStream.Write(responseMessage, 0, responseMessage.Length);
                    response.OutputStream.Close();
                    response.Close();

                }
            });
        }

        private HttpStatusCode GetCode(StopSignal stopSignal)
        {
            switch (stopSignal)
            {
                case StopSignal.BadGateway:
                    return HttpStatusCode.BadGateway;
                case StopSignal.BadRequest:
                    return HttpStatusCode.BadRequest;
                case StopSignal.InternalServerError:
                    return HttpStatusCode.InternalServerError;
                case StopSignal.TooManyRequests:
                    return HttpStatusCode.TooManyRequests;
                default:
                    return HttpStatusCode.OK;
            }
        }
    }
}
