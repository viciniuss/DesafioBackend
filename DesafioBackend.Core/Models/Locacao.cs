using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DesafioBackend.Core.Models
{
    public class Locacao
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public ObjectId EntregadorId { get; set; } 

        public ObjectId MotoId { get; set; } 

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; } 

        public string Status { get; set; } 
    }
}
