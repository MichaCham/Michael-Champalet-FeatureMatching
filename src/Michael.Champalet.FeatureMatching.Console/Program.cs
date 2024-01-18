// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using Michael.Champalet.FeatureMatching;

var args0 = await File.ReadAllBytesAsync(args[0]);
IList<byte[]> list = new List<byte[]>();
string[] args1 = Directory.GetFiles(args[1]);


var objectDetection = new ObjectDetection();

foreach (var img in args1)
{
    byte[] sceneImageData = File.ReadAllBytes(img);
    var detectObjectInScenesResults = objectDetection.DetectObjectInScene(args0, sceneImageData);

    Console.WriteLine($"Points: {JsonSerializer.Serialize(detectObjectInScenesResults.Points)}");
}