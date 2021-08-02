using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.ExternalData.Configuration.Contracts;
using FL.Web.API.Core.ExternalData.Domain.Dto;
using FL.Web.API.Core.ExternalData.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FL.Web.API.Core.ExternalData.Infrastructure.Repositories
{
    public class SpeciesRepository : ISpeciesRepository
    {
        private readonly IBirdsConfiguration iBirdsConfiguration;
        private readonly ILogger<SpeciesRepository> iLogger;

        public SpeciesRepository(
            ILogger<SpeciesRepository> iLogger,
            IBirdsConfiguration iBirdsConfiguration)
        {
            this.iLogger = iLogger;
            this.iBirdsConfiguration = iBirdsConfiguration;
        }


        public async Task<List<SpecieItem>> GetAllSpecies()
        {
            var result = new List<SpecieItem>();

            SqlConnection conn = new SqlConnection(this.iBirdsConfiguration.ConnectionString);
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand($"SELECT SpeciesId, ScienceName, UrlSpecie FROM BirdSpecies", conn);
                

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        result.Add(new SpecieItem()
                        {
                            SpecieId = Guid.Parse(reader["SpeciesId"].ToString()),
                            ScienceName = reader["ScienceName"].ToString(),
                            UrlSpecie = reader["UrlSpecie"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public async Task<List<SpecieItem>> GetSpeciesByLanguage(Guid languageId)
        {
            var result = new List<SpecieItem>();

            SqlConnection conn = new SqlConnection(this.iBirdsConfiguration.ConnectionString);
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand($"SELECT c.SpecieId, c.Name, b.ScienceName, b.UrlSpecie FROM BirdSpeciesLanguages c INNER JOIN BirdSpecies b ON b.SpeciesId = c.SpecieId WHERE LanguageId = @languageId", conn);
                command.Parameters.AddWithValue("@languageId", languageId);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        result.Add(new SpecieItem()
                        {
                            SpecieId = Guid.Parse(reader["SpecieId"].ToString()),
                            Name = reader["Name"].ToString(),
                            ScienceName = reader["ScienceName"].ToString(),
                            UrlSpecie = reader["UrlSpecie"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

    }
}
