using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController : MonoBehaviour
{
     // VARIABLES
     private Rigidbody rb;  // Rigidbody del jugador     
     private float movementX;  // Movimiento en los ejes X e Y
     private float movementY;  
     public float speed = 10;  // Velocidad a la que se mueve el jugador  
     public TextMeshProUGUI puntuacionText;  // Texto que muestra la puntuacion   
     private int puntuacion;  // Variable local de puntuacion     
     public TextMeshProUGUI vidasText;  // Texto que muestra las vidas  
     public int vidas = 3;  // Variable local para las vidas          
     public GameObject winTextObject;  // Texto mostrado al completar el objetivo     
     public GameObject loseTextObject;  // Texto mostrado al fallar el objetivo 
     public Animator animator;

     // Referenciar GameObject de Puerta
     //public GameObject puerta;


     // Start es llamado antes del primer frame
     void Start()
     {
          // Almacena el componente Rigidbody del player
          rb = GetComponent<Rigidbody>();

          // Almacena el componente animator del player
          //animator = GetComponent<Animator>();  

          puntuacion = 0;
          vidas = 3;

          SetPuntuacionText();
          SetVidasText();

          winTextObject.SetActive(false);  // Esconde el texto que muestra al ganar
          loseTextObject.SetActive(false);  // Esconde el texto que muestra al perder

          //puerta.GetComponent<Puerta>();

     }

     void Update() 
     {
          if (animator != null)
          {
               if (Input.GetKey(KeyCode.E))
               {
                    animator.SetTrigger("TrOpen");
                    Debug.Log("E PRESIONADO");
               }
               if (Input.GetKey(KeyCode.Q))
               {
                    animator.SetTrigger("TrClose");
                    Debug.Log("Q PRESIONADO");
               }
          }
          
     }

     // Se llama a FixedUpdate una vez por frame fijado
     private void FixedUpdate() 
     {
          // Crea vector de movimiento 3D usando los inputs de X e Y
          Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

          // Aplica fuerza al Rigidbody para mover al jugador (duplicado)
          rb.AddForce((movement * speed)*2); 
     }

     // Se llama a OnMove cuando se detecta un input de movimiento
     void OnMove(InputValue movementValue)
     {
          // Conviete el valor del input en un 'Vector2' para el movimiento
          Vector2 movementVector = movementValue.Get<Vector2>();

          // Almacena los componentes X e Y del movimiento
          movementX = movementVector.x; 
          movementY = movementVector.y; 
     }
 
     // En trigger, colision con 'other'.
     void OnTriggerEnter(Collider other) 
     {
          // Se asugura que el objecto con el que ha colisionado el player tiene el tag "PickUp"
          if (other.gameObject.CompareTag("PickUp")) 
          {
               // Desactiva el objeto con el que ha colisionado (haciendolo desaparecer).
               other.gameObject.SetActive(false);

               puntuacion = puntuacion + 1; // Suma uno a la puntuacion

               SetPuntuacionText(); // Actualiza el texto que muestra la puntuacion
          }
          // Si es enemigo se le resta una vida
          if (other.gameObject.CompareTag("Enemy"))
          {
               // Desactiva el objeto con el que ha colisionado (haciendolo desaparecer).
               other.gameObject.SetActive(false);

               vidas = vidas - 1; // Quitar una vida
               
               if (vidas>=0) // Solo actualiza el texto si tiene 0 o mas vidas (evitarmos vidas negativas)
               {
                    SetVidasText();
               }
          }

     }

     // Metodo que maneja el contenido del texto de puntuacion
     void SetPuntuacionText() 
     {
          puntuacionText.text =  "Puntuacion: " + puntuacion.ToString();

          if (puntuacion >= 10)
          {
               winTextObject.SetActive(true); // Muestra el texto de ganar
          }
     }

     void SetVidasText()
     {
          vidasText.text = "Vidas: " + vidas.ToString();

          if (vidas == 0)
          {
               loseTextObject.SetActive(true);

               Application.Quit(); // Cierra el codigo (una vez construido el proyecto)
               UnityEditor.EditorApplication.isPlaying = false; // Cierra el modo Jugar en el Unity Editor
          }
     }
}