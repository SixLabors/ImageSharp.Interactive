# SixLabors.ImageSharp.Interactive
### A [.NET Interactive](https://github.com/dotnet/interactive/) extension for [ImageSharp](https://github.com/SixLabors/ImageSharp)

## Getting started
Load the nuget pacakge and namespaces
```csharp --project
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
```

Create and diplay an image

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
var image = Image.Load(inputStream);
image.Display();
```

## Processing images

Import the namespaces

```csharp --project
using SixLabors.ImageSharp.Processing;
```

Resize the image in place using `Mutate`

```csharp --project
image.Mutate(x => x.Resize(image.Width / 2, image.Height / 2));
image.Display();
```

Or get a processed clone using `Clone`

```csharp --project
var copy = image.Clone(x => x.GaussianBlur(2));
copy.Display();
```
