using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulsivePlayerImpl : Player {

	public override bool WillBuyEstate(Estate e){
		return e.price <= Controller.instance.GetMoney(this);
	}
}
