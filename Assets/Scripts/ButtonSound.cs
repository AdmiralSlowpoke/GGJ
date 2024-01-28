using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioClip hoverSound;

    public void PlaySoundOnHover()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(hoverSound);
    }
}
