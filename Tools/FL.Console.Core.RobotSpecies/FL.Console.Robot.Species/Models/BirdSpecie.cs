using System;

namespace FL.Console.Robot.Species.Models
{
    public class BirdSpecie
    {
        public Guid SpecieId { get; set; }

        public string ScienceName { get; set; }

        public BirdDataLanguage Data_EN { get; set; }

        public BirdDataLanguage Data_IT { get; set; }

        public BirdDataLanguage Data_ES { get; set; }

        public BirdDataLanguage Data_FR { get; set; }

        public BirdDataLanguage Data_PT { get; set; }

        public BirdDataLanguage Data_DE { get; set; }
    }
}
