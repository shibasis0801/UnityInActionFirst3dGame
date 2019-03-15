using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SceneController : MonoBehaviour
{
    // Serialize + private will show up in Inspector but not in Code.
    [SerializeField] private GameObject enemyPrefab;
    private GameObject _enemy;
//    private GameObject[] _enemies;
    
//    public int enemyCount = 5;
    
    // Start is called before the first frame update
    void Start()
    {
//        _enemies = new GameObject[enemyCount];
    }

    // Update is called once per frame
    void Update()
    {
        // Buggy Code
//        StartCoroutine(createEnemies());

        
        if (_enemy == null)
        {
            
            _enemy = Instantiate(enemyPrefab) as GameObject;
                        
            var x = Random.Range(-5, 5);
            var y = 1;
            var z = Random.Range(-8, 8);

            _enemy.transform.position = new Vector3(x, y, z);

            var angle = Random.Range(-110, 110);
            
            _enemy.transform.Rotate(0, angle, 0);
        }
    }
    
    /*
    IEnumerator createEnemies(int currentEnemy = 0)
    {
        var enemy = _enemies[currentEnemy];
        if (enemy == null)
        {
            enemy = Instantiate(enemyPrefab) as GameObject;

            var x = Random.Range(-5, 5);
            var y = 1;
            var z = Random.Range(-8, 8);

            enemy.transform.position = new Vector3(x, y, z);

            var angle = Random.Range(-110, 110);
            
            enemy.transform.Rotate(0, angle, 0);
        }
        
        yield return new WaitForSeconds(1);

        if (currentEnemy < enemyCount)
        { 
            Debug.Log(currentEnemy);
            StartCoroutine(createEnemies(currentEnemy + 1));
        }        
    }
    */
}
