using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBulletscript : MonoBehaviour
{
    public Transform target;
    public float Speed;
    public float Damage;
    private PlayerController Player;
    bool DamageSecurity;

    // Update is called once per frame
    void Update()
    {
        float step = Speed * Time.deltaTime * 2;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && !DamageSecurity)
        {
            Player = other.gameObject.GetComponent<PlayerController>();
            DamageSecurity = true;
            Player.Damage += Damage;
        }
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BulletTrigger")
        {
            Destroy(this.gameObject, 0.5f);
        }
    }
}
