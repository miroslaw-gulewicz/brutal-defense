using System;

public interface IHighlightable
{
    void HighLight(bool highlighted);
}

public interface IHighlitableObjectHolder
{
    public int Layer { get; }
    void ForEachObject(Action<IHighlightable> cmd);      
}