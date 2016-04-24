using UnityEngine;
using UnityEngine.UI;
using Leap.Unity;
using System.Collections;
using System.Collections.Generic;

public class MenuScript : MonoBehaviour {

    public GameObject Menu;
    public GameObject LeapHandController;

    private LeapServiceProvider controller;

    private float previousDownVelocity = 0;

	// Use this for initialization
	void Start () {
        controller = LeapHandController.GetComponent<LeapServiceProvider>();
        
        CloseMenu();
	}
	
	// Update is called once per frame
	void Update () {
        Leap.Hand left = null, right = null;
        List<Leap.Finger> leftFingers = null, rightFingers = null;

        controller.CurrentFrame.Hands.ForEach(delegate (Leap.Hand hand)
        {
            if (hand.IsLeft)
            {
                left = hand;
                leftFingers = hand.Fingers;
            }
            else if (hand.IsRight)
            {
                right = hand;
                rightFingers = hand.Fingers;
            }
        });

        if (right != null)
        {
            if (previousDownVelocity > -2 && rightFingers[1].TipVelocity.y < -2 && Vector3.Angle(rightFingers[1].Direction.ToVector3(), right.Direction.ToVector3()) < 60 && Vector3.Angle(rightFingers[2].Direction.ToVector3(), right.Direction.ToVector3()) > 120 && Vector3.Angle(rightFingers[3].Direction.ToVector3(), right.Direction.ToVector3()) > 120)
            {
                ToggleMenu();
            }

            previousDownVelocity = rightFingers[1].TipVelocity.y;
        }
        
    }

    public void OpenMenu()
    {
        Menu.SetActive(true);
    }

    public void CloseMenu()
    {
        Menu.SetActive(false);
    }

    public void ToggleMenu()
    {
        if (Menu.activeSelf)
        {
            CloseMenu();
        }
        else
        {
            OpenMenu();
        }
    }
}
