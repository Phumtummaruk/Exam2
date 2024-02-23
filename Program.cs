using HTTPUtils;
using System.Text.Json;
using AnsiTools;
using Colors = AnsiTools.ANSICodes.Colors;
using System.Linq;


Console.Clear();
Console.WriteLine("Starting Assignment 2");

// SETUP 
const string myPersonalID = "3f0cd06c12293f7c1721728508095628e85e55fe0bd5d61ee8870d6e03a4bf66"; // GET YOUR PERSONAL ID FROM THE ASSIGNMENT PAGE https://mm-203-module-2-server.onrender.com/
const string baseURL = "https://mm-203-module-2-server.onrender.com/";
const string startEndpoint = "start/"; // baseURl + startEndpoint + myPersonalID
const string taskEndpoint = "task/";   // baseURl + taskEndpoint + myPersonalID + "/" + taskID
// Creating a variable for the HttpUtils so that we dont have to type HttpUtils.instance every time we want to use it
HttpUtils httpUtils = HttpUtils.instance;

//#### REGISTRATION
// We start by registering and getting the first task
Response startRespons = await httpUtils.Get(baseURL + startEndpoint + myPersonalID);
Console.WriteLine($"Start:\n{Colors.Magenta}{startRespons}{ANSICodes.Reset}\n\n"); // Print the response from the server to the console
string taskID = "psu31_4"; // We get the taskID from the previous response and use it to get the task (look at the console output to find the taskID)

//#### FIRST TASK 
// Fetch the details of the task from the server.
Response task1Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + taskID); // Get the task from the server
Console.WriteLine(task1Response);

Task task1 = JsonSerializer.Deserialize<Task>(task1Response.content);
Console.WriteLine($"TASK1: {ANSICodes.Effects.Bold}{task1?.title}{ANSICodes.Reset}\n{task1?.description}\nParameters: {Colors.Yellow}{task1?.parameters}{ANSICodes.Reset}");

var answer = task1?.parameters.Split(",").Select(p => int.Parse(p.Trim())).Aggregate<int, int>(0, (acc, n) => acc + n);


Response task1AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + taskID, answer.ToString());
Console.WriteLine($"Answer: {Colors.Green}{answer}\n {task1AnswerResponse}{ANSICodes.Reset}");


Console.WriteLine("\n\n");
Console.WriteLine("\n----------------------------------------------------------------------\n");

//task2
taskID = "aAaa23";
Response task2Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + taskID);
Console.WriteLine(task2Response);

Task task2 = JsonSerializer.Deserialize<Task>(task2Response.content);
Console.WriteLine($"TASK2: {ANSICodes.Effects.Bold}{task2?.title}{ANSICodes.Reset}\n{task2?.description}\nParameters: {Colors.Yellow}{task2?.parameters}{ANSICodes.Reset}");
double FahrenheitToCelsius(double fahrenheit)
{
    return (fahrenheit - 32) * 5 / 9;
}
var answer2 = task2?.parameters.Split(",").Select(p => FahrenheitToCelsius(double.Parse(p.Trim()))).Select(a => a.ToString("F2")).ToList();

Response task2AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + taskID, string.Join(", ", answer2));
Console.WriteLine($"Answer: {Colors.Green}{string.Join(", ", answer2)}\n{task2AnswerResponse}{ANSICodes.Reset}");


Console.WriteLine("\n\n");
Console.WriteLine("\n----------------------------------------------------------------------\n");

//task3
taskID = "otYK2";
Response task3Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + taskID);
Console.WriteLine(task3Response);


Task task3 = JsonSerializer.Deserialize<Task>(task3Response.content);
Console.WriteLine($"TASK3: {ANSICodes.Effects.Bold}{task3?.title}{ANSICodes.Reset}\n{task3?.description}\nParameters: {Colors.Yellow}{task3?.parameters}{ANSICodes.Reset}");

string uniqueWords(string input)
{

    string[] words = input.Split(',');

    List<string> uniqueList = new List<string>();

    foreach (string word in words)
    {
        string cleanWord = word.Trim();
        cleanWord = cleanWord.ToLower();
        if (!uniqueList.Any(w => w.ToLower() == cleanWord))
        {
            uniqueList.Add(cleanWord);
        }
    }

    uniqueList.Sort();

    return string.Join(",", uniqueList.Select(w => char.ToUpper(w[0]) + w.Substring(1)));
}

string input = task3?.parameters ?? "";
string answer3 = uniqueWords(input);
Response task3AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + taskID, answer3);
Console.WriteLine($"Answer: {Colors.Green}{string.Join(", ", answer3)}\n{task3AnswerResponse}{ANSICodes.Reset}");


Console.WriteLine("\n\n");
Console.WriteLine("\n----------------------------------------------------------------------\n");

//task4
taskID = "rEu25ZX";
Response task4Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + taskID);
Console.WriteLine(task4Response);


Task task4 = JsonSerializer.Deserialize<Task>(task4Response.content);
Console.WriteLine($"TASK4: {ANSICodes.Effects.Bold}{task4?.title}{ANSICodes.Reset}\n{task4?.description}\nParameters: {Colors.Yellow}{task4?.parameters}{ANSICodes.Reset}");

int RomanToInt(string s)
{
    Dictionary<char, int> romanValues = new Dictionary<char, int>
    {
        {'I', 1},
        {'V', 5},
        {'X', 10},
        {'L', 50},
        {'C', 100}
    };

    int total = 0;

    for (int i = 0; i < s.Length; i++)
    {
        if (i < s.Length - 1 && romanValues[s[i]] < romanValues[s[i + 1]])
            total -= romanValues[s[i]];
        else
            total += romanValues[s[i]];
    }

    return total;
}

int answer4 = RomanToInt(task4.parameters);
Response task4answerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + taskID, answer4.ToString());
Console.WriteLine($"Answer: {Colors.Green}{answer4}\n{task4answerResponse}{ANSICodes.Reset}");


class Task
{
    public string? title { get; set; }
    public string? description { get; set; }
    public string? taskID { get; set; }
    public string? usierID { get; set; }
    public string? parameters { get; set; }
}
