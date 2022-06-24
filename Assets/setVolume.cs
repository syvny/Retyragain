using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setVolume : MonoBehaviour
{

    public Slider volumeSlider;
    public AudioListener listener;
    // Start is called before the first frame update

    void Start()
    {
        listener = GameObject.FindObjectOfType<AudioListener>();
    }

    // Update is called once per frame
    void Update()
    {
        float newvolume = volumeSlider.value;
        AudioListener.volume = newvolume;
    }
    
}
