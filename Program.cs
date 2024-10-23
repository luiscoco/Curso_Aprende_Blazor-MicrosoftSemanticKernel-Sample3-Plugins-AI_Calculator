using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Plugins;

string modelId = "gpt-4o";
string endpoint = "https://azureopenailuiscoco.openai.azure.com";
string apiKey = "ab4b134527844fbfa5cc209d050482e2";

var builder = Kernel.CreateBuilder().AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);

builder.Plugins.AddFromType<MathPlugin>();
Kernel kernel = builder.Build();
var chat = kernel.GetRequiredService<IChatCompletionService>();

Console.WriteLine("Azure OpenAI ChatGPT Math Console Application using Semantic Kernel");
Console.WriteLine("Enter your mathematical question or use commands like 'add', 'subtract', 'multiply', 'divide', 'power', or 'sqrt' followed by numbers:");

string userInput = Console.ReadLine();

var history = new ChatHistory();
history.AddSystemMessage("You are a helpful assistant for mathematical operations.");
history.AddUserMessage(userInput);

// Split input for operation and parameters
var parts = userInput.Split(' ');

if (parts.Length > 0)
{
    string operation = parts[0].ToLower();

    try
    {
        switch (operation)
        {
            case "sqrt":
                if (parts.Length == 2 && double.TryParse(parts[1], out double sqrtNumber))
                {
                    double sqrtResult = MathPlugin.Sqrt(sqrtNumber);
                    Console.WriteLine($"The square root of {sqrtNumber} is {sqrtResult}");
                }
                else
                {
                    Console.WriteLine("Usage: sqrt <number>");
                }
                break;

            case "add":
                if (parts.Length == 3 && double.TryParse(parts[1], out double addNumber1) && double.TryParse(parts[2], out double addNumber2))
                {
                    double addResult = MathPlugin.Add(addNumber1, addNumber2);
                    Console.WriteLine($"{addNumber1} + {addNumber2} = {addResult}");
                }
                else
                {
                    Console.WriteLine("Usage: add <number1> <number2>");
                }
                break;

            case "subtract":
                if (parts.Length == 3 && double.TryParse(parts[1], out double subNumber1) && double.TryParse(parts[2], out double subNumber2))
                {
                    double subResult = MathPlugin.Subtract(subNumber1, subNumber2);
                    Console.WriteLine($"{subNumber1} - {subNumber2} = {subResult}");
                }
                else
                {
                    Console.WriteLine("Usage: subtract <number1> <number2>");
                }
                break;

            case "multiply":
                if (parts.Length == 3 && double.TryParse(parts[1], out double mulNumber1) && double.TryParse(parts[2], out double mulNumber2))
                {
                    double mulResult = MathPlugin.Multiply(mulNumber1, mulNumber2);
                    Console.WriteLine($"{mulNumber1} * {mulNumber2} = {mulResult}");
                }
                else
                {
                    Console.WriteLine("Usage: multiply <number1> <number2>");
                }
                break;

            case "divide":
                if (parts.Length == 3 && double.TryParse(parts[1], out double divNumber1) && double.TryParse(parts[2], out double divNumber2))
                {
                    double divResult = MathPlugin.Divide(divNumber1, divNumber2);
                    Console.WriteLine($"{divNumber1} / {divNumber2} = {divResult}");
                }
                else
                {
                    Console.WriteLine("Usage: divide <number1> <number2>");
                }
                break;

            case "power":
                if (parts.Length == 3 && double.TryParse(parts[1], out double powBase) && double.TryParse(parts[2], out double powExponent))
                {
                    double powResult = MathPlugin.Power(powBase, powExponent);
                    Console.WriteLine($"{powBase} ^ {powExponent} = {powResult}");
                }
                else
                {
                    Console.WriteLine("Usage: power <base> <exponent>");
                }
                break;

            default:
                // Handle general chat completion for other inputs
                var result = await chat.GetChatMessageContentsAsync(history);
                Console.WriteLine(result[^1].Content);
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}
else
{
    Console.WriteLine("No input provided.");
}
