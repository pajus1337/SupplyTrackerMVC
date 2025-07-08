using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.ViewModels.ReportVm;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace SupplyTrackerMVC.Infrastructure.ExternalServices.Reporting
{
    public class ReportGenerator : IReportGenerator
    {
        public byte[] GeneratePdf(ListReportDeliveryVm reportData)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(50);

                    page.Header().Text("Delivery Report").FontSize(20).Bold().AlignCenter();

                    page.Content().PaddingTop(15).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(30);  // No.
                            columns.ConstantColumn(80);  // Date
                            columns.RelativeColumn();    // Product
                            columns.RelativeColumn();    // Sender
                            columns.RelativeColumn();    // Receiver
                            columns.RelativeColumn();    // Branch
                            columns.ConstantColumn(80);  // Weight
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("No.").SemiBold();
                            header.Cell().Text("Date").SemiBold();
                            header.Cell().Text("Product").SemiBold();
                            header.Cell().Text("Sender").SemiBold();
                            header.Cell().Text("Receiver").SemiBold();
                            header.Cell().Text("Branch").SemiBold();
                            header.Cell().Text("Weight").SemiBold();
                        });

                        int index = 1;
                        foreach (var delivery in reportData.ReportDeliveries)
                        {
                            table.Cell().Text(index.ToString());
                            table.Cell().Text(delivery.DeliveryDateTime.ToShortDateString());
                            table.Cell().Text(delivery.ProductName);
                            table.Cell().Text(delivery.SenderName);
                            table.Cell().Text(delivery.ReceiverName);
                            table.Cell().Text(delivery.ReceiverBranchName);
                            table.Cell().Text($"{delivery.ProductDeliveryWeight} kg");

                            index++;
                        }
                    });

                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.Span("Generated: ");
                        text.Span(DateTime.Now.ToString("g"));
                    });
                });
            });

            return document.GeneratePdf();
        }
    }
}



