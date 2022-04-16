using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public GameController gameController;
    public PlayerPos playerpos;
    [SerializeField] int MaxHealth;
    private int Health;
    public Text HPDisplay;


    private void Start()
    {
        Health = MaxHealth;
        if(this.tag == "Player")
        {
            HPDisplay.text = "HP: " + Health + "/" + MaxHealth;
        }
    }
    public void Damage(int dmg)
    {
        Health -= dmg;
        if (HPDisplay != null)
        {
            HPDisplay.text = "HP: " + Health + "/" + MaxHealth;
        }
        Debug.Log(Health);
        if( Health <=0)
        {
            Kill();
        }
    }

    public void Heal(int dmg)
    {
        Health += dmg;
        HPDisplay.text = "HP: " + Health + "/" + MaxHealth;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
            HPDisplay.text = "HP: " + Health + "/" + MaxHealth;
        }
    }

    void Kill()
    {
        if (this.gameObject.tag  == "Player")
        {
            Health = MaxHealth;
            playerpos.Reload();
            HPDisplay.text = "HP: " + Health + "/" + MaxHealth;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Upgrade()
    {
        MaxHealth += 5;
        //Health += 5;
        HPDisplay.text = "HP: " + Health + "/" + MaxHealth;
    }
}
