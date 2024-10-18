
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ColliderManager : UdonSharpBehaviour
{
    public GameObject gameManager;
    public GameObject puck;
    public int player;
    private GameManager _gameManager;
    void Start()
    {
        _gameManager = gameManager.GetComponent<GameManager>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == puck.name && Networking.LocalPlayer.isMaster)
        {
            _gameManager.OnScored(player);
        }
    }
}
