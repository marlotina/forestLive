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
    public class CountriesRepository : ICountriesRepository
    {
        private readonly IBirdsConfiguration iBirdsConfiguration;
        private readonly ILogger<CountriesRepository> iLogger;

        public CountriesRepository(
            ILogger<CountriesRepository> iLogger,
            IBirdsConfiguration iBirdsConfiguration)
        {
            this.iLogger = iLogger;
            this.iBirdsConfiguration = iBirdsConfiguration;
        }

        public async Task<List<CountryItem>> GetCountryByLanguage(Guid languageId)
        {
            var result = new List<CountryItem>();

            SqlConnection conn = new SqlConnection(this.iBirdsConfiguration.ConnectionString);
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand($"SELECT c.CountryId, c.Name FROM CountyLanguages c WHERE c.LanguageId = @languageId", conn);
                command.Parameters.AddWithValue("@languageId", languageId);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        result.Add(new CountryItem()
                        {
                            CountryId = reader["CountryId"].ToString(),
                            Name = reader["Name"].ToString()
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
