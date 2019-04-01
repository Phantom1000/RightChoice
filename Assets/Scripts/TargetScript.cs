using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace RightChoice
{
    public class TargetScript : MonoBehaviour
    {
        [SerializeField] private float _maxScale = 10f;
        [SerializeField] private float _minScale = 4f;
        [SerializeField] private int _greenChance = 40;
        [SerializeField] private int _yellowChance = 30;
        [SerializeField] private int _magentaChance = 20;
        [SerializeField] private int _redChance = 10;
        [SerializeField] private int _greenPrice = 1;
        [SerializeField] private int _yellowPrice = 2;
        [SerializeField] private int _magentaPrice = 3;
        [SerializeField] private int _redPrice = 4;
        [SerializeField] private GameObject _labelPref;
        private float textOffsetX = -3f;
        private float textOffsetY = -3f;
        private float _speed;
        public Point currentPoint { get; set; }
        private int price;
        private const float damage = -1f;
        
        private void Start()
        {
            _speed = GameScript.Instance.ScaleSpeed;
            int chance = Random.Range(0, 100);
            if (chance < _greenChance) 
            {
                price = _greenPrice;
                GetComponent<SpriteRenderer>().color = Color.green;
            }
            else if (chance < _greenChance + _yellowChance) 
            {
                price = _yellowPrice;
                GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else if (chance < _greenChance + _yellowChance + _magentaChance) 
            {
                price = _magentaPrice;
                GetComponent<SpriteRenderer>().color = Color.magenta;
            }
            else if (chance < _greenChance + _yellowChance + _magentaChance + _redChance) 
            {
                price = _redPrice;
                GetComponent<SpriteRenderer>().color = Color.red;
            }
        }

        private void Update()
        {
            if (transform.localScale.x < _maxScale) transform.localScale += new Vector3(_speed * Time.deltaTime, _speed * Time.deltaTime, 0);
            else GameScript.Instance.GameOver();
        }

        private void OnMouseDown() 
        {
            GameObject t = Instantiate(_labelPref, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity);
            t.GetComponent<TextMesh>().text = "+ " + price;
            transform.localScale += new Vector3(damage, damage, 0);
            if (transform.localScale.x < _minScale) 
            {
                Destroy(gameObject);
                GameScript.Instance.FreePoints.Add(currentPoint);
            }
            GameScript.Instance.AddPoints(price);
        }
    }
}
