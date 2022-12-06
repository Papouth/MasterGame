using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomsEvent : MonoBehaviour
{
    public UnityEvent[] events;

    public CustomsTriggers[] myTrigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual bool isCompleted(){
        return false;
    }
    public virtual bool OnComplete(){
        return false;
    }
}
