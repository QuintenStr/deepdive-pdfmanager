using API.Entities;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Forms;
using QRCoder;
using System.Drawing.Imaging;
using iText.IO.Image;

namespace API.Helpers
{
    /// <summary>
    /// Provides functionality to generate PDFs based on given input data.
    /// </summary>
    public static class GenerationPdf
    {
        private static string pdfTemplatePath = ".\\assets\\template.pdf";

        /// <summary>
        /// Generates a PDF for excursion participation confirmation.
        /// </summary>
        /// <param name="input">Data transfer object containing all necessary information.</param>
        /// <returns>A byte array representing the generated PDF.</returns>
        public static byte[] GenerateParticipationConfirmationPdf(ParticipationConfirmationInputDto input)
        {
            // Generate a QR code from the specified key.
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(input.QrCodeKey, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            byte[] imageBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                qrCode.GetGraphic(20).Save(ms, ImageFormat.Png);
                imageBytes = ms.ToArray();
            }

            // Fill in the PDF template with the QR code and input data.
            using (MemoryStream outPdfStream = new MemoryStream())
            using (PdfReader pdfReader = new PdfReader(pdfTemplatePath))
            using (PdfWriter pdfWriter = new PdfWriter(outPdfStream))
            {
                using (PdfDocument pdfDoc = new PdfDocument(pdfReader, pdfWriter))
                {
                    Document document = new Document(pdfDoc);
                    PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);
                    form.GetField("Location").SetValue(input.Coordinates.Lat + " " + input.Coordinates.Long);
                    form.GetField("Date").SetValue(input.DateTime.ToString("dd-MM-yyyy"));
                    form.GetField("Time").SetValue(input.DateTime.ToString("HH:mm"));

                    float x = 72, y = 224, width = 144, height = 144;
                    ImageData imageData = ImageDataFactory.Create(imageBytes);
                    iText.Layout.Element.Image qrImage = new iText.Layout.Element.Image(imageData)
                        .ScaleAbsolute(width, height)
                        .SetFixedPosition(1, x, y);

                    document.Add(qrImage);

                    form.FlattenFields();
                }
                return outPdfStream.ToArray();
            }
        }
    }
}
