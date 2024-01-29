using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.Reflection;

var urlLogo = "https://fidelimax-homologacao.s3.amazonaws.com/Vouchers/logo-mini-fdlmx.png";
var giftPhoto = "https://fidelimax-homologacao.s3.amazonaws.com/Vouchers/photo1.jpg";
var voucherTaked = "https://fidelimax-homologacao.s3.amazonaws.com/Vouchers/voucher_entregue.jpg";
byte[] urlLogoByte;
byte[] giftPhotoByte;
byte[] voucherTakedByte;
using (var httpClient = new HttpClient())
{
    urlLogoByte = httpClient.GetByteArrayAsync(urlLogo).Result;
    giftPhotoByte = httpClient.GetByteArrayAsync(giftPhoto).Result;
    voucherTakedByte = httpClient.GetByteArrayAsync(voucherTaked).Result;
}
Document
    .Create(doc =>
    {
        doc.Page(page =>
        {
            page.Margin((float)1, Unit.Inch);

            page.Header().MaxWidth(120).Image(urlLogoByte).WithCompressionQuality(ImageCompressionQuality.High);
            page.Content().Shrink().BorderBottom(5).BorderLeft(5).BorderRight(5).BorderTop(5).BorderColor(Colors.Grey.Medium).Column(col =>
            {  
                col.Spacing((float)0.2, Unit.Inch);
                col.Item().AlignCenter().Text("Entregue").FontSize(28).SemiBold().Underline();
                //col.Item().Text($"Nome: {"voucher.Nome"} {"voucher.Sobrenome"}").Black().FontSize(15);
                //col.Item().Text($"Nome do Prêmio: ai que nao sei oq nao sei oq la").Black().FontSize(15);
                col.Item().Padding(8).Row(row =>
                {
                    row.RelativeItem().Text($"{"Descrição Produto"}:").Black().FontSize(15);
                    //row.RelativeItem().PaddingRight((float)1.5, Unit.Inch).MaxWidth(120).Image(giftPhotoByte);
                    row.RelativeItem().PaddingLeft((float)-1.0, Unit.Inch).Width(120).Image(giftPhotoByte);
                });
                col.Item().PaddingLeft(8).Text($"{"Quantidade do produto"}: aa").Black().FontSize(15);
                //col.Item().Text($"Prêmio:").Black().FontSize(15);
                //col.Item().AlignRight().Width(120).Image(giftPhotoByte).FitWidth();
                col.Item().PaddingLeft(8).Text("- Dados do Consumidor").Black().FontSize(15);
                col.Item().PaddingLeft(8).Text($"     Nome: {"voucher.Nome"} {"voucher.Sobrenome"}").Black().FontSize(15);
                col.Item().PaddingLeft(8).Text($"     Documento: {"voucher.CPF"}").Black().FontSize(15);
                col.Item().PaddingLeft(8).Text($"     Email: {"voucher.Email"}").Black().FontSize(15);
                col.Item().PaddingLeft(8).Text($"Código de Voucher: {"voucher da silva"}").FontSize(13).FontColor(Colors.Grey.Darken2).SemiBold();
                col.Item().PaddingLeft(8).Text($"Gerado em: {DateTime.UtcNow.ToString("dd/MM/yyyy")}").FontSize(13).FontColor(Colors.Red.Darken2).SemiBold();
                col.Item().PaddingLeft(8).PaddingBottom(5).Text($"Entregue em: {DateTime.UtcNow.ToString("dd/MM/yyyy")}").FontSize(13).FontColor(Colors.Red.Darken2).SemiBold();
            });

            page.Footer().AlignCenter()
            .Text(text =>
            {
                text.DefaultTextStyle(x => x.FontSize(16));
                text.CurrentPageNumber();
                text.Span(" / ");
                text.TotalPages();
            });

        });
    }).ShowInPreviewer();