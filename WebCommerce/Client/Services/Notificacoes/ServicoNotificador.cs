using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCommerce.Client.Services.Notificacoes
{
    public class ServicoNotificador
    {
        private List<string> servicos = new List<string>();
        private event Func<Task> Notificacao;

        public void Atualizar()
        {
            if (Notificacao != null)
            {
                Notificacao.Invoke();
            }
        }

        public void Adicionar(string nome, Func<Task> evento)
        {
            if (!servicos.Contains(nome.ToLower()))
            {
                Notificacao += evento;
                servicos.Add(nome.ToLower());
            }
        }

        public void Remover(string nome, Func<Task> evento)
        {
            if (servicos.Contains(nome.ToLower()))
            {
                Notificacao -= evento;
                servicos.Remove(nome.ToLower());
            }
        }
    }

}
