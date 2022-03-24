using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    public GameController gameController;
    [SerializeField] int MaxHealth;
    private int Health;

    private void Start()
    {
        Health = MaxHealth;
    }
    public void Damage(int dmg)
    {
        Health -= dmg;
        Debug.Log(Health);
        if( Health <=0)
        {
            Kill();
        }
    }

    public void Heal(int dmg)
    {
        Health += dmg;
        if(Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }

    void Kill()
    {
        if (this.gameObject.tag  == "Player")
        {
            gameController.Reload();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
