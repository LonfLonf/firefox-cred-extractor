using System.Text.RegularExpressions;

class Program
{
    public static void Main(string[] args)
    {
        string Path = $"C:\\Users\\{Environment.UserName}\\AppData\\Roaming\\Mozilla\\Firefox\\Profiles";
        string Pattern = @"release";
        var Directories = Directory.GetDirectories(Path);

        RegexOptions RegexOptions = RegexOptions.Multiline;

        foreach(string Folder in Directories)
        {
            var Matches = Regex.Matches(Folder, Pattern, RegexOptions);

            foreach(Match match in Matches)
            {
                Console.WriteLine($"'{match.Value}' found at index {match.Index}.");
                Console.WriteLine(Folder);
            }
        }


    }
}
