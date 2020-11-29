using BLL.Constants;
using BLL.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System.Xml.Serialization;
using System.IO;
using BLL.Models.XML;
using System.Text;

namespace WebCommerce.Server.Services.Frete
{
    public class WSCorreiosCalcularFrete
    {
        private IConfiguration _configuration;
        private HttpClient _http;

        public WSCorreiosCalcularFrete(IConfiguration configuration, HttpClient http)
        {
            _configuration = configuration;
            _http = http;
        }

        public async Task<ValorPrazoFrete> CalcularFrete(String cepDestino, String tipoFrete, List<Pacote> pacotes)
        {
            List<ValorPrazoFrete> ValorDosPacotesPorFrete = new List<ValorPrazoFrete>();
            foreach (var pacote in pacotes)
            {
                var resultado = await CalcularValorPrazoFrete(cepDestino, tipoFrete, pacote);

                if (resultado != null)
                    ValorDosPacotesPorFrete.Add(resultado);
            }

            if (ValorDosPacotesPorFrete.Count > 0)
            {
                ValorPrazoFrete ValorDosFretes = ValorDosPacotesPorFrete
                .GroupBy(a => a.TipoFrete)
                .Select(list => new ValorPrazoFrete
                {
                    TipoFrete = list.First().TipoFrete,
                    CodTipoFrete = list.First().CodTipoFrete,
                    Prazo = list.Max(c => c.Prazo),
                    Valor = list.Sum(c => c.Valor)

                }).ToList().First();

                return ValorDosFretes;
            }

            return null;

        }

        private async Task<ValorPrazoFrete> CalcularValorPrazoFrete(String cepDestino, String tipoFrete, Pacote pacote)
        {


            Servicos resultado = await Calcular(tipoFrete, cepDestino, pacote);

            if (resultado.cServico.Erro == "0")
            {
                var valorLimpo = resultado.cServico.Valor.Replace(".", "");
                var valorFinal = double.Parse(valorLimpo);

                return new ValorPrazoFrete()
                {
                    TipoFrete = TipoFreteConstant.GetNames(tipoFrete),
                    CodTipoFrete = tipoFrete,
                    Prazo = int.Parse(resultado.cServico.PrazoEntrega),
                    Valor = valorFinal
                };
            }
            else if (resultado.cServico.Erro == "008" || resultado.cServico.Erro == "-888")
            {
                //Ex.: SEDEX10 - não entrega naquela região
                return null;
            }
            else
            {
                throw new Exception("Erro: " + resultado.cServico.MsgErro);
            }
        }

        public async Task<Servicos> Calcular(string nCdServico, string sCepDestino, Pacote pacote)
        {
            var scepOrigem = _configuration.GetValue<string>("Frete:CepOrigem");
            var maoPropria = _configuration.GetValue<string>("Frete:MaoPropria");
            var sCdAvisoRecebimento = _configuration.GetValue<string>("Frete:AvisoRecebimento");
            var nCdEmpresa = _configuration.GetValue<string>("Frete:nCdEmpresa");
            var sDsSenha = _configuration.GetValue<string>("Frete:sDsSenha");
            var nCdFormato = _configuration.GetValue<string>("Frete:nCdFormato");
            var StrRetorno = _configuration.GetValue<string>("Frete:StrRetorno");
            var nIndicaCalculo = _configuration.GetValue<string>("Frete:nIndicaCalculo");
            var nVlValorDeclarado = _configuration.GetValue<string>("Frete:nVlValorDeclarado");
            var diametro = Math.Max(Math.Max(pacote.Comprimento, pacote.Largura), pacote.Altura);
            var sb = new StringBuilder();

            sb.Append("http://ws.correios.com.br/calculador/CalcPrecoPrazo.aspx?");
            sb.Append($"nCdEmpresa={nCdEmpresa}&");
            sb.Append($"sDsSenha={sDsSenha}&");
            sb.Append($"sCepOrigem={scepOrigem}&");
            sb.Append($"sCepDestino={sCepDestino}&");
            sb.Append($"nVlPeso={pacote.Peso}&");
            sb.Append($"nCdFormato={nCdFormato}&");
            sb.Append($"nVlComprimento={pacote.Comprimento}&");
            sb.Append($"nVlAltura={pacote.Altura}&");
            sb.Append($"nVlLargura={pacote.Largura}&");
            sb.Append($"sCdMaoPropria={maoPropria}&");
            sb.Append($"nVlValorDeclarado={nVlValorDeclarado}&");
            sb.Append($"sCdAvisoRecebimento={sCdAvisoRecebimento}&");
            sb.Append($"nCdServico={nCdServico}&");
            sb.Append($"nVlDiametro={diametro}&");
            sb.Append($"StrRetorno={StrRetorno}&");
            sb.Append($"nIndicaCalculo={nIndicaCalculo}");


            var teste = await (await _http.PostAsync(sb.ToString(), null)).Content.ReadAsStreamAsync();


            XmlSerializer serializer = new XmlSerializer(typeof(Servicos));

            using (StreamReader reader = new StreamReader(teste))
            {
                return (Servicos)serializer.Deserialize(reader);
            }
        }
    }
}
