using System.Drawing;

namespace practico_02_b;

public abstract class AbstractHandler
{
    protected AbstractHandler _nextHandler;
    
    public AbstractHandler SetNext(AbstractHandler handler)
    {
        this._nextHandler = handler;

        return handler;
    }

    public virtual string Handle(string imagePath)
    {
        return imagePath;
    }
    
    public virtual string Handle(Bitmap image)
    {
        return "";
    }
}