using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] int speed;
    [SerializeField] bool direction;
    float maxX, minX;

    //health related stuff
    public int health;
    [SerializeField] int numOfHearths;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // Start is called before the first frame update
    void Start()
    {

        Vector2 esquinaInfDer = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        maxX = esquinaInfDer.x;

        Vector2 esquinaInfIzq = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        minX = esquinaInfIzq.x;
    }

    // Update is called once per frame
    void Update()
    {
        ManageHeartDisplay();
        ManageMovement();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            if(--health <= 0) Destroy(this.gameObject);
        }
    }
    void ManageMovement()
    {
        if (direction)
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));
        else
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0));

        if (transform.position.x >= maxX)
            direction = false;
        else if (transform.position.x <= minX)
            direction = true;
    }

    void ManageHeartDisplay()
    {
        if (health > numOfHearths)
        {
            health = numOfHearths;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearths)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
