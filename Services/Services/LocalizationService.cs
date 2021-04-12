using Barayand.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Barayand.Services.Services
{
    public class LocalizationService : ILocalizationService
    {
        private readonly IConfiguration _configuration;
        public LocalizationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetLang()
        {
            return _configuration["Language"];
        }
    }
}
