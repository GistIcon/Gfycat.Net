﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Gfycat.Analytics
{
    internal static class QueryExtensions
    {
        internal static string CreateQuery(this Impression impression)
        {
            return Utils.CreateQueryString(
                ("app_id", impression.AppId), 
                ("ver", impression.Version),
                ("utc", impression.UserTrackingCookie),
                ("stc", impression.SessionTrackingCookie),
                ("gfyid", impression.GfycatId),
                ("context", impression.Context),
                ("keyword", impression.Keyword),
                ("flow", impression.Flow),
                ("viewtag", impression.ViewTag));
        }

        internal static string CreateQuery(this IEnumerable<Impression> impressions)
        {
            Impression impression = impressions.FirstOrDefault();

            IEnumerable<(string, object)> queryStringEnumerable = new (string, object)[] {
                ("app_id", impression.AppId),
                ("ver", impression.Version),
                ("utc", impression.UserTrackingCookie),
                ("stc", impression.SessionTrackingCookie),
                ("gfyid", impression.GfycatId),
                ("context", impression.Context),
                ("keyword", impression.Keyword),
                ("flow", impression.Flow),
                ("viewtag", impression.ViewTag)
            };

            for(int i = 1; i < impressions.Count(); i++)
            {
                impression = impressions.ElementAt(i);
                queryStringEnumerable.Concat(new(string, object)[] {
                    ($"gfyid_{i}", impression.GfycatId),
                    ($"context_{i}", impression.Context),
                    ($"keyword_{i}", impression.Keyword),
                    ($"flow_{i}", impression.Flow),
                    ($"viewtag_{i}", impression.ViewTag)
                });
            }

            return Utils.CreateQueryString(queryStringEnumerable.ToArray());
        }
    }
}
