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
        enemyes = GameObject.FindGameObjectsWithTag("Enemy");
        score = GetComponent<ScoreController>();
        StartCoroutine(Spawn());
    }

    void Update()
    {
        enemyes = GameObject.FindGameObjectsWithTag("Enemy");
    }

    IEnumerator Spawn()
    {
        float timespawn = 0f;
        float quantityEnemy = 2f;
        while (true)
        {
            if (enemyes.Count() <= quantityEnemy)
            {
                yield return new WaitForSeconds(timespawn);
                float x, x2, y, z, z2;
                x = player.transform.position.x + 40;
                x2 = player.transform.position.x - 40;
                y = player.transform.position.y;
                z = player.transform.position.z + 40;
                z2 = player.transform.position.z - 40;

                offset = new Vector3(Random.Range(x, x2), player.transform.position.y, Random.Range(z, z2));
                mutant.GetComponent<EnemyState>().maxHp = score.value + 1;
                Instantiate(mutant, offset, Quaternion.identity);
                timespawn = Random.Range(0f, 6f);
            }
            else
            {
                yield return new WaitForSeconds(5f);
                quantityEnemy = Random.Range(1f,6f);
            }
        }
    }
}
