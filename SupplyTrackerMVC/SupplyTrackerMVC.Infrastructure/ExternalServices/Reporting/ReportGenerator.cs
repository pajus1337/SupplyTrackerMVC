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

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(100); // Date
                            columns.RelativeColumn();    // Product
                            columns.RelativeColumn();    // Receiver
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Date").SemiBold();
                            header.Cell().Text("Product").SemiBold();
                            header.Cell().Text("Receiver").SemiBold();
                        });

                        foreach (var delivery in reportData.ReportDeliveries)
                        {
                            table.Cell().Text(delivery.DeliveryDate.ToShortDateString());
                            table.Cell().Text(delivery.ProductName);
                            table.Cell().Text(delivery.ReceiverName);
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
    

