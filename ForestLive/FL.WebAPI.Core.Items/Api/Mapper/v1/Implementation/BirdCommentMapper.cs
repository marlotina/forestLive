using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Models.v1.Request;
using FL.WebAPI.Core.Items.Models.v1.Response;

namespace FL.WebAPI.Core.Items.Mapper.v1.Implementation
{
    public class BirdCommentMapper : IBirdCommentMapper
    {
        public BirdComment Convert(BirdCommentRequest source)
        {
            var result = default(BirdComment);
            if (source != null)
            {
                result = new BirdComment()
                {
                    ItemId = source.ItemId,
                    Text = source.Text,
                    UserId = source.UserId
                };
            }
            return result;
        }

        public BirdCommentResponse Convert(BirdComment source)
        {
            var result = default(BirdCommentResponse);
            if (source != null)
            {
                result = new BirdCommentResponse()
                {
                    Id = source.Id,
                    Text = source.Text,
                    UserId = source.UserId,
                    CreateDate = source.CreateDate,
                    ItemId = source.ItemId
                };
            }
            return result;
        }
    }
}
