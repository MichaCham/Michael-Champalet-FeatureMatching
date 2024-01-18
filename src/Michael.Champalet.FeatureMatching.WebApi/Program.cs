using System.Text;
using Michael.Champalet.FeatureMatching;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapPost("/FeatureMatching", async ([FromForm] IFormFileCollection files, ObjectDetection objectDetection) =>
{
    if (files.Count != 2)
        return Results.BadRequest();

    using var objectSourceStream = files[0].OpenReadStream();
    using var objectMemoryStream = new MemoryStream();
    await objectSourceStream.CopyToAsync(objectMemoryStream);
    var imageObjectData = objectMemoryStream.ToArray();

    using var sceneSourceStream = files[1].OpenReadStream();
    using var sceneMemoryStream = new MemoryStream();
    await sceneSourceStream.CopyToAsync(sceneMemoryStream);
    var imageSceneData = sceneMemoryStream.ToArray();
    // Your implementation code
    var results = await objectDetection.DetectObjectInScenesAsync(imageObjectData, new List<byte[]> { imageSceneData });
    // La méthode ci-dessous permet de retourner une image depuis un tableau de bytes, var imageData = new bytes[];
    return Results.File(results[0].ImageData, "image/png");
}).DisableAntiforgery();


app.UseSwagger();
app.UseSwaggerUI();

app.Run();