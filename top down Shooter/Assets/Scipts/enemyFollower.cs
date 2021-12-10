using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class enemyFollower : MonoBehaviour{
    public float speed;
    float frequency = 6f;
    float magnitude = 0.18f;
    
    private Vector3 move;
    private Transform targetPos;
    private Animator anim;

    private void changeFrequence(){
        System.Boolean boolValue = (Random.Range(0, 2) == 0);
        if(boolValue == true){
           frequency = 6f;
           magnitude = 0.18f;
        } else{
            frequency = 3.3f;
            magnitude = 0.041f;
        }
    }


    private void change(){
        changeFrequence();

    }
    private void Start(){
        anim = this.GetComponent<Animator>();
        targetPos = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Update(){
        Vector2 directionMove = new Vector2(transform.position.x - targetPos.position.x, targetPos.position.y - transform.position.y);
        doAnimation(directionMove);
    }

    private void FixedUpdate(){
        move = Vector2.MoveTowards(transform.position, targetPos.position, speed * Time.fixedDeltaTime);
        move += (new Vector3(0, enemySpawnManager.convert(1 * doPositive(transform.position.x - targetPos.position.x), 0f, 10, 0.1f, 1), 0) + new Vector3(enemySpawnManager.convert(1 * doPositive(targetPos.position.y - transform.position.y), 0f, 10, 0.1f, 1),0,0)) * Mathf.Sin(Time.time * frequency) * magnitude;
        change();
        GetComponent<Rigidbody2D>().MovePosition(move);
    }

    private void doAnimation(Vector2 direction){
        anim.SetFloat("Horizontal", direction.x);
        anim.SetFloat("Vertical", direction.y);
        anim.SetFloat("Speed", doPositive(direction.x) + doPositive(direction.y));

        if (direction.x < 0)
            this.GetComponent<SpriteRenderer>().flipX = true;
        else
            this.GetComponent<SpriteRenderer>().flipX = false;

    }

    private float doPositive(float p){
        if (p < 0)
            return p * -1;
        return p;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("bullet")){
            scoreManager.addScore();
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }

}
