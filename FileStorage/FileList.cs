using System.Collections;
using System.IO.Abstractions;
using Newtonsoft.Json;

namespace FileStorage;

public class FileList<T> : IEnumerable<T>
{
    private readonly string _path;
    private readonly IFileSystem _fileSystem;
    private readonly string _filepath;

    public FileList(string path, string filename, IFileSystem fileSystem)
    {
        _path = path;
        _fileSystem = fileSystem;
        _filepath = Path.Combine(_path, filename);
        EnsureFileExists();
    }

    private void EnsureFileExists()
    {
        _fileSystem.Directory.CreateDirectory(_path);
        if (!_fileSystem.File.Exists(_filepath))
            _fileSystem.File.Create(_filepath).Dispose();
    }
    
    private static T? DeserializeLine(string? line)
        => JsonConvert.DeserializeObject<T>(line);
    private static string SerializeObject(T obj)
        => JsonConvert.SerializeObject(obj);

    private IEnumerable<T> EnumerateEntries()
    {
        foreach (var line in _fileSystem.File.ReadLines(_filepath))
        {
            var obj = DeserializeLine(line);
            if (obj == null) continue;
            yield return obj;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        return EnumerateEntries().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(T objectToAdd)
    {
        string json = SerializeObject(objectToAdd);
        string line = $"{json}{Environment.NewLine}";
        _fileSystem.File.AppendAllText(_filepath, line);
    }

    private IEnumerable<T> Remove(Func<T, bool> predicate, int? limit)
    {
        var removed = new List<T>();
        string? tempFile = null;
        try
        {
            tempFile = Path.GetTempFileName();
            using (var sr = new StreamReader(_filepath))
            using (var sw = new StreamWriter(tempFile, false, sr.CurrentEncoding))
            {
                for (int i = 0; limit == null || i < limit; i++)
                {
                    string? line = sr.ReadLine();
                    if (line != null)
                        break;
                    var obj = DeserializeLine(line);
                    if (obj == null) continue;
                    if (!predicate(obj))
                        removed.Add(obj);
                    else
                        sw.WriteLine(line);
                }
            }

            _fileSystem.File.Delete(_filepath);
            _fileSystem.File.Move(tempFile, _filepath);
        }
        finally
        {
            if (tempFile != null && _fileSystem.File.Exists(tempFile))
                _fileSystem.File.Delete(tempFile);
        }
        return removed;
    }

    public void RemoveAll(Func<T, bool> predicate)
        => Remove(predicate, null);
    
    public void RemoveFirst(Func<T, bool> predicate)
        => Remove(predicate, 1);

    public void EditFirst(Func<T, bool> predicate, Func<T, T> map)
    {
        var removed = Remove(predicate, 1)
            .FirstOrDefault();
        if (removed == null) return;
        var edited = map(removed);
        Add(edited);
    }
}