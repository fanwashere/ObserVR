using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Leap.Unity;
using Zeptomoby.OrbitTools;
using System.Net;
using System.IO;
using System;

public class EarthScript : MonoBehaviour {

    public GameObject kamera;
    public GameObject LeapHandController;
    public Mesh SphereMesh;
    public Font DefaultFont;
    public GameObject RightRaycastLine;

    public GameObject CubsatPrefab;
    public GameObject GPSSatPrefab;
    public GameObject ScienceSatPrefab;
    public GameObject StationPrefab;

    private LeapServiceProvider controller;

	// Use this for initialization
	void Start () {
        controller = LeapHandController.GetComponent<LeapServiceProvider>();
        /*
        WebRequest request = WebRequest.Create("http://celestrak.com/NORAD/elements/cubesat.txt");
        WebResponse response = request.GetResponse();
        Stream dataStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(dataStream);
        
        Site referencePoint = new Site(90, 0, 0);
        while (reader.Peek() > -1)
        {
            string name = reader.ReadLine();
            string line1 = reader.ReadLine();
            string line2 = reader.ReadLine();

            Tle tle = new Tle(name, line1, line2);
            Satellite sat = new Satellite(tle);
            Eci eci = sat.PositionEci(DateTime.UtcNow);
            
            TopoTime topo = referencePoint.GetLookAngle(new EciTime(eci, new Julian(DateTime.UtcNow)));

            // Topocentric to xyz coordinates: a = azimuth, e = elevation, r = range.
            // x = r * cos(e)sin(a), y = r * cos(e)sin(a), z = r * sin(e)
            float x = (float)topo.Range * Mathf.Cos((float)topo.ElevationRad) * Mathf.Sin((float)topo.AzimuthRad);
            float y = (float)topo.Range * Mathf.Cos((float)topo.ElevationRad) * Mathf.Cos((float)topo.AzimuthRad);
            float z = (float)topo.Range * Mathf.Sin((float)topo.ElevationRad);

            Vector3 lookVector = new Vector3(x, y, z);
            Debug.Log(lookVector);
            
            GameObject satellite = Instantiate(CubsatPrefab);
            satellite.GetComponent<SatelliteScript>().Setup(name);
            satellite.GetComponent<SatelliteScript>().AimCanvas(kamera);
            satellite.transform.parent = transform;
            satellite.transform.position = (lookVector / 1000) + new Vector3(0, 0, 6.371f);     
        }

        request = WebRequest.Create("http://www.celestrak.com/NORAD/elements/gps-ops.txt");
        response = request.GetResponse();
        dataStream = response.GetResponseStream();
        reader = new StreamReader(dataStream);

        referencePoint = new Site(90, 0, 0);
        while (reader.Peek() > -1)
        {
            string name = reader.ReadLine();
            string line1 = reader.ReadLine();
            string line2 = reader.ReadLine();

            Tle tle = new Tle(name, line1, line2);
            Satellite sat = new Satellite(tle);
            Eci eci = sat.PositionEci(DateTime.UtcNow);

            TopoTime topo = referencePoint.GetLookAngle(new EciTime(eci, new Julian(DateTime.UtcNow)));

            // Topocentric to xyz coordinates: a = azimuth, e = elevation, r = range.
            // x = r * cos(e)sin(a), y = r * cos(e)sin(a), z = r * sin(e)
            float x = (float)topo.Range * Mathf.Cos((float)topo.ElevationRad) * Mathf.Sin((float)topo.AzimuthRad);
            float y = (float)topo.Range * Mathf.Cos((float)topo.ElevationRad) * Mathf.Cos((float)topo.AzimuthRad);
            float z = (float)topo.Range * Mathf.Sin((float)topo.ElevationRad);

            Vector3 lookVector = new Vector3(x, y, z);
            Debug.Log(lookVector);

            GameObject satellite = Instantiate(GPSSatPrefab);
            satellite.GetComponent<SatelliteScript>().Setup(name);
            satellite.GetComponent<SatelliteScript>().AimCanvas(kamera);
            satellite.transform.parent = transform;
            satellite.transform.position = (lookVector / 1000) + new Vector3(0, 0, 6.371f);
        }

        request = WebRequest.Create("http://celestrak.com/NORAD/elements/science.txt");
        response = request.GetResponse();
        dataStream = response.GetResponseStream();
        reader = new StreamReader(dataStream);

        referencePoint = new Site(90, 0, 0);
        while (reader.Peek() > -1)
        {
            string name = reader.ReadLine();
            string line1 = reader.ReadLine();
            string line2 = reader.ReadLine();

            Tle tle = new Tle(name, line1, line2);
            Satellite sat = new Satellite(tle);
            Eci eci = sat.PositionEci(DateTime.UtcNow);

            TopoTime topo = referencePoint.GetLookAngle(new EciTime(eci, new Julian(DateTime.UtcNow)));

            // Topocentric to xyz coordinates: a = azimuth, e = elevation, r = range.
            // x = r * cos(e)sin(a), y = r * cos(e)sin(a), z = r * sin(e)
            float x = (float)topo.Range * Mathf.Cos((float)topo.ElevationRad) * Mathf.Sin((float)topo.AzimuthRad);
            float y = (float)topo.Range * Mathf.Cos((float)topo.ElevationRad) * Mathf.Cos((float)topo.AzimuthRad);
            float z = (float)topo.Range * Mathf.Sin((float)topo.ElevationRad);

            Vector3 lookVector = new Vector3(x, y, z);
            Debug.Log(lookVector);

            GameObject satellite = Instantiate(ScienceSatPrefab);
            satellite.GetComponent<SatelliteScript>().Setup(name);
            satellite.GetComponent<SatelliteScript>().AimCanvas(kamera);
            satellite.transform.parent = transform;
            satellite.transform.position = (lookVector / 1000) + new Vector3(0, 0, 6.371f);
        }

        request = WebRequest.Create("http://celestrak.com/NORAD/elements/stations.txt");
        response = request.GetResponse();
        dataStream = response.GetResponseStream();
        reader = new StreamReader(dataStream);

        referencePoint = new Site(90, 0, 0);
        while (reader.Peek() > -1)
        {
            string name = reader.ReadLine();
            string line1 = reader.ReadLine();
            string line2 = reader.ReadLine();

            Tle tle = new Tle(name, line1, line2);
            Satellite sat = new Satellite(tle);
            Eci eci = sat.PositionEci(DateTime.UtcNow);

            TopoTime topo = referencePoint.GetLookAngle(new EciTime(eci, new Julian(DateTime.UtcNow)));

            // Topocentric to xyz coordinates: a = azimuth, e = elevation, r = range.
            // x = r * cos(e)sin(a), y = r * cos(e)sin(a), z = r * sin(e)
            float x = (float)topo.Range * Mathf.Cos((float)topo.ElevationRad) * Mathf.Sin((float)topo.AzimuthRad);
            float y = (float)topo.Range * Mathf.Cos((float)topo.ElevationRad) * Mathf.Cos((float)topo.AzimuthRad);
            float z = (float)topo.Range * Mathf.Sin((float)topo.ElevationRad);

            Vector3 lookVector = new Vector3(x, y, z);
            Debug.Log(lookVector);

            GameObject satellite = Instantiate(StationPrefab);
            satellite.GetComponent<SatelliteScript>().Setup(name);
            satellite.GetComponent<SatelliteScript>().AimCanvas(kamera);
            satellite.transform.parent = transform;
            satellite.transform.position = (lookVector / 1000) + new Vector3(0, 0, 6.371f);
        }

        UpdateCameraBound();*/
    }
	
