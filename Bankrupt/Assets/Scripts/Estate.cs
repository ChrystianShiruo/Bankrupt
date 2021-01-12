using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Estate {

	public int ownerId;
	public int price;
	public int rent;

	public Estate( int price1, int rent1){
		price = price1;
		rent = rent1;
		ownerId = -1;
	}
}
