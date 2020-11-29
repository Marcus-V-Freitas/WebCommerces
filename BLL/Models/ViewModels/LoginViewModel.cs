using BLL.Library.ExtensionsMethods;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BLL.Library.Validacoes;
using Validation = BLL.Library.Resources.Models.Validation;
using Model = BLL.Library.Resources.Models.Models;

namespace BLL.Models.ViewModels
{
    public class LoginViewModel
    {
        [LocalizedDisplayName(nameof(Model.Email), typeof(Model))]
        public string Email { get; set; }

        [LocalizedDisplayName(nameof(Model.Senha), typeof(Model))]

        [MinLength(8, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.MinimoValor))]
        public string Senha { get; set; }
    }
}
