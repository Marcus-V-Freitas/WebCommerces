using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BLL.Models.XML
{
        // OBSERVAÇÃO: o código gerado pode exigir pelo menos .NET Framework 4.5 ou .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class Servicos
        {

            private ServicosCServico cServicoField;

            /// <remarks/>
            public ServicosCServico cServico
            {
                get
                {
                    return this.cServicoField;
                }
                set
                {
                    this.cServicoField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class ServicosCServico
        {

            private ushort codigoField;

            private string valorField;

            private string prazoEntregaField;

            private string valorSemAdicionaisField;

            private string valorMaoPropriaField;

            private string valorAvisoRecebimentoField;

            private string valorValorDeclaradoField;

            private string entregaDomiciliarField;

            private string entregaSabadoField;

            private object obsFimField;

            private string erroField;

            private object msgErroField;

            /// <remarks/>
            public ushort Codigo
            {
                get
                {
                    return this.codigoField;
                }
                set
                {
                    this.codigoField = value;
                }
            }

            /// <remarks/>
            public string Valor
            {
                get
                {
                    return this.valorField;
                }
                set
                {
                    this.valorField = value;
                }
            }

            /// <remarks/>
            public string PrazoEntrega
            {
                get
                {
                    return this.prazoEntregaField;
                }
                set
                {
                    this.prazoEntregaField = value;
                }
            }

            /// <remarks/>
            public string ValorSemAdicionais
            {
                get
                {
                    return this.valorSemAdicionaisField;
                }
                set
                {
                    this.valorSemAdicionaisField = value;
                }
            }

            /// <remarks/>
            public string ValorMaoPropria
            {
                get
                {
                    return this.valorMaoPropriaField;
                }
                set
                {
                    this.valorMaoPropriaField = value;
                }
            }

            /// <remarks/>
            public string ValorAvisoRecebimento
            {
                get
                {
                    return this.valorAvisoRecebimentoField;
                }
                set
                {
                    this.valorAvisoRecebimentoField = value;
                }
            }

            /// <remarks/>
            public string ValorValorDeclarado
            {
                get
                {
                    return this.valorValorDeclaradoField;
                }
                set
                {
                    this.valorValorDeclaradoField = value;
                }
            }

            /// <remarks/>
            public string EntregaDomiciliar
            {
                get
                {
                    return this.entregaDomiciliarField;
                }
                set
                {
                    this.entregaDomiciliarField = value;
                }
            }

            /// <remarks/>
            public string EntregaSabado
            {
                get
                {
                    return this.entregaSabadoField;
                }
                set
                {
                    this.entregaSabadoField = value;
                }
            }

            /// <remarks/>
            public object obsFim
            {
                get
                {
                    return this.obsFimField;
                }
                set
                {
                    this.obsFimField = value;
                }
            }

            /// <remarks/>
            public string Erro
            {
                get
                {
                    return this.erroField;
                }
                set
                {
                    this.erroField = value;
                }
            }

            /// <remarks/>
            public object MsgErro
            {
                get
                {
                    return this.msgErroField;
                }
                set
                {
                    this.msgErroField = value;
                }
            }
        }
}
