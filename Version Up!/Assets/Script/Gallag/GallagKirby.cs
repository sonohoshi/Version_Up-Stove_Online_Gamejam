using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GallagKirby : MonoBehaviour
{
    AudioSource hitSound;
    public FadeImage fadeImage;

    public GameObject key_prefab;
    public GameObject spawnedKey;

    public GameObject portal;

    public float moveSpeed;
    private float h, v;

    void Start()
    {
        hitSound = GetComponent<AudioSource>();
        spawnedKey = Instantiate(key_prefab, new Vector2(0, 0), Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("KeyItem"))
        {
            portal.SetActive(true);
        }

        else if (collision.CompareTag("Portal"))
        {
            portal.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<Collider2D>().enabled = false;
            fadeImage.PublicFadeOut();
        }

        else if (collision.CompareTag("enemy") || collision.CompareTag("EnemyBullet"))
        {
            hitSound.Play();
            transform.position = new Vector2(-7, -8);
            portal.SetActive(false);
            if (spawnedKey == null)
                spawnedKey = Instantiate(key_prefab, new Vector2(0,0), Quaternion.identity);
            else
            {
                Destroy(spawnedKey);
                spawnedKey = Instantiate(key_prefab, new Vector2(0, 0), Quaternion.identity);
            }
        }
    }

    private void FixedUpdate()
    {
        if (FadeImage.isOuted)
            SceneManager.LoadScene("PackMan");
            
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (transform.position.x <= -8)
            transform.position = new Vector2(-8, transform.position.y);
        else if (transform.position.x >= 8)
            transform.position = new Vector2(8, transform.position.y);
        else if (transform.position.y < -9)
            transform.position = new Vector2(transform.position.x, -9);

        transform.Translate(new Vector2(h, v) * moveSpeed * Time.deltaTime);
        
        Vector3 worldPos = Camera.main.WorldToViewportPoint(this.transform.position);
        if (worldPos.x < 0f) worldPos.x = 0f;
        if (worldPos.y < 0f) worldPos.y = 0f;
        if (worldPos.x > 1f) worldPos.x = 1f;
        if (worldPos.y > 1f) worldPos.y = 1f;
        this.transform.position = Camera.main.ViewportToWorldPoint(worldPos);
    }
}
