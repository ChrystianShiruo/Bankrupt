using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Reader {
	

	public static Estate[] CreateBoard(){
		TextAsset configFile = Resources.Load<TextAsset> ("gameConfig");
		string[] lines = configFile.text.Split ('\n');
        Estate[] estates = new Estate[lines.Length];

        int c = 0;
        foreach (string line in lines) {

            string[] values = Regex.Split(line, @"\D+");
            
			int price = int.Parse(values[0]);
            try {
                int rent = int.Parse(values[1]);
                estates[c] = new Estate(price, rent);

            } catch {
                Debug.Log(values[1]);

                Debug.Log(values[1].Trim(' '));
                Debug.Break();
            }
			c++;

		}
		return estates;
	}
}
