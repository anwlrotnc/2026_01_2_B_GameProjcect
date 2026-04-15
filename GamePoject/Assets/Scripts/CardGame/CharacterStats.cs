using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CharacterStats : MonoBehaviour
{

    public string characterName;
    public int maxHealth = 100;
    public int currentHealth;

    //UI 요소
    public Slider healthBar;

    //새로 추가되는 마나 변수
    public int maxMana = 10;                //최대 마나
    public int currentMana;                 //현재 마나
    public Slider manaBar;                  //마나 바 UI
    public TextMeshProUGUI manaText;        //마나 텍스트 ui

    public TextMeshProUGUI healthText;

    void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        UpdateUI();
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
    }

    public void UseMana(int amount)
    {
        currentMana -= amount;
        if(currentMana < 0)
        {
            currentMana = 0;
        }
        UpdateUI();
    }

    public void GainMana(int amount)
    {
        currentMana += amount;

        if(currentMana > maxMana)
        {
            currentMana = maxMana;  
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        if(healthBar != null)
        {
            healthBar.value = (float)currentHealth / maxHealth;
        }    

        if(healthText != null)
        {
            healthText.text = $"{currentHealth} / {maxHealth}";
        }

        if(manaBar != null)
        {
            manaBar.value = (float)currentMana / maxMana;
        }

        if(manaText != null)
        {
            manaText.text = $"{currentMana} / {maxMana}";
        }
    }
}
