using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableBehavior : MonoBehaviour
{
    public List<Sprite> sprites;

    public AudioClip sound;

    SpriteRenderer renderer;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        //Random vegetable
        int spriteIndex = Random.Range(0, 4);
        renderer.sprite = sprites[spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().CollectVegetable();
            //Play sound
            AudioSource.PlayClipAtPoint(sound, transform.position);
            Destroy(gameObject);
        }
    }
}
