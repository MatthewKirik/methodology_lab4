using Newtonsoft.Json;

namespace FileStorage;

public class FileList<T>
{
    private readonly string _path;
    private readonly string _filepath;

    public FileList(string path, string filename)
    {
        _path = path;
        _filepath = Path.Combine(_path, filename);
        EnsureFileExists();
    }

    private void EnsureFileExists()
    {
        Directory.CreateDirectory(_path);
        if (!File.Exists(_filepath))
            File.Create(_filepath).Dispose();
    }

    private IEnumerable<T> EnumerateEntries()
    {
       foreach (var line in File.ReadLines(_filepath))
       {  
           var obj = JsonConvert.DeserializeObject<T>(line);
           if (obj == null) continue;
           yield return obj;
       }
    }
}