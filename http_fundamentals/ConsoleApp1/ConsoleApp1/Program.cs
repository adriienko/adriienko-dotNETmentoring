class Program
{
    static async Task Main(string[] args)
    {
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync("http://localhost:5678/Hello");
        var responseText = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseText);
    }
}