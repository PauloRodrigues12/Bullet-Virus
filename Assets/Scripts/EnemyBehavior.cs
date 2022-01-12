using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Jogador e AI")]
        public NavMeshAgent Enemy;
        public GameObject target;

    [Header("Scripts")]
        public FPSController fpsController;
        public Spawner spawner;
    
    [Header("Estatísticas do zombie")]
        public float attackDamage;
        public float healthPointsEnemy; 
    
    [Header("Aúdio")]
        public AudioClip dmgSound;
        public AudioSource audioSource;


    void Start() 
    {
        target =  GameObject.FindGameObjectWithTag("Player"); 
        fpsController = GameObject.FindGameObjectWithTag("Player").GetComponent<FPSController>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
        audioSource = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
    }

    void Update()
    {      
        #region Movimento do Inimigo

            //Seguir e olhar para o player
            Vector3 lookAt = target.transform.position;
            lookAt.y = transform.position.y;
            transform.LookAt(lookAt);
            Enemy.SetDestination(target.transform.position);

        #endregion

        if(healthPointsEnemy <= 0)
        {   
            //Atualiza os pontos do jogador
            fpsController.points += 10;

            spawner.enemyCount --;

            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            audioSource.clip = dmgSound;
            audioSource.Play();

            fpsController.currentHealthPoints -= attackDamage;
        }
    }
    
    public void Hit(float damage)
    {     
        healthPointsEnemy -= damage;
    }
}
