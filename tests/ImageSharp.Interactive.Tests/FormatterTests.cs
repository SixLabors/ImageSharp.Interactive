// Copyright (c) Six Labors.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.DotNet.Interactive.Formatting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Interactive;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Xunit;

namespace ImageSharp.Interactive.Tests
{
    public class FormatterTests : IDisposable
    {
        public FormatterTests() => KernelExtension.RegisterFormatters();

        [Fact]
        public async Task Formats_image_as_png()
        {
            var image = new Image<Rgb24>(100, 100);
            string html = image.ToDisplayString(HtmlFormatter.MimeType);
            var parser = new HtmlParser();
            AngleSharp.Html.Dom.IHtmlDocument document = await parser.ParseDocumentAsync(html);
            AngleSharp.Dom.IElement img = document.QuerySelector("img");
            using var _ = new AssertionScope();

            img.Should().NotBeNull();
            img.Attributes["src"].Value.Should().Contain("data:image/png;base64,");
        }

        [Fact]
        public async Task Formats_images_with_multiple_frames_as_gif()
        {
            var image = new Image<Rgba32>(400, 400);
            image.Mutate(context => context.Fill(Color.Coral));
            for (int i = 0; i < 10; ++i)
            {
                var frame = new Image<Rgba32>(400, 400);
                frame.Mutate(x => x.Fill(Color.Black));
                image.Frames.AddFrame(frame.Frames[0]);
            }

            string html = image.ToDisplayString(HtmlFormatter.MimeType);
            var parser = new HtmlParser();
            AngleSharp.Html.Dom.IHtmlDocument document = await parser.ParseDocumentAsync(html);
            AngleSharp.Dom.IElement img = document.QuerySelector("img");
            using var _ = new AssertionScope();

            img.Should().NotBeNull();
            img.Attributes["src"].Value.Should().Contain("data:image/gif;base64,");
        }

        public void Dispose() => Formatter.ResetToDefault();
    }
}
