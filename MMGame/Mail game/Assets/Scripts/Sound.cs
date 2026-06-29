using UnityEngine;

[System.Serializable]
public class Sound
{
    //the name of the sound file
    public string name;
    //The audio file attached
    public AudioClip audioClip;
    [Range(0f,1f)]
    public float volume = 1f;
    [Range(0.1f,1f)]
    public float pitch = 1f;
    
    public bool loop = false;


    [HideInInspector]

    public AudioSource audioSource;

}