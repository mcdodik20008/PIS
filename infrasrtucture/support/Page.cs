﻿namespace PISWF.infrasrtucture.page;

public class Page
{
    public int Number { get; }
    
    public int Size { get; }

    public Page(int number, int size)
    {
        Number = number;
        Size = size;
    }
}