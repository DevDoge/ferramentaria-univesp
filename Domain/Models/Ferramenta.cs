﻿using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Ferramenta : BaseModel
    {
        public string Codigo { get; private set; }
        public string Descricao { get; private set; }
        public string NumeroPatrimonial { get; private set; }
        public string Fabricante { get; private set; }
        public int QuantidadeDisponivel { get; private set; }
        public int QuantidadeTotal { get; private set; }
        public double ValorCompra { get; private set; }
        public string Localizacao { get; private set; }
        public StatusFerramentaEnum Status { get; private set; } = StatusFerramentaEnum.Disponivel;
        public Guid CategoriaID { get; private set; }
        public Categoria Categoria { get; set; }
        public Guid? AfericaoID { get; private set; }
        public Afericao Afericao { get; set; }

        public ICollection<Emprestimo> Emprestimos { get; set; }
        public ICollection<Reparo> Reparos { get; set; }

        public override void Ativar()
        {
            Ativo = false;
            Status = StatusFerramentaEnum.Disponivel;
        }

        public override void Inativar()
        {
            Ativo = false;
            Status = StatusFerramentaEnum.Indisponivel;
        }

        public void AtualizarDisponibilidade(Ferramenta antiga)
        {
            QuantidadeDisponivel = antiga.QuantidadeDisponivel;
        }

        public void Cadastrar()
        {
            QuantidadeDisponivel = QuantidadeTotal;
        }

        public void Emprestar(int quantidade)
        {          
            var quantidadeRestante = QuantidadeDisponivel - quantidade;
            if (quantidadeRestante < 0)
                throw new InvalidOperationException("Não há ferramenta suficiente na quantidade solicitada.");

            QuantidadeDisponivel = quantidadeRestante;

            if (quantidadeRestante == 0)
                Status = StatusFerramentaEnum.Emprestada;
            else
                Status = StatusFerramentaEnum.Disponivel;
        }

        public void Devolver(int quantidade)
        {            
            QuantidadeDisponivel += quantidade;
            Status = StatusFerramentaEnum.Disponivel;
        }
    }
}
