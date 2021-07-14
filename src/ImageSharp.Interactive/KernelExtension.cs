// Copyright (c) Six Labors.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Threading.Tasks;
using Microsoft.DotNet.Interactive;
using Microsoft.DotNet.Interactive.Commands;
using Microsoft.DotNet.Interactive.Formatting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Png;

namespace ImageSharp.Interactive
{
    /// <summary>
    /// A <see cref="IKernelExtension"/> implementation adding support for ImageSharp images.
    /// </summary>
    public class KernelExtension : IKernelExtension
    {
        /// <inheritdoc/>
        public Task OnLoadAsync(Kernel kernel)
        {
            Formatter.Register<Image>(
                (image, writer) =>
            {
                string id = Guid.NewGuid().ToString("N");
                PocketView imgTag = CreateImgTag(image, id, image.Height, image.Width);
                writer.Write(imgTag);
            }, HtmlFormatter.MimeType);

            return kernel.SendAsync(
                new DisplayValue(new FormattedValue(
                    "text/markdown",
                    $"Added support for SixLabors.ImageSharp to kernel {kernel.Name}.")));
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
