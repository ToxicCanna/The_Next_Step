using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource gameAudio;
    [SerializeField] AudioSource backgroundMusic;

    public AudioClip PlayerShoot;
    public AudioClip Explosion;
    public AudioClip PlayerHit;
    public AudioClip PlayerDie;
    public AudioClip LevelChange;
    public AudioClip background;

    private void Start()
    {
        //background music
        backgroundMusic.clip = background;
        backgroundMusic.Play();

    }

    public void PlaySFX(AudioClip clip)
    {
        gameAudio.PlayOneShot(clip);
        //gameAudio.volume = vol;
    }
}
