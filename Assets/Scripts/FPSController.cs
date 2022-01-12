using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FPSController : MonoBehaviour
{
    [Header("Sensibilidade")]
        public float mouseSensitivity = 100f;
        private float xRotation;
        private float yRotation;
        public Transform playerBody;
        public Transform playerCamera;

    [Header("Movimento")]
        public CharacterController controller;
        public float moveSpeed;
        private float gravity = -19.62f;
        Vector3 velocity;

    [Header("Vida")]
        public float maxhealthPoints;
        public float currentHealthPoints;
        public Text healthPointsText;

    [Header("Arma")]
        public Weapons gun;

    [Header("Pontos")]
        public float points;
        public Text pointsText;

    [Header("Inimigo")]
        public EnemyBehavior enemyBehavior;
    
    [Header("Animação")]
        public Animator animator;

    [Header("Cenário de Morte")]
        public int DeathScene;

    [Header("Áudio")]
        public AudioClip stepsSounds;
        public AudioSource audioSource;
        public float stepRate = .5f;
        public float stepCooldown;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        maxhealthPoints = 100f;
        currentHealthPoints = maxhealthPoints;
        healthPointsText.text = currentHealthPoints.ToString();

        points = 0f;
        pointsText.text = points.ToString();
    }

    void Update()
    {         
        if(Input.GetMouseButton(0))
        {
            //Animação
            animator.SetBool("Disparar", true);

            gun.Shoot();
        }
        else //Animação
            animator.SetBool("Disparar", false);
        
        if(Input.GetKeyDown(KeyCode.R))
            gun.Reload();

        #region Rotação

            float Y = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float X = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            //Definir os eixos de rotação
            xRotation -= X;
            yRotation += Y;
            //Limites da câmera
            xRotation = Mathf.Clamp(xRotation, -60f, 60f);         
            //Rotação da câmera
            playerCamera.transform.eulerAngles = new Vector3(xRotation, yRotation);
            //Rotação do jogador
            playerBody.Rotate(Vector3.up * Y);

        #endregion
        
        #region Movimento
        
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            //Correr
            if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
                moveSpeed = 18f;
            else
                moveSpeed = 12f;

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * moveSpeed * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

        #endregion
        //Som de passos
        stepCooldown -= Time.deltaTime;
        if ((Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) && stepCooldown <= 0f)
        {
            audioSource.clip = stepsSounds;
            audioSource.Play();
            stepCooldown = stepRate;
        }

        pointsText.text = points.ToString();
        
        //Atualiza a vida do jogador
        healthPointsText.text = currentHealthPoints.ToString();
        
        if(currentHealthPoints <= 0)
            PlayerDead();       
    }

    void PlayerDead()
    {
        SceneManager.LoadScene(DeathScene);
    }  
}