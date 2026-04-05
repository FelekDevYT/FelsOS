using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace FenixOS.System.EventSystem;

public class EventManager
{
    private Dictionary<String, List<object>> listeners;

    public void init()
    {
        listeners = new Dictionary<String, List<object>>();
    }

    public void registerListener<TE>(EventListener<TE> listener) where TE : Event
    {
        String key = typeof(TE).FullName;
        if (!listeners.ContainsKey(key))
        {
            listeners[key] = new List<object>();
        }
        
        listeners[key].Add(listener);
    }
    
    public void callEvent<TE>(TE e) where TE : Event{
        String key = typeof(TE).FullName;

        if (listeners != null && listeners.ContainsKey(key))
        {
            foreach (var listener in listeners[key])
            {
                ((EventListener<TE>) listener).onEvent(e);
            }
        }
    }
}