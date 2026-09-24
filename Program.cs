using System;
using System.IO;
using QRCoder;

    string outPutFolder = "/Users/MAC/Desktop/QRGenerator/OutPut";
    string fullPath,qrFileName;
    bool continueProcess=true;

    Console.ForegroundColor= ConsoleColor.Green;

    while(continueProcess)
    {
        Console.Clear();
        Console.WriteLine("--- Generador de Código QR ---");
        Console.Write("\nIntroduca el texto o URL para el QR: ");
        string textOrLink = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(textOrLink))
        {
            Console.WriteLine("El texto no puede estar vacío.");
            return;
        }
        else
        {
            if(textOrLink.Equals("exit",StringComparison.OrdinalIgnoreCase)
            || textOrLink.Equals("salir",StringComparison.OrdinalIgnoreCase)
            )
            {
                continueProcess=false;
            }
        }

        if(continueProcess)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(textOrLink, QRCodeGenerator.ECCLevel.Q);
                
                // Usamos PngByteQRCode para no depender de librerías nativas de gráficos de Windows
                PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
                byte[] qrCodeBytes = qrCode.GetGraphic(20); // 20 representa el tamaño de los módulos/píxeles

                // 4. Guardar la imagen en disco
                qrFileName= $"QR_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                fullPath = Path.Combine(outPutFolder, qrFileName);

                File.WriteAllBytes(fullPath, qrCodeBytes);

                Console.WriteLine($"\n¡Éxito! Imagen guardada en:");
                Console.WriteLine(fullPath);
                Console.ReadKey();
            }
        }
    }
