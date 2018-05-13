using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junk : MonoBehaviour {

    public ParticleSystem flames;
    
    public List<Sprite> sprites = new List<Sprite>();

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D myBody;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        myBody = GetComponent<Rigidbody2D>();
    }


    private void Start() {
        SetRandomSprite();
        SetRandomVelocity();
        StartFlames();
    }



    private void SetRandomSprite() {
        int randomIndex = Random.Range(0, sprites.Count);
        spriteRenderer.sprite = sprites[randomIndex];
    }

    private void SetRandomVelocity() {
        float force = Random.Range(5f, 10f);
        float rotforce = Random.Range(-360f, 360f);
        Vector2 dir = Random.insideUnitCircle * force;

        myBody.velocity = dir;
        myBody.angularVelocity = rotforce;
    }

    private void StartFlames() {
        int rand = Random.Range(0, 2);
        if(rand == 1) {
            flames.Play();
        }
    }



}
