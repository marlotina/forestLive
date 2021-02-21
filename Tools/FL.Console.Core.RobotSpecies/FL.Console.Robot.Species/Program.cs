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
            try
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

                        var nodes = GetNodes(birdPageHtml, "//div[@class= 'mw-parser-output']//p//i");

                        if (nodes != null && nodes.Any()) {
                            birdItem.ScienceName = nodes.FirstOrDefault().InnerText;
                            var birdDataLanguage_EN = new BirdDataLanguage()
                            {
                                Url = url_EN,
                                Name = HttpUtility.HtmlEncode(GetNodes(birdPageHtml, "//div[@class= 'mw-parser-output']//p//b").FirstOrDefault().InnerText),
                                LanguageId = Guid.Parse(ID_LANGUAGE_EN)
                            };
                            birdItem.Data_EN = birdDataLanguage_EN;
                        }
                        

                        //spanish
                        birdItem.Data_ES = GetBirdLanguageData(birdPageHtml, LANGUAGE_ES, ID_LANGUAGE_ES);
                        //portugues
                        birdItem.Data_PT = GetBirdLanguageData(birdPageHtml, LANGUAGE_PT, ID_LANGUAGE_PT);
                        //Deutch
                        birdItem.Data_DE = GetBirdLanguageData(birdPageHtml, LANGUAGE_DE, ID_LANGUAGE_DE);
                        //Italian
                        birdItem.Data_IT = GetBirdLanguageData(birdPageHtml, LANGUAGE_IT, ID_LANGUAGE_IT);
                        //French
                        birdItem.Data_FR = GetBirdLanguageData_FR(birdPageHtml, LANGUAGE_FR, ID_LANGUAGE_FR);

                        InstertInDatabse(birdItem);
                    }
                }
            }
            catch (Exception ex) 
            { 
            }
        }

        private static BirdDataLanguage GetBirdLanguageData(string html, string languageClassHtml, string languageId) 
        {
            var nodeBirdLanguage = GetNodes(html, $"//li[@class= '{languageClassHtml}']//a");
            if (nodeBirdLanguage != null && nodeBirdLanguage.Any())
            {
                var birdDataLanguage = new BirdDataLanguage();
                var url = nodeBirdLanguage.FirstOrDefault().Attributes["href"].Value;
                var birdPageHtml = GetTheHtml(url);

                birdDataLanguage.Url = url;
                var node = GetNodes(birdPageHtml, "//div[@class= 'mw-parser-output']//p//b");
                birdDataLanguage.Name = node != null && node.Any() ? HttpUtility.HtmlEncode(node.FirstOrDefault().InnerText) : "NoName";
                birdDataLanguage.LanguageId = Guid.Parse(languageId);
                return birdDataLanguage;
            }

            return null;
        }

        private static BirdDataLanguage GetBirdLanguageData_FR(string html, string languageClassHtml, string languageId)
        {
            var nodeBirdLanguage = GetNodes(html, $"//li[@class= '{languageClassHtml}']//a");
            if (nodeBirdLanguage != null && nodeBirdLanguage.Any())
            {
                var birdDataLanguage = new BirdDataLanguage();

                var url_FR = nodeBirdLanguage.FirstOrDefault().Attributes["href"].Value;
                
                var birdPageHtml = GetTheHtml(url_FR);

                birdDataLanguage.Url = url_FR;
                var node = GetNodes(birdPageHtml, "//div[@class= 'mw-parser-output']//p//b");
                birdDataLanguage.Name = node != null && node.Any() ? HttpUtility.HtmlEncode(node.FirstOrDefault().InnerText) : "NoName";
                birdDataLanguage.LanguageId = Guid.Parse(languageId);
                return birdDataLanguage;
            }

            return null;
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
            SqlConnection con = new SqlConnection(@"data source=DESKTOP-JIFKME0\SQLEXPRESS; initial catalog=BirdSpecies; Integrated Security=True;");
            string query = $"INSERT INTO [dbo].[BirdSpecies] ([SpeciesId],[ScienceName]) VALUES ('{birdSpecie.SpecieId}' ,'{birdSpecie.ScienceName}')";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();

                if (birdSpecie.Data_EN != null)
                {
                    var query_EN = $"INSERT INTO [dbo].[BirdSpeciesLanguages] ([SpecieId] ,[LanguageId] ,[Name] ,[Url_Wiki]) VALUES ('{birdSpecie.SpecieId}' ,'{birdSpecie.Data_EN.LanguageId}' ,'{birdSpecie.Data_EN.Name}' , '{birdSpecie.Data_EN.Url}')";
                    cmd = new SqlCommand(query_EN, con);
                    cmd.ExecuteNonQuery();
                }

                if (birdSpecie.Data_ES != null)
                {
                    var query_ES = $"INSERT INTO [dbo].[BirdSpeciesLanguages] ([SpecieId] ,[LanguageId] ,[Name] ,[Url_Wiki])  VALUES ('{birdSpecie.SpecieId}' ,'{birdSpecie.Data_ES.LanguageId}' ,'{birdSpecie.Data_ES.Name}' ,'{birdSpecie.Data_ES.Url}')";
                    cmd = new SqlCommand(query_ES, con);
                    cmd.ExecuteNonQuery();
                }

                if (birdSpecie.Data_DE != null)
                {
                    var query_DE = $"INSERT INTO [dbo].[BirdSpeciesLanguages] ([SpecieId] ,[LanguageId] ,[Name] ,[Url_Wiki])  VALUES ('{birdSpecie.SpecieId}' ,'{birdSpecie.Data_DE.LanguageId}' ,'{birdSpecie.Data_DE.Name}' ,'{birdSpecie.Data_DE.Url}')";
                    cmd = new SqlCommand(query_DE, con);
                    cmd.ExecuteNonQuery();
                }

                if (birdSpecie.Data_IT != null)
                {
                    var query_IT = $"INSERT INTO [dbo].[BirdSpeciesLanguages] ([SpecieId] ,[LanguageId] ,[Name] ,[Url_Wiki])  VALUES ('{birdSpecie.SpecieId}' ,'{birdSpecie.Data_IT.LanguageId}' ,'{birdSpecie.Data_IT.Name}' ,'{birdSpecie.Data_IT.Url}')";
                    cmd = new SqlCommand(query_IT, con);
                    cmd.ExecuteNonQuery();
                }

                if (birdSpecie.Data_FR != null)
                {
                    var query_FR = $"INSERT INTO [dbo].[BirdSpeciesLanguages] ([SpecieId] ,[LanguageId] ,[Name] ,[Url_Wiki])  VALUES ('{birdSpecie.SpecieId}' ,'{birdSpecie.Data_FR.LanguageId}' ,'{birdSpecie.Data_FR.Name}' ,'{birdSpecie.Data_FR.Url}')";
                    cmd = new SqlCommand(query_FR, con);
                    cmd.ExecuteNonQuery();
                }

                if (birdSpecie.Data_PT != null)
                {
                    var query_PT = $"INSERT INTO [dbo].[BirdSpeciesLanguages] ([SpecieId] ,[LanguageId] ,[Name] ,[Url_Wiki])  VALUES ('{birdSpecie.SpecieId}' ,'{birdSpecie.Data_PT.LanguageId}' ,'{birdSpecie.Data_PT.Name}' ,'{birdSpecie.Data_PT.Url}')";
                    cmd = new SqlCommand(query_PT, con);
                    cmd.ExecuteNonQuery();
                }
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
