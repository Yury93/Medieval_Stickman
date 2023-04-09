using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public event Action<Window> OnOpen, OnClose;
   
    public virtual void Open()
    {
        OnOpen?.Invoke(this);
    }

    // Update is called once per frame
    public virtual void Close()
    {
        OnClose?.Invoke(this);
    }
}
