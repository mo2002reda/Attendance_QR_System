using Bll.Interfaces_Repository;
using Dll.DataContext;
using Dll.Entity;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography.X509Certificates;

namespace PllDoctor.Models
{
    public static class QRCodeGeneratorService
    {
        public static byte[] BitmapToByteArray(this Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }

        public static string[] GenerateQRCode(IUniteOfWork uniteOfWork, string SubjectName)
        {

            QRCodeGenerator QrGenerator = new QRCodeGenerator();

            string QR1 = $"{Guid.NewGuid()}@{SubjectName}@{DateTime.Now}";

            string QR2 = $"{Guid.NewGuid()}@{SubjectName}@{DateTime.Now}";

            string QR3 = $"{Guid.NewGuid()}@{SubjectName}@{DateTime.Now}";

            string[] parts = { QR1, QR2, QR3 };
            //add To database
            for (int i = 0; i < parts.Length; i++)
            {
                var mapped = new QRCodeModel
                {
                    QRText = parts[i]
                };
                uniteOfWork.QRCodeRepository.Add(mapped);
                uniteOfWork.Complete();
            }

            string[] QrUriArray = new string[parts.Length];
            // Iterate through each part of the QR string
            for (int i = 0; i < parts.Length; i++)
            {
                string part = parts[i];
                // Generate QR code for the current part
                QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(part, QRCodeGenerator.ECCLevel.Q);
                QRCode QrCode = new QRCode(QrCodeInfo);
                Bitmap QrBitmap = QrCode.GetGraphic(60);
                byte[] BitmapArray = QrBitmap.BitmapToByteArray();
                string QrUri = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));

                // Store the URI in the array To Send it
                QrUriArray[i] = QrUri;
            }
            return QrUriArray;

        }

        public static void DeleteQRCode(string input, IUniteOfWork uniteOfWork)
        {
            var mapped = new QRCodeModel
            {
                QRText = input
            };

            uniteOfWork.QRCodeRepository.Delete(mapped);

        }


    }
}

