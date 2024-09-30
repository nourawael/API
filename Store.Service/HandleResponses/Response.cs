using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.HandleResponses
{
    public class Response
    {
        public Response(int statusCode, string? message=null)
        {
            StatusCode = statusCode;
            Message =message?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
            => statusCode switch 
            {
                100 => "100 Continue: Initial part of a request has been received; the client should continue.",
                101 => "101 Switching Protocols: The server is switching protocols as requested by the client.",
                200 => "200 OK: The request was successful.",
                201 => "201 Created: A new resource has been created successfully.",
                202 => "202 Accepted: The request has been accepted for processing, but not yet completed.",
                203 => "203 Non-Authoritative Information: The server successfully processed the request but returned modified data.",
                204 => "204 No Content: The request was successful, but there's no content to return.",
                205 => "205 Reset Content: The server successfully processed the request and asks the client to reset the view.",
                206 => "206 Partial Content: The server is delivering only part of the resource due to a range header.",
                400 => "Bad Request: The server cannot process the request due to a client error.",
                401 => "Unauthorized: Authentication is required and has failed or has not been provided.",
                403 => "Forbidden: The server understands the request but refuses to authorize it.",
                404 => "Not Found: The requested resource could not be found.",
                500 => "Internal Server Error: The server encountered an unexpected condition.",
                503 => "Service Unavailable: The server is currently unable to handle the request.",
                _ => "Unknown Status Code: "
            };
    }
}
