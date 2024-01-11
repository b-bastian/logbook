using LogBook.Lib.Models;

namespace LogBook.Lib.Interfaces;

public interface IRepository
{
    bool Add(Entry entry);

    bool Delete(Entry entry);

    bool Update(Entry entry);

    bool Save();

    List<Entry> GetAll();
}