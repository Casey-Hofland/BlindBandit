using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float moveMultiplier = .1f;

    [SerializeField]
    private float rotateMultiplier = 5f;

    public void Move(float speed)
    {
        transform.Translate(transform.forward * speed * Time.deltaTime * moveMultiplier);
    }

    public void Rotate(float angle)
    {
        transform.Rotate(Vector3.up, angle * Time.deltaTime * rotateMultiplier);
    }
}
