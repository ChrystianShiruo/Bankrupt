using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CautiousPlayerImpl : Player {

	public override bool WillBuyEstate(Estate e){
        return Controller.instance.GetMoney(this) - e.price >= 80;
	}
}
