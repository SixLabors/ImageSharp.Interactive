// Copyright (c) Six Labors.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Threading.Tasks;
using Microsoft.DotNet.Interactive;
using Microsoft.DotNet.Interactive.Commands;
using Microsoft.DotNet.Interactive.Formatting;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using static Microsoft.DotNet.Interactive.Formatting.PocketViewTags;

namespace SixLabors.ImageSharp.Interactive
{
    /// <summary>
    /// A <see cref="IKernelExtension"/> implementation adding support for ImageSharp images.
    /// </summary>
    public class KernelExtension : IKernelExtension
    {
        /// <inheritdoc/>
        public Task OnLoadAsync(Kernel kernel)
        {
            RegisterFormatters();

            return kernel.SendAsync(
                new DisplayValue(new FormattedValue(
                    "text/markdown",
                    $"Added support for SixLabors.ImageSharp to kernel {kernel.Name}.")));
        }

        /// <summary>
        /// Registers the formatters.
        /// </summary>
        public static void RegisterFormatters()
        {
            Formatter.Register<Image>(
                (image, writer) =>
                {
                    string id = Guid.NewGuid().ToString("N");
                    PocketView imgTag = CreateImgTag(image, id, image.Height, image.Width);
                    writer.Write(imgTag);
                }, HtmlFormatter.MimeType);

            Formatter.Register<Color>(
                (color, writer) =>
                {
                    var img = new Image<Rgba32>(36, 24, color);

                    string id = Guid.NewGuid().ToString("N");
                    PocketView imgTag = CreateImgTag(img, id, img.Height, img.Width);
                    img.Dispose();
                    PocketView colorSwatch = div(table(tr(
                        td[style: "vertical-align:middle"](
                            div[style: $"height: {img.Height}px; width: {img.Width}px; border-style:solid; border-width:thin; border-color:rgb(126,126,126)"](
                                imgTag)),
                        td[style: "vertical-align:middle"](color.ToString())))
                    );
                    writer.Write(colorSwatch.ToDisplayString());
                }, HtmlFormatter.MimeType);
        }

        private static PocketView CreateImgTag(Image image, string id, int height, int width)
        {
            IImageFormat format = image.Frames.Count > 1
                ? (IImageFormat)GifFormat.Instance
                : PngFormat.Instance;

            string imageSource = image.ToBase64String(format);

            return (PocketView)PocketViewTags.img[id: id, src: imageSource, height: height, width: width]();
        }
    }
}
