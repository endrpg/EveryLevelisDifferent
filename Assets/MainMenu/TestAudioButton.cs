using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudioButton : MonoBehaviour
{
    public AudioSource src;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playAlert()
    {
        src.Play();
    }
}
