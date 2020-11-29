using BLL.Models;
using BLL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Library.Gerenciador.Frete
{
    public class CalcularPacote
    {
        public List<Pacote> CalcularPacotesDeProdutos(List<ItemVendaViewModel> produtos)
        {
            List<Pacote> pacotes = new List<Pacote>();

            Pacote pacote = new Pacote();
            foreach (var item in produtos)
            {
                for (int i = 0; i < item.Quantidade; i++)
                {
                    var peso = pacote.Peso + item.Produto.Peso;
                    var comprimento = (pacote.Comprimento > item.Produto.Comprimento) ? pacote.Comprimento : item.Produto.Comprimento;
                    var largura = (pacote.Largura > item.Produto.Largura) ? pacote.Largura : item.Produto.Largura;
                    var altura = pacote.Altura + item.Produto.Altura;

                    var dimensao = comprimento + largura + altura;

                    if (peso > 30 || dimensao > 200 || altura > 105 || comprimento > 105 || largura > 105)
                    {
                        pacotes.Add(pacote);
                        pacote = new Pacote();
                    }

                    pacote.Peso += item.Produto.Peso;
                    pacote.Comprimento = (pacote.Comprimento > item.Produto.Comprimento) ? pacote.Comprimento : item.Produto.Comprimento;
                    pacote.Largura = (pacote.Largura > item.Produto.Largura) ? pacote.Largura : item.Produto.Largura;
                    pacote.Altura += item.Produto.Altura;

                }
            }
            pacotes.Add(pacote);

            return pacotes;
        }
    }
}
