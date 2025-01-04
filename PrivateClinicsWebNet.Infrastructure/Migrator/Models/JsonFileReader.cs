using PrivateClinicsWebNet.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PrivateClinicsWebNet.Infrastructure.Migrator.Abstractions;
using Microsoft.Extensions.Logging;

namespace PrivateClinicsWebNet.Infrastructure.Migrator.Models
{
    public class JsonFileReader : IFileReader
    {
        private readonly ILogger _logger;

        public JsonFileReader(ILogger<JsonFileReader> logger)
        {
            _logger = logger;
        }
        public IEnumerable<Doctor> Read(string path)
        {
            JsonSerializer serializer = new JsonSerializer();
            var doctorsList = new List<Doctor>();
            //try
            //{
                using StreamReader fileReader = File.OpenText(path);
                using var jsonReader = new JsonTextReader(fileReader);
                while (jsonReader.Read())
                {
                    if (jsonReader.TokenType == JsonToken.StartObject)
                    {
                        var doctor = serializer.Deserialize<Doctor>(jsonReader);
                        yield return doctor;
                    }
                }
            ////}
            ////catch (Exception ex)
            ////{
            //    _logger.LogError(ex, "An error occured while was started file reading",
            //        DateTime.UtcNow.ToLongTimeString());
            //}
        }
    }
}
