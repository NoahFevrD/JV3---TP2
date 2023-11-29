using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempFloatyText : MonoBehaviour
{
    // -------------------------
    // Variables
    // -------------------------

    [SerializeField] float upSpeed;
    [SerializeField] float duration;

    private void Start() {
        Invoke("Destroy", duration);
    }

    private void Update() {
        transform.Translate(Vector3.up * upSpeed * Time.deltaTime);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
