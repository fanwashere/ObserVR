using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour {

    public Sprite NormalIcon;
    public Sprite ActiveIcon;

    private float delay = 0.25f;
    private bool onDelay = false;
    private bool active = false;

    private Image img;

    // Use this for initialization
    void Start () {
        img = GetComponent<Image>();
    }

    void OnTriggerEnter()
    {
        if (!onDelay)
        {
            active = true;
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
        img.sprite = NormalIcon;
    }
}
