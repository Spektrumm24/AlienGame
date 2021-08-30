using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] int speed;
    [SerializeField] GameObject bullet;
    [SerializeField] float nextFire;
    private bool toggleShoot = true;

    float minX, maxX, minY, maxY, tamX, tamY, canFire;

    // Start is called before the first frame update
    void Start()
    {
        tamX = (GetComponent<SpriteRenderer>()).bounds.size.x;
        tamY = (GetComponent<SpriteRenderer>()).bounds.size.y;

        Vector2 esquinaSupDer = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        maxX = esquinaSupDer.x - tamX/2;
        maxY = esquinaSupDer.y - tamX/2;

        Vector2 esquinaInfIzq = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        minX = esquinaInfIzq.x + tamX/2;
        minY = esquinaInfIzq.y + 7;

        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Fire();
        ToggleShoot();
    }

    void Movement()
    {
        float movH = Input.GetAxis("Horizontal");
        float movV = Input.GetAxis("Vertical");
        transform.Translate(new Vector2(movH * Time.deltaTime * speed, movV * Time.deltaTime * speed));

        float newX = Mathf.Clamp(transform.position.x, minX, maxX);
        float newY = Mathf.Clamp(transform.position.y, minY, maxY);

        transform.position = new Vector2(newX, newY);
    }
    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= canFire && toggleShoot)
        {
            Instantiate(bullet, transform.position - new Vector3(0, tamY/2, 0), transform.rotation);
            canFire = Time.time + nextFire;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && Time.time >= canFire && !toggleShoot)
        {
            GameObject bullet1 = Instantiate(bullet, transform.position - new Vector3(-0.5f, tamY / 2, 0), transform.rotation);
            GameObject bullet2 = Instantiate(bullet, transform.position - new Vector3(0, tamY / 2, 0), transform.rotation);
            GameObject bullet3 = Instantiate(bullet, transform.position - new Vector3(0.5f, tamY / 2, 0), transform.rotation);
            //add forces
            bullet1.GetComponent<Rigidbody2D>().AddForce(new Vector2(200, 0));
            bullet2.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 0));
            bullet3.GetComponent<Rigidbody2D>().AddForce(new Vector2(-200, 0));
            //change gravity
            bullet1.GetComponent<Rigidbody2D>().gravityScale -= 1.3f;
            bullet2.GetComponent<Rigidbody2D>().gravityScale -= 1.3f;
            bullet3.GetComponent<Rigidbody2D>().gravityScale -= 1.3f;
            canFire = Time.time + nextFire;
        }
    }
    void ToggleShoot()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            toggleShoot = !toggleShoot;
        }
    }
}
