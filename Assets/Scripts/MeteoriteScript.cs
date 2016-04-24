using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MeteoriteScript : MonoBehaviour {

    public GameObject Canvas;
    public GameObject Name;
    public GameObject Indicator;

    private float mass = 0;

    public void Setup(string name, float temp)
    {
        transform.name = name;
        Name.GetComponent<Text>().text = name;
        mass = temp;
    }

    public void AimCanvas(GameObject target)
    {
        Canvas.transform.LookAt(target.transform.position);
        Canvas.transform.Rotate(0, 180, 0);
    }

    public void DisplayMass()
    {
        transform.localScale = new Vector3(0.1f, 1 * mass / 42000, 0.1f);
        transform.localPosition += new Vector3(0, 0.5f * mass / 42000, 0);
    }

    public void HideMass()
    {
        transform.localPosition -= new Vector3(0, 0.5f * mass / 42000, 0);
        transform.localScale = new Vector3(0.1f, 1, 0.1f);
    }
}
