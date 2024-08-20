using System.Drawing;

namespace practico_02_b;

public class AddVintageHandler : AbstractHandler
{
    public override string Handle(Bitmap image)
    {
        Bitmap newImage = new Bitmap(image.Width, image.Height);

        for (int y = 0; y < image.Height; y++)
        {
            for (int x = 0; x < image.Width; x++)
            {
                // Obtenemos el color del pixel original
                Color originalColor = image.GetPixel(x, y);
                
                // Convertimos a escala de grises
                int gray = (int)(originalColor.R * 0.3 + originalColor.G * 0.59 + originalColor.B * 0.11);
                //Ajuste de tinte Sepia
                int r = Math.Min(255, (int)(gray + 40 * 2));
                int g = Math.Min(255, (int)(gray + 20));
                int b = Math.Min(255, gray);

                Color vintageColor = Color.FromArgb(r, g, b);
                
                newImage.SetPixel(x,y,vintageColor); 
            }
        }
        
        return this._nextHandler.Handle(newImage);
    }
}