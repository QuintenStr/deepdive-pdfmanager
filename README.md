# Excursion Participation Confirmation Microservice

This ASP.NET Core microservice generates PDF documents to confirm participation in excursions based on the provided data. It uses QR codes and input data to populate a predefined PDF template.

## Features

- **Generate PDF**: Create a PDF confirmation document for excursion participants.
- **QR Code Integration**: Each PDF includes a QR code specific to the participant's input.
- **Template-based PDF Generation**: Utilizes a PDF template to ensure consistent formatting and structure.

## Requirements

- .NET 6.0 or higher
- iText 7 for .NET (PDF manipulation library)
- QRCoder (QR code generation library)
