using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SatelliteScript : MonoBehaviour {

    public GameObject Canvas;
    public GameObject Name;
    public GameObject Indicator;

    private bool focus = false;

    void Update()
    {
        Indicator.SetActive(focus);

        focus = false;
    } 

    public void Highlight()
    {
        focus = true;
    }

    public void Setup(string name)
    {
        transform.name = name;
        Name.GetComponent<Text>().text = name;
    }

    public void AimCanvas(GameObject target)
    {
        Canvas.transform.LookAt(target.transform.position);
        Canvas.transform.Rotate(0, 180, 0);
    }
}
