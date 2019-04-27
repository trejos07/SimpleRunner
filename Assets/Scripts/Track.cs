using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public bool isMoving = false;
    public static float velMov = 4f;
    static bool suscribed = false;

    public static float VelMov
    {
        get
        {
            return velMov;
        }

        set
        {

            velMov = value;
            if (OnVelMovChange != null&&value!=0)
                OnVelMovChange();
        }
    }

    public delegate void Action();
    public static event Action OnVelMovChange;
    
    private void Start()
    {
        if(!suscribed)
        {
            suscribed = true;
            Race.Instance.OnEndRace += () => VelMov = 0;
            Race.Instance.OnStartRace += () => VelMov = 5;
            Race.Instance.OnDificultiUp += () => VelMov += VelMov * 0.05f;
        }
    }
    public void Go(Vector3 pos)
    {
        isMoving = true;
        transform.position = pos;
    }
    private void Update()
    {
        if (isMoving)
        {
            transform.position += new Vector3(0, 0, -1) * VelMov * Time.deltaTime;
            if (transform.position.z < -10)
            {
                isMoving = false;
            }
        }

        
    }
    public static void SetVelMov(float f)
    {
        VelMov = f;
    }
}
