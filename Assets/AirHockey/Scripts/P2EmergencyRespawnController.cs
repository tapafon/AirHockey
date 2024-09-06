
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class P2EmergencyRespawnController : UdonSharpBehaviour
{
    public GameObject _puck;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = _puck.GetComponent<GameManager>();
    }

    public override void Interact()
    {
        _gameManager.p2EmergencyRespawn();
    }
}
