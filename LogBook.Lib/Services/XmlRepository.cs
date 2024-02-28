using System.Diagnostics;
using System.Xml.Linq;
using LogBook.Lib.Interfaces;
using LogBook.Lib.Models;
using Microsoft.VisualBasic;

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

        if (entry.Description != null || entry.Description != string.Empty)
        {
            node.Add(entry.Description);
        }

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
        var item = (from e in this._rootElement.Descendants("entry")
                    where ((string)e.Attribute("id") ?? "") == entry.Id
                    select e).FirstOrDefault();

        if (item != null)
        {
            item.SetAttributeValue("start", entry.Start.ToString());
            item.SetAttributeValue("end", entry.End.ToString());
            item.SetAttributeValue("startkm", entry.StartKM.ToString());
            item.SetAttributeValue("endkm", entry.EndKM.ToString());
            item.SetAttributeValue("numberplate", entry.NumberPlate.ToString());
            item.SetAttributeValue("to", entry.To.ToString());
            item.SetAttributeValue("from", entry.From.ToString());

            // ID nicht, da sonst das Element nicht mehr gefunden wird

            return this.Save();
        }
        else
        {
            return false;
        }

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
                      select new Entry(
                            (DateTime)entry.Attribute("start"),
                            (DateTime)entry.Attribute("end"),
                            (int)entry.Attribute("startkm"),
                            (int)entry.Attribute("endkm"),
                            (string)entry.Attribute("numberplate"),
                            (string)entry.Attribute("from"),
                            (string)entry.Attribute("to"),
                            (string)entry.Attribute("id"))
                      {
                          Description = entry.Value
                      };


        return entries.ToList<Entry>();
    }

    public Entry? Find(string id)
    {
        var item = (from e in this._rootElement.Descendants("entry")
                    where ((string)e.Attribute("id") ?? "") == id
                    select new Entry(
                            (DateTime)e.Attribute("start"),
                            (DateTime)e.Attribute("end"),
                            (int)e.Attribute("startkm"),
                            (int)e.Attribute("endkm"),
                            (string)e.Attribute("numberplate"),
                            (string)e.Attribute("from"),
                            (string)e.Attribute("to"),
                            (string)e.Attribute("id"))
                    {
                        Description = e.Value
                    }).FirstOrDefault();

        if (item != null)
        {
            return item;
        }
        else
        {
            return null;
        }
    }
}