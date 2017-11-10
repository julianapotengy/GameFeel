using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    private float counter = 0;
	
	void Update ()
    {
        counter += Time.deltaTime;
        if (counter >= 1)
        {
            Destroy(gameObject);
        }
	}
}
