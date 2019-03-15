using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float maxScale = 10f;
    [SerializeField] float minScale = 4f;
    [SerializeField] int greenChance = 40;
    [SerializeField] int yellowChance = 30;
    [SerializeField] int magentaChance = 20;
    [SerializeField] int redChance = 10;
    [SerializeField] int greenPrice = 1;
    [SerializeField] int yellowPrice = 2;
    [SerializeField] int magentaPrice = 3;
    [SerializeField] int redPrice = 4;
    public int Row { get; set; }
    public int Column { get; set; }
    int price;
    const float damage = -1f;
    void Start()
    {
        int chance = Random.Range(0, 100);
        if (chance < greenChance) 
        {
            price = greenPrice;
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (chance < greenChance + yellowChance) 
        {
            price = yellowPrice;
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else if (chance < greenChance + yellowChance + magentaChance) 
        {
            price = magentaPrice;
            GetComponent<SpriteRenderer>().color = Color.magenta;
        }
        else if (chance < greenChance + yellowChance + magentaChance + redChance) 
        {
            price = redPrice;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    void Update()
    {
        if (transform.localScale.x < maxScale) transform.localScale += new Vector3(speed * Time.deltaTime, speed * Time.deltaTime, 0);
        else GameScript.Instance.GameOver();
    }

    void OnMouseDown() 
    {
        transform.localScale += new Vector3(damage, damage, 0);
        if (transform.localScale.x < minScale) 
        {
            Destroy(gameObject);
            GameScript.Instance.Positions[Row][Column] = false;
        }
        GameScript.Instance.AddPoints(price);
    }
}
