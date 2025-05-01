using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerX : MonoBehaviour
{
    private float bottomBound = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < bottomBound && !gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
