using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject Door;
    void Start()
    {
        Door.SetActive(true);
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Door.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
