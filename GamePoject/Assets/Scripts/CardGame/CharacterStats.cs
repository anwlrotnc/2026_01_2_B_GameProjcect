using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CharacterStats : MonoBehaviour
{

    public string characterName;
    public int maxHealth = 100;
    public int currentHealth;

    //UI ¿ä¼Ò
    public Slider healthBar;
    public TextMeshProUGUI healthText;

    void Start()
    {
        
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
}
