using System.Xml.Linq;
using LogBook.Lib.Interfaces;
using LogBook.Lib.Models;

namespace LogBook.Lib.Services;

public class XmlRepository : IRepository
{
    XElement _rootElement;

    public XmlRepository(string file) {
        if (File.Exists(file))
        {
            this._rootElement = XElement.Load(file);
        }
        else
        {
            this._rootElement = new XElement("entries");
        }
    }
    
    public bool Add(Entry entry) {
        throw new NotImplementedException();
    }

    public bool Delete(Entry entry) {
        throw new NotImplementedException();
    }

    public bool Update(Entry entry) {
        throw new NotImplementedException();
    }

    public bool Save() {
        throw new NotImplementedException();
    }

    public List<Entry> GetAll() {
        var entries = from entry in this._rootElement.Descendants("entry")
                        select entry;

        throw new NotImplementedException();
    }
}