using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject character;
    public Animator animator;
    Rigidbody playerRigidbody;

    public float moveSpeed = 15f;
    bool isMove = false;

    bool isSlide = false;
    public float slideTime = .8f;
    public float slideForce = 600f;

    bool isAttack = false;
    public float kickRange = 2f;
    public float kickForce = 350f;

    void Start()
    {
        mainCamera = Camera.main;
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        ActSlide();
        Attack();
    }

    Vector3 destination = new Vector3();
    void Move() {
        if(Input.GetMouseButton(1)){
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit)) {
                isMove = true;
                destination = new Vector3(hit.point.x, 0, hit.point.z);
            }
        }
        if(isMove && !isSlide && !isAttack){
            Vector3 direction = destination - transform.position;
            Vector3 targetDirection = destination - transform.position;
            targetDirection.y = 0;
            
            transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
            character.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDirection), 360f);

            if(Vector3.Distance(transform.position, destination) < .1f) {
                isMove = false;
            }
        }
        animator.SetBool("isMove", isMove);
    }

    void Attack() {
        if(
            Input.GetMouseButtonDown(0)
            && !isSlide
            && !isAttack
        ){
            Vector3 targetPoint = new Vector3();
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit)) {
                isAttack = true;
                animator.SetBool("isAttack", false);
                animator.SetBool("isAttack", isAttack);
                targetPoint = new Vector3(hit.point.x, 0, hit.point.z);
                StartCoroutine(Kick(targetPoint));
            }
        }
    }
    IEnumerator Kick(Vector3 targetPoint) {
        character.transform.LookAt(targetPoint);

        yield return new WaitForSeconds(.4f);
        RaycastHit hit;
        playerRigidbody.AddForce(character.transform.forward * 150f, ForceMode.Force);
        if(Physics.Raycast(transform.position, character.transform.forward, out hit, kickRange)) {
            Vector3 attackDirection = (hit.collider.gameObject.transform.position - transform.position).normalized;
            if(hit.collider.gameObject.GetComponent<Rigidbody>() != null) {
                hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(attackDirection * 300f, ForceMode.Force);
            }
        }

        yield return new WaitForSeconds(.6f);
        isAttack = false;
        animator.SetBool("isAttack", isAttack);
        CheckWantMove();
    }

    void ActSlide() {
        if(
            Input.GetButtonDown("Jump") 
            && !isSlide 
            && !isAttack
        ){
            isSlide = true;
            StartCoroutine("Slide");
        }
        animator.SetBool("isSlide", isSlide);
    }
    IEnumerator Slide() {
        yield return new WaitForSeconds(.15f);
        playerRigidbody.AddForce(character.transform.forward * slideForce, ForceMode.Force);
        yield return new WaitForSeconds(slideTime);
        isSlide = false;
        playerRigidbody.velocity = Vector3.zero;
        destination = transform.position + character.transform.forward;
        CheckWantMove();
    }
    void OnCollisionEnter(Collision collisionInfo) {
        
    }

    void CheckWantMove(){
        if(Input.GetMouseButton(1)) {
            isMove = true;
        } else {
            isMove = false;
        }
    }
}
