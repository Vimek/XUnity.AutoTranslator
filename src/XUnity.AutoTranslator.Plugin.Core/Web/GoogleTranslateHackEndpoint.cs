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
    public class GoogleTranslateHackEndpoint : KnownEndpoint
    {
        private static ServicePoint ServicePoint;
   
        private static readonly string HttpsServicePointTemplateUrl = "https://translate.google.com/m?hl=pl&sl={0}&tl={1}&ie=UTF-8&q={2}";

        // Author: Johnny Cee (https://stackoverflow.com/questions/10709821/find-text-in-string-with-c-sharp)
        private static string getBetween(string strSource, string strStart, string strEnd)
        {
            const int kNotFound = -1;

            var startIdx = strSource.IndexOf(strStart);
            if (startIdx != kNotFound)
            {
                startIdx += strStart.Length;
                var endIdx = strSource.IndexOf(strEnd, startIdx);
                if (endIdx > startIdx)
                {
                    return strSource.Substring(startIdx, endIdx - startIdx);
                }
            }
            return String.Empty;
        }

        public GoogleTranslateHackEndpoint()
           : base(KnownEndpointNames.GoogleTranslateHack)
        {

        }

        public override void ApplyHeaders(Dictionary<string, string> headers)
        {
            headers["User-Agent"] = "Opera/9.80 (J2ME/MIDP; Opera Mini/5.1.21214/28.2725; U; en) Presto/2.8.119 Version/11.10";
            headers["Accept"] = "*/*";
            headers["Accept-Charset"] = "UTF-8";
        }

        public override void ConfigureServicePointManager()
        {
            try
            {

                ServicePoint = ServicePointManager.FindServicePoint(new Uri("https://translate.google.com"));
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

                String extracted = getBetween(result, "class=\"t0\">", "</div>");
                if (String.IsNullOrEmpty(extracted))
                {
                    translated = null;
                    return false;
                }
                else
                {
                    translated = RestSharp.Contrib.HttpUtility.HtmlDecode(extracted);
                    return true;
                }
            }
            catch
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
