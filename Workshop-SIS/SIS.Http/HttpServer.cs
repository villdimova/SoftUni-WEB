﻿

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Http
{
    public class HttpServer : IHttpServer
    {
       
        IDictionary<string, Func<HttpRequest, HttpResponse>> routTable = new Dictionary<string, Func<HttpRequest, HttpResponse>>();
        public void AddRoute(string path, Func<HttpRequest, HttpResponse> action)
        {
            if (routTable.ContainsKey(path))
            {
                routTable[path] = action;
            }
            else
            {
                routTable.Add(path, action);
            }
            
        }

        public async Task StartAsync(int port)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback,port);
            tcpListener.Start();
            while (true)
            {
               TcpClient tcpClient= await tcpListener.AcceptTcpClientAsync();

                ProcessClientAsync(tcpClient);
            }
        }

        private async Task ProcessClientAsync(TcpClient tcpClient)
        {
            try
            {
                using (NetworkStream stream = tcpClient.GetStream())
                {
                    List<byte> data = new List<byte>();
                    int position = 0;
                    byte[] buffer = new byte[HttpConstants.BufferSize];
                    while (true)
                    {
                        int count = await stream.ReadAsync(buffer, position, buffer.Length);
                        position += count;

                        if (count < buffer.Length)
                        {
                            var partialBuffer = new byte[count];
                            Array.Copy(buffer, partialBuffer, count);
                            data.AddRange(partialBuffer);
                            break;
                        }
                        else
                        {
                            data.AddRange(buffer);
                        }


                        if (count == 0)
                        {
                            break;
                        }
                    }

                    //byte[]=>string(text) --- Encoding

                    var requestAsString = Encoding.UTF8.GetString(data.ToArray());
                    var request = new HttpRequest(requestAsString);
                    Console.WriteLine($"{ request.Method} { request.Path} => {request.Headers.Count} headers");

                    HttpResponse response;
                    if (this.routTable.ContainsKey(request.Path))
                    {
                        var action = this.routTable[request.Path];
                        response = action(request);
                    }
                    else
                    {
                        response = new HttpResponse("text/html", new byte[0], HttpStatusCode.NotFound);
                    }

                    response.Cookies.Add(new ResponseCookie("sid", Guid.NewGuid().ToString())
                    { HttpOnly = true, MaxAge = 60 * 24 * 60 * 60 });
                    response.Headers.Add(new Header("Server", "SUS Server 1.0"));
                    var responseHeaderBytes = Encoding.UTF8.GetBytes(response.ToString());
                    await stream.WriteAsync(responseHeaderBytes, 0, responseHeaderBytes.Length);
                    await stream.WriteAsync(response.Body, 0, response.Body.Length);

                    tcpClient.Close();


                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex); 
            }
            
           
           
           
        }
    }
}