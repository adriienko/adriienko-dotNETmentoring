using ConsoleApp1.Bug;
using ConsoleApp1.Logger;
using System.Diagnostics;

namespace DebugFundamentals;
[DebuggerDisplay("Id = {Id}, Name = {Name}")]
public class NameId
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}

internal class Programm
{
    static void Main(string[] args)
    {
        MyLogger logger = new MyLogger();
        try
        {
            var error = new RandomException(1);

            logger.Log("Program stated");

            Console.WriteLine(args != null && args.Length > 0 ? args[0] : "",
                args != null && args.Length > 1 ? args[1] : "");

            var list = new List<NameId>();
            for (int i = 1; i <= 50; i++)
            {
                var item = new NameId()
                {
                    Id = i,
                    Name = $"Name{i}",
                };

                error.RandomlyThrowException();

                logger.Log($"itiem #{i} created");

                list.Add(item);

                logger.Log($"itiem #{i} added to the list");
            }

            logger.Log($"Program finished");
        }
        catch (Exception ex) 
        {
            logger.Log($"Exception: {ex}",1);
        }
    }
}        
