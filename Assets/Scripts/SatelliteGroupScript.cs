using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.IO;
using Zeptomoby.OrbitTools;
using System.Collections.Generic;

public class SatelliteGroupScript : MonoBehaviour
{

    public string URL;
    public GameObject Prefab;

    private List<Satellite> sats = new List<Satellite>();
    private Site referencePoint = new Site(0, 0, 0);

    void Start()
    {
        WebRequest request = WebRequest.Create(URL);
        WebResponse response = request.GetResponse();
        Stream dataStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(dataStream);

        while (reader.Peek() > -1)
        {
            string name = reader.ReadLine();
            string line1 = reader.ReadLine();
            string line2 = reader.ReadLine();

            Tle tle = new Tle(name, line1, line2);
            Satellite sat = new Satellite(tle);
            sats.Add(sat);
            Eci eci = sat.PositionEci(DateTime.UtcNow);

            TopoTime topo = referencePoint.GetLookAngle(new EciTime(eci, new Julian(DateTime.UtcNow)));

            // Topocentric to xyz coordinates: a = azimuth, e = elevation, r = range.
            // x = r * cos(e)sin(a), y = r * cos(e)sin(a), z = r * sin(e)
            float x = (float)topo.Range * Mathf.Cos((float)topo.ElevationRad) * Mathf.Sin((float)topo.AzimuthRad);
            float y = (float)topo.Range * Mathf.Cos((float)topo.ElevationRad) * Mathf.Cos((float)topo.AzimuthRad);
            float z = (float)topo.Range * Mathf.Sin((float)topo.ElevationRad);

            Vector3 lookVector = new Vector3(x, y, z);

            GameObject satellite = Instantiate(Prefab);
            satellite.GetComponent<SatelliteScript>().Setup(name);
            satellite.transform.parent = transform;
            satellite.transform.localPosition = (lookVector / 1000) + new Vector3(0, 0, 6.371f);
        }

        InvokeRepeating("StartLocationUpdate", 0, 1.0f);
    }

    private void StartLocationUpdate()
    {
        StartCoroutine(UpdateSatLocation());
    }

    IEnumerator UpdateSatLocation()
    {
        foreach (Satellite sat in sats)
        {
            Destroy(transform.FindChild(sat.Name).gameObject);
            Eci eci = sat.PositionEci(DateTime.UtcNow);

            TopoTime topo = referencePoint.GetLookAngle(new EciTime(eci, new Julian(DateTime.UtcNow)));

            // Topocentric to xyz coordinates: a = azimuth, e = elevation, r = range.
            // x = r * cos(e)sin(a), y = r * cos(e)sin(a), z = r * sin(e)
            float x = (float)topo.Range * Mathf.Cos((float)topo.ElevationRad) * Mathf.Sin((float)topo.AzimuthRad);
            float y = (float)topo.Range * Mathf.Cos((float)topo.ElevationRad) * Mathf.Cos((float)topo.AzimuthRad);
            float z = (float)topo.Range * Mathf.Sin((float)topo.ElevationRad);

            Vector3 lookVector = new Vector3(x, y, z);

            GameObject satellite = Instantiate(Prefab);
            satellite.GetComponent<SatelliteScript>().Setup(sat.Name);
            satellite.transform.parent = transform;
            satellite.transform.localPosition = (lookVector / 1000) + new Vector3(0, 0, 6.371f);

            yield return null;
        }
    }
}
