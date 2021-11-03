using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Security.Cryptography;
using TMPro;
using System;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl playerControl;
    [SerializeField] private int playerHealth;
    [SerializeField] private Settings settings;
    public GameObject TTPText;
    public Animator animator;
    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private GameObject treePrefab;
    [SerializeField] private GameObject turrentPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemyInsPoint;

    [SerializeField] private Transform bagTranform1;
    [SerializeField] private Transform bagTranform2;
    [SerializeField] private Transform bagTranform3;
    [SerializeField] private Transform bagTranform4;
    public int count = 1;
    public float height = 0;
    float rand;
    int randIns;
    [SerializeField] private GameObject towerPrefab;

    int enemyNumber;
    public Action instAction;

    private List<GameObject> stackList = new List<GameObject>();

    private List<GameObject> towerList = new List<GameObject>();


    public List<GameObject> StackList { get => stackList; set => stackList = value; }
    public List<GameObject> allyList { get => towerList; set => towerList = value; }

    private void Awake()
    {
        if (playerControl == null)
            playerControl = this;
    }

    void Start()
    {
        GetComponent<JoystickPlayerExample>().Speed = 0;
        enemyNumber = 1;
        settings.deadEnemy = 0;
        TTPText.SetActive(true);
        animator.SetBool("attacking", false);
        rand = UnityEngine.Random.Range(3, 5);
        randIns = UnityEngine.Random.Range(4, 8);
        settings.stackPoint = 0;
        settings.buildCheck = false;

        towerList.Add(gameObject);
    }
    public void TTPButton()
    {
        GetComponent<JoystickPlayerExample>().Speed = 120;
        TTPText.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("stack"))
        {
            StackList.Add(other.gameObject);
            switch (count)
            {
                case 1:
                    AddStack(other, bagTranform1);
                    break;
                case 2:
                    AddStack(other, bagTranform2);
                    break;
                case 3:
                    AddStack(other, bagTranform3);
                    break;
                case 4:
                    AddStack(other, bagTranform4);
                    count = 1;
                    height += 0.015f;
                    break;
                default:
                    break;
            }
        }
        if (other.CompareTag("tree"))
        {
            other.gameObject.GetComponent<Collider>().isTrigger = false;
            StartCoroutine(SetOffset(other.gameObject));
        }
        if (other.CompareTag("enemyButton"))
        {
            for (int i = 0; i < enemyNumber; i++)
            {
                StartCoroutine(EnemyToCall());
            }
            enemyNumber++;
            other.transform.DOLocalMoveY(other.transform.position.y - 0.25f, .9f);
            if (enemyNumber == 3)
            {
                turrentPrefab.SetActive(true);
            }
        }
        if (other.CompareTag("enemy"))
        {
            animator.SetBool("attacking", true);
            playerHealth--;
            if (playerHealth==0)
            {
                GetComponent<JoystickPlayerExample>().Speed = 0;
                animator.SetBool("failing", true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("tree"))
        {
            animator.SetBool("attacking", false);
        }
        if (other.CompareTag("enemyButton"))
        {
            other.transform.DOLocalMoveY(other.transform.position.y + 0.25f, 0.5f);
        }
    }
    IEnumerator EnemyToCall()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject enemy = Instantiate(enemyPrefab, enemyInsPoint.transform.position, Quaternion.identity);
        settings.buildCheck = true;
    }
    void AddStack(Collider other, Transform transform)
    {
        other.transform.parent = transform;
        other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        other.gameObject.GetComponent<Rigidbody>().useGravity = false;
        other.transform.position = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
        other.transform.localRotation = transform.localRotation;
        other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        settings.stackPoint++;
        count++;
    }
    void Stack()
    {
        for (int i = 0; i < randIns; i++)
        {
            GameObject wood = Instantiate(woodPrefab, new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z), Quaternion.identity);
            wood.AddComponent<Rigidbody>();
            wood.GetComponent<Rigidbody>().useGravity = true;
            wood.GetComponent<Rigidbody>().AddForce(Vector3.up * rand, ForceMode.Impulse);
        }
    }
    IEnumerator SetOffset(GameObject other)
    {
        for (int i = 0; i < 4; i++)
        {
            animator.SetBool("attacking", true);
            yield return new WaitForSeconds(1.05f);
            other.transform.DOLocalMoveY(other.transform.position.y - 0.56f, 0.6f);
            yield return new WaitForSeconds(.1f);
            Stack();
        }
    }
}
