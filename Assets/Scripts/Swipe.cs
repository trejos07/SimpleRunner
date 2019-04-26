using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour {

    public static Swipe Instance;

    bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    Vector2 starTouch, swipeDelta;
    bool isDragging;
    bool canTap;
    AudioSource SwipeSound;

    #region accesores
    public bool Tap
    {
        get
        {
            return tap;
        }

        set
        {
            tap = value;
        }
    }
    public bool SwipeLeft
    {
        get
        {
            return swipeLeft;
        }

        set
        {
            swipeLeft = value;
            canTap = false;
        }
    }
    public bool SwipeRight
    {
        get
        {
            return swipeRight;
        }

        set
        {
            swipeRight = value;
            canTap = false;
        }
    }
    public bool SwipeUp
    {
        get
        {
            return swipeUp;
        }

        set
        {
            swipeUp = value;
            canTap = false;
        }
    }
    public bool SwipeDown
    {
        get
        {
            return swipeDown;
        }

        set
        {
            swipeDown = value;
            canTap = false;
        }
    }
    public Vector2 StarTouch
    {
        get
        {
            return starTouch;
        }

        set
        {
            starTouch = value;
        }
    }
    public Vector2 SwipeDelta
    {
        get
        {
            return swipeDelta;
        }

        set
        {
            swipeDelta = value;
        }
    }
    public bool IsDragging
    {
        get
        {
            return isDragging;
        }

        set
        {
            isDragging = value;
        }
    }
    #endregion

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }
    private void Start()
	{
        SwipeSound = GetComponent<AudioSource>();
	}
	void Update () {

        //if(!IsDragging)
        //    canTap = true;
        //if (Tap) Tap = false;
        //if (SwipeLeft) SwipeLeft = false;
        //if (SwipeRight) SwipeRight = false;
        //if (SwipeUp) SwipeUp = false;
        //if (SwipeDown) SwipeDown = false;
        Tap = false;
       
        #region PcInputs
        if (Input.GetMouseButtonDown(0))
        {
            IsDragging = true;
            starTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            IsDragging = false;
            Reset();
        }
        #endregion
        #region MobileInputs
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                starTouch = Input.touches[0].position;
                canTap = true;
                IsDragging = true;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                if(canTap)
                    Tap = true;
                IsDragging = false;
                Reset();
            }
        }
        #endregion
        if(!IsDragging)
            SwipeLeft = SwipeRight = SwipeUp = SwipeDown = false;
        swipeDelta = Vector2.zero;

        if (IsDragging)
        {
            if (Input.touches.Length > 0)
            {
                swipeDelta = Input.touches[0].position - starTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - starTouch;

            }

        } // Calcular La Distancia de Swipe

        // Verificar Si El Swipe es suficiente (pasé la deadzone)
        if (swipeDelta.magnitude > 20)
        {
            Tap = false;
            SwipeSound.Play();
            float PosicionX = swipeDelta.x;
            float PosicionY = swipeDelta.y;
            if (Mathf.Abs(PosicionX) > Mathf.Abs(PosicionY))  //Es Un Swipe Horizontal
            {
                if (PosicionX < 0)
                    SwipeLeft = true;
                else
                    SwipeRight = true;
            }
            else                                           //Es Un Swipe Vertical
            {
                if (PosicionY < 0)
                    SwipeDown = true;
                else
                    SwipeUp = true;
            }
            Reset();
        }
               
    }
    private void Reset()
    {
        starTouch = swipeDelta = Vector2.zero;
        IsDragging = false;
        
    }
}
