using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    [SerializeField] float speed;
    public GameObject gameManager;
    int price;
    const float damage = -1f;
    void Start()
    {
        int chance = Random.Range(0, 100);
        if (chance < 40) 
        {
            price = 1;
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (chance < 70) 
        {
            price = 2;
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else if (chance < 90) 
        {
            price = 3;
            GetComponent<SpriteRenderer>().color = Color.magenta;
        }
        else if (chance < 100) 
        {
            price = 4;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    void Update()
    {
        if (transform.localScale.x < 10) transform.localScale += new Vector3(speed * Time.deltaTime, speed * Time.deltaTime, 0);
        else gameManager.GetComponent<GameScript>().GameOver();
    }

    void OnMouseDown() 
    {
        transform.localScale += new Vector3(damage, damage, 0);
        if (transform.localScale.x < 4) Destroy(gameObject);
        gameManager.GetComponent<GameScript>().AddPoints(price);
    }
}
