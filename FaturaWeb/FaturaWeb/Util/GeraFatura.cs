using FaturaWeb.Models;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;

namespace FaturaWeb.Util
{
    public static class GeraFatura
    {
        //public static FileStreamResult CreatePdf(Fatura f)
        //{
        //    var pdf = GerarFatura(f);

        //    var stream = new MemoryStream();

        //    pdf.Save(stream, true);


        //    return new FileStreamResult(stream, "application/pdf");
        //}

        public static MemoryStream GerarFatura(Fatura f)
        {
            var stream = new MemoryStream();
            using (var doc = new PdfDocument())
            {
                #region Configuracao de pagina
                var pagina = doc.AddPage();
                pagina.Size = PdfSharp.PageSize.A4;
                pagina.TrimMargins.Top = 50;
                pagina.TrimMargins.Bottom = 60;
                pagina.TrimMargins.Right = 30;
                pagina.TrimMargins.Left = 30;
                #endregion

                var fontRegular = new XFont("Consolas", 20, XFontStyle.Regular);
                var grafico = XGraphics.FromPdfPage(pagina);
                var formatacao = new PdfSharp.Drawing.Layout.XTextFormatter(grafico);

                #region Conteudo
                var corFonte = XBrushes.Black;
                //Dados da Empresa
                grafico.DrawString(f.Emissor.Nome, new XFont("Consolas", 20, XFontStyle.Bold), corFonte, 180, 40, XStringFormats.Center);
                grafico.DrawString(string.Concat(Constante.CAMPO_CNPJ, f.Emissor.Cnpj), new XFont("Consolas", 10, XFontStyle.Bold), corFonte, 180, 60, XStringFormats.Center);
                grafico.DrawString(string.Join(", ", f.Emissor.Logradouro, f.Emissor.Numero, f.Emissor.Bairro, f.Emissor.Municipio, f.Emissor.Uf), new XFont("Consolas", 8, XFontStyle.BoldItalic), corFonte, 180, 75, XStringFormats.Center);
                grafico.DrawString(string.Concat(Constante.CAMPO_CEP, f.Emissor.Cep, " ", Constante.CAMPO_FONE, f.Emissor.Fone), new XFont("Consolas", 8, XFontStyle.BoldItalic), corFonte, 180, 85, XStringFormats.Center);


                //Dados da Fatura
                int alinhamento = 360;
                grafico.DrawString(Constante.DESC_MODELO, new XFont("Consolas", 10, XFontStyle.Bold), corFonte, 390, 12);
                grafico.DrawString(Constante.CAMPO_EMISSAO, new XFont("Consolas", 10, XFontStyle.Bold), corFonte, alinhamento, 30);
                grafico.DrawString(f.DataEmissao.ToString("dd/MM/yyyy"), new XFont("Consolas", 10, XFontStyle.Regular), corFonte, 440, 30);

                grafico.DrawString(Constante.CAMPO_INSC_M, new XFont("Consolas", 10, XFontStyle.Bold), corFonte, alinhamento, 50);
                grafico.DrawString(f.Emissor.InscMunicipal, new XFont("Consolas", 10, XFontStyle.Regular), corFonte, 470, 50);

                grafico.DrawString(Constante.CAMPO_NAT_OP, new XFont("Consolas", 10, XFontStyle.Bold), corFonte, alinhamento, 60);
                grafico.DrawString(Constante.NAT_OP, new XFont("Consolas", 10, XFontStyle.Regular), corFonte, 480, 60);

                grafico.DrawString(Constante.SERIE, new XFont("Consolas", 10, XFontStyle.Bold), corFonte, alinhamento, 70);
                grafico.DrawString(Constante.SERIE_TIPO, new XFont("Consolas", 10, XFontStyle.Regular), corFonte, 400, 70);

                grafico.DrawString(string.Concat("Nº: ", f.Numero.ToString("00000")), new XFont("Consolas", 15, XFontStyle.Bold), corFonte, 440, 90);

                //Dados do cliente
                alinhamento = 10;
                grafico.DrawString(Constante.CAMPO_NOME_CLIENTE, new XFont("Consolas", 12, XFontStyle.Bold), corFonte, alinhamento, 120);
                grafico.DrawString(f.Cliente.Nome, new XFont("Consolas", 12, XFontStyle.Regular), corFonte, 120, 120);

                grafico.DrawString(Constante.CAMPO_ENDERECO, new XFont("Consolas", 12, XFontStyle.Bold), corFonte, alinhamento, 135);
                grafico.DrawString(string.Concat(f.Cliente.Logradouro, ", ", f.Cliente.Numero), new XFont("Consolas", 12, XFontStyle.Regular), corFonte, 80, 135);

                grafico.DrawString(Constante.CAMPO_MUNICIPIO, new XFont("Consolas", 12, XFontStyle.Bold), corFonte, alinhamento, 150);
                grafico.DrawString(f.Cliente.Municipio, new XFont("Consolas", 12, XFontStyle.Regular), corFonte, 100, 150);

                grafico.DrawString(Constante.CAMPO_ESTADO, new XFont("Consolas", 12, XFontStyle.Bold), corFonte, 300, 150);
                grafico.DrawString(f.Cliente.Uf, new XFont("Consolas", 12, XFontStyle.Regular), corFonte, 350, 150);

                grafico.DrawString(Constante.CAMPO_CEP, new XFont("Consolas", 12, XFontStyle.Bold), corFonte, 400, 150);
                grafico.DrawString(f.Cliente.Cep, new XFont("Consolas", 12, XFontStyle.Regular), corFonte, 440, 150);

                grafico.DrawString(Constante.CAMPO_CNPJ, new XFont("Consolas", 12, XFontStyle.Bold), corFonte, alinhamento, 165);
                grafico.DrawString(f.Cliente.Cnpj, new XFont("Consolas", 12, XFontStyle.Regular), corFonte, 50, 165);

                grafico.DrawString(Constante.CAMPO_INSC_M, new XFont("Consolas", 12, XFontStyle.Bold), corFonte, 250, 165);
                grafico.DrawString(f.Cliente.InscMunicipal, new XFont("Consolas", 12, XFontStyle.Regular), corFonte, 380, 165);

                //Valor por extenso
                grafico.DrawString(Constante.CAMPO_VALOR_EXTENSO, new XFont("Consolas", 12, XFontStyle.Bold), corFonte, alinhamento, 190);
                grafico.DrawString(DecimalToExtenso(f.ValorTotal), new XFont("Consolas", 12, XFontStyle.Regular), corFonte, pagina.Width / 2, 210, XStringFormats.Center);

                //Devem a 
                formatacao.DrawString(string.Format("Devem a, {0} ou a sua ordem na praca e vencimento a cima indicado a importancia a cima, correspondete a Prestacao de Servico a baixo descriminados.",
                                                      f.Emissor.Nome), new XFont("Consolas", 12), XBrushes.Black, new XRect(5, 250, pagina.Width - 5, pagina.Height));

                //Lista de itens, quantidade e valores
                grafico.DrawString("Item Descrição", new XFont("Consolas", 12, XFontStyle.Bold), corFonte, 100, 310, XStringFormats.Center);
                grafico.DrawString("Quant.", new XFont("Consolas", 12, XFontStyle.Bold), corFonte, 300, 310, XStringFormats.Center);
                grafico.DrawString("Vl.Unitário", new XFont("Consolas", 12, XFontStyle.Bold), corFonte, 400, 310, XStringFormats.Center);
                grafico.DrawString("Valor", new XFont("Consolas", 12, XFontStyle.Bold), corFonte, 530, 310, XStringFormats.Center);
                var linha = 325;
                foreach (var item in f.Itens)
                {
                    grafico.DrawString(item.Descricao, new XFont("Consolas", 12, XFontStyle.Regular), corFonte, 5, linha, XStringFormats.BaseLineLeft);
                    grafico.DrawString(item.Quantidade.ToString(), new XFont("Consolas", 12, XFontStyle.Regular), corFonte, 300, linha, XStringFormats.Center);
                    grafico.DrawString(item.Valor.ToString("C2"), new XFont("Consolas", 12, XFontStyle.Regular), corFonte, 400, linha, XStringFormats.Center);
                    grafico.DrawString((item.Quantidade * item.Valor).ToString("C2"), new XFont("Consolas", 12, XFontStyle.Regular), corFonte, 530, linha, XStringFormats.Center);
                    linha += 15;
                }

                //Periodo, prestacao e numero do empenho
                grafico.DrawString(Constante.CAMPO_PER_SERV, new XFont("Consolas", 10, XFontStyle.Bold), corFonte, alinhamento, 490);
                grafico.DrawString(f.TempoPrestacao, new XFont("Consolas", 10, XFontStyle.Regular), corFonte, 120, 490);

                grafico.DrawString(Constante.DADOS_BANCO, new XFont("Consolas", 10, XFontStyle.Bold), corFonte, alinhamento, 510);

                grafico.DrawString(Constante.CAMPO_DADOS_BANCO_BC, new XFont("Consolas", 10, XFontStyle.Bold), corFonte, alinhamento, 520);
                grafico.DrawString(f.Emissor.NomeBanco, new XFont("Consolas", 10, XFontStyle.Regular), corFonte, 50, 520);

                grafico.DrawString(Constante.CAMPO_DADOS_BANCO_AG, new XFont("Consolas", 10, XFontStyle.Bold), corFonte, alinhamento, 530);
                grafico.DrawString(f.Emissor.Agencia, new XFont("Consolas", 10, XFontStyle.Regular), corFonte, 60, 530);

                grafico.DrawString(Constante.CAMPO_DADOS_BANCO_CC, new XFont("Consolas", 10, XFontStyle.Bold), corFonte, alinhamento, 540);
                grafico.DrawString(f.Emissor.Conta, new XFont("Consolas", 10, XFontStyle.Regular), corFonte, 95, 540);

                grafico.DrawString(Constante.CAMPO_EMPENHO, new XFont("Consolas", 10, XFontStyle.Bold), corFonte, alinhamento, 555);
                grafico.DrawString(f.Empenho, new XFont("Consolas", 10, XFontStyle.Regular), corFonte, 95, 555);

                grafico.DrawString(Constante.CAMPO_OBS, new XFont("Consolas", 10, XFontStyle.Bold), corFonte, 195, 490);
                formatacao.DrawString(f.Observacao, new XFont("Consolas", 12), XBrushes.Black, new XRect(195, 500, pagina.Width - 200, pagina.Height));

                //Legislacao e valor
                grafico.DrawString(Constante.LEGIS_1, new XFont("Consolas", 15, XFontStyle.Bold), corFonte, 210, 580, XStringFormats.Center);
                formatacao.DrawString(Constante.LEGIS_2, new XFont("Consolas", 12, XFontStyle.BoldItalic), XBrushes.Black, new XRect(alinhamento, 600, pagina.Width * (0.8), pagina.Height));

                grafico.DrawString(Constante.CAMPO_VALOR, new XFont("Consolas", 15, XFontStyle.Bold), corFonte, 530, 580, XStringFormats.Center);
                grafico.DrawString(f.ValorTotal.ToString("C2"), new XFont("Consolas", 12, XFontStyle.Bold), corFonte, 530, 600, XStringFormats.Center);

                //Protocolo de recebimento
                formatacao.DrawString(string.Format("Recebi(emos) da {0} os serviços constantes nesta Nota Fiscal de Serviços - FATURA - Serie ÚNICA Nº {1}",
                                                     f.Emissor.Nome, f.Numero), new XFont("Consolas", 15), XBrushes.Black, new XRect(5, 710, pagina.Width - 5, pagina.Height));
                grafico.DrawString("Data: _____/_____/__________", new XFont("Consolas", 15, XFontStyle.Regular), corFonte, pagina.Width / 2, 770, XStringFormats.Center);
                grafico.DrawString("Assinatura: __________________________________________", new XFont("Consolas", 15, XFontStyle.Regular), corFonte, pagina.Width / 2, 810, XStringFormats.Center);
                #endregion   

                #region Separadores
                var cor = XPens.Black;
                //Dados da Empresa
                var posicao = 0;
                const int espaco = 2;
                var tamanho = 100;
                grafico.DrawRoundedRectangle(cor, 0, posicao, pagina.Width * (0.6), tamanho, 5, 5);

                //Dados da Fatura
                grafico.DrawRoundedRectangle(cor, pagina.Width * (0.6), posicao, pagina.Width * (0.4), 100, 5, 5);

                //Dados do cliente
                posicao += tamanho + espaco;
                tamanho = 70;
                grafico.DrawRoundedRectangle(cor, 0, posicao, pagina.Width, tamanho, 5, 5);

                //Valor por extenso
                posicao += tamanho + espaco;
                tamanho = 60;
                grafico.DrawRoundedRectangle(cor, 0, posicao, pagina.Width, tamanho, 5, 5);

                //Descricao da divida
                posicao += tamanho + espaco;
                tamanho = 60;
                grafico.DrawRoundedRectangle(cor, 0, posicao, pagina.Width, tamanho, 5, 5);

                //Lista de itens, quantidade e valores
                posicao += tamanho + espaco;
                tamanho = 180;
                grafico.DrawRoundedRectangle(cor, 0, posicao, pagina.Width, tamanho, 5, 5);

                //Periodo, prestacao e numero do empenho
                posicao += tamanho + espaco;
                tamanho = 80;
                grafico.DrawRoundedRectangle(cor, 0, posicao, pagina.Width * (0.30), tamanho, 5, 5);
                grafico.DrawRoundedRectangle(cor, pagina.Width * (0.30), posicao, pagina.Width * (0.70), tamanho, 5, 5);

                //Legislacao e valor 
                posicao += tamanho + espaco;
                tamanho = 80;
                grafico.DrawRoundedRectangle(cor, 0, posicao, pagina.Width * (0.8), tamanho, 5, 5);
                grafico.DrawRoundedRectangle(cor, pagina.Width * (0.8), posicao, pagina.Width * (0.2), tamanho, 5, 5);

                //Divisoria
                posicao += tamanho + 30;
                tamanho = 1;
                grafico.DrawRoundedRectangle(cor, 0, posicao, pagina.Width, tamanho, 5, 5);

                //Protocolo de recibo
                posicao += tamanho + 30;
                tamanho = 140;
                grafico.DrawRoundedRectangle(cor, 0, posicao, pagina.Width, tamanho, 5, 5);
                #endregion   

                doc.Save(stream, true);

                return stream;

            }
        }



