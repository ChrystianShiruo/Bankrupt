using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory {

	public static IPlayer GetPlayer(PlayerType type){

		switch (type) {
		case PlayerType.Impulsive:
			return new ImpulsivePlayerImpl();
		case PlayerType.Demanding:
			return new DemandingPlayerImpl();
		case PlayerType.Cautious:
			return new CautiousPlayerImpl();
		case PlayerType.Random:
			return new RandomPlayerImpl();
		default:
			throw new System.NotImplementedException ("Não existe implementação para: " + type);

		}

	}
}
