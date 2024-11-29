using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DesafioBackend.Core.Models
{
    public class Entregador
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("Identificador")]
        public string Identficador { get; set; }

        [BsonElement("nome")]
        public string Nome { get; set; }

        [BsonElement("cnpj")]
        public string CNPJ { get; set; }

        [BsonElement("data_nascimento")]
        public DateTime DataNascimento { get; set; }

        [BsonElement("numero_cnh")]
        public string NumeroCNH { get; set; }

        [BsonElement("tipo_cnh")]
        public string TipoCNH { get; set; }

        [BsonElement("imagem_cnh")]
        public string? ImagemCNH { get; set; }
    }
}
