using LogBook.Lib.Interfaces;
using LogBook.Lib.Models;

namespace LogBook.Lib.Services;

public class CsvRepository : IRepository
{
    public CsvRepository(string file)
    {
        if (File.Exists(file))
        {

        }
        else
        {

        }
    }

    public bool Add(Entry entry)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Entry entry)
    {
        throw new NotImplementedException();
    }

    public bool Update(Entry entry)
    {
        throw new NotImplementedException();
    }

    public bool Save()
    {
        throw new NotImplementedException();
    }

    public List<Entry> GetAll()
    {
        throw new NotImplementedException();
    }

    public Entry? Find(string id)
    {
        throw new NotImplementedException();
    }
}