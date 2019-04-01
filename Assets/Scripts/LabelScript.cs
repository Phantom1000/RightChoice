using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelScript : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedTransparency;
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        Color c = GetComponent<MeshRenderer>().material.color;
        c.a -= _speedTransparency * Time.deltaTime;
        Mathf.Clamp(c.a, 0, 1);
        GetComponent<MeshRenderer>().material.color = c;
        if (c.a <= 0) Destroy(gameObject);
    }
}
