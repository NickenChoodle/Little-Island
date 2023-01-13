using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotate : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        this.transform.Rotate(0, Random.Range(0, 180f), 0);
    }

}
