using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : IPlayer {

    public virtual bool WillBuyEstate(Estate e) {
        return false;
    }
}
