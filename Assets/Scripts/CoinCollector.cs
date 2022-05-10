using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollector : MonoBehaviour
{
    private int Coins = 0;
    [SerializeField] private Text CoinsNumber;

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Coin")){
            Destroy(collision.gameObject);
            Coins++;
            CoinsNumber.text = "Coins : "+Coins;
        }
    }
}
