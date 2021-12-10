using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float attackTime;
    public float startTimeAttack;

    public Transform attackLocation;
    public float attackRange;
    public LayerMask enemies;

    public GameObject slash1, slash2;

    public float speed;

    private Vector2 move;
    private Vector3 mousePos;

    private float angle;

    public Transform gun;
    public Transform shootPoint;
    private Animator anim;

    public GameObject bullet;

    public float shootSpeed;
    public float bulletSpeed;

    private bool canShoot = true;

    private void Awake(){
        canShoot = true;
    }

    private void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    Vector2 lastDirection;
    private void Update()
    {
        mousePos = Input.mousePosition;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;

        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;


        if(!pauseMenu.inPause)
            doAnimation(mousePos - this.transform.position);

        if (Input.GetMouseButton(0) && canShoot)
            StartCoroutine(shootGun());

        if (attackTime <= 0){
            if (Input.GetMouseButton(1)){
                Collider2D[] damage = Physics2D.OverlapCircleAll(attackLocation.position, attackRange, enemies);
                slash1.GetComponent<Animator>().Play("slash");
                slash2.GetComponent<Animator>().Play("slash");

                for (int i = 0; i < damage.Length; i++){
                    scoreManager.addScore();
                    Destroy(damage[i].gameObject);
                }
                attackTime = startTimeAttack;
            }
        }
        else{
            attackTime -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackLocation.position, attackRange);
    }
    private void doAnimation(Vector2 direction){

        anim.SetFloat("Horizontal", direction.x);
        anim.SetFloat("Vertical", direction.y);
        anim.SetFloat("Speed", doPositive(direction.x)+doPositive(direction.y));

        string name = this.GetComponent<SpriteRenderer>().sprite.name;
        if (name == "2_north_0" || name == "2_north_1" || name == "2_north_2" || name == "2_north_3"){
            GameObject gun = GameObject.FindGameObjectWithTag("gun");
            gun.transform.localPosition = new Vector2(2.77f, -1.75f);
        }
        else if (direction.x < 0){
            this.GetComponent<SpriteRenderer>().flipX = true;
            GameObject gun = GameObject.FindGameObjectWithTag("gun");
            gun.GetComponent<SpriteRenderer>().flipX = true;
            if(name == "2_side_0" || name == "2_side_1" || name == "2_side_2" || name == "2_side_3")
                gun.transform.localPosition = new Vector2(-2f, -2.49f);
        }
        else{
            this.GetComponent<SpriteRenderer>().flipX = false;
            GameObject gun = GameObject.FindGameObjectWithTag("gun");
            gun.GetComponent<SpriteRenderer>().flipX = false;
            gun.transform.localPosition = new Vector2(2.74f, -2.45f);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("enemy")){
            Destroy(collision.gameObject);
            FindObjectOfType<heartManager>().hited();
        }
        if (collision.gameObject.CompareTag("heart")){
            if(FindObjectOfType<heartManager>().powerUp())
                Destroy(collision.gameObject);
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction;
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
            direction.x = Input.GetAxisRaw("Horizontal");
            direction.y = Input.GetAxisRaw("Vertical");

            lastDirection = direction;
        }
        else{
            direction = lastDirection;
        }

        move = direction * speed;

        gun.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.Translate(move * Time.fixedDeltaTime);
    }

    public IEnumerator shootGun()
    {
        canShoot = false;
        GameObject bulletCreated = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        Rigidbody2D bulletRigidbody = bulletCreated.GetComponent<Rigidbody2D>();

        bulletRigidbody.AddForce(shootPoint.right * bulletSpeed);

        Destroy(bulletCreated, 5);
        yield return new WaitForSeconds(shootSpeed);
        canShoot = true;
    }

    private float doPositive(float p){
        if (p < 0)
            return p * -1;
        return p;
    }

}       
