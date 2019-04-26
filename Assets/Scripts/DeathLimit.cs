using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLimit : MonoBehaviour
{
    //public static DeathLimit Instance;

    //private void Awake()
    //{
    //    if (Instance == null)
    //        Instance = this;
    //}

    public delegate void Action();
    public static event Action OnDeathTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Player")
        {
            if (OnDeathTrigger != null)
                OnDeathTrigger();
        }
    }

}
