using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{

    [SerializeField] private int coinsTotal;
    [SerializeField] private Text coinTxt;
    [SerializeField] private Image healthBar;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddCoin()
    {
        coinsTotal++;
        coinTxt.text = coinsTotal.ToString();
    }

    public void LoseLife(float health)
    {
        healthBar.fillAmount = health / 5; 
    }
}
