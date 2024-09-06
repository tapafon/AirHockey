
using System;
using System.Runtime.CompilerServices;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

public class GameResetButtonController : UdonSharpBehaviour
{
    public GameObject _puck;

    private GameManager _gameManager;
    
    private void Start()
    {
        _gameManager = _puck.GetComponent<GameManager>();
    }

    public override void Interact()
    {
        _gameManager.insertCoin();
    }
}
