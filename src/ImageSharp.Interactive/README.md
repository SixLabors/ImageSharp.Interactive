# SixLabors.ImageSharp.Interactive
### A [.NET Interactive](https://github.com/dotnet/interactive/) extension for [ImageSharp](https://github.com/SixLabors/ImageSharp)

## Getting started
Load the nuget pacakge and namespaces 
```csharp --project
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
```

Create and diplay an image, this extension registers formatters for the `Image` type.

```csharp --project
var image = new Image<Rgba32>(100, 100);
image.Mutate(c => c.BackgroundColor(Color.Red));
image.Display();
```

Load images

```csharp --project
using System.IO;
using System.Net;
using System.Net.Http;


var httpClient = new HttpClient();
var response = await httpClient.GetAsync("https://raw.githubusercontent.com/SixLabors/Branding/master/icons/imagesharp/sixlabors.imagesharp.png");
var inputStream = await response.Content.ReadAsStreamAsync();
image = Image.Load(inputStream);
image.Display();
```

## Processing images

Import the namespace `SixLabors.ImageSharp.Processing` to have access to image processing apis. 

```csharp --project
using SixLabors.ImageSharp.Processing;
```

The `Mutate` api will modify the current image, this cell resizes the image in place using `Mutate`.

```csharp --project
image.Mutate(x => x.Resize(image.Width / 2, image.Height / 2));
image.Display();
```

Using `Clone` you can obtain a copy of the image that is the outcome of the transfomation;

```csharp --project
var copy = image.Clone(x => x.GaussianBlur(2));
copy.Display();
image.Display();
```
