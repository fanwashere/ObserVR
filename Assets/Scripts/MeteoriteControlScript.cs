using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class MeteoriteControlScript : MonoBehaviour
{

    public Sprite NormalIcon;
    public Sprite ActiveIcon;

    public GameObject MeteoriteControl;
    public GameObject MeteoritesLayer;

    private float delay = 0.25f;
    private bool onDelay = false;
    private bool active = false;
    private bool on = false;

    private Image img;

    // Use this for initialization
    void Start()
    {
        img = GetComponent<Image>();
    }

    void Update()
    {
        if (on && !MeteoriteControl.GetComponent<LayerControlScript>().on)
        {
            active = false;
            on = false;
            img.sprite = NormalIcon;
            MeteoritesLayer.GetComponent<MeteoriteSiteScript>().HideMass();
        }
    }

    void OnTriggerEnter()
    {
        if (!onDelay)
        {
            active = true;
            on = !on;
            if (on)
            {
                MeteoritesLayer.GetComponent<MeteoriteSiteScript>().ShowMass();
            }
            else
            {
                MeteoritesLayer.GetComponent<MeteoriteSiteScript>().HideMass();
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
