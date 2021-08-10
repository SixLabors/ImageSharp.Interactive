// Copyright (c) Six Labors.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Microsoft.DotNet.Interactive.Formatting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Interactive;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;

namespace ImageSharp.Interactive.Tests
{
    public class FormatterTests : IDisposable
    {
        public FormatterTests() => KernelExtension.RegisterFormatters();

        [Fact]
        public async Task ImageIsFormattedAsPng()
        {
            using var image = new Image<Rgb24>(Configuration.Default, 400, 400, Color.Black);
            string html = image.ToDisplayString(HtmlFormatter.MimeType);
            var parser = new HtmlParser();
            IHtmlDocument document = await parser.ParseDocumentAsync(html);
            IElement img = document.QuerySelector("img");
            Assert.NotNull(img);
            Assert.Contains("data:image/png;base64,", img.Attributes["src"].Value);
        }

        [Fact]
        public async Task ColorIsFormattedAsTable()
        {
            Color color = Color.Red;
            string html = color.ToDisplayString(HtmlFormatter.MimeType);
            var parser = new HtmlParser();
            IHtmlDocument document = await parser.ParseDocumentAsync(html);
            IElement img = document.QuerySelector("table img");
            Assert.NotNull(img);
            Assert.Contains("data:image/png;base64,", img.Attributes["src"].Value);
        }

        [Fact]
        public async Task AnImageWithMultipleFramesIsFormattedAsGif()
        {
            using var image = new Image<Rgba32>(Configuration.Default, 400, 400, Color.Coral);
            for (int i = 0; i < 10; ++i)
            {
                var frame = new Image<Rgba32>(Configuration.Default, 400, 400, Color.Black);
                image.Frames.AddFrame(frame.Frames[0]);
            }

            string html = image.ToDisplayString(HtmlFormatter.MimeType);
            var parser = new HtmlParser();
            IHtmlDocument document = await parser.ParseDocumentAsync(html);
            IElement img = document.QuerySelector("img");
            Assert.NotNull(img);
            Assert.Contains("data:image/gif;base64,", img.Attributes["src"].Value);
        }

        public void Dispose() => Formatter.ResetToDefault();
    }
}
