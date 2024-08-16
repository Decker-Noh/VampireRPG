using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType
    {
        Exp,
        Level,
        Time,
        Kill,
        Health
    }
    public InfoType infoType;

    Text myText;
    Slider mySlider;

    private void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (infoType)
        {
            case InfoType.Exp:
                float exp = GameManager.Instance.exp;
                float maxExp = GameManager.Instance.nextExp[GameManager.Instance.level];
                mySlider.value = exp / maxExp;
                break;
            case InfoType.Level:
                myText.text = string.Format("LV. {0:F0}", GameManager.Instance.level);
                break;
            case InfoType.Time:
                float remainTime = GameManager.Instance.maxGameTime - GameManager.Instance.currentGameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:F0}:{1:F0}", min, sec);
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.Instance.kill);
                break;
            case InfoType.Health:
                float health = GameManager.Instance.health;
                float maxHealth = GameManager.Instance.maxHealth;
                mySlider.value = health / maxHealth;
                break;
        }
    }

}
