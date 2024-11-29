using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DesafioBackend.Core.Models
{
    public class Locacao
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("identificador")]
        public string Identificador { get; set; }
        [BsonElement("entregador_id")]
        public string EntregadorId { get; set; }
        [BsonElement("moto_id")]
        public string MotoId { get; set; }
        [BsonElement("data_inicio")]
        public DateTime DataInicio { get; set; }
        [BsonElement("data_termino")]
        public DateTime DataTermino { get; set; }
        [BsonElement("data_previsao_termino")]
        public DateTime DataPrevisaoTermino { get; set; }
        [BsonElement("data_devolucao")]
        public DateTime? DataDevolucao { get; set; }
        [BsonElement("valor_diaria")]
        public int Plano { get; set; }
        public decimal ValorDiaria { get; set; }
        public decimal CustoTotal { get; set; }
        public decimal Multa { get; set; }
    }
}
