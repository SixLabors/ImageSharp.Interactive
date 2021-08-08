<h1 align="center">

<img src="https://github.com/SixLabors/Branding/raw/master/icons/imagesharp/sixlabors.imagesharp.svg?sanitize=true" alt="SixLabors.ImageSharp" width="256"/>
<br/>
SixLabors.ImageSharp.Interactive
</h1>

<div align="center">

[![Build Status](https://img.shields.io/github/workflow/status/SixLabors/ImageSharp.Interactive/Build/main)](https://github.com/SixLabors/ImageSharp.Interactive/actions)
[![Code coverage](https://codecov.io/gh/SixLabors/ImageSharp.Interactive/branch/main/graph/badge.svg)](https://codecov.io/gh/SixLabors/ImageSharp.Interactive)
[![License: Apache 2.0](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
[![Twitter](https://img.shields.io/twitter/url/http/shields.io.svg?style=flat&logo=twitter)](https://twitter.com/intent/tweet?hashtags=imagesharp,dotnet,oss&text=ImageSharp.+A+new+cross-platform+2D+graphics+API+in+C%23&url=https%3a%2f%2fgithub.com%2fSixLabors%2fImageSharp&via=sixlabors)

</div>

### A [.NET Interactive](https://github.com/dotnet/interactive/) extension for ImageSharp

Load the nuget package and then you can display images
```csharp --project
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Interactive;
using SixLabors.ImageSharp.PixelFormats;

using var image = new Image<Rgb24>(100, 100);
image.Mutate(c => c.BackgroundColor(Color.AliceBlue));
image.Display(); 
```

