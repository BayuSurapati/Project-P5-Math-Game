using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public Vector3 moveDir; //Direction to move
    public float moveSpeed; //speed to move at along moveDir

    private float aliveTime = 8.0f; //time before the object is destroyed

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, aliveTime);
    }

    // Update is called once per frame
    void Update()
    {
        //menggerakan obstacle ini ke beberapa direction
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        //rotate obstacle
        transform.Rotate(Vector3.back * moveDir.x * (moveSpeed * 20) * Time.deltaTime);
    }
}
