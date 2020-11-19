using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int health;
    public int enemyScore;
    public Sprite[] sprites;
    public string enemyName;

    public float maxShotDelay;
    private float curShotDelay;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject ItemBoom;
    public GameObject ItemPower;
    public GameObject player;

    public int pattern;
    public int curPatternCount;
    public int[] maxPatternCount;

    void Update()
    {
        if (enemyName == "Boss")
            return;

        Fire();
        Reload();
    }

    void Awake()
    {
        if (enemyName == "Boss")
            Invoke("Stop", 2);
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.down * speed;
    }

    void Stop()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        Invoke("Think", 2);
    }

    void Think()
    {
        pattern = pattern == 5 ? 0 : pattern + 1;
        curPatternCount = 0;

        switch(pattern)
        {
            case 0:
                FireFoward();
                break;
            case 1:
                FireShot();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                FireRand();
                break;
            case 4:
                FireAround();
                break;
            case 5:
                case5();
                break;
        }
    }

    void FireFoward()
    {
        // 한 번에 네 발 일직선으로
        GameObject bullet1 = Instantiate(bulletObjB, transform.position + Vector3.right * 0.3f, transform.rotation);
        GameObject bullet2 = Instantiate(bulletObjB, transform.position + Vector3.right * 0.45f, transform.rotation);
        GameObject bullet3 = Instantiate(bulletObjB, transform.position + Vector3.left * 0.3f, transform.rotation);
        GameObject bullet4 = Instantiate(bulletObjB, transform.position + Vector3.left * 0.45f, transform.rotation);

        Rigidbody2D rigid1 = bullet1.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid2 = bullet2.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid3 = bullet3.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid4 = bullet4.GetComponent<Rigidbody2D>();

        Vector3 dirVec1 = player.transform.position - (transform.position);
        Vector3 dirVec2 = player.transform.position - (transform.position);
        Vector3 dirVec3 = player.transform.position - (transform.position);
        Vector3 dirVec4 = player.transform.position - (transform.position);

        rigid1.AddForce(dirVec1.normalized * 10, ForceMode2D.Impulse);
        rigid2.AddForce(dirVec2.normalized * 10, ForceMode2D.Impulse);
        rigid3.AddForce(dirVec3.normalized * 10, ForceMode2D.Impulse);
        rigid4.AddForce(dirVec4.normalized * 10, ForceMode2D.Impulse);

        curPatternCount++;

        if (curPatternCount < maxPatternCount[pattern])
            Invoke("FireFoward", 1);
        else
            Invoke("Think", 3);
    }
    void FireShot()
    {
        // 5발 동시에 부채꼴 형태
        for(int i=0;i<10;i++)
        {
            GameObject bullet = Instantiate(bulletObjB, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 7, ForceMode2D.Impulse);
        }
        

        curPatternCount++;

        if (curPatternCount < maxPatternCount[pattern])
            Invoke("FireShot", 1);
        else
            Invoke("Think", 3);
    }
    void FireArc()
    {
        Debug.Log("부채꼴 형태로 공격");
        GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(Mathf.Sin(Mathf.PI * 8 * curPatternCount/maxPatternCount[pattern]), -1);
        rigid.AddForce(dirVec.normalized * 4, ForceMode2D.Impulse);

        curPatternCount++;

        if (curPatternCount < maxPatternCount[pattern])
            Invoke("FireArc", 0.03f);
        else
            Invoke("Think", 3);
    }

    void FireRand()
    {
        GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(Mathf.Sin(UnityEngine.Random.Range(0, 1000)), -1);
        rigid.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);

        curPatternCount++;

        if (curPatternCount < maxPatternCount[pattern])
            Invoke("FireRand", 0.015f);
        else
            Invoke("Think", 5);
    }
    void FireAround()
    {
        int numA = 50;
        int numB = 60;
        int numC = curPatternCount%2 == 0 ? numA : numB;
        Debug.Log("원 형태로 전체 공격");
        for(int i=0;i< numC; i++)
        {
            GameObject bullet = Instantiate(bulletObjB, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / numC),
                                         Mathf.Sin(Mathf.PI * 2 * i / numC));
            rigid.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);
        }

        curPatternCount++;

        if (curPatternCount < maxPatternCount[pattern])
            Invoke("FireAround", 0.7f);
        else
            Invoke("Think", 5);
    }

    void case5()
    {
        // 한 번에 네 발 일직선으로
        GameObject bullet1 = Instantiate(bulletObjB, transform.position + Vector3.right * 0.3f, transform.rotation);
        GameObject bullet2 = Instantiate(bulletObjB, transform.position + Vector3.right * 0.45f, transform.rotation);
        GameObject bullet3 = Instantiate(bulletObjB, transform.position + Vector3.left * 0.3f, transform.rotation);
        GameObject bullet4 = Instantiate(bulletObjB, transform.position + Vector3.left * 0.45f, transform.rotation);

        Rigidbody2D rigid1 = bullet1.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid2 = bullet2.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid3 = bullet3.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid4 = bullet4.GetComponent<Rigidbody2D>();

        Vector3 dirVec1 = player.transform.position - (transform.position);
        Vector3 dirVec2 = player.transform.position - (transform.position);
        Vector3 dirVec3 = player.transform.position - (transform.position);
        Vector3 dirVec4 = player.transform.position - (transform.position);

        rigid1.AddForce(dirVec1.normalized * 10, ForceMode2D.Impulse);
        rigid2.AddForce(dirVec2.normalized * 10, ForceMode2D.Impulse);
        rigid3.AddForce(dirVec3.normalized * 10, ForceMode2D.Impulse);
        rigid4.AddForce(dirVec4.normalized * 10, ForceMode2D.Impulse);

        for (int i = 0; i < 10; i++)
        {
            GameObject bullet = Instantiate(bulletObjB, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 7, ForceMode2D.Impulse);
        }

        curPatternCount++;

        if (curPatternCount < maxPatternCount[pattern])
            Invoke("case5", 1);
        else
            Invoke("Think", 3);
    }

    public void OnHit(int dmg)
    {
        if (health <= 0)
            return;

        health -= dmg;
        spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite", 0.1f);

        if (health <= 0)
        {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;

            // Random Ratio Item Drop
            int ran = enemyName == "Boss" ? 0 : UnityEngine.Random.Range(0, 10);
            if(ran < 6)
            {
                Debug.Log("Not Item");
            }
            else if(ran <8)
            { // Boom 20%
                Instantiate(ItemBoom, transform.position, ItemBoom.transform.rotation);
            }
            else if(ran < 10)
            { // Power 20%
                Instantiate(ItemPower, transform.position, ItemPower.transform.rotation);
            }
            Destroy(gameObject);
        }
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border" && enemyName != "Boss")
            Destroy(gameObject);
        else if(collision.gameObject.tag == "Player_Bullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);

            Destroy(collision.gameObject);
        }
    }

    void Fire()
    {
        if (curShotDelay < maxShotDelay)
            return;
        
        if(enemyName == "S")
        {
            GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 4, ForceMode2D.Impulse);
        }
        else if (enemyName == "M")
        {
            GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
        }
        else if(enemyName == "B")
        {
            GameObject bulletR = Instantiate(bulletObjB, transform.position + Vector3.right * 0.3f, transform.rotation);
            GameObject bulletL = Instantiate(bulletObjB, transform.position + Vector3.left * 0.3f, transform.rotation);

            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);

            rigidR.AddForce(dirVecR.normalized * 4, ForceMode2D.Impulse);
            rigidL.AddForce(dirVecL.normalized * 4, ForceMode2D.Impulse);
        }

        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
}
