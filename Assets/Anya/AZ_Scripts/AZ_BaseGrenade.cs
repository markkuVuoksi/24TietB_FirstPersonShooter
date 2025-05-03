using System.Collections;
using UnityEngine;

public abstract class AZ_BaseGrenade : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  

    // Update is called once per frame
    void Update()
    {
        
    }
    public float delay = 3f;

    protected virtual void Start()
    {
        StartCoroutine(ExplodeAfterDelay());
    }

    protected virtual IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        Explode();
    }

    protected abstract void Explode();
}
