using System.Text.Json;

namespace CSExam.Domain.Repositories;


public interface IHasId
{
    public int Id { get; }
}


public record JsonRootWrapper<T>(T Root);


public abstract class BaseRepo<T> where T : IHasId
{
    private readonly string _filename;
    private List<T>? _data;
    
    protected List<T> Data
    {
        get
        {
            if (_data != null) return _data;
            using var fs = File.OpenRead(_filename);
            using var json = JsonDocument.Parse(fs);
            var wrappedData = json.Deserialize<JsonRootWrapper<List<T>>>();
            _data = wrappedData?.Root;
            if (_data == null)
            {
                throw new Exception("Can't load data");
            }
            return _data;
        }
    }

    protected BaseRepo(string filename)
    {
        _filename = filename;
    }

    public void Commit()
    {
        using var fs = File.OpenWrite(_filename);
        var data = JsonSerializer.Serialize(new JsonRootWrapper<List<T>>(Data));
        using var fileWriter = new StreamWriter(fs);
        fileWriter.Write(data);
    }
    
    public void Add(T obj)
    {
        var actual = GetById(obj.Id);
        if (actual is not null)
        {
            Data.Remove(actual);
        }
        Data.Add(obj);
    }
    
    public int GetNextId()
    {
        return Data.Count;
    }
    
    public IEnumerable<T> GetAll()
    {
        return Data;
    }
    
    public T? GetById(int id)
    {
        return Data.FirstOrDefault(obj => obj.Id == id);
    }
}
