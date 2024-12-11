using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Lexicon_Concert_CRUD_app;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string filePath = "C:\\Lexicon\\Assignments\\2024-12\\2024-12-11\\Assignment\\Concerts.xml";

app.MapGet("/", () => "Hello World!");
app.MapGet("/concertform", async (HttpRequest request) => 
{ 
    var content = await request.ReadFormAsync();

    string location = content["location"];
    int capacity = Convert.ToInt32(content["capacity"]);
    string performer = content["performer"];
    string date = content["date"];

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
