
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using Random = UnityEngine.Random;

public class SoundPlayer : UdonSharpBehaviour
{
    private AudioSource audio;
    public AudioClip coin;
    public AudioClip drop;
    public AudioClip[] paddleHit;
    public AudioClip[] wallBump;
    public AudioClip[] paddleBump;
    public AudioClip[] score;
    void Start()
    {
        audio = this.GetComponent<AudioSource>();
    }

    public void OnInsertingCoin()
    {
        audio.PlayOneShot(coin);
    }

    public void OnDroppingPuck()
    {
        audio.PlayOneShot(drop);
    }

    public void OnHittingPuckWithPaddle()
    {
        int i = Random.Range(0, paddleHit.Length - 1);
        audio.PlayOneShot(paddleHit[i]);
    }

    public void OnBumpingWithWalls()
    {
        int i = Random.Range(0, wallBump.Length - 1);
        audio.PlayOneShot(wallBump[i]);
    }

    public void OnBumpingWithPaddle()
    {
        int i = Random.Range(0, paddleBump.Length - 1);
        audio.PlayOneShot(paddleBump[i]);
    }

    public void OnScoring()
    {
        int i = Random.Range(0, score.Length - 1);
        audio.PlayOneShot(score[i]);
    }
}
