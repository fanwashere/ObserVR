using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.IO;
using Zeptomoby.OrbitTools;
using System.Collections.Generic;
using SimpleJSON;

public class MeteoriteSiteScript : MonoBehaviour {

    public string URL;
    public GameObject Prefab;

    float earthRadius = 6.371f;

    JSONArray sites;
    
	// Use this for initialization
	void Start () {
        WWW www = new WWW(URL);
        StartCoroutine(WaitForRequest(www));
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        if (www.error == null)
        {
            sites = JSON.Parse(www.text).AsArray;
            Debug.Log(sites[0]["name"]);
            Debug.Log(sites[0]["geolocation"]["longitude"]);
            OnReceiveData();
        }
        else
        {
            Debug.Log("Error");
        }
    }
    
    public void OnReceiveData()
    {
        for (int i = 0; i < sites.Count; i++)
        {
            if (sites[i]["geolocation"]["latitude"] == null || sites[i]["geolocation"]["longitude"] == null || sites[i]["mass"] == null)
            {
                continue; 
            }

            float lat = Mathf.PI * float.Parse(sites[i]["geolocation"]["latitude"]) / 180;
            float lng = Mathf.PI * float.Parse(sites[i]["geolocation"]["longitude"]) / 180;

            float x = earthRadius * Mathf.Cos(lat) * Mathf.Sin(lng); 
            float y = earthRadius * Mathf.Cos(lat) * Mathf.Cos(lng);
            float z = earthRadius * Mathf.Sin(lat);

            GameObject site = Instantiate(Prefab);
            site.GetComponent<MeteoriteScript>().Setup(sites[i]["name"], float.Parse(sites[i]["mass"]));
            site.transform.parent = transform;
            site.transform.localPosition = new Vector3(x, y, z);
            site.transform.localRotation = Quaternion.FromToRotation(site.transform.up, site.transform.localPosition);   
        }
    }

    public void ShowMass()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < sites.Count; i++)
        {
            if (sites[i]["geolocation"]["latitude"] == null || sites[i]["geolocation"]["longitude"] == null || sites[i]["mass"] == null)
            {
                continue;
            }

            float lat = Mathf.PI * float.Parse(sites[i]["geolocation"]["latitude"]) / 180;
            float lng = Mathf.PI * float.Parse(sites[i]["geolocation"]["longitude"]) / 180;

            float x = earthRadius * Mathf.Cos(lat) * Mathf.Sin(lng);
            float y = earthRadius * Mathf.Cos(lat) * Mathf.Cos(lng);
            float z = earthRadius * Mathf.Sin(lat);

            GameObject site = Instantiate(Prefab);
            site.GetComponent<MeteoriteScript>().Setup(sites[i]["name"], float.Parse(sites[i]["mass"]));
            site.transform.parent = transform;
            site.transform.localScale = new Vector3(0.1f, 1 * float.Parse(sites[i]["mass"]) / 100000, 0.1f);
            site.transform.localPosition = new Vector3(x, y, z);
            site.transform.localPosition += new Vector3(0, 0.5f * float.Parse(sites[i]["mass"]) / 100000, 0);
            site.transform.localRotation = Quaternion.FromToRotation(site.transform.up, site.transform.localPosition);
        }
    }

    public void HideMass()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < sites.Count; i++)
        {
            if (sites[i]["geolocation"]["latitude"] == null || sites[i]["geolocation"]["longitude"] == null || sites[i]["mass"] == null)
            {
                continue;
            }

            float lat = Mathf.PI * float.Parse(sites[i]["geolocation"]["latitude"]) / 180;
            float lng = Mathf.PI * float.Parse(sites[i]["geolocation"]["longitude"]) / 180;

            float x = earthRadius * Mathf.Cos(lat) * Mathf.Sin(lng);
            float y = earthRadius * Mathf.Cos(lat) * Mathf.Cos(lng);
            float z = earthRadius * Mathf.Sin(lat);

            GameObject site = Instantiate(Prefab);
            site.GetComponent<MeteoriteScript>().Setup(sites[i]["name"], float.Parse(sites[i]["mass"]));
            site.transform.parent = transform;
            site.transform.localPosition = new Vector3(x, y, z);
            site.transform.localRotation = Quaternion.FromToRotation(site.transform.up, site.transform.localPosition);
        }
    }
}
