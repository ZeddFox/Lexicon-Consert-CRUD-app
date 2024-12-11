var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Guess the number. Write \"/g/<number>\" to guess.");
app.MapGet("/g/{number}", (int number) =>
{
    int guessNumber = 15;

    if (number < guessNumber)
    {
        return "Correct number is higher";
    }
    else if (number > guessNumber)
    {
        return "Correct number is lower";
    }
    else if (number == guessNumber)
    {
        return "Correct!";
    }

    return "Error";
});

app.Run();
