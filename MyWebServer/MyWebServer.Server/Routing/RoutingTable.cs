﻿using MyWebServer.Server.Common;
using MyWebServer.Server.Http;
using MyWebServer.Server.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server.Routing
{
    public class RoutingTable : IRoutingTable
    {
        private readonly Dictionary<HttpMethod, Dictionary<string, HttpResponse>> routes;

        public RoutingTable() => this.routes = new()
        {
            [HttpMethod.Get] = new (),
            [HttpMethod.Post] = new (),
            [HttpMethod.Put] = new (),
            [HttpMethod.Delete] = new (),

        };

        public IRoutingTable Map(
            HttpMethod method,
            string path,
            HttpResponse response)
        {
            Guard.AgainstNull(path, nameof(path));
            Guard.AgainstNull(response, nameof(response));

            this.routes[method][path] = response;

            return this;
        }
        public IRoutingTable Map(
            string url,
            HttpMethod method,
            HttpResponse response)
            => method switch
            {
                HttpMethod.Get => this.MapGet(url, response),
                _ => throw new InvalidOperationException($"Method '{method}' is not supported."),
            };


        public IRoutingTable MapGet(
            string path,
            HttpResponse response)
            => Map(HttpMethod.Get, path, response);

        public IRoutingTable MapPost(
           string path,
           HttpResponse response)
           => Map(HttpMethod.Post, path, response);

        public HttpResponse MatchRequest(HttpRequest request)
        {
            var requestMethod = request.Method;
            var requestPath = request.Path;

            if (!this.routes.ContainsKey(requestMethod)
                || !this.routes[requestMethod].ContainsKey(requestPath))
            {
                return new NotFoundResponse();
            }

            return this.routes[requestMethod][requestPath];
        }
    }
}
