
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SoundTrigger : UdonSharpBehaviour
{
    public GameObject soundPlayer;
    public GameObject puck;
    public GameObject controller;
    public string mode;
    private SoundPlayer _soundPlayer;
    private PlayerController _controller;
    void Start()
    {
        _soundPlayer = soundPlayer.GetComponent<SoundPlayer>();
        if (mode == "paddle") _controller = controller.GetComponent<PlayerController>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == puck.name)
        {
            switch (mode)
            {
                case "wall":
                    _soundPlayer.OnBumpingWithWalls();
                    break;
                case "table":
                    _soundPlayer.OnDroppingPuck();
                    break;
                case "paddle":
                    if (_controller.IsHolding()) _soundPlayer.OnHittingPuckWithPaddle();
                    else _soundPlayer.OnBumpingWithPaddle();
                    break;
            }
        }
    }
}
