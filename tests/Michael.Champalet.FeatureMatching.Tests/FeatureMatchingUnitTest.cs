using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Michael.Champalet.FeatureMatching.Tests;


public class FeatureMatchingUnitTest
{
    [Fact]
    public async Task ObjectShouldBeDetectedCorrectly()
    {
        var executingPath = GetExecutingPath();
        var imageScenesData = new List<byte[]>();
        foreach (var imagePath in Directory.EnumerateFiles(Path.Combine(executingPath,
                     "../../../Scenes")))
        {
            var imageBytes = await File.ReadAllBytesAsync(imagePath);
            imageScenesData.Add(imageBytes);
        }
        var objectImageData = await File.ReadAllBytesAsync(Path.Combine(executingPath,
            "../../../Champalet-Michaël-object.jpg"));
        var detectObjectInScenesResults = await new
            ObjectDetection().DetectObjectInScenesAsync(objectImageData, imageScenesData);
        Assert.Equal("[{\"X\":824,\"Y\":546},{\"X\":329,\"Y\":2174},{\"X\":1551,\"Y\":2572},{\"X\":2039,\"Y\":969}]",JsonSerializer.Serialize(detectObjectInScenesResults[0].Points));
        Assert.Equal("[{\"X\":1011,\"Y\":2281},{\"X\":1233,\"Y\":3396},{\"X\":2105,\"Y\":3221},{\"X\":1802,\"Y\":2043}]",JsonSerializer.Serialize(detectObjectInScenesResults[1].Points));
    }
    private static string GetExecutingPath()
    {
        var executingAssemblyPath = Assembly.GetExecutingAssembly().Location;
        var executingPath = Path.GetDirectoryName(executingAssemblyPath);
        return executingPath;
    }
}