using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArmReslingScript : MonoBehaviour
{
    [SerializeField]
    private Image pudgeHandImage, steveHandImage;
    [SerializeField]
    private Sprite steveCringe, steveWin, steveNormal;
    [SerializeField]
    private Sprite pudgeCringe, pudgeWin;
    [SerializeField]
    private List<Sprite> pudges;
    [SerializeField]
    private TextMeshProUGUI powerScore,time;

    private int strength = 0;
    private float timer = 20f;
    private bool pudgeVictory = false;
    private bool steveVictory = false;
    private void Start()
    {
        StartCoroutine(Timer());
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            strength++;
            powerScore.text = strength.ToString();
            if (strength < 20)
            {
                pudgeHandImage.sprite=pudges[0];
            }
            else if (strength>=20&&strength < 40)
            {
                pudgeHandImage.sprite = pudges[1];
            }
            else if(strength>=40&&strength < 60)
            {
                pudgeHandImage.sprite = pudges[2];
            }
            else
            {
                pudgeHandImage.sprite = pudgeWin;
                steveHandImage.gameObject.SetActive(false);
                StopAllCoroutines();
            }
        }
    }

    IEnumerator Timer()
    {
        time.text = ((int)timer).ToString();
        for(int i=0;i<20;i++)
        {
            if (pudgeVictory) { break; }
            yield return new WaitForSecondsRealtime(1);
            timer -= 1;
            time.text = ((int)timer).ToString();
        }
        if(timer<=0) steveVictory = true;
        pudgeHandImage.gameObject.SetActive(false);
        steveHandImage.sprite = pudgeCringe;
    }
}
