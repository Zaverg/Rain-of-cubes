using System;
using UnityEngine;

public interface IReleased<T> where T : class
{
    public event Action<T> Released;
}
