using System.Drawing;
using System.Drawing.Imaging;

namespace practico_02_b;

public class ReadHandler : AbstractHandler
{
    public override string Handle(string imagePath)
    {
        // Ruta relativa teniendo en cuenta que el .exe se guarda en la carpeta: bin/Debug/net/
        string absolutePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"))
                              + imagePath;
        
        Bitmap image = new Bitmap(absolutePath);

        using (MemoryStream memoryStream = new MemoryStream())
        {
            image.Save(memoryStream, ImageFormat.Jpeg);
        }

        return this._nextHandler.Handle(image);
    }
}