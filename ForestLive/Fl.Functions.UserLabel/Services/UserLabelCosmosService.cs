using Fl.Functions.UserLabel.Dto;
using FL.Functions.UserLabel.Services;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabelItem = Fl.Functions.UserLabel.Model.UserLabel;

namespace FL.Functions.UserPost.Services
{
    public class UserLabelCosmosService : IUserLabelCosmosService
    {
        private Container usersContainer;

        public UserLabelCosmosService(CosmosClient dbClient, string databaseName)
        {
            this.usersContainer = dbClient.GetContainer(databaseName, "userlabels");
        }

        public async Task AddLabelAsync(IEnumerable<LabelItem> labels)
        {
            try
            {
                if (labels != null && labels.Any()) 
                {
                    foreach (var label in labels)
                    {
                        ItemResponse<LabelItem> response = null;
                        try
                        {
                            response = await this.usersContainer.ReadItemAsync<LabelItem>(label.Id, new PartitionKey(label.UserId));
                        }
                        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound) { }

                        if (response != null && !string.IsNullOrEmpty(response.Resource.Id))
                        {
                            var obj = new dynamic[] { response.Resource.Id };
                            await this.usersContainer.Scripts.ExecuteStoredProcedureAsync<string>("updateLabelCount", new PartitionKey(response.Resource.UserId), obj);
                        }
                        else
                        {
                            await this.usersContainer.CreateItemAsync(label, new PartitionKey(label.UserId));
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public async Task RemovePostLabelAsync(IEnumerable<RemoveLabelDto> removeLabels)
        {
            try
            {
                if (removeLabels != null && removeLabels.Any())
                {
                    foreach (var label in removeLabels)
                    {
                        var obj = new dynamic[] { label.Label };
                        await this.usersContainer.Scripts.ExecuteStoredProcedureAsync<string>("deletePostLabelCount", new PartitionKey(label.UserId), obj);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
