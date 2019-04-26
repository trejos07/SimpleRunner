using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Player : MonoBehaviour
{
    [SerializeField]  ThirdPersonUserControl userControl;
    [SerializeField] int speed;

    private void Start()
    {
        Race.Instance.OnStartRace += ()=>TurnWalkSound(true);
        Race.Instance.OnEndRace += ()=>TurnWalkSound(false);
    }

    void TurnWalkSound(bool b)
    {
        if(b)
        {
            userControl.Character.OnGroundInteraction += DoWalkSound;
            DoWalkSound(true);
        }
        else
        {
            userControl.Character.OnGroundInteraction -= DoWalkSound;
            DoWalkSound(false);
        }
            
    }

    public void DoWalkSound(bool _do)
    {
        if (_do)
            SoundManager.instance.Play("Steps");
        else
            SoundManager.instance.Stop("Steps");
    }

    void Update()
    {
        if (Swipe.Instance.Tap)
        {
            if(userControl.Crouch)
                userControl.Crouch = false;
            else
            {
                userControl.Jump = true;
                SoundManager.instance.Play("Jump");
            }
        }
        if (Swipe.Instance.SwipeDown)
            userControl.Crouch = true;
        if (Swipe.Instance.SwipeUp)
            userControl.Crouch = false;



        Vector3 posicionDeseada = transform.position;
        float x_input = AcelerationController.Instance.Aceleration.x;
        posicionDeseada += Vector3.right * x_input;
        posicionDeseada.x = Mathf.Clamp(posicionDeseada.x, -2, 2);
        transform.position = Vector3.MoveTowards(userControl.transform.position, posicionDeseada, speed * Time.deltaTime);
        userControl.V = 1;
        userControl.H = x_input * speed;
    }
}
