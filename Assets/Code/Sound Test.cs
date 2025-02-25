using UnityEngine;

public class SoundTest : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;


    public AudioClip Background;
    public AudioClip Coin;
    public AudioClip Lava;
    public AudioClip Water;
    public AudioClip Death;
    public AudioClip Topo;
    public AudioClip Cane;
    public AudioClip Gatto;
    public AudioClip Toro;

    private void Start()
    {

        musicSource.clip = Background;
        musicSource.Play();

    }

}
