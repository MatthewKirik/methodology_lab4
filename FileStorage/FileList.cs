using System.Collections;
using System.IO.Abstractions;
using Newtonsoft.Json;

namespace FileStorage;

public class FileList<T> : IEnumerable<T>
{
    private readonly IFileSystem _fileSystem;
    private string _filepath;

    private string FilePath
    {
        get
        {
            EnsureFileExists();
            return _filepath;
        }
    }

    public FileList(string filepath, IFileSystem fileSystem)
    {
        _filepath = filepath;
        _fileSystem = fileSystem;
    }

    private void EnsureFileExists()
    {
        if (_fileSystem.File.Exists(_filepath)) return;
        string directory = _fileSystem.Path.GetDirectoryName(_filepath);
        _fileSystem.Directory.CreateDirectory(directory);
        _fileSystem.File.Create(_filepath).Dispose();
    }

    private static T? DeserializeLine(string? line)
        => JsonConvert.DeserializeObject<T>(line);

    private static string SerializeObject(T obj)
        => JsonConvert.SerializeObject(obj);

    private IEnumerable<T> EnumerateEntries()
    {
        foreach (var line in _fileSystem.File.ReadLines(FilePath))
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
        _fileSystem.File.AppendAllText(FilePath, line);
    }

    private IEnumerable<T> Remove(Func<T, bool> predicate, int? limit)
    {
        var removed = new List<T>();
        string? tempFile = null;
        try
        {
            tempFile = Path.GetTempFileName();
            using (var sr = new StreamReader(FilePath))
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

            _fileSystem.File.Delete(FilePath);
            _fileSystem.File.Move(tempFile, FilePath);
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