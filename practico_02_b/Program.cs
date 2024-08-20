// See https://aka.ms/new-console-template for more information

using practico_02_b;

AbstractHandler pipeline = new ReadHandler();

pipeline.SetNext(new AddVintageHandler()).SetNext(new SaveHandler());

Console.WriteLine($"Su imagen con filtro vintage se ha guardado en: " +
                  $"{pipeline.Handle(@"assets\pipeline-example.jpeg")}");