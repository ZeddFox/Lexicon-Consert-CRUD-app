using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Lexicon_Concert_CRUD_app;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string filePath = "C:\\Lexicon\\Assignments\\2024-12\\2024-12-11\\Assignment\\Concerts.xml";

app.MapGet("/", () => "Hello World!");
app.MapPost("/concertform", async (HttpRequest request) => 
{ 
    var content = await request.ReadFormAsync();

    string location;
    int capacity;
    string performer;
    string date;

    try
    {
        location = content["location"];
        capacity = Convert.ToInt32(content["capacity"]);
        performer = content["performer"];
        date = content["date"];
    }
    catch
    {
        return "One or more fields were empty. Please go back and fill out all the fields.";
    }

    try
    {
        ConcertBuilder.BruteWriteToXML(filePath, location, capacity, performer, date);
        return "Added successfully";
    }
    catch
    {
        return "Form was filled out incorrectly or file path is invalid";
    }
});

app.Run();