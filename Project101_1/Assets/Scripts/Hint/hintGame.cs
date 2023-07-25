using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintGame : MonoBehaviour
{
    public int cost = 1; // The cost of the hint in coins
    public GameObject hintObject; // The game object to display as the hint

    private bool isShown = false; // Tracks whether the hint has been shown

    public void ShowHint()
    {
        if (isShown)
        {
            return; // Hint has already been shown
        }

        Coin coin = GameObject.FindObjectOfType<Coin>();
        if (coin.coins < cost)
        {
            return; // Player doesn't have enough coins to pay for hint
        }

        coin.RemoveCoins(cost); // Deduct cost of hint from player's coins

        hintObject.SetActive(true); // Show the hint
        isShown = true; // Mark the hint as shown
    }
}
