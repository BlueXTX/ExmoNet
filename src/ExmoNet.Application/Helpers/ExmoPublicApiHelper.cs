﻿using System.Text.Json.Nodes;
using ExmoNet.Application.Exceptions;
using RestSharp;

namespace ExmoNet.Application.Helpers;

public static class ExmoPublicApiHelper
{
    internal static RestClient CreateDefaultClient()
    {
        return new RestClient(
            new RestClientOptions("https://api.exmo.com/v1.1")
        );
    }

    internal static RestRequest AddContentTypeHeader(this RestRequest request)
    {
        return request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
    }

    internal static JsonObject ToJsonObject(this RestResponse response)
    {
        if (response.Content is null) throw new NoResponseContentException();

        return JsonNode.Parse(response.Content)?.AsObject() ?? throw new ResponseToJsonException();
    }
}
