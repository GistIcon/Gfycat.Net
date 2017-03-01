﻿using Newtonsoft.Json;
using System;
using System.Net;

namespace Gfycat
{
    [JsonObject("errorMessage")]
    public class GfycatException : Exception
    {
        public HttpStatusCode HttpCode { get; set; }

        [JsonProperty("message")]
        public string ServerMessage { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
        
        public override string Message
        {
            get
            {
                if (ServerMessage != null)
                    return $"The server responded with {HttpCode}: {ServerMessage}";
                else if (Description != null)
                    return $"The server responded with {HttpCode} {Code}: {Description}";
                else if (Description == null && ServerMessage == null)
                    return $"The server responded with {(int)HttpCode}: {HttpCode}";
                else
                    return base.Message;
            }
        }

        public GfycatException() : base() { }

        internal static GfycatException CreateFromResponse(RestResponse restResponse)
        {
            GfycatException exception = new GfycatException()
            {
                HttpCode = restResponse.Status
            };
            string result = restResponse.ReadAsString();
            if (!string.IsNullOrWhiteSpace(result) && !result.StartsWith("<html>"))
                JsonConvert.PopulateObject(result, exception);
            return exception;
        }
    }
}
