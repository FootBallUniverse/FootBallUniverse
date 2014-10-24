using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class CSV_test : MonoBehaviour {

    private string gui = "";

	// Use this for initialization
	void Start () {
        /*
    string path = Application.dataPath;

    string filePath = path + "/Resources/CSV/HumanData.csv";

    string[,] cols = new string[4,]; 
    FileInfo fi = new FileInfo(filePath);
    using (StreamReader sr = new StreamReader(fi.OpenRead(), Encoding.GetEncoding("Shift_JIS")))    
    {
        string tes = sr.ReadLine();

        int i = 0;
        while (sr.ReadLine() != null)
        {
            cols[i,] = tes.Split(',');
            ++i;
        }
        for (int n = 0; n < cols[i].Length; ++n)
            Debug.Log(cols[n] + "\t");
        Debug.Log("");
    }

    gui = filePath;
    gui += "\n";

    for( int i = 0; i < 10; ++ i )
        gui += cols[i] + " : ";
        */

    }

    void OnGUI()
    {
        GUI.TextArea(new Rect(5, 5, Screen.width, 50), gui);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
