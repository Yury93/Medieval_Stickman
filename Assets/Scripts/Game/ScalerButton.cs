using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalerButton : MonoBehaviour
{
    Coroutine coroutine;
    [SerializeField] private float scale;
    private void OnEnable()
    {
        coroutine = StartCoroutine(CorUpScale());
    }
    IEnumerator CorUpScale()
    {
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(0.01f);
        while (transform.localScale.x <= scale)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(scale, scale, scale), 0.5f);
            yield return null;

        }
    }
    private void OnDisable()
    {
       if(coroutine!= null) StopCoroutine(coroutine);
    }
}
