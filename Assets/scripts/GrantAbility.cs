using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrantAbility : MonoBehaviour
{
    public enum AbilityType { Grow, Shrink }
    public AbilityType abilityType;
    public GameObject growhint;
    public GameObject shrinkhint;

    void Start()
    {
        growhint.SetActive(false);
        shrinkhint.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {   
        Debug.Log("Triggered");
        if (other.CompareTag("Player"))
        {
            Movement movement = other.GetComponent<Movement>();
            if (movement != null)
            {   
                Debug.Log("Player detected");
                if (abilityType == AbilityType.Grow)
                {
                    movement.canGrow = true;
                    growhint.SetActive(true);
                }
                else if (abilityType == AbilityType.Shrink)
                {
                    movement.canShrink = true;
                    shrinkhint.SetActive(true);
                }
                Destroy(gameObject); // 摧毁道具物品
            }
        }
    }
}
