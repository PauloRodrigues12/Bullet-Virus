using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [Header("Dano das Armas")]
        public float damage;

    void Update() 
    {
        StartCoroutine(DestroyBullet());
    }

    void OnCollisionEnter(Collision other) 
    {
        //Só da dano se a bala acertar no inimigo
        EnemyBehavior target = other.gameObject.GetComponent<EnemyBehavior>();

        if(target != null) 
        {
            target.Hit(damage);  
            Destroy(this.gameObject);   
        }
    }

    IEnumerator DestroyBullet()
    {     
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject); 
    }
}
