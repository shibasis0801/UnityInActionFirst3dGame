using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    public void ReactToHit()
    {
        var AI = GetComponent<WanderingAI>();
        if (AI != null && AI.GetAlive())
        {
            AI.SetAlive(false);
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        transform.Rotate(-75, 0, 0);
        
        yield return new WaitForSeconds(1.5f);
        
        Destroy(this.gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
