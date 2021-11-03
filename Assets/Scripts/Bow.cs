using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject bowRot;
    [SerializeField] private Settings settings;
    private void Start()
    {
        InvokeRepeating("Killing", 1, 1.5f);
    }
    void Looking()
    {
        if (enemyPrefab!=null)
        {
            if (settings.buildCheck==true)
            {
                GameObject arrow = Instantiate(arrowPrefab, new Vector3(transform.position.x,transform.position.y+0.30f,transform.position.z), transform.rotation);
                arrow.GetComponent<Rigidbody>().velocity = transform.forward * 17;
            }
        }
    }
    private void FixedUpdate()
    {
        if (!enemyPrefab)
            return;
        transform.LookAt(enemyPrefab.transform.position);
    }
    void Killing()
    {
        enemyPrefab = GameObject.Find("enemy(Clone)");
        Looking();
    }
}