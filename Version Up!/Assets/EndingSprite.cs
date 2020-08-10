using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSprite : MonoBehaviour
{
    public FadeImage fadeImage;

    public SpriteRenderer currentSprite;
    public Sprite[] sprites;
    int spriteIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (FadeImage.isOuted)
            MoveScene.instance.WarpScene9();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (spriteIndex >= sprites.Length)
            {
                fadeImage.PublicFadeOut();
            }
            currentSprite.sprite = sprites[spriteIndex];
            spriteIndex++;
        }

    }
}
