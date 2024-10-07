using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardEffectScript : MonoBehaviour
{
    private GameObject target;
    private Vector3 rotValues;

    private void Awake()
    {
        target = Camera.main.gameObject;
    }

    void Update()
    {
        transform.LookAt(target.transform.position);
        transform.Rotate(rotValues);
    }
}