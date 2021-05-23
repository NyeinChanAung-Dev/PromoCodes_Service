using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodes_Service.Helper
{
    public static class QRCodeHelper
    {
        public static string GenerateQRCode(string contentString)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(contentString, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            string saveFilePath = "";

            using (var qrCodeImage = qrCode.GetGraphic(20))
            {
                var fileName = Guid.NewGuid().ToString() + ".jpg";
                var filePath = FileHelper.GetFullFilePath("Images", fileName);
                saveFilePath = Path.Combine("Images", fileName);
                qrCodeImage.Save(filePath, ImageFormat.Png);
            }
            return saveFilePath;
        }

    }
}
