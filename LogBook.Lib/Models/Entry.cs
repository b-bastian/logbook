namespace LogBook.Lib.Models;

public class Entry
{
    public string Description { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public int StartKM { get; set; }

    public int EndKM { get; set; }

    public string NumberPlate { get; set; }

    public string From { get; set; }

    public string To { get; set; }

    public string Id { get; set; }
    
    public Entry(DateTime start, DateTime end, int startKM, int endKM, string numberPlate, string from, string to, string id) {
        this.Start = start;
        this.End = end;
        this.StartKM = startKM;
        this.EndKM = endKM;
        this.NumberPlate = numberPlate;
        this.From = from;
        this.To = to;
        this.Id = id;
    }   
    
    public Entry(DateTime start, DateTime end, int startKM, int endKM, string numberPlate, string from, string to) {
        this.Start = start;
        this.End = end;
        this.StartKM = startKM;
        this.EndKM = endKM;
        this.NumberPlate = numberPlate;
        this.From = from;
        this.To = to;
        this.Id = Guid.NewGuid().ToString();
    }   
}