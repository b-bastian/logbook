using System;

namespace LogBook.Lib.Models;

public class Entry
{
    public string Description { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public int StartKM { get; set; }

    public int EndKM { get; set; }

    public int Distance => EndKM - StartKM;

    public string NumberPlate { get; set; }

    public string From { get; set; }

    public string To { get; set; }

    public string Id { get; set; }

    public bool Favorite { get; set; } = false;

    public Entry(DateTime start, DateTime end, int startKM, int endKM, string numberPlate, string from, string to, string id, bool favorite)
    {
        this.Start = start;
        this.End = end;
        this.StartKM = startKM;
        this.EndKM = endKM;
        this.NumberPlate = numberPlate;
        this.From = from;
        this.To = to;
		this.Favorite = favorite;
		this.Id = id;
    }

    public Entry(DateTime start, DateTime end, int startKM, int endKM, string numberPlate, string from, string to, bool favorite)
    {
        this.Start = start;
        this.End = end;
        this.StartKM = startKM;
        this.EndKM = endKM;
        this.NumberPlate = numberPlate;
        this.From = from;
        this.To = to;
        this.Favorite = favorite;
        this.Id = Guid.NewGuid().ToString();
    }

    public override string ToString()
    {
        return String.Format($"{this.From} nach {this.To}");
    }
}