//https://xkcd.com/info.0.json

Console.WriteLine("Insert a number");
string str = Console.ReadLine();
int choice = int.Parse(str);



//These 4 lines connect to an API server and get back an instance of our class 
//=========================
HttpClient web = new HttpClient();
web.BaseAddress = new Uri("https://xkcd.com/");
var connection = await web.GetAsync($"{choice}/info.0.json ");

try
{
    Comic com = await connection.Content.ReadAsAsync<Comic>(); //This creates an instance of the API as a class
    Console.WriteLine(com.alt);
    Console.WriteLine(com.img);
}
catch (Exception e)
{
    Console.WriteLine("Sorry, I could not find that number");
}
//=========================

//string result = await connection.Content.ReadAsStringAsync();
//Console.WriteLine(result);


class Comic
{
    public int month { get; set; }
    public int num { get; set; }
    public string link { get; set; }
    public int year { get; set; }
    public string news { get; set; }
    public string safe_title { get; set; }
    public string transcript { get; set; }
    public string alt { get; set; }
    public string img { get; set; }
    public string title { get; set; }
    public int day { get; set; }
}