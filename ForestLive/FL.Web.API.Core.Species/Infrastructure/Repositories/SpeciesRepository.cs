﻿using FL.Web.API.Core.Species.Configuration.Contracts;
using FL.Web.API.Core.Species.Domain.Model;
using FL.Web.API.Core.Species.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FL.Web.API.Core.Species.Infrastructure.Repositories
{
    public class SpeciesRepository : ISpeciesRepository
    {
        private readonly IBirdsConfiguration birdsConfiguration;

        public SpeciesRepository(
            IBirdsConfiguration birdsConfiguration)
        {
            this.birdsConfiguration = birdsConfiguration;
        }

        public List<SpecieItem> GetSpeciesByLanguage(Guid languageId)
        {
            var result = new List<SpecieItem>();

            SqlConnection conn = new SqlConnection(this.birdsConfiguration.ConnectionString);
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand($"SELECT c.SpecieId, c.Name, b.ScienceName FROM BirdSpeciesLanguages c INNER JOIN BirdSpecies b ON b.SpeciesId = c.SpecieId WHERE LanguageId = @languageId", conn);
                command.Parameters.AddWithValue("@languageId", languageId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new SpecieItem()
                        {
                            SpecieId = Guid.Parse(reader["SpecieId"].ToString()),
                            Name = reader["Name"].ToString(),
                            ScienceName = reader["ScienceName"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
            }

            return result;
        }

    }
}