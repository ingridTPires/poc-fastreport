using System;
using System.Collections.Generic;

namespace WpfApp
{
    public class Comissao
    {
        public int Id { get; set; }
        public DateTime DtGeracao { get; set; }
        public DateTime DtPrevisao { get; set; }

        public List<ComissaoDetalhe> ComissaoDetalhe { get; set; }
    }

    public class ComissaoDetalhe
    {
        public string Produto { get; set; }
        public string Corretor { get; set; }
        public DateTime DtCadastro { get; set; }
        public DateTime DtAssinatura { get; set; }
        public DateTime DtVigencia { get; set; }
        public int Proposta { get; set; }
        public int Parcela { get; set; }
        public decimal VlParcela { get; set; }
        public int Porcentagem { get; set; }
        public decimal VlBruto { get; set; }
        public decimal Taxa { get; set; }
        public int Adm { get; set; }
        public decimal DescAdm { get; set; }
        public decimal ISS { get; set; }
        public decimal Liquido { get; set; }
    }
}
