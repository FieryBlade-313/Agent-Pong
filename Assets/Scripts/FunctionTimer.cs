using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionTimer
{
    public static FunctionTimer Create(Action action, float timer)
    {
        GameObject gameObject = new GameObject("FunctionTimer", typeof(MonoBehaviourHook));

        FunctionTimer functionTimer = new FunctionTimer(action, timer, gameObject);

        gameObject.GetComponent<MonoBehaviourHook>().onUpdate = functionTimer.Update;

        return functionTimer;
    }
    public class MonoBehaviourHook : MonoBehaviour
    {
        public Action onUpdate;
        private void Update()
        {
            onUpdate?.Invoke();
        }
    }
    private Action action;
    private float time;
    private GameObject gameObject;
    private bool isDestroyed;
    public FunctionTimer(Action action,float time,GameObject gameObject)
    {
        this.action = action;
        this.time = time;
        this.gameObject = gameObject;
        this.isDestroyed = false;
    }

    public void Update()
    {
        if (!isDestroyed)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                action();
                DestroySelf();
            }
        }
    }

    private void DestroySelf()
    {
        isDestroyed = true;
        UnityEngine.Object.Destroy(gameObject); 
    }
}
