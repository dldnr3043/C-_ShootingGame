using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public int power;
    public int maxpower;
    public int boom;
    public int maxboom;

    public float maxShotDelay;
    private float curShotDelay;

    public int life;
    public int score;

    private bool isTouchTop;
    private bool isTouchBottom;
    private bool isTouchRight;
    private bool isTouchLeft;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject BoomEffect;

    public GameManager manager;
    public bool isHit;
    public bool isBoomTime;

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        Boom();
        Reload();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1)) h = 0;

        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1)) v = 0;

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;
    }

    void Boom()
    {
        if (!Input.GetKey(KeyCode.Z))
            return;
        if (isBoomTime)
            return;

        if (boom == 0)
            return;
        boom--;
        isBoomTime = true;
        manager.UpdateBoomIcon(boom);


        // Effect visible
        BoomEffect.SetActive(true);
        Invoke("OffBoomEffect", 2f);

        // Remove Enemy
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemyLogic = enemies[i].GetComponent<Enemy>();
            enemyLogic.OnHit(1000);
        }

        // Remove Enemy Bullet
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Enemy_Bullet");
        for (int i = 0; i < bullets.Length; i++)
        {
            Destroy(bullets[i]);
        }
    }

    void Fire()
    {
        if (!Input.GetKey(KeyCode.X))
            return;

        if (curShotDelay < maxShotDelay)
            return;

        switch(power)
        {
            case 1: // Power One
                GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2: // Power Two
                GameObject bulletR = Instantiate(bulletObjA, transform.position+Vector3.right*0.1f, transform.rotation);
                GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.1f, transform.rotation);
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject bulletRR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.25f, transform.rotation);
                GameObject bulletCC = Instantiate(bulletObjB, transform.position, transform.rotation);
                GameObject bulletLL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.25f, transform.rotation);
                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
                rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 4:
                GameObject bulletRRR4 = Instantiate(bulletObjA, transform.position + Vector3.right * 0.5f, transform.rotation);
                GameObject bulletRR4 = Instantiate(bulletObjA, transform.position + Vector3.right * 0.25f, transform.rotation);
                GameObject bulletCC4 = Instantiate(bulletObjB, transform.position, transform.rotation);
                GameObject bulletLL4 = Instantiate(bulletObjA, transform.position + Vector3.left * 0.25f, transform.rotation);
                GameObject bulletLLL4 = Instantiate(bulletObjA, transform.position + Vector3.left * 0.5f, transform.rotation);
                Rigidbody2D rigidRRR4 = bulletRRR4.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidRR4 = bulletRR4.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCC4 = bulletCC4.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLL4 = bulletLL4.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLLL4 = bulletLLL4.GetComponent<Rigidbody2D>();
                rigidRRR4.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidRR4.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidCC4.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidLL4.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidLLL4.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 5:
                GameObject bulletR5 = Instantiate(bulletObjA, transform.position + Vector3.right * 0.25f, transform.rotation);
                GameObject bulletC5 = Instantiate(bulletObjB, transform.position + Vector3.up * 0.4f, transform.rotation);
                GameObject bulletC25 = Instantiate(bulletObjB, transform.position, transform.rotation);
                GameObject bulletL5 = Instantiate(bulletObjA, transform.position + Vector3.left * 0.25f, transform.rotation);
                Rigidbody2D rigidR5 = bulletR5.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidC5 = bulletC5.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidC25 = bulletC25.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL5 = bulletL5.GetComponent<Rigidbody2D>();
                rigidR5.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidC5.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidC25.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidL5.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
        }

        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border")
        {
            switch(collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
            }
        }
        else if (collision.gameObject.tag == "Enemy_Bullet" || collision.gameObject.tag == "Enemy")
        {
            if (isHit)
                return;

            isHit = true;
            life--;
            manager.UpdateLifeIcon(life);

            if(life == 0)
            {
                manager.GameOver();
            }
            else
            {
                manager.RespawnPlayer();
            }
            
            gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag == "Item")
        {
            Destroy(collision.gameObject);
            Item item = collision.gameObject.GetComponent<Item>();
            switch(item.type)
            {
                case "Boom":
                    if (boom == maxboom)
                        score += 500;
                    else
                    {
                        boom++;
                        manager.UpdateBoomIcon(boom);
                    }
                    break;
                case "Power":
                    if (power == maxpower)
                        score += 500;
                    else
                        power++;
                    break;
            }
        }
    }

    void OffBoomEffect()
    {
        BoomEffect.SetActive(false);
        isBoomTime = false;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
            }
        }
    }
}
