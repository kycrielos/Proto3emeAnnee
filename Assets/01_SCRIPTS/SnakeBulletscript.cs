using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBulletscript : MonoBehaviour
{
    public Transform target;
    public float Speed;
    public float Damage;
    private PlayerController Player;

    // Update is called once per frame
    void Update()
    {
        float step = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player = other.gameObject.GetComponent<PlayerController>();
            Player.Damage += Damage;
            Player.Damaged();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
