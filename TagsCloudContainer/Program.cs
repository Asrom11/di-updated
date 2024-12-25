using System.Drawing;
using Autofac;
using TagsCloudContainer;
using TagsCloudVisualization;
using TagsCloudVisualization.Interfaces;

var container = DependencyInjectionConfig.BuildContainer();

using var scope = container.BeginLifetimeScope();
var tagCloudGenerator = scope.Resolve<ITagCloudGenerator>();

var inputFilePath = "input.txt";  
var outputFilePath = "output.png";
var options = new RenderingOptions
{
    BackgroundColor = Color.Navy,
    WordColors = [Color.White, Color.Yellow, Color.Orange],
    ImageSize = new Size(1200, 800)
};

tagCloudGenerator.GenerateCloud(inputFilePath, outputFilePath, options);