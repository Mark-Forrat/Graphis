using System;
using System.Collections.Generic;
using System.Drawing;

public class Line
{
    public Point Start;
    public Point End;
    public int StartZ;
    public int EndZ;
    public int StartOK;
    public int EndOK;
    public Point DistanceStart;
    public Point DistanceEnd;
    public bool StartSelected;
    public bool EndSelected;
    public bool MidSelected;

    public double DistanceToStart(Point p)
    {
        return Math.Sqrt(Math.Pow(Start.X - p.X, 2) + Math.Pow(Start.Y - p.Y, 2));
    }
    public double DistanceToEnd(Point p)
    {
        return Math.Sqrt(Math.Pow(End.X - p.X, 2) + Math.Pow(End.Y - p.Y, 2));
    }
    public double DistanceToMid(Point p)
    {
        return Math.Sqrt(Math.Pow((End.X + Start.X)/2 - p.X, 2) + Math.Pow((End.Y + Start.Y)/2 - p.Y, 2));
    }

    public void FromMidStart(Point p)
    {

        Start.X = p.X + DistanceStart.X;
        Start.Y = p.Y + DistanceStart.Y;
        End.X = p.X + DistanceEnd.X;
        End.Y = p.Y + DistanceEnd.Y;


    }


}