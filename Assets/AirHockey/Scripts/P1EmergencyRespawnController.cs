
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class P1EmergencyRespawnController : UdonSharpBehaviour
{
    public GameObject _puck;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = _puck.GetComponent<GameManager>();
    }

    public override void Interact()
    {
        _gameManager.p1EmergencyRespawn();
    }
}
