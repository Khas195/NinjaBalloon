using UnityEngine;
using System.Collections;

public class BalloonMovement : MonoBehaviour {

    [SerializeField]
    float floatingSpeed;

    void Update()
    {
        Vector3 temp = new Vector3(0, floatingSpeed * Time.deltaTime, 0);
        this.transform.position += temp;
    }
}
