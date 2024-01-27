using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DamageState
{
    public Sprite spriteToUse;
    public float targetHealthPercentage;
    public GameObject childToEnable;
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
            GameManager.Instance.AddScore((1 / maxHealth) * score); // Add to the score based on the percentage of damage done
        }
        else if(TryGetComponent(out DamageableProp prop)) // If the prop is being damaged by another prop, 
        {
            // Take damage equal to the damager's weight divided by this prop's durability.
            // Clamp it between 0 and currentHealth so we do not go below 0 and add more score than intended
            float damageTaken = Mathf.Clamp(prop.weight / durability, 0, currentHealth);
            currentHealth -= damageTaken;
            GameManager.Instance.AddScore(damageTaken / maxHealth * score);

            // Debug version
            //currentHealth--;
        }

        spriteRenderer.sprite = ChangeSprite();
    }

    Sprite ChangeSprite()
    {
        float healthPercent = currentHealth / maxHealth;

        foreach (DamageState damageState in damageStates)
        {
            if (healthPercent <= damageState.targetHealthPercentage)
            {
                if (damageState.childToEnable && !damageState.childToEnable.activeInHierarchy) // If there is a child that the prop needs to enable, enable it
                {
                    damageState.childToEnable.SetActive(true);
                }

                return damageState.spriteToUse;
            }
        }

        return spriteRenderer.sprite;
    }

    
}
