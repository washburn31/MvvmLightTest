﻿using MvvmLightTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvvmLightTest.Services
{
    public class RssService : IRssService
    {
        public async Task<List<FeedItem>> GetNewsAsync(string url)
        {
            HttpClient client = new HttpClient();
            string result = await client.GetStringAsync(url);
            XDocument xdoc = XDocument.Parse(result);

            return (from item in xdoc.Descendants("item")
                    select new FeedItem
                    {
                        Title = (string)item.Element("title"),
                        Description = (string)item.Element("description"),
                        Link = (string)item.Element("link"),
                        PublishDate = DateTime.Parse((string)item.Element("pubDate"))
                    }).ToList();
        }
    }
}
