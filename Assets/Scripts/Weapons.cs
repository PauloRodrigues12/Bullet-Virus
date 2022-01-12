using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons : MonoBehaviour
{
    public enum ShootState 
    {
        Ready,
        Shooting,
        Reloading
    }

    [Header("Scripts")]
        public FPSController fPSController;

    [Header("Elementos UI")]
        public Text remainingAmmunitionText;

    [Header("Audio")]
        public AudioSource audioSource;
        public AudioClip arReloadSound;
        public AudioClip arSound;      

    [Header("Carregador")]
        public GameObject round;
        public GameObject roundPosition;
        public int ammunition;
        [Range(0.5f, 10)] public float reloadTime;
        private int remainingAmmunition;

    [Header("Estatísticas da arma")]
        [Range(0.25f, 100)] public float fireRate;
        public int roundsPerShot;
        [Range(0.5f, 100)] public float roundSpeed;
        private ShootState shootState = ShootState.Ready;
        private float nextShootTime = 0;
    
    [Header("Animação")]
        public Animator animator;

    void Start() 
    {
        remainingAmmunition = ammunition;
        remainingAmmunitionText.text = "31";
    }

    void Update() 
    {
        switch(shootState) 
        {
            case ShootState.Shooting:
                // Se a arma está pronta para disparar...
                if(Time.time > nextShootTime) 
                    shootState = ShootState.Ready;
                break;
            case ShootState.Reloading:
                // Se a arma acabou de recarregar...
                if(Time.time > nextShootTime) 
                {
                    remainingAmmunition = ammunition;
                    remainingAmmunitionText.text = remainingAmmunition.ToString();
                    shootState = ShootState.Ready;

                    animator.SetTrigger("NotReloading");
                }
                break;
        }
    }

    public void Shoot() 
    {
        //Verificar se a arma está pronta para disparar
        if(shootState == ShootState.Ready) 
        {
            #region Comportamento da bala

                for(int i = 0; i < roundsPerShot; i++) 
                {              
                    //Adiciona o som do disparo
                    audioSource.clip = arSound;
                    audioSource.Play();

                    // Instancia a bala
                    GameObject spawnedRound = Instantiate(
                        round,
                        roundPosition.transform.position,
                        roundPosition.transform.rotation
                    );                
                
                    //Lança a bala para a frente
                    Rigidbody rb = spawnedRound.GetComponent<Rigidbody>();
                    rb.velocity = spawnedRound.transform.forward * roundSpeed;
                }

            #endregion
            
            #region Recarregar

                //Retira uma bala do carregador
                remainingAmmunition--;
                remainingAmmunitionText.text = remainingAmmunition.ToString();

                if(remainingAmmunition > 0) 
                {
                    nextShootTime = Time.time + (1 / fireRate);
                    shootState = ShootState.Shooting;
                } 
                else
                    Reload();                 

            #endregion
        }
    }

    public void Reload()
    {
        // Verifica se a arma está pronta para recarregar
        if(shootState == ShootState.Ready) 
        {   
            //Animação
            animator.SetTrigger("Reload");

            //Adiciona o som de recarregar
            audioSource.clip = arReloadSound;    
            audioSource.Play();

            nextShootTime = Time.time + reloadTime;
            shootState = ShootState.Reloading;
        }
    }
}
