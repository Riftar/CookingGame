using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip sound;
    private AudioSource source;

    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        source = gameObject.GetComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;

        //button.onClick.AddListener(() => playSound());
    }

    public void playSound()
    {
        source.Play();
    }
}
