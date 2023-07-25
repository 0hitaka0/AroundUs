using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Powerup
{
    public Inventory playerInventory;
    public static Coin instance;
    public int coins; // Add coins variable

    // Start is called before the first frame update
    void Start()
    {
        powerupSignal.Raise();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static Coin GetInstance()
    {
        return instance;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerInventory.coins += 1; // Update coins count in the inventory
            powerupSignal.Raise();
            Destroy(this.gameObject);
        }
    }



    public void RemoveCoins(int amount)
    {
        coins -= amount;
        powerupSignal.Raise();
    }

}
