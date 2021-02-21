using FL.Console.Robot.Species.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace FL.Console.Robot.Species
{
    class Program
    {
        private const string URL_WIKI_BIRDS_BY_COMMON_NAME = "https://en.wikipedia.org/wiki/List_of_birds_by_common_name";
        private const string DOMAIN_WIKI_EN = "https://en.wikipedia.org";

        private const string LANGUAGE_DE = "interlanguage-link interwiki-de";
        private const string LANGUAGE_FR = "interlanguage-link interwiki-fr";
        private const string LANGUAGE_IT = "interlanguage-link interwiki-it";
        private const string LANGUAGE_PT = "interlanguage-link interwiki-pt";
        private const string LANGUAGE_ES = "interlanguage-link interwiki-es";


        private const string ID_LANGUAGE_EN = "1d134297-b070-4149-a2e0-c2643de48f95";
        private const string ID_LANGUAGE_DE = "17368404-4355-49c8-b289-08f357a92b9f";
        private const string ID_LANGUAGE_FR = "9be90c07-abf3-43ac-863b-1f5ae1581a47";
        private const string ID_LANGUAGE_IT = "8d988fc8-83df-4ae8-b5ca-cd6ba55ae6e1";
        private const string ID_LANGUAGE_PT = "3217cde9-75c1-4268-b8fe-490255ca5f48";
        private const string ID_LANGUAGE_ES = "c7bc511d-562b-4a59-a434-2d754fe40f5c";

        static void Main(string[] args)
        {
            var comonBirdsHtml = GetTheHtml(URL_WIKI_BIRDS_BY_COMMON_NAME);
            var birdsGropuByLetter = GetNodes(comonBirdsHtml, "//div[@class= 'div-col']");

            foreach (HtmlNode node in birdsGropuByLetter)
            {
                var links = GetNodes(node.InnerHtml, "//a");

                foreach (var link in links)
                {
                    var birdItem = new BirdSpecie();
                    birdItem.SpecieId = Guid.NewGuid();

                    var url_EN = DOMAIN_WIKI_EN + link.Attributes["href"].Value;
                    var birdPageHtml = GetTheHtml(url_EN);

                    birdItem.ScienceName = GetNodes(birdPageHtml, "//div[@class= 'mw-parser-output']//p//i").FirstOrDefault().InnerText;
                    var birdDataLanguage_EN = new BirdDataLanguage()
                    {
                        Url = url_EN,
                        Name = HttpUtility.HtmlDecode(GetNodes(birdPageHtml, "//div[@class= 'mw-parser-output']//p//b").FirstOrDefault().InnerText),
                        LanguageId = Guid.Parse(ID_LANGUAGE_EN)
                    };
                    birdItem.Data_EN = birdDataLanguage_EN;

                    //spanish
                    var nodeBirdLanguage = GetNodes(birdPageHtml, $"//li[@class= '{LANGUAGE_ES}']//a");
                    if (nodeBirdLanguage != null && nodeBirdLanguage.Any())
                    {
                        var url_ES = nodeBirdLanguage.FirstOrDefault().Attributes["href"].Value;
                        birdPageHtml = GetTheHtml(url_ES);

                        var birdDataLanguage_ES = new BirdDataLanguage()
                        {
                            Url = url_ES,
                            Name = HttpUtility.HtmlDecode(GetNodes(birdPageHtml, "//div[@class= 'mw-parser-output']//p//b").FirstOrDefault().InnerText),
                            LanguageId = Guid.Parse(ID_LANGUAGE_ES)
                        };
                        birdItem.Data_ES = birdDataLanguage_ES;
                    }

                    //portugues
                    nodeBirdLanguage = GetNodes(birdPageHtml, $"//li[@class= '{LANGUAGE_PT}']//a");
                    if (nodeBirdLanguage != null && nodeBirdLanguage.Any())
                    {
                        var url_PT = nodeBirdLanguage.FirstOrDefault().Attributes["href"].Value;
                        birdPageHtml = GetTheHtml(url_PT);

                        var birdDataLanguage_PT = new BirdDataLanguage()
                        {
                            Url = url_PT,
                            Name = HttpUtility.HtmlDecode(GetNodes(birdPageHtml, "//div[@class= 'mw-parser-output']//p//b").FirstOrDefault().InnerText),
                            LanguageId = Guid.Parse(ID_LANGUAGE_PT)
                        };
                        birdItem.Data_PT = birdDataLanguage_PT;
                    }

                    //Deutch
                    nodeBirdLanguage = GetNodes(birdPageHtml, $"//li[@class= '{LANGUAGE_DE}']//a");
                    if (nodeBirdLanguage != null && nodeBirdLanguage.Any())
                    {
                        var url_DE = nodeBirdLanguage.FirstOrDefault().Attributes["href"].Value;
                        birdPageHtml = GetTheHtml(url_DE);

                        var birdDataLanguage_DE = new BirdDataLanguage()
                        {
                            Url = url_DE,
                            Name = HttpUtility.HtmlDecode(GetNodes(birdPageHtml, "//div[@class= 'mw-parser-output']//p//b").FirstOrDefault().InnerText),
                            LanguageId = Guid.Parse(ID_LANGUAGE_DE)
                        };
                        birdItem.Data_DE = birdDataLanguage_DE;
                    }

                    //Italian
                    nodeBirdLanguage = GetNodes(birdPageHtml, $"//li[@class= '{LANGUAGE_IT}']//a");
                    if (nodeBirdLanguage != null && nodeBirdLanguage.Any())
                    {
                        var url_IT = nodeBirdLanguage.FirstOrDefault().Attributes["href"].Value;
                        birdPageHtml = GetTheHtml(url_IT);

                        var birdDataLanguage_IT = new BirdDataLanguage()
                        {
                            Url = url_IT,
                            Name = HttpUtility.HtmlDecode(GetNodes(birdPageHtml, "//div[@class= 'mw-parser-output']//p//b").FirstOrDefault().InnerText),
                            LanguageId = Guid.Parse(ID_LANGUAGE_IT)
                        };
                        birdItem.Data_IT = birdDataLanguage_IT;
                    }

                    //French
                    nodeBirdLanguage = GetNodes(birdPageHtml, $"//li[@class= '{LANGUAGE_FR}']//a");
                    if (nodeBirdLanguage != null && nodeBirdLanguage.Any())
                    {
                        var url_FR = nodeBirdLanguage.FirstOrDefault().Attributes["href"].Value;
                        birdPageHtml = GetTheHtml(url_FR);

                        var birdDataLanguage_FR = new BirdDataLanguage()
                        {
                            Url = url_FR,
                            Name = HttpUtility.HtmlDecode(GetNodes(birdPageHtml, "//div[@class= 'mw-parser-output']//p//b").FirstOrDefault().InnerText),
                            LanguageId = Guid.Parse(ID_LANGUAGE_FR)
                        };
                        birdItem.Data_FR = birdDataLanguage_FR;
                    }

                    InstertInDatabse(birdItem);
                }
            }
        }

        private static BirdDataLanguage GetLanguageData(string html, string languageClassHtml) 
        {
            var nodeBirdLanguage = GetNodes(html, $"//li[@class= '{LANGUAGE_FR}']//a");
            if (nodeBirdLanguage != null && nodeBirdLanguage.Any())
            {
                var url_FR = nodeBirdLanguage.FirstOrDefault().Attributes["href"].Value;
                var birdPageHtml = GetTheHtml(url_FR);

                var birdDataLanguage = new BirdDataLanguage()
                {
                    Url = url_FR,
                    Name = HttpUtility.HtmlDecode(GetNodes(birdPageHtml, "//div[@class= 'mw-parser-output']//p//b").FirstOrDefault().InnerText),
                    LanguageId = Guid.Parse(ID_LANGUAGE_FR)
                };
                return birdDataLanguage;
            }
        }

        private static string GetTheHtml(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (String.IsNullOrWhiteSpace(response.CharacterSet))
                    readStream = new StreamReader(receiveStream);
                else
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                string data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();

                return data;
            }

            return null;
        }

        private static HtmlNodeCollection GetNodes(string html, string filter)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var nodes = doc.DocumentNode.SelectNodes(filter);

            return nodes;
        }

        private static void InstertInDatabse(BirdSpecie birdSpecie) 
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ComputerShop;Integrated Security=True");
            string query = $"INSERT INTO [dbo].[BirdSpecies] ([SpeciesId],[ScienceName]) VALUES ({birdSpecie.SpecieId} ,{birdSpecie.ScienceName})";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();

                var query_EN = $"INSERT INTO [dbo].[BirdSpeciesLanguages] ([SpecieId] ,[LanguageId] ,[Name] ,[Url_Wiki]) VALUES ({birdSpecie.SpecieId} ,{birdSpecie.Data_EN.LanguageId} ,{birdSpecie.Data_EN.Name} ,{birdSpecie.Data_EN.Url})";
                cmd = new SqlCommand(query_EN, con);
                cmd.ExecuteNonQuery();

                var query_ES = $"INSERT INTO [dbo].[BirdSpeciesLanguages] ([SpecieId] ,[LanguageId] ,[Name] ,[Url_Wiki])  VALUES ({birdSpecie.SpecieId} ,{birdSpecie.Data_ES.LanguageId} ,{birdSpecie.Data_ES.Name} ,{birdSpecie.Data_ES.Url})";
                cmd = new SqlCommand(query_ES, con);
                cmd.ExecuteNonQuery();

                var query_DE = $"INSERT INTO [dbo].[BirdSpeciesLanguages] ([SpecieId] ,[LanguageId] ,[Name] ,[Url_Wiki])  VALUES ({birdSpecie.SpecieId} ,{birdSpecie.Data_DE.LanguageId} ,{birdSpecie.Data_DE.Name} ,{birdSpecie.Data_DE.Url})";
                cmd = new SqlCommand(query_DE, con);
                cmd.ExecuteNonQuery();

                var query_IT = $"INSERT INTO [dbo].[BirdSpeciesLanguages] ([SpecieId] ,[LanguageId] ,[Name] ,[Url_Wiki])  VALUES ({birdSpecie.SpecieId} ,{birdSpecie.Data_IT.LanguageId} ,{birdSpecie.Data_IT.Name} ,{birdSpecie.Data_IT.Url})";
                cmd = new SqlCommand(query_IT, con);
                cmd.ExecuteNonQuery();

                var query_FR = $"INSERT INTO [dbo].[BirdSpeciesLanguages] ([SpecieId] ,[LanguageId] ,[Name] ,[Url_Wiki])  VALUES ({birdSpecie.SpecieId} ,{birdSpecie.Data_FR.LanguageId} ,{birdSpecie.Data_FR.Name} ,{birdSpecie.Data_FR.Url})";
                cmd = new SqlCommand(query_FR, con);
                cmd.ExecuteNonQuery();

                var query_PT = $"INSERT INTO [dbo].[BirdSpeciesLanguages] ([SpecieId] ,[LanguageId] ,[Name] ,[Url_Wiki])  VALUES ({birdSpecie.SpecieId} ,{birdSpecie.Data_PT.LanguageId} ,{birdSpecie.Data_PT.Name} ,{birdSpecie.Data_PT.Url})";
                cmd = new SqlCommand(query_PT, con);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                //Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                con.Close();
            }
        }
    }
}
