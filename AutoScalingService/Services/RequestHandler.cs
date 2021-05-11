using AutoScalingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoScalingService.Services
{
    public class RequestHandler: IRequestHandler
    {
        private Dictionary<string, Request> requestById;

        public RequestHandler()
        {
            this.requestById = new Dictionary<string, Request>();
        }

        public string Push(Request request)
        {
            string requestId = Guid.NewGuid().ToString();
            request.Id = requestId;
            this.requestById.Add(requestId, request);
            return requestId;
        }
        public Request Delete(string requestId)
        {
            var request = this.Get(requestId);
            this.requestById.Remove(requestId);
            return request;
        }

        public Request Get(string requestId)
        {
            if(!this.requestById.ContainsKey(requestId))
            {
                throw new Exception($"unrecognised request ({requestById})");
            }
            var request = this.requestById[requestId];
            return request;
        }
    }
}
