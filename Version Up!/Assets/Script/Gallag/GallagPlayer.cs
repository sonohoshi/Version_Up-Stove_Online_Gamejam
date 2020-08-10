using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class GallagPlayer : MonoBehaviour
{
    public static GallagPlayer instance;

    public List<GameObject> lifeSprite;

    private AudioSource audioSource;
    private SpriteRenderer sr;
    private Collider2D coll;

    public GameObject explosion;
    public GameObject bulletHolder;
    private List<GameObject> bulletList = new List<GameObject>();

    private bool move = false;
    private Vector2 targetPos;
    private float attackCoolTime = 0;
    public int life = 3;
    private bool isDead = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        for (int i = 0; i < bulletHolder.transform.childCount; i++)
            bulletList.Add(bulletHolder.transform.GetChild(i).gameObject);

        audioSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet") || collision.CompareTag("enemy"))
        {
            life -= 1;
            isDead = true;

            explosion.SetActive(true);
            explosion.GetComponent<Animator>().Play("Explosion");
            explosion.transform.position = transform.position;

            if(lifeSprite.Count>0)
            {
                lifeSprite[0].SetActive(false);
                lifeSprite.RemoveAt(0);
            }

            if (life > 0)
            {
                sr.enabled = false;
                coll.enabled = false;
                transform.position = new Vector2(0, -8);
                Invoke("Retry", 2f);
            }
            else
                gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isDead)
            return;

        if (!move)
        {
            Vector2 dir;
            int random = Random.Range(0, 2);
            if (random == 0)
                dir = Vector2.right;
            else
                dir = Vector2.left;

            int moveAmount = Random.Range(0, 5);

            targetPos = (Vector2)transform.position + dir * moveAmount;

            move = true;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, 0.1f);
            if (((Vector2)transform.position - targetPos).sqrMagnitude < 0.01f)
                move = false;

            if (transform.position.x <= -8)
            {
                transform.position = new Vector2(-8, transform.position.y);
                move = false;
            }
            else if (transform.position.x >= 8)
            {
                transform.position = new Vector2(8, transform.position.y);
                move = false;
            }
        }

        attackCoolTime -= Time.fixedDeltaTime;
        if (attackCoolTime <= 0)
        {
            GetBullet();
            attackCoolTime = Random.Range(0, 2f);
            audioSource.Play();
        }
    }

    private GameObject GetBullet()
    {
        GameObject bullet = bulletList[0];
        bullet.SetActive(true);
        bullet.transform.position = (Vector2)transform.position + new Vector2(0, 0.5f);
        bulletList.RemoveAt(0);
        return bullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.position = Vector3.zero;
        bulletList.Add(bullet);
    }

    private void Retry()
    {
        isDead = false;
        sr.enabled = true;
        coll.enabled = true;
    }
}
