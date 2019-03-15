using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private int _health;
    // Start is called before the first frame update
    void Start()
    {
        _health = 5;
    }

    public void Hurt(int damage)
    {
        _health -= damage;
        if (_health == 0)
        {
            Debug.Log("Player Dead");
            Destroy(this.gameObject);
        }
    }
}
