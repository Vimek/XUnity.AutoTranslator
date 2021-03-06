﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using SimpleJSON;
using UnityEngine;
using XUnity.AutoTranslator.Plugin.Core.Configuration;
using XUnity.AutoTranslator.Plugin.Core.Constants;
using XUnity.AutoTranslator.Plugin.Core.Extensions;

namespace XUnity.AutoTranslator.Plugin.Core.Web
{
    public class WatsonTranslateEndpoint : KnownEndpoint
    {
        private static ServicePoint ServicePoint;

        private static readonly string HttpsServicePointTemplateUrl = Settings.WatsonAPIUrl.TrimEnd('/')+ "/v2/translate?model_id={0}-{1}&text={2}";

        public WatsonTranslateEndpoint()
           : base(KnownEndpointNames.WatsonTranslate)
        {

        }

        public override void ApplyHeaders(Dictionary<string, string> headers)
        {
            headers["User-Agent"] = "curl/7.55.1";
            headers["Accept"] = "application/json";
            headers["Accept-Charset"] = "UTF-8";
            headers["Authorization"] = "Basic "+System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(Settings.WatsonAPIUsername+":"+Settings.WatsonAPIPassword));
        }

        public override void ConfigureServicePointManager()
        {
            try
            {
                ServicePoint = ServicePointManager.FindServicePoint(new Uri(Settings.WatsonAPIUrl));
                ServicePoint.ConnectionLimit = Settings.MaxConcurrentTranslations;
            }
            catch
            {
            }
        }

        public override bool TryExtractTranslated(string result, out string translated)
        {
            try
            {
                var obj = JSON.Parse(result);
                var lineBuilder = new StringBuilder(result.Length);
                
                foreach (JSONNode entry in obj.AsObject["translations"].AsArray) {
                    var token = entry.AsObject["translation"].ToString();
                    token = token.Substring(1, token.Length - 2).UnescapeJson();

                    if (!lineBuilder.EndsWithWhitespaceOrNewline()) lineBuilder.Append("\n");

                    lineBuilder.Append(token);
                }
                translated = lineBuilder.ToString();               
                return true;
            }
            catch (Exception)
            {
                translated = null;
                return false;
            }
        }

        public override string GetServiceUrl(string untranslatedText, string from, string to)
        {            
            return string.Format(HttpsServicePointTemplateUrl, from, to, WWW.EscapeURL(untranslatedText));
        }
    }
}
