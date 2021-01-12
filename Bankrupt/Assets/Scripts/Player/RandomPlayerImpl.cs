using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlayerImpl : Player {

	public override bool WillBuyEstate(Estate e){
        bool r = (Random.Range(0, 2) == 1);
        return (r);
	}
}
