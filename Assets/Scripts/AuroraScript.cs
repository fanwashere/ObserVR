using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System;

public class AuroraScript : MonoBehaviour {

	public string URL = "http://services.swpc.noaa.gov/text/aurora-nowcast-map.txt";
	public Material TargetMaterial;
	public int NominalValue = 40f;

	// Use this for initialization
	void Start () {
		WebRequest request = WebRequest.Create(URL);
		WebResponse response = request.GetResponse();
		Stream dataStream = response.GetResponseStream();
		StreamReader reader = new StreamReader(dataStream);

		int col = 0, row = 0;
		Texture2D texture = new Texture2D(512, 1024, TextureFormat.ARGB32, false);

		while (reader.Peek() > -1) {
			string line = reader.ReadLine ();

			if (line.StartsWith("#")) {
				// The text file beings with several lines starting with '#'. Skip.
				continue;
			}
			else {
				// Remove all whitespaces and tabs.
				//line = line.Replace(" ", "");
				//line = line.Replace ("\t", "");

				// Make into char array, then convert to int array.
				line = line.Remove(0, 3);
				string[] valuesAsString = System.Text.RegularExpressions.Regex.Split(line, @"\s{2,}");

				// Convert to ints.
				int[] values = Array.ConvertAll<string, int>(valuesAsString, int.Parse);

				// Each latitude value will be two rows on the texture to make a square texture.
				foreach (int value in values) {
					// Each pixel will be green. The percent value (divided by nominal) will set alpha.
					texture.SetPixel(col, row, new Color(0.0f, 1.0f, 0.0f, (float)value / NominalValue));
					texture.SetPixel(col++, row + 1, new Color(0.0f, 1.0f, 0.0f, (float)value / NominalValue));
				}

				// Iterate.
				col = 0;
				row += 2;
			}

			// Apply texture to this gameobject.
			texture.alphaIsTransparency = true;
			texture.Apply();
			GetComponent<Renderer>().material.mainTexture = texture;
		}
	}
}
