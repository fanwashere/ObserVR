using UnityEngine;
using UnityEngine.UI;
using Leap.Unity;
using System.Collections;
using UnityEngine.EventSystems;

public class ViewControlScript : MonoBehaviour
{

    public Sprite NormalIcon;
    public Sprite ActiveIcon;
    public bool on = false;

    public GameObject Frame;
    public GameObject Planets;
    public GameObject Earth;
    public GameObject Clouds;
    public GameObject RightRaycastLight;

    private float delay = 0.25f;
    private bool onDelay = false;
    private bool active = false;

    private Image img;

    // Use this for initialization
    void Start()
    {
        img = GetComponent<Image>();

        Planets.SetActive(on);
    }

    void OnTriggerEnter()
    {
        if (!onDelay)
        {
            active = true;
            on = !on;

            if (on)
            {
                Frame.GetComponent<EarthScript>().enabled = false;
                RightRaycastLight.SetActive(false);
                foreach (Transform child in Frame.transform)
                {
                    child.gameObject.SetActive(false);
                }
                Planets.SetActive(true);
            }
            else
            {
                Frame.GetComponent<EarthScript>().enabled = true;
                Planets.SetActive(false);
                Earth.SetActive(true);
                Clouds.SetActive(true);
                RightRaycastLight.SetActive(true);
            }

            OnClick();
        }
    }

    void OnTriggerExit()
    {
        active = false;
        onDelay = true;
        OnRelease();
        Invoke("ClearDelay", delay);
    }

    private void ClearDelay()
    {
        onDelay = false;
    }

    private void OnClick()
    {
        img.sprite = ActiveIcon;
    }

    private void OnRelease()
    {
        if (!on)
        {
            img.sprite = NormalIcon;
        }
    }
}
