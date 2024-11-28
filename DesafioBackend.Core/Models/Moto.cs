using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DesafioBackend.Core.Models
{
    public class Moto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement("Ano")]
        public int ano { get; set; }

        [BsonElement("Modelo")]
        public string modelo { get; set; }

        [BsonElement("Placa")]
        public string placa { get; set; }
    }
}
