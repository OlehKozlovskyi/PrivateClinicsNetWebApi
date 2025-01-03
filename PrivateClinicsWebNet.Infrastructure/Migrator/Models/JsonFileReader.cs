using PrivateClinicsWebNet.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PrivateClinicsWebNet.Infrastructure.Migrator.Abstractions;

namespace PrivateClinicsWebNet.Infrastructure.Migrator.Models
{
    public class JsonFileReader : IFileReader
    {
        public List<Doctor> Read(string link)
        {
            JsonSerializer serializer = new JsonSerializer();
            var doctorsList = new List<Doctor>();
            using (StreamReader fileReader = File.OpenText(link))
            using (JsonTextReader jsonReader = new JsonTextReader(fileReader))
            {
                while (jsonReader.Read())
                {
                    if (jsonReader.TokenType == JsonToken.StartObject)
                    {
                        var doctor = serializer.Deserialize<Doctor>(jsonReader);
                        doctorsList.Add(doctor);
                    }
                }
            }
            return doctorsList;
        }
    }
}
