using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerSound : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<AudioClip> audioClips;

    public void PlayAudio()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(audioClips[Random.Range(0, audioClips.Count)]);
    }
}
