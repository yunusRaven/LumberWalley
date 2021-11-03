using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Tower : MonoBehaviour
{
    [SerializeField] private int towerStack;
    [SerializeField] private Settings settings;
    [SerializeField] private GameObject smoke;
    [SerializeField] private int fixedStack;
    [SerializeField] private int textPoint=20;
    [SerializeField] private int nesStack;
    public List<GameObject> towerComponents;
    public TMPro.TextMeshPro stackText;
    bool towerDestroy;
    bool isBuild;
    int count2 = 1;
    int m;
    int k;

    private PlayerControl stacks;
    public int TowerStack { get => towerStack; set => towerStack = value; }
    private void Start()
    {
        towerDestroy = false;
        isBuild = false;
    }
    private void Update()
    {
        stackText.text = TowerStack.ToString() + ("/") + nesStack.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            stacks = other.GetComponent<PlayerControl>();
            StartCoroutine(Building());
            StartCoroutine(ThrowStack(other, transform));
        }

    }
    IEnumerator Building()
    {
        PlayerControl.playerControl.allyList.Add(gameObject);
        if ((7 * settings.stackPoint) / fixedStack > 7 && isBuild == false)
        {
            for (k = 0; k < 7; k++)
            {
                yield return new WaitForSeconds(0.18f);
                towerComponents[k].SetActive(true);
                GameObject smokeFx = Instantiate(smoke, towerComponents[k].transform.position, Quaternion.identity);
            }
            isBuild = true;
            settings.buildCheck = true;
        }
        if ((7 * settings.stackPoint) / fixedStack < 7 && isBuild == false)
        {
            for (k = 0; k < (7 * settings.stackPoint) / fixedStack; k++)
            {
                yield return new WaitForSeconds(0.18f);
                towerComponents[k].SetActive(true);
                GameObject smokeFx = Instantiate(smoke, towerComponents[k].transform.position, Quaternion.identity);
            }
            if (k == 7)
            {
                isBuild = true;
                settings.buildCheck = true;
            }
        }
    }
    IEnumerator ThrowStack(Collider other, Transform transform)
    {
        count2 = PlayerControl.playerControl.count;
        if (TowerStack < textPoint && isBuild == false)
        {
            if (stacks.StackList.Count <= textPoint)
            {
                m = settings.stackPoint;
                for (int i = 0; i < m; i++)
                {
                    switch (count2)
                    {
                        case 1:
                            yield return new WaitForSeconds(0.05f);
                            ThrowVoid(other);
                            break;
                        case 2:
                            yield return new WaitForSeconds(0.05f);
                            ThrowVoid(other);
                            break;
                        case 3:
                            yield return new WaitForSeconds(0.05f);
                            ThrowVoid(other);
                            break;
                        case 4:
                            yield return new WaitForSeconds(0.05f);
                            ThrowVoid(other);
                            count2 = 1;
                            PlayerControl.playerControl.height -= 0.015f;
                            break;
                        default:
                            break;
                    }
                }
            }
            if (settings.stackPoint > textPoint)
            {
                for (int i = 0; i < textPoint-m; i++)
                {
                    switch (count2)
                    {
                        case 1:
                            yield return new WaitForSeconds(0.05f);
                            ThrowVoid(other);
                            break;
                        case 2:
                            yield return new WaitForSeconds(0.05f);
                            ThrowVoid(other);
                            break;
                        case 3:
                            yield return new WaitForSeconds(0.05f);
                            ThrowVoid(other);
                            break;
                        case 4:
                            yield return new WaitForSeconds(0.05f);
                            ThrowVoid(other);
                            count2 = 1;
                            PlayerControl.playerControl.height -= 0.015f;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        if (isBuild == true && TowerStack < fixedStack)
        {
            if (settings.stackPoint> fixedStack - towerStack)
            {
                int b = fixedStack - towerStack;
                for (int i = 0; i < b; i++)
                {
                    switch (count2)
                    {
                        case 1:
                            yield return new WaitForSeconds(0.12f);
                            ThrowVoid(other);
                            break;
                        case 2:
                            yield return new WaitForSeconds(0.12f);
                            ThrowVoid(other);
                            break;
                        case 3:
                            yield return new WaitForSeconds(0.12f);
                            ThrowVoid(other);
                            break;
                        case 4:
                            yield return new WaitForSeconds(0.12f);
                            ThrowVoid(other);
                            count2 = 1;
                            PlayerControl.playerControl.height -= 0.015f;
                            break;
                        default:
                            break;
                    }
                }
            }
            if (settings.stackPoint <= fixedStack - towerStack)
            {
                int b = settings.stackPoint;
                for (int i = 0; i < b; i++)
                {
                    switch (count2)
                    {
                        case 1:
                            yield return new WaitForSeconds(0.12f);
                            ThrowVoid(other);
                            break;
                        case 2:
                            yield return new WaitForSeconds(0.12f);
                            ThrowVoid(other);
                            break;
                        case 3:
                            yield return new WaitForSeconds(0.12f);
                            ThrowVoid(other);
                            break;
                        case 4:
                            yield return new WaitForSeconds(0.12f);
                            ThrowVoid(other);
                            count2 = 1;
                            PlayerControl.playerControl.height -= 0.015f;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
    public IEnumerator TowerDemolition()
    {
        if (TowerStack>0)
        {
            TowerStack--;
        }
        if (towerDestroy == false)
        {
            if (TowerStack == 0)
            {
                isBuild = false;
                towerDestroy = true;
                GameObject smokes = Instantiate(smoke, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), Quaternion.identity);
                for (int i = 0; i < 7; i++)
                {
                    towerComponents[i].SetActive(false);
                }
                PlayerControl.playerControl.allyList.Remove(gameObject);
                gameObject.SetActive(false);
                settings.buildCheck = false;
                k = 0;
            }
            yield return new WaitForSeconds(10);
            gameObject.SetActive(true);
        }
    }
    void ThrowVoid(Collider other)
    {
        stacks.StackList[stacks.StackList.Count - 1].GetComponent<Rigidbody>().isKinematic = true;
        stacks.StackList[stacks.StackList.Count - 1].GetComponent<BoxCollider>().enabled = false;
        stacks.StackList[stacks.StackList.Count - 1].transform.parent = null;
        stacks.StackList[stacks.StackList.Count - 1].transform.DOMove(transform.position, 0.08f);
        stacks.StackList.Remove(stacks.StackList[stacks.StackList.Count - 1]);
        settings.stackPoint--;
        TowerStack++;
        count2++;
    }
}
