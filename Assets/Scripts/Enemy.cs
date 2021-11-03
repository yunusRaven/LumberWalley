using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    PlayerControl playerControl;
    public Animator animator;
    [SerializeField] private Settings settings;
    [SerializeField] private GameObject enemyPrefab;
    private int health = 0;
    private bool isDead;
    private Coroutine coroutine;
    //private Tower tower;
    [SerializeField] private List<Renderer> enemyComponent;


    void Start()
    {
        isDead = false;
        agent = GetComponent<NavMeshAgent>();
        animator.SetBool("enemyAttacking", false);
        animator.SetBool("enemyRunning", true);
        animator.SetBool("enemyDamage", false);

        playerControl = PlayerControl.playerControl;

    }
    void Update()
    {
        if (isDead == false)
        {
            //if (playerControl.allyList[0] != null)
            {
                agent.destination = new Vector3(playerControl.allyList[playerControl.allyList.Count-1].transform.position.x + 0.5f, playerControl.allyList[playerControl.allyList.Count - 1].transform.position.y, playerControl.allyList[playerControl.allyList.Count - 1].transform.position.z + 0.5f);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("turrent"))
        {
            Tower tower = other.GetComponent<Tower>();
            gameObject.transform.GetComponent<Rigidbody>().freezeRotation = true;
            animator.SetBool("enemyRunning", false);
            animator.SetBool("enemyAttacking", true);
            coroutine = StartCoroutine(decreaseTower(tower));
        }
        if (other.CompareTag("arrow"))
        {
            StartCoroutine(DamageCntrol(other.gameObject));
            if (health == 3)
            {
                animator.SetBool("enemyRunning", false);
                animator.SetBool("enemyAttacking", false);
                animator.SetBool("enemyDamage", false);
                animator.SetBool("enemyDying", true);
                settings.deadEnemy++;
                gameObject.transform.DOLocalMoveZ(transform.localPosition.z + 1.0f, 1f);
                gameObject.GetComponent<NavMeshAgent>().speed = 0;
                for (int i = 0; i < 4; i++)
                {
                    enemyComponent[i].GetComponent<Renderer>().material.DOFade(0, 2.5f);
                }
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                isDead = true;
                Destroy(gameObject, 1.5f);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("turrent"))
        {
            animator.SetBool("enemyAttacking", false);
        }
    }
    IEnumerator DamageCntrol(GameObject other)
    {
        gameObject.GetComponent<NavMeshAgent>().speed = 0;
        animator.SetBool("enemyDamage", true);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("enemyDamage", false);
        gameObject.GetComponent<NavMeshAgent>().speed = 3.5f;
        health++;
        Destroy(other.gameObject);
    }
    IEnumerator decreaseTower(Tower tower)
    {
        for (int i = tower.TowerStack; i > 0; i--)
        {
            StartCoroutine(tower.TowerDemolition());
            yield return new WaitForSeconds(1.1f);
            //animator.SetBool("enemyRunning", false);
            animator.SetBool("enemyAttacking", true);

            if (isDead == true)
                break;
        }
    }
}
