
using System;
using MMMaellon;
using MMMaellon.LightSync;
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

public class GameManager : UdonSharpBehaviour
{
    public GameObject p1Paddle;
    public GameObject p1Controller;
    public GameObject p1Collider;
    public GameObject p2Collider;
    public GameObject p2Paddle;
    public GameObject p2Controller;
    public GameObject score;
    public GameObject score2;
    public GameObject winner;
    public GameObject puck;
    public GameObject soundPlayer;
    public int maxScore = 7;
    [UdonSynced] private int _p1Score;
    [UdonSynced] private int _p2Score;
    [UdonSynced] private bool _isGameOver;
    [UdonSynced] private string _winnerName;
    [UdonSynced] private int _winnerNumber = 0;
    [UdonSynced] private bool _init;
    private LightSync _lsPuck;
    private LightSync _lsP1Paddle;
    private LightSync _lsP2Paddle;
    private LightSync _lsP1Controller;
    private LightSync _lsP2Controller;
    private Rigidbody _rbPuck;
    private Transform _tfPuck;
    private TextMeshPro _tmpScore;
    private TextMeshPro _tmpScore2;
    private TextMeshPro _tmpWinner;
    private SoundPlayer _soundPlayer;
    private Vector3 _zero = new Vector3(0, 0, 0);
    void Start()
    {
        _lsPuck = puck.GetComponent<LightSync>();
        _lsP1Paddle = p1Paddle.GetComponent<LightSync>();
        _lsP1Controller = p1Controller.GetComponent<LightSync>();
        _lsP2Paddle = p2Paddle.GetComponent<LightSync>();
        _lsP2Controller = p2Controller.GetComponent<LightSync>();
        _rbPuck = puck.GetComponent<Rigidbody>();
        _tfPuck = puck.GetComponent<Transform>();
        _tmpScore = score.GetComponent<TextMeshPro>();
        _tmpScore2 = score2.GetComponent<TextMeshPro>();
        _tmpWinner = winner.GetComponent<TextMeshPro>();
        _soundPlayer = soundPlayer.GetComponent<SoundPlayer>();
        //SendCustomNetworkEvent(NetworkEventTarget.All, "gameInit");
    }
    
    void Update()
    {
        if ((_p1Score >= maxScore || _p2Score >= maxScore) && !_isGameOver)
        {
            SendCustomEventDelayedSeconds("gameOver", 0.25f);
        }
        
        if (_isGameOver)
        {
            _tmpWinner.text = $"{_winnerName} wins!";
            if (_winnerNumber == 1) _tmpWinner.color = Color.red;
            if (_winnerNumber == 2) _tmpWinner.color = Color.blue;
            winner.transform.Rotate(Vector3.up, 90f * Time.deltaTime * 2f);
        }

        if (!_init)
        {
            winner.transform.Rotate(Vector3.up, 90f * Time.deltaTime * 2f);
        }
        _tmpScore.text=$"{_p2Score}:{_p1Score}";
        _tmpScore2.text=$"{_p1Score}:{_p2Score}";
    }

    public void gameOver()
    {
        _isGameOver = true;
        _tmpWinner.enabled = true;
        _tmpScore.color = Color.yellow;
        _tmpScore2.color = Color.yellow;
        if (_p1Score >= maxScore)
        { 
            _winnerNumber = 1;
            _winnerName = Networking.GetOwner(p1Controller).displayName;
        }
        else if (_p2Score >= maxScore)
        {
            _winnerNumber = 2;
            _winnerName = Networking.GetOwner(p2Controller).displayName;
        }
        else
        {
            _winnerNumber = 0;
            _winnerName = "Who?";
        }
        
    }
    
    public void gameInit()
    {
        _soundPlayer.OnInsertingCoin();
        _init = true;
        _p1Score = 0;
        _p2Score = 0;
        _isGameOver = false;
        p1EmergencyRespawn();
        p2EmergencyRespawn();
        puckEmergencyRespawn();
        _tmpWinner.enabled = false;
        _tmpScore.color = Color.white;
        _tmpScore2.color = Color.white;
    }

    public void insertCoin()
    {
        SendCustomNetworkEvent(NetworkEventTarget.All, "gameInit");
    }

    public void p1EmergencyRespawn()
    {
        _lsP1Paddle.Respawn();
        _lsP1Controller.Respawn();
    }
    
    public void p2EmergencyRespawn()
    {
        _lsP2Paddle.Respawn();
        _lsP2Controller.Respawn();
    }

    public void puckEmergencyRespawn()
    {
        _rbPuck.velocity = _zero;
        _lsPuck.Respawn();
    }

    public void OnScored(int player)
    {
        if (!_isGameOver)
        {
            if (player == 1) SendCustomNetworkEvent(NetworkEventTarget.All, "P1Scored");
            else SendCustomNetworkEvent(NetworkEventTarget.All, "P2Scored");
        }
    }

    public void P1Scored()
    {
        _soundPlayer.OnScoring();
        _p2Score++;
        _rbPuck.velocity = _zero;
        _tfPuck.localPosition = new Vector3(-1.5f, 1.71f, 0);
    }

    public void P2Scored()
    {
        _soundPlayer.OnScoring();
        _p1Score++;
        _rbPuck.velocity = _zero;
        _tfPuck.localPosition = new Vector3(1.5f, 1.71f, 0);
    }
}
