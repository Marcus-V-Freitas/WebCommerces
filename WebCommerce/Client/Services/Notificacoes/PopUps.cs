using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCommerce.Client.Services.Notificacoes
{
    public static class PopUps
    {
        public static NotificationMessage info(string sumario, string detalhe)
        {
            return ConfigurarMensagem(NotificationSeverity.Info, sumario, detalhe);
        }

        public static NotificationMessage Error(string sumario, string detalhe)
        {
            return ConfigurarMensagem(NotificationSeverity.Error, sumario, detalhe);
        }

        public static NotificationMessage Warning(string sumario, string detalhe)
        {
            return ConfigurarMensagem(NotificationSeverity.Warning, sumario, detalhe);
        }


        public static NotificationMessage Success(string sumario, string detalhe)
        {
            return ConfigurarMensagem(NotificationSeverity.Success, sumario, detalhe);
        }

        private static NotificationMessage ConfigurarMensagem(NotificationSeverity notification, string sumario, string detalhe)
        {
            return new NotificationMessage()
            {
                Severity = notification,
                Summary = sumario,
                Detail = detalhe,
                Duration = 4000
            };
        }

    }
}
