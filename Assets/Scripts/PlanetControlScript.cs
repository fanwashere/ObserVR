using UnityEngine;
using Leap.Unity;
using System.Collections;
using System.Collections.Generic;

public class PlanetControlScript : MonoBehaviour {

    public GameObject LeapHandController;

    private LeapServiceProvider controller;

    // Use this for initialization
    void Start () {
        controller = LeapHandController.GetComponent<LeapServiceProvider>();
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

        if (left != null)
        {
            if (Vector3.Angle(leftFingers[1].Direction.ToVector3(), left.Direction.ToVector3()) > 120)
            {
                transform.position += new Vector3(0, 0, -1);
            }
            else if (Vector3.Angle(leftFingers[1].Direction.ToVector3(), left.Direction.ToVector3()) < 50)
            {
                transform.position += new Vector3(0, 0, 1);
            }
        }
    }
}
