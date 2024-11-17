using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EventManager : SingletonDontDestroy<EventManager>
{
    public enum Channel
    {
        Achievement
        
    }
    private readonly Dictionary<Channel, List<Delegate>> _eventHandlers = new Dictionary<Channel, List<Delegate>>();
    
    public void Subscribe<TEvent>(Channel channel, Action<TEvent> handler) where TEvent : EventArgs
    {
        if (false == _eventHandlers?.ContainsKey(channel))
            _eventHandlers[channel] = new List<Delegate>();

        _eventHandlers?[channel].Add(handler);
    }

    public void Unsubscribe<TEvent>(Channel channel, Action<TEvent> handler) where TEvent : EventArgs
    {
        Type eventType = typeof(TEvent);
        
        if (_eventHandlers.TryGetValue(channel, out var eventHandler))
            eventHandler.Remove(handler);
        
    }
    
    public void Publish<TEvent>(Channel channel, TEvent eventArgs) where TEvent : EventArgs
    {
        Type eventType = typeof(TEvent);
        if (false == _eventHandlers?.ContainsKey(channel))
            return;
        
        var handlers = _eventHandlers?[channel];
        if (handlers == null) return;
        
        foreach (var handler in handlers)
        {
            if (false == handler is Action<TEvent>)
                Debug.LogError("publish error : sub event type is not " + typeof(TEvent));

            ((Action<TEvent>)handler)(eventArgs);
        }
    }
}

