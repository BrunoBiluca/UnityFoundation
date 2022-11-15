using UnityEngine;
using UnityEngine.UI;
using UnityFoundation.Code.Timer;

public class CooldownIndicator : MonoBehaviour
{
    private Image icon;
    private Image cooldownImage;
    private Timer timer;

    public void Awake()
    {
        icon = transform.Find("icon").GetComponent<Image>();
        cooldownImage = transform.Find("cooldown_display").GetComponent<Image>();
    }

    public CooldownIndicator Setup(Timer timer)
    {
        this.timer = timer;
        return this;
    }

    public CooldownIndicator Setup(Sprite icon, Timer timer)
    {
        this.icon.sprite = icon;
        this.timer = timer;
        return this;
    }

    private void Update()
    {
        if(timer == null) return;

        if(timer.Completion <= 0f)
        {
            cooldownImage.fillAmount = 0f;
            return;
        }

        cooldownImage.fillAmount = 1f - timer.Completion / 100f;
    }

}
