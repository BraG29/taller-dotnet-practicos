using System.Drawing;

namespace practico_02_b;

public class SaveHandler : AbstractHandler
{
    private const string WritePath = @"assets\vintage-image.jpeg";

    public override string Handle(Bitmap image)
    {
        // Ruta relativa teniendo en cuenta que el .exe se guarda en la carpeta: bin/Debug/net/
        string absolutePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"))
                              + WritePath;
        
        image.Save(absolutePath);
        return absolutePath;
    }
}