	// Update is called once per frame
	void Update () {
        Leap.Hand leftHand = null, rightHand = null;

        controller.CurrentFrame.Hands.ForEach(delegate (Leap.Hand hand)
        {
            if (hand.IsLeft)
            {
                leftHand = hand;
            }
            else if (hand.IsRight)
            {
                rightHand = hand;
            }
        });

        if (rightHand != null)
        {
            var rightFingers = rightHand.Fingers;

            if (Vector3.Angle(rightFingers[1].Direction.ToVector3(), rightHand.Direction.ToVector3()) > 120)
            {
                transform.eulerAngles += new Vector3(0, 1, 0);
                UpdateCameraBound();
            }

            if (Vector3.Angle(rightFingers[1].Direction.ToVector3(), rightHand.Direction.ToVector3()) < 40 && Vector3.Angle(rightFingers[2].Direction.ToVector3(), rightHand.Direction.ToVector3()) > 120)
            {
                RightRaycastLine.SetActive(true);
                RightRaycastLine.GetComponent<LineRenderer>().SetPosition(0, rightFingers[1].TipPosition.ToVector3());
                RightRaycastLine.GetComponent<LineRenderer>().SetPosition(1, rightFingers[1].TipPosition.ToVector3() + LeapHandController.transform.position + (rightFingers[1].Direction.ToVector3() * 1000));

                Vector3 fwd = rightFingers[1].Direction.ToVector3();
                RaycastHit hit; 

                if (Physics.Raycast(rightFingers[1].TipPosition.ToVector3(), fwd, out hit))
                {
                    RightRaycastLine.GetComponent<LineRenderer>().SetPosition(1, hit.transform.position);
                    hit.transform.gameObject.GetComponent<SatelliteScript>().Highlight();
                }
            }
            else
            {
                RightRaycastLine.SetActive(false);
            }
        }
        else
        {
            RightRaycastLine.SetActive(false);
        }
        
        if (leftHand != null)
        {
            var leftFingers = leftHand.Fingers;

            if (Vector3.Angle(leftFingers[1].Direction.ToVector3(), leftHand.Direction.ToVector3()) > 120)
            {
                transform.eulerAngles += new Vector3(0, -1, 0);
                
            }
        } 

        if (leftHand != null && rightHand != null)
        {
            //Debug.Log(Vector3.Angle(leftHand.PalmNormal.ToVector3(), rightHand.PalmNormal.ToVector3()));
            if (Vector3.Angle(leftHand.PalmNormal.ToVector3(), Vector3.right) < 30)
            {
                Debug.Log("Left Horizontal");
            }

            if (Vector3.Angle(rightHand.PalmNormal.ToVector3(), Vector3.left) < 30)
            {
                Debug.Log("Right Horizontal");
            }
        }
    }

    void LateUpdate()
    {
        UpdateCameraBound();
    }

    private void UpdateCameraBound()
    {
        var canvases = GameObject.FindGameObjectsWithTag("Camera Bound");
        foreach (GameObject canvas in canvases)
        {
            canvas.transform.LookAt(kamera.transform.position);
            canvas.transform.Rotate(0, 180, 0);
        }
    }
}
