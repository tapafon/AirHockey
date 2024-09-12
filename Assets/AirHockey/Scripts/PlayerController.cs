using System;
using MMMaellon.LightSync;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;
using Object = UnityEngine.Object;

public class PlayerController : UdonSharpBehaviour
{
    public GameObject playerPaddle;
    public float speed = 0.001f;
    Quaternion OGrotation;
    Vector3 OGposition;
    Vector3 OLDposition;
    Vector3 NEWposition;
    Vector3 direction;
    Rigidbody rigidbody3d;
    VRCPickup vrcPickup;
    void Start()
    {
        rigidbody3d = playerPaddle.GetComponent<Rigidbody>();
        vrcPickup = this.GetComponent<VRCPickup>();
    }

    void Update()
    {
        playerPaddle.transform.GetPositionAndRotation(out OLDposition, out OGrotation);
        transform.GetPositionAndRotation(out OGposition, out OGrotation);

        NEWposition = new Vector3(OGposition.x, OLDposition.y, OGposition.z);
        
        direction = NEWposition - OLDposition;
        direction.y = 0f;
        direction.Normalize();
        rigidbody3d.AddForceAtPosition(direction*speed, NEWposition, ForceMode.Impulse);
    }

    public bool IsHolding()
    {
        return vrcPickup.IsHeld;
    }
}