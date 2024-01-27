using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DamageState
{
    public Sprite spriteToUse;
    public float targetHealthPercentage;
    public bool thresholdReached;
}

public class DamageableProp : MonoBehaviour
{
    public int score; // Score to earn for destroying prop

    [Header("Damage System")]
    public int weight, durability;
    [SerializeField] float maxHealth;
    float currentHealth;

    [Tooltip("Ensure that damageStates are arranged in ascending order of targetHealthPercentage")]
    public DamageState[] damageStates; 

    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug function
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    DamageProp(this.gameObject);
        //}
    }

    public void DamageProp(GameObject damager) // This will be called by the player whenever it attacks and the prop is in range.
    {
        if (currentHealth <= 0) return; // Dont run the code if we damage a destroyed object
        if (damager.tag == "Player") // If the player was the one causing the damage to this object, simply subtract from the health of the object
        {
            currentHealth--;
        }

        currentHealth--;

        spriteRenderer.sprite = ChangeSprite();
        Debug.Log(ChangeSprite());

        
        
    }

    Sprite ChangeSprite()
    {
        float healthPercent = currentHealth / maxHealth;
        Sprite spriteToUse;

        foreach (DamageState damageState in damageStates)
        {
            if (healthPercent <= damageState.targetHealthPercentage)
            {
                return damageState.spriteToUse;
            }
        }

        return spriteRenderer.sprite;
    }

    
}
