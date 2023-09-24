using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb2d;
    private Animator anime;
    [SerializeField]
    private float rayLength;
    [SerializeField]
    private float rayPositionOffset;
    Vector3 RayPositionCenter;
    [SerializeField]
    private LayerMask Ground;
    private bool canJump;
   [SerializeField] float moveSpeed = 3f;
   [SerializeField] float jumpSpeed = 3f;

   float keyHorizontal;
   bool keyJump;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anime=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        keyHorizontal = Input.GetAxisRaw("Horizontal");
        keyJump = Input.GetKeyDown(KeyCode.Space);
        rb2d.velocity = new Vector2(keyHorizontal * moveSpeed, rb2d.velocity.y);
        if(keyJump && canJump)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
            canJump= false;
        }
    }

    void FixedUpdate() {
       Reverse();
       anime.SetFloat("velocity", Mathf.Abs(keyHorizontal));
       RayPositionCenter= transform.position;
       float distance;

       RaycastHit2D ray= Physics2D.Raycast(RayPositionCenter, Vector2.down,10,Ground);

       distance= ray.distance;

       anime.SetFloat("height", distance);
       if(distance> 0.02f){
       canJump= false;

       }
       if (distance < 0.02f) {
              canJump=true;
       }
    } 

    private void Reverse(){
        if(keyHorizontal> 0){
            transform.rotation= Quaternion.Euler(0,0,0);

        }
        if (keyHorizontal < 0) {
            transform.rotation= Quaternion.Euler(0,180,0);
        }
    }

}
