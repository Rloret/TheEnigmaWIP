using UnityEngine;
using System.Collections;

public class DestroyMedicalaid : MonoBehaviour {

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.tag == "MockIA")
            Destroy(gameObject);
    }
}
