using System;
using System.Collections.Generic;

namespace FL.Console.Robot.Species
{
    public static class LanguageHelper
    {
        public const string DefoultLanguageId = "1d134297-b070-4149-a2e0-c2643de48f95";
        public const string DefoultCode = "en";
        
        public static Guid GetLanguageByCode(string code) 
        {
            var languageList = new Dictionary<string, string>
                    {
                        { "en", "1d134297-b070-4149-a2e0-c2643de48f95" },
                        { "es", "c7bc511d-562b-4a59-a434-2d754fe40f5c" },
                        { "de", "17368404-4355-49c8-b289-08f357a92b9f" },
                        { "it", "8d988fc8-83df-4ae8-b5ca-cd6ba55ae6e1" },
                        { "fr", "9be90c07-abf3-43ac-863b-1f5ae1581a47" },
                        { "pt", "3217cde9-75c1-4268-b8fe-490255ca5f48" }
                    };
            var response = languageList[code];

            if (string.IsNullOrEmpty(response))
                response = DefoultLanguageId;

            return Guid.Parse(response);
        }

        public static string GetLanguageById(string languageId)
        {
            var languageList = new Dictionary<string, string>
                    {
                        { "1d134297-b070-4149-a2e0-c2643de48f95", "en" },
                        { "c7bc511d-562b-4a59-a434-2d754fe40f5c", "es" },
                        { "17368404-4355-49c8-b289-08f357a92b9f", "de" },
                        { "8d988fc8-83df-4ae8-b5ca-cd6ba55ae6e1", "it" },
                        { "9be90c07-abf3-43ac-863b-1f5ae1581a47", "fr" },
                        { "3217cde9-75c1-4268-b8fe-490255ca5f48", "pt" }
                    };
            var response = languageList[languageId];

            if (string.IsNullOrEmpty(response))
                response = DefoultCode;

            return response;
        }
    }
}
