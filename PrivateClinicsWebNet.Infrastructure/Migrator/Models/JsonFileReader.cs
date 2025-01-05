using PrivateClinicsWebNet.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PrivateClinicsWebNet.Infrastructure.Migrator.Abstractions;
using Microsoft.Extensions.Logging;
using PrivateClinicsWebNet.Infrastructure.Migrator.DTOs;

namespace PrivateClinicsWebNet.Infrastructure.Migrator.Models
{
    public class JsonFileReader : IFileReader
    {
        private readonly ILogger _logger;

        public JsonFileReader(ILogger<JsonFileReader> logger)
        {
            _logger = logger;
        }
        public IEnumerable<MigrationDto> Read(string path)
        {
            JsonSerializer serializer = new JsonSerializer();
            var migrationsList = new List<MigrationDto>();
            using StreamReader fileReader = File.OpenText(path);
            using var jsonReader = new JsonTextReader(fileReader);
            while (jsonReader.Read())
            {
                if (jsonReader.TokenType == JsonToken.StartObject)
                {
                    var migrationDto = serializer.Deserialize<MigrationDto>(jsonReader);
                    yield return migrationDto;
                }
            }
            _logger.LogInformation("File has reading successfully");
        }
    }
}
