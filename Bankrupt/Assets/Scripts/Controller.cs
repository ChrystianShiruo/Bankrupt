using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    public static Controller instance;

    [SerializeField] private int _startMoney = 300;
    [SerializeField] private int _lapReward = 100;
    [SerializeField] private int _roundLimit = 1000;

    private int _round;
    private List<IPlayer> _players = new List<IPlayer>();

    private int _startingPlayer;
    private int[] playerWins;
    private PlayerInfo[] _playersInfo;
    private Estate[] _estates;

    private int _overtimeGames = 0;

    private class PlayerInfo {
        public int money = 0;
        public int position = -1;
    }
    private bool _hasTwoOrMorePlayers {
        get {
            int validPlayers = 0;
            foreach (PlayerInfo info in _playersInfo) {
                if (info.money > -1) {
                    validPlayers++;
                    if (validPlayers >= 2) {
                        return true;
                    }
                }
            }
            return false;
        }
    }
    private int _index;
    private int index {
        get {
            return _index;
        }
        set {
            _index = value;
            if (_index >= _players.Count) {
                _index = 0;
                _round++;
            }
        }
    }
    private int _turnsAverage = 0;
    private int turnsAverage {
        get {
            return _turnsAverage;
        }
        set {
            _turnsAverage = value;

            for (int i = 0; i < _playersInfo.Length; i++) {
                if (_playersInfo[i].money >= 0) {
                    playerWins[i]++;
                    n++;
                }
            }
        }
    }
    public int n, n2, n3;
    private void Awake() {
        instance = this;

        _estates = Reader.CreateBoard();

        _players.Add(PlayerFactory.GetPlayer(PlayerType.Impulsive));
        _players.Add(PlayerFactory.GetPlayer(PlayerType.Cautious));
        _players.Add(PlayerFactory.GetPlayer(PlayerType.Demanding));
        _players.Add(PlayerFactory.GetPlayer(PlayerType.Random));

        playerWins = new int[_players.Count];
        StartCoroutine(RunSimulation(300));

    }

    private IEnumerator RunSimulation(int totalGames) {
        for (int game = 0; game < totalGames; game++) {

            PrepareGame(_startMoney);

            index = _startingPlayer;
            while (_hasTwoOrMorePlayers && _round < 1000) {
                if (_playersInfo[index].money >= 0) {

                    MovePlayer(index);
                    CheckPosition(_playersInfo[index].position);
                    if (!_hasTwoOrMorePlayers) {
                        break;
                    }
                }

                index++;
            }
            CheckGameResult();

            if (game % 50 == 0) {
                yield return null;
            }
        }
        ShowSimulationResults();
    }

    private void ShowSimulationResults() {

        Debug.Log("Test over.");
        Debug.Log("Timeout finishes: " + _overtimeGames);
        Debug.Log("Turns average per game: " + (float)turnsAverage / (300 - _overtimeGames));
        Debug.Log("Impulsive player wins %: " + (float)playerWins[0] / 3 + "%");
        Debug.Log("Cautious player wins %: " + (float)playerWins[1] / 3 + "%");
        Debug.Log("Demanding player wins %: " + (float)playerWins[2] / 3 + "%");
        Debug.Log("Random player wins %: " + (float)playerWins[3] / 3 + "%");

        int wins = -1;
        int mostWinsId = -1;
        int playerId = 0;
        do {
            if (wins < playerWins[playerId]) {
                mostWinsId = playerId;
            }
            playerId++;
        } while (playerId >= playerWins.Length);

        string winnerName = "";
        if (mostWinsId == 0) {
            winnerName = "Impulsive";
        }
        if (mostWinsId == 1) {
            winnerName = "Cautious";
        }
        if (mostWinsId == 2) {
            winnerName = "Demanding";
        }
        if (mostWinsId == 3) {
            winnerName = "Random";
        }

        Debug.Log(string.Format("Most wins behaviour: {0} with {1} wins", winnerName, playerWins[mostWinsId]));


        string resultString = string.Format("Test over.\nTimeout finishes: {0}" +
            "\nTurns average per game: {1}" +
            "\nImpulsive player wins %: {2}" +
            "\nCautious player wins %: {3}" +
            "\nDemanding player wins %: {4}" +
            "\nRandom player wins %: {5}" +
            "\nMost wins behaviour: {6} with {7} wins",
            _overtimeGames,
            (float)turnsAverage / (300 - _overtimeGames),
            (float)playerWins[0] / 3 + "%",
            (float)playerWins[1] / 3 + "%",
            (float)playerWins[2] / 3 + "%",
            (float)playerWins[3] / 3 + "%",
            winnerName,
            playerWins[mostWinsId]);

        System.IO.File.WriteAllText(string.Format("{0}/LastExecutionResults.txt", Application.dataPath), resultString);
    }

    private void CheckGameResult() {
        if (!_hasTwoOrMorePlayers) {
            turnsAverage += _round;
        } else {
            int winner = -1;
            int winnerCoins = -1;
            int playerId = _startingPlayer;
            do {
                playerId--;
                if (0 > playerId) {
                    playerId = _players.Count - 1;
                }
                if (winnerCoins < _playersInfo[playerId].money) {
                    winner = playerId;
                    winnerCoins = _playersInfo[playerId].money;
                }

            } while (playerId != _startingPlayer);

            playerWins[winner]++;
            n3++;
            _overtimeGames++;
        }
    }

    private void MovePlayer(int index) {
        _playersInfo[index].position += RollD6();
        if (_playersInfo[index].position > 19) {
            _playersInfo[index].position += -20;
            AddMoney(_lapReward, index);
        }
    }

    private void CheckPosition(int position) {
        if (_estates[_playersInfo[index].position].ownerId != -1) {
            if (_estates[_playersInfo[index].position].ownerId != index) {
                PayRent(_playersInfo[index].position, index);
            }
        } else if (_players[index].WillBuyEstate(_estates[_playersInfo[index].position])) {
            BuyEstate(_playersInfo[index].position, index);
        }
    }

    private void BuyEstate(int estateId, int playerId) {

        if (_playersInfo[playerId].money >= _estates[estateId].price) {//buy
            _playersInfo[playerId].money += -_estates[estateId].price;
            _estates[estateId].ownerId = playerId;
        }
    }

    private void PrepareGame(int startMoney) {
        _playersInfo = new PlayerInfo[_players.Count];
        for (int i = 0; i < _players.Count; i++) {
            _playersInfo[i] = new PlayerInfo();
            _playersInfo[i].money = startMoney;
            _playersInfo[i].position = -1;
        }
        foreach (Estate e in _estates) {
            e.ownerId = -1;
        }
        _round = 0;
        _startingPlayer = UnityEngine.Random.Range(0, _players.Count);
    }


    private void PayRent(int estateId, int playerId) {
        TransferMoney(_estates[estateId].rent, playerId, _estates[estateId].ownerId);


    }

    private void AddMoney(int amount, int playerId) {
        _playersInfo[playerId].money += amount;
    }
    private void TransferMoney(int amount, int from, int to) {

        if (amount > _playersInfo[from].money) {//bankrupt
            _playersInfo[to].money += _playersInfo[from].money;
            _playersInfo[from].money = -1;
        } else {//pay
            _playersInfo[to].money += amount;
            _playersInfo[from].money -= amount;
        }
    }

    private int RollD6() {
        return UnityEngine.Random.Range(1, 7);
    }

    public int GetMoney(IPlayer player) {
        return _playersInfo[_players.IndexOf(player)].money;
    }
}