        private static string DecimalToExtenso(Decimal valor)
        {
            if (valor == 0)
            {
                return "ZERO REAIS";
            }
            string[] struni = new string[] { "", "Um", "Dois", "Tres", "Quatro", "Cinco", "Seis", "Sete", "Oito", "Nove", "Dez", "Onze", "Doze", "Treze", "Quatorze", "Quinze", "Dezessis", "Dezessete", "Dezoito", "Dezenove", "Vinte" };
            string[] strdez = new string[] { "", "", "Vinte", "Trinta", "Quarenta", "Cinquenta", "Sessenta", "Setenta", "Oitenta", "Noventa" };
            List<string[]> strcen = new List<string[]>() { new string[] { "", "" }, new string[] { "Cem", "Cento" }, new string[] { "Duzentos", "Duzentos" }, new string[] { "Trezentos", "Trezentos" }, new string[] { "Quatrocentos", "Quatrocentos" }, new string[] { "Quinhentos", "Quinhentos" }, new string[] { "Seiscentos", "Seiscentos" }, new string[] { "Setecentos", "Setecentos" }, new string[] { "Oitocentos", "Oitocentos" }, new string[] { "Novecentos", "Novecentos" } };
            List<string[]> moeda = new List<string[]>() { new string[] { " Trilhao", " Trilhoes" }, new string[] { " Bilhao", " Bilhoes" }, new string[] { " Milhao", " Milhoes" }, new string[] { " Mil", " Mil" }, new string[] { " Real", " Reais" }, new string[] { " Centavo", " Centavos" }, };
            List<string[]> result = new List<string[]>();
            string[] arrValor = Decimal.Round(valor, 2).ToString("0|0|0,0|0|0,0|0|0,0|0|0,0|0|0.0|0").Replace(",", ",0|").Replace(",", ".").Split('.');
            for (int i = arrValor.Length - 1; i >= 0; i--)
            {
                string[] z = arrValor[i].Split('|');
                int a = Convert.ToInt32(z[0]);
                int b = Convert.ToInt32(z[1]);
                int c = Convert.ToInt32(z[2]);
                int d = Convert.ToInt32(b.ToString() + c.ToString());
                int k = Convert.ToInt32(a.ToString() + b.ToString() + c.ToString());
                string r = (d >= 1 && d <= 20) ? string.Format("{0}", k == 0 ? "" : struni[d]) : string.Format("{0}{1}{2}", strdez[b], c > 0 ? " e " : "", k == 0 ? "" : struni[c]);
                r = k < 100 ? r : string.Format("{0}{1}{2}", strcen[a][d == 0 ? 0 : 1], d == 0 ? "" : " e ", r);
                result.Add(new string[] { i.ToString(), k.ToString(), r, " e ", moeda[i][k == 1 ? 0 : 1] });
            }
            if (Convert.ToInt32(result[1][1]) == 0)
            {
                string xmoeda = result[1][4];
                for (int i = 2; i <= result.Count - 1; i++)
                {
                    if (Convert.ToInt32(result[i][1]) > 0)
                    {
                        result[i][4] += " " + (i == 3 || i == 4 || i == 5 ? " de " : "") + xmoeda;
                        result[i][3] = " e ";
                        break;
                    }
                }
            }
            for (int i = result.Count - 1; i >= 0; i--) { if (Convert.ToInt32(result[i][1]) == 0) result.Remove(result[i]); }
            result[0][3] = "";
            for (int i = 2; i <= result.Count - 1; i++) { result[i][3] = ", "; }
            string extenso = "";
            for (int i = 0; i <= result.Count - 1; i++) { extenso = result[i][2] + result[i][4] + result[i][3] + extenso; }
            return extenso.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").ToUpper(); ;
        }



    }

}
