using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject mutant;
    public GameObject[] enemyes;

    private ScoreController score;
    private Vector3 offset;

    private void Start()
    {
        score = GetComponent<ScoreController>();
    }

    void Update()
    {
        enemyes = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void LateUpdate()
    {
        Spawn();
    }

    void Spawn()
    {
        if (enemyes.Count() != 10)
        {
            float x, x2, y, z, z2;

            x = Random.Range(player.transform.position.x + 30, player.transform.position.x + 40);
            x2 = Random.Range(player.transform.position.x - 30, player.transform.position.x - 40);
            y = player.transform.position.y;
            z = Random.Range(player.transform.position.z + 30, player.transform.position.z + 40);
            z2 = Random.Range(player.transform.position.z - 30, player.transform.position.z - 40);

            offset = new Vector3(Random.Range(x,x2), player.transform.position.y, Random.Range(z,z2));
            mutant.GetComponent<EnemyState>().maxHp = score.value + 1; 
            Instantiate(mutant, offset, Quaternion.identity);
        }
    }
}
