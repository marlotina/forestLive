using System;

namespace FL.WebAPI.Core.Pendings.Api.Models.v1.Response
{
    public class BirdPendingResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string AltImage { get; set; }

        public DateTime CreateDate { get; set; }

        public string UserId { get; set; }

        public int CommentsCount { get; set; }

        public string BirdSpecie { get; set; }

        public Guid SpecieId { get; set; }

        public string UserUrl { get; set; }

        public string ObservationDate { get; set; }

        public Guid PostId { get; set; }
    }
}
