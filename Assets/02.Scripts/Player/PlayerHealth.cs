using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    //public Slider healthSlider;
    //public Image damageImage;
    public AudioClip deathClip;
    //public float flashSpeed = 5f;
    //public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    public Text playerHP;


    //Animator anim;
    AudioSource playerAudio;
    PlayerMove playerMove;
    PlayerShooting plyerShooting;
    bool isDead;
    bool damaged;

    private void Awake()
    {
        //anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMove = GetComponent<PlayerMove>();
        plyerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startingHealth;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (damaged)
        //{
        //    damageImage.color = flashColour;
        //}
        //else
        //{
        //    damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        //}
        //damaged = false;
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        //healthSlider.value = currentHealth;

        playerHP.text = "HP : "+ currentHealth.ToString();

        playerAudio.Play();
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;
        plyerShooting.DisableEffects ();
        //anim.SetTrigger("Die");
        playerAudio.clip = deathClip;
        playerAudio.Play();
        playerMove.enabled = false;
        plyerShooting.enabled = false;
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

}
