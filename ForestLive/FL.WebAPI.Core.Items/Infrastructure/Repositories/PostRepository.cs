using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Repositories;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Exceptions;
using Gremlin.Net.Structure.IO.GraphSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IItemConfiguration itemConfiguration; 
        public PostRepository(IItemConfiguration itemConfiguration)
        {
            this.itemConfiguration = itemConfiguration;
        }


        private static Dictionary<string, string> gremlinQueries = new Dictionary<string, string>
        {
            { "Cleanup",        "g.V().drop()" },
            { "AddPost", "g.addV('post').property('id', 'c54d19c7-cd15-4dcf-a242-0bb5899a5c47').property('title', 'title1').property('text', 'text1').property('pkbd', 'c54d19c7-cd15-4dcf-a242-0bb5899a5c47')" },
            { "AddPost1", "g.addV('post').property('id', 'fa271515-d426-4acb-b340-9735d3108925').property('title', 'title2').property('text', 'text2').property('pkbd', 'fa271515-d426-4acb-b340-9735d3108925')" },
            { "AddPost2", "g.addV('post').property('id', '0189a588-342e-4aa5-8386-5b1695013f32').property('title', 'title3').property('text', 'text3').property('pkbd', '0189a588-342e-4aa5-8386-5b1695013f32')" },
            { "AddPost3", "g.addV('post').property('id', '6eb667e2-daef-4f8b-85ad-2455aa8ef4ea').property('title', 'title4').property('text', 'text4').property('pkbd', '6eb667e2-daef-4f8b-85ad-2455aa8ef4ea')" },


            { "AddComment", "g.addV('comment').property('id', 'cdce96cd-e703-400d-a055-21442d04bd66').property('title', 'title1').property('text', 'text1').property('pkbd', 'cdce96cd-e703-400d-a055-21442d04bd66')" },
            { "AddComment1", "g.addV('comment').property('id', 'b5730d93-f3fc-478a-833b-ca90c70d46ab').property('title', 'title2').property('text', 'text2').property('pkbd', 'b5730d93-f3fc-478a-833b-ca90c70d46ab')" },
            { "AddComment2", "g.addV('comment').property('id', '4d1d7d56-2670-4896-b4a4-be959d7a23b3').property('title', 'title3').property('text', 'text3').property('pkbd', '4d1d7d56-2670-4896-b4a4-be959d7a23b3')" },
            { "AddComment3", "g.addV('comment').property('id', '974d679a-a8bf-4ef0-a0a6-3ea660e680d3').property('title', 'title4').property('text', 'text4').property('pkbd', '974d679a-a8bf-4ef0-a0a6-3ea660e680d3')" },


            { "AddUser", "g.addV('user').property('id', 'thomasbird').property('name', 'name1').property('email', 'text1@gmail.com').property('pkbd', 'ecb56c1b-4e44-4905-b6be-26e575292c5e')" },
            { "AddUser1", "g.addV('user').property('id', 'freebird').property('name', 'name2').property('email', 'text2@gmail.com').property('pkbd', 'a163e2fb-0f89-4ebd-9441-1b7e6e5c62f1')" },
            { "AddUser2", "g.addV('user').property('id', 'herrerillo').property('name', 'name3').property('email', 'text3@gmail.com').property('pkbd', '4f65c2ef-79cf-4c2c-9c42-5a5cc2bd434e')" },
            { "AddUser3", "g.addV('user').property('id', 'petirrojo').property('name', 'name4').property('email', 'text4@gmail.com').property('pkbd', '691a0eaa-2ae0-4f95-9343-9d4899f7a9b8')" },


            { "AddBird", "g.addV('bird').property('id', 'birdtecnico1').property('name', 'bird1').property('idSpecie', 'sdf-sdf34-fer43-ddf').property('pkbd', '0060fea1-b696-4858-9e44-59c52770af65')" },
            { "AddBird1", "g.addV('bird').property('id', 'birdtecnico2').property('name', 'bird2').property('idSpecie', 'sdf-sdf-sdf-3443').property('pkbd', '6b006a46-46d2-4155-8201-eb580460c3e9')" },
            { "AddBird2", "g.addV('bird').property('id', 'birdtecnico3).property('name', 'bird3').property('idSpecie', 'sfds-sdfs-344-sdf').property('pkbd', '0ee228db-a459-4b58-b4e3-497fb9949097')" },


            { "AddLabel", "g.addV('label').property('id', 'free').property('number', '1').property('pkbd', 'free')" },
            { "AddLabel1", "g.addV('label').property('id', 'fly').property('number', '4').property('pkbd', 'fly')" },
            { "AddLabel2", "g.addV('label').property('id', 'flymonths').property('number', '5').property('pkbd', 'flymonths')" },
            { "AddLabel3", "g.addV('label').property('id', 'bird').property('number', '4').property('pkbd', 'brid')" },



            { "AddEdgeLabel 1",      "g.V('free').addE('hastag').to(g.V('c54d19c7-cd15-4dcf-a242-0bb5899a5c47'))" },
            { "AddEdgeLabel 2",      "g.V('free').addE('hastag').to(g.V('fa271515-d426-4acb-b340-9735d3108925'))" },
            { "AddEdgeLabel 3",      "g.V('fly').addE('hastag').to(g.V('c54d19c7-cd15-4dcf-a242-0bb5899a5c47'))" },
            { "AddEdgeLabel 4",      "g.V('flymonths').addE('hastag').to(g.V('fa271515-d426-4acb-b340-9735d3108925'))" },
            { "AddEdgeLabel 5",      "g.V('flymonths').addE('hastag').to(g.V('0189a588-342e-4aa5-8386-5b1695013f32'))" },
            { "AddEdgeLabel 6",      "g.V('bird').addE('hastag').to(g.V('6eb667e2-daef-4f8b-85ad-2455aa8ef4ea'))" },


            { "AddEdgeFollow 1",      "g.V('thomasbird').addE('follow').to(g.V('freebird'))" },
            { "AddEdgeFollow 2",      "g.V('freebird').addE('follow').to(g.V('thomasbird'))" },
            { "AddEdgeFollow 3",      "g.V('herrerillo').addE('follow').to(g.V('petirrojo'))" },
            { "AddEdgeFollow 4",      "g.V('petirrojo').addE('follow').to(g.V('thomasbird'))" },


            { "AddEdgeBelong 1",      "g.V('birdtecnico1').addE('belongs').to(g.V('c54d19c7-cd15-4dcf-a242-0bb5899a5c47'))" },
            { "AddEdgeBelong 2",      "g.V('birdtecnico2').addE('belongs').to(g.V('fa271515-d426-4acb-b340-9735d3108925'))" },
            { "AddEdgeBelong 3",      "g.V('birdtecnico3').addE('belongs').to(g.V('0189a588-342e-4aa5-8386-5b1695013f32'))" },
            { "AddEdgeBelong 4",      "g.V('birdtecnico3').addE('belongs').to(g.V('6eb667e2-daef-4f8b-85ad-2455aa8ef4ea'))" },



            { "AddEdgeOwner 1",      "g.V('thomasbird').addE('owner').to(g.V('c54d19c7-cd15-4dcf-a242-0bb5899a5c47'))" },
            { "AddEdgeOwner 2",      "g.V('petirrojo').addE('owner').to(g.V('fa271515-d426-4acb-b340-9735d3108925'))" },
            { "AddEdgeOwner 3",      "g.V('thomasbird').addE('owner').to(g.V('0189a588-342e-4aa5-8386-5b1695013f32'))" },
            { "AddEdgeOwner 4",      "g.V('petirrojo').addE('owner').to(g.V('6eb667e2-daef-4f8b-85ad-2455aa8ef4ea'))" },

            { "AddEdgeCommentOwner 1",      "g.V('thomasbird').addE('knows').to(g.V('cdce96cd-e703-400d-a055-21442d04bd66'))" },
            { "AddEdgeCommentOwner 2",      "g.V('herrerillo').addE('knows').to(g.V('b5730d93-f3fc-478a-833b-ca90c70d46ab'))" },
            { "AddEdgeCommentOwner 3",      "g.V('thomasbird').addE('knows').to(g.V('4d1d7d56-2670-4896-b4a4-be959d7a23b3'))" },
            { "AddEdgeCommentOwner 4",      "g.V('petirrojo').addE('knows').to(g.V('974d679a-a8bf-4ef0-a0a6-3ea660e680d3'))" },

            { "AddEdgeComment 1",      "g.V('c54d19c7-cd15-4dcf-a242-0bb5899a5c47').addE('commentOwner').to(g.V('cdce96cd-e703-400d-a055-21442d04bd66'))" },
            { "AddEdgeComment 2",      "g.V('c54d19c7-cd15-4dcf-a242-0bb5899a5c47').addE('commentOwner').to(g.V('b5730d93-f3fc-478a-833b-ca90c70d46ab'))" },
            { "AddEdgeComment 3",      "g.V('fa271515-d426-4acb-b340-9735d3108925').addE('commentOwner').to(g.V('4d1d7d56-2670-4896-b4a4-be959d7a23b3'))" },
            { "AddEdgeComment 4",      "g.V('fa271515-d426-4acb-b340-9735d3108925').addE('commentOwner').to(g.V('974d679a-a8bf-4ef0-a0a6-3ea660e680d3'))" },
        };


        public async Task<BirdPost> AddBirdPost(BirdPost birdPost)
        {
            try
            {
                int port = this.itemConfiguration.GraphPort;
                var graphEnableSsl = this.itemConfiguration.GraphEnableSSL;

                string containerLink = "/dbs/" + this.itemConfiguration.GraphDatabase + "/colls/" + this.itemConfiguration.GraphContainer;
                

                var gremlinServer = new GremlinServer(this.itemConfiguration.GraphHost, port, enableSsl: graphEnableSsl,
                                                        username: containerLink,
                                                        password: this.itemConfiguration.GraphPrimaryKey);

                ConnectionPoolSettings connectionPoolSettings = new ConnectionPoolSettings()
                {
                    MaxInProcessPerConnection = 10,
                    PoolSize = 30,
                    ReconnectionAttempts = 3,
                    ReconnectionBaseDelay = TimeSpan.FromMilliseconds(500)
                };

                var webSocketConfiguration =
                    new Action<ClientWebSocketOptions>(options =>
                    {
                        options.KeepAliveInterval = TimeSpan.FromSeconds(10);
                    });


                using (var gremlinClient = new GremlinClient(
                    gremlinServer,
                    new GraphSON2Reader(),
                    new GraphSON2Writer(),
                    GremlinClient.GraphSON2MimeType,
                    connectionPoolSettings,
                    webSocketConfiguration))
                {
                    foreach (var query in gremlinQueries)
                    {
                        Console.WriteLine(String.Format("Running this query: {0}: {1}", query.Key, query.Value));

                        // Create async task to execute the Gremlin query.
                        var resultSet = SubmitRequest(gremlinClient, query).Result;
                        if (resultSet.Count > 0)
                        {
                            Console.WriteLine("\tResult:");
                            foreach (var result in resultSet)
                            {
                                // The vertex results are formed as Dictionaries with a nested dictionary for their properties
                                string output = JsonConvert.SerializeObject(result);
                                Console.WriteLine($"\t{output}");
                            }
                            Console.WriteLine();
                        }

                        // Print the status attributes for the result set.
                        // This includes the following:
                        //  x-ms-status-code            : This is the sub-status code which is specific to Cosmos DB.
                        //  x-ms-total-request-charge   : The total request units charged for processing a request.
                        //  x-ms-total-server-time-ms   : The total time executing processing the request on the server.
                        PrintStatusAttributes(resultSet.StatusAttributes);
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                //this.logger.LogError(ex);
            }

            return birdPost;
        }
        private static Task<ResultSet<dynamic>> SubmitRequest(GremlinClient gremlinClient, KeyValuePair<string, string> query)
        {
            try
            {
                return gremlinClient.SubmitAsync<dynamic>(query.Value);
            }
            catch (ResponseException e)
            {
                Console.WriteLine("\tRequest Error!");

                // Print the Gremlin status code.
                Console.WriteLine($"\tStatusCode: {e.StatusCode}");

                // On error, ResponseException.StatusAttributes will include the common StatusAttributes for successful requests, as well as
                // additional attributes for retry handling and diagnostics.
                // These include:
                //  x-ms-retry-after-ms         : The number of milliseconds to wait to retry the operation after an initial operation was throttled. This will be populated when
                //                              : attribute 'x-ms-status-code' returns 429.
                //  x-ms-activity-id            : Represents a unique identifier for the operation. Commonly used for troubleshooting purposes.
                PrintStatusAttributes(e.StatusAttributes);
                Console.WriteLine($"\t[\"x-ms-retry-after-ms\"] : { GetValueAsString(e.StatusAttributes, "x-ms-retry-after-ms")}");
                Console.WriteLine($"\t[\"x-ms-activity-id\"] : { GetValueAsString(e.StatusAttributes, "x-ms-activity-id")}");

                throw;
            }
        }

        private static void PrintStatusAttributes(IReadOnlyDictionary<string, object> attributes)
        {
            Console.WriteLine($"\tStatusAttributes:");
            Console.WriteLine($"\t[\"x-ms-status-code\"] : { GetValueAsString(attributes, "x-ms-status-code")}");
            Console.WriteLine($"\t[\"x-ms-total-server-time-ms\"] : { GetValueAsString(attributes, "x-ms-total-server-time-ms")}");
            Console.WriteLine($"\t[\"x-ms-total-request-charge\"] : { GetValueAsString(attributes, "x-ms-total-request-charge")}");
        }

        public static string GetValueAsString(IReadOnlyDictionary<string, object> dictionary, string key)
        {
            return JsonConvert.SerializeObject(GetValueOrDefault(dictionary, key));
        }

        public static object GetValueOrDefault(IReadOnlyDictionary<string, object> dictionary, string key)
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }

            return null;
        }

        public async Task<bool> DeleteBirdPost(Guid idPost)
        {
            try
            {
                
                return true;
            }
            catch (Exception ex)
            {
                //this.logger.LogError(ex);
            }

            return false;
        }
    }
}
