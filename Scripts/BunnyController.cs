using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BunnyController : MonoBehaviour {

    private Rigidbody2D myRigidBody;
    private Animator myAnim;
    public float bunnyJumpForce = 500f;
    private float bunnyHurtTime = -1;
    private Collider2D myCollider;
    public Text scoreText;
    private float startTime;
    private int jumpsLeft = 2;
    public AudioSource jumpSfx;
    public AudioSource deathSfx;
    private int level = 1;
    private float score = 0;
    private float oldScore = 0;

    // Use this for initialization
    void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();

        startTime = Time.time;
        oldScore= PlayerPrefs.GetFloat("CurrentScore", 0);
        level = PlayerPrefs.GetInt("CurrentLevel", 1);
    }

    public void Continue()
    {

    }
	
	// Update is called once per frame
	void Update () {
        score = getScore();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }
        if (bunnyHurtTime == -1)
        {
            if ((Input.GetButtonUp("Jump") || Input.GetButtonUp("Fire1")) && jumpsLeft > 0)
            {
                if (myRigidBody.velocity.y < 0)
                {
                    myRigidBody.velocity = Vector2.zero;
                }
                if (jumpsLeft == 1)
                {
                    myRigidBody.AddForce(transform.up * bunnyJumpForce * 0.75f);
                }
                else
                {
                    myRigidBody.AddForce(transform.up * bunnyJumpForce);
                }
                jumpsLeft--;

                jumpSfx.Play();
            }
            myAnim.SetFloat("vVelocity", myRigidBody.velocity.y);
            
            scoreText.text = score.ToString("0.0");
            
        }
        else {       
            if (Time.time > bunnyHurtTime + 2)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        if (score <= 5)
        {
            level = 1;
        }
        else if (score > 5 && score < 10)
        {
            level = 2;
        }
        else if (score > 10 && score < 15)
        {
            level = 3;
        }
        else if (score > 15 && score < 20)
        {
            level = 4;
        }
        else if (score > 20 && score < 25)
        {
            level = 5;
        }
        else if (score > 25 && score < 30)
        {
            level = 6;
        }
        else if (score > 30 && score < 35)
        {
            level = 7;
        }
        else if (score > 35 && score < 40)
        {
            level = 8;
        }
        else if (score > 40 && score < 45)
        {
            level = 9;
        }
        else if (score > 50 && score < 55)
        {
            level = 10;
        }
        else if (score > 60 && score < 65)
        {
            level = 11;
        }
        else if (score > 70 && score < 75)
        {
            level = 12;
        }
        else if (score > 80 && score < 85)
        {
            level = 13;
        }
        else if (score > 90 && score < 95)
        {
            level = 14;
        }
        else if (score > 100 && score < 105)
        {
            level = 16;
        }
        else if (score > 110 && score < 115)
        {
            level = 17;
        }
        else if (score > 120 && score < 125)
        {
            level = 18;
        }
        else if (score > 130 && score < 15)
        {
            level = 19;
        }
        else if (score > 140 && score < 145)
        {
            level = 20;
        }
       





        if (level == 1)
        {
            enableFire(false);
        }
        if (level == 2)
        {
            enableFire(true);
        }

        if (level == 3)
        {
            enableFire(true);
        }

        PlayerPrefs.SetFloat("CurrentScore", score);
        PlayerPrefs.SetInt("CurrentLevel", level);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            foreach (PrefabSpawner spawner in FindObjectsOfType<PrefabSpawner>())
            {
                spawner.enabled = false;
            }

            foreach (MoveLeft moveLefter in FindObjectsOfType<MoveLeft>())
            {
                moveLefter.enabled = false;
            }

            enableFire(false);
            bunnyHurtTime = Time.time;
            myAnim.SetBool("bunnyHurt", true);
            myRigidBody.velocity = Vector2.zero;
            myRigidBody.AddForce(transform.up * bunnyJumpForce);
            myCollider.enabled = false;

            deathSfx.Play();

            float currentBestScore = PlayerPrefs.GetFloat("BestScore", 0);
            float currentScore = getScore();



            if (currentScore > currentBestScore)
            {
                PlayerPrefs.SetFloat("BestScore", currentScore);
            }
            SceneManager.LoadScene("Continue");

        }
        else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            jumpsLeft = 3;
        }
    }

    private float getScore()
    {
        return oldScore + (Time.time - startTime);
    }

    private void enableFire(bool enable)
    {
        foreach (PrefabSpawnerAtes spawner in FindObjectsOfType<PrefabSpawnerAtes>())
        {
            spawner.enabled = enable;
        }

        foreach (MoveAtes moveAtes in FindObjectsOfType<MoveAtes>())
        {
            moveAtes.enabled = enable;
        }
    }
}
