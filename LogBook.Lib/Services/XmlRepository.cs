using System.Diagnostics;
using System.Xml.Linq;
using LogBook.Lib.Interfaces;
using LogBook.Lib.Models;

namespace LogBook.Lib.Services;

public class XmlRepository : IRepository
{
    readonly XElement _rootElement;
    private string _file;

    public XmlRepository(string file)
    {
        this._file = file;

        if (File.Exists(file))
        {
            this._rootElement = XElement.Load(file);
        }
        else
        {
            this._rootElement = new XElement("entries");
        }
    }

    public bool Add(Entry entry)
    {
        XElement node = new XElement("entry");

        var idAttrib = new XAttribute("id", entry.Id.ToString());
        node.Add(idAttrib);

        var startAttrib = new XAttribute("start", entry.Start.ToString());
        node.Add(startAttrib);

        var endAttrib = new XAttribute("end", entry.End.ToString());
        node.Add(endAttrib);

        var startKmAttrib = new XAttribute("startkm", entry.StartKM.ToString());
        node.Add(startKmAttrib);

        var endKmAttrib = new XAttribute("endkm", entry.EndKM.ToString());
        node.Add(endKmAttrib);

        var fromAttrib = new XAttribute("from", entry.From.ToString());
        node.Add(fromAttrib);

        var toAttrib = new XAttribute("to", entry.To.ToString());
        node.Add(toAttrib);

        var numPlateAttrib = new XAttribute("numberplate", entry.NumberPlate.ToString());
        node.Add(numPlateAttrib);


        node.Add(entry.Description.ToString());

        _rootElement.Add(node);

        return this.Save();
    }

    public bool Delete(Entry entry)
    {
        var itemsDel = from e in this._rootElement.Descendants("entry")
                       where ((string)e.Attribute("id") ?? "") == entry.Id
                       select e;

        itemsDel.Remove();

        return this.Save();
    }

    public bool Update(Entry entry)
    {
        throw new NotImplementedException();
    }

    public bool Save()
    {
        try
        {
            this._rootElement.Save(this._file);
            return true;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            return false;
        }
    }

    public List<Entry> GetAll()
    {
        var entries = from entry in this._rootElement.Descendants("entry")
                      select entry;

        throw new NotImplementedException();
    }
}