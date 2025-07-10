using System;

public interface IReleasable<T> where T : class
{
    public event Action<T> Released;
}