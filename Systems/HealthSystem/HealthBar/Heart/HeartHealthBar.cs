using Assets.UnityFoundation.Code;
using UnityEngine;
using UnityEngine.UI;

public class HeartHealthBar : MonoBehaviour, IHealthBar
{
    [SerializeField] private GameObject heartTemplate;
    [SerializeField] private Transform hearts;

    private float baseHealth;

    private void Awake()
    {
        if(hearts == null)
        {
            hearts = transform.Find("hearts");
        }
    }

    public void Setup(float baseHealth)
    {
        this.baseHealth = baseHealth;

        TransformUtils.RemoveChildObjects(hearts);
        for(int i = 1; i <= baseHealth; i++)
        {
            var go = Instantiate(heartTemplate, hearts);
            go.name = $"heart_{i}";
        }
    }

    public void SetCurrentHealth(float currentHealth)
    {
        for(float currHeart = baseHealth; currHeart >= 1; currHeart--)
        {
            if(currHeart <= 0) break;

            var heart = hearts.Find($"heart_{currHeart}");

            var amount = Mathf.Clamp01(currHeart - currentHealth);

            heart.Find("mask").GetComponent<Image>().fillAmount = 1 - amount;
        }
        
    }
}
