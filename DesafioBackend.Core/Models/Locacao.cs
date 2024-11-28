using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DesafioBackend.Core.Models
{
    public class Locacao
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public ObjectId EntregadorId { get; set; } // Relacionado ao entregador

        public ObjectId MotoId { get; set; } // Relacionado à moto

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; } // Pode ser nulo se ainda estiver em andamento

        public string Status { get; set; } // Ex: "Ativa", "Concluída", "Cancelada"
    }
}
