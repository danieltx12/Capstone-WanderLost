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
    public float iframes;
    public Material red;
    public Material normal;
    SpriteRenderer spriteRend;
    public int numHearts;
    public Image[] Hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public AudioSource audioSource;
    public AudioClip deathClip;
    bool invuln = false;
    bool cd = false;
    public AudioClip damageClip;
    private void Start()
    {
        Health = MaxHealth;
        if(this.tag == "Player")
        {
            spriteRend = this.GetComponent<SpriteRenderer>();
        }
    }

    public void Damage(int dmg)
    {
        if (!invuln)
        {
            Health -= dmg;
            Debug.Log(Health);
            audioSource.clip = damageClip;
            audioSource.Play();
            if (Health <= 0)
            {
                Kill();
            }

            if (this.tag == "Player")
            {
                invuln = !invuln;
                StartCoroutine("iframe");
                StartCoroutine("flash");
            }
        }
    }

    public void Heal(int dmg)
    {
        Health += dmg;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }

    void Kill()
    {
        if (this.gameObject.tag  == "Player")
        {
            audioSource.clip = deathClip;
            audioSource.Play();
            Health = MaxHealth;
            playerpos.Reload();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Upgrade()
    {
        if (!cd)
        {
            MaxHealth += 1;
            numHearts += 1;
            //Health += 5;
        }
    }

    IEnumerator iframe()
    {
        
        yield return new WaitForSeconds(0.1f);
        Physics2D.IgnoreLayerCollision(8, 10, true);
        yield return new WaitForSeconds(iframes);
        Physics2D.IgnoreLayerCollision(8, 10, false);
        StopCoroutine("flash");
        invuln = !invuln;
        spriteRend.material = normal;

    }

    IEnumerator cooldown()
    {
        cd = true;
        yield return new WaitForSeconds(0.5f);
        cd = false;
    }

    IEnumerator flash()
    {
        while (true)
        {
        spriteRend.material = red;
        Debug.Log("SWAP RED");
        yield return new WaitForSeconds(0.1f);
        Debug.Log("SWAP NORM");
        spriteRend.material = normal;
        yield return new WaitForSeconds(0.1f);
        }
       
        
    }

    public void Update()
    {
        if(this.gameObject.tag == "Player")
        {
            if (Health > numHearts)
            {
                Health = numHearts;
            }
            for (int i = 0; i < Hearts.Length; i++)
            {
               
                if(i < Health)
                {
                    Hearts[i].sprite = fullHeart;
                }
                else
                {
                    Hearts[i].sprite = emptyHeart;
                }
                if(i < numHearts)
                {
                    Hearts[i].enabled = true;
                }
                else
                {
                    Hearts[i].enabled = false;
                }
            }
        }
    }

}
