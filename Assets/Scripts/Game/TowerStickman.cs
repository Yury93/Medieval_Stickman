using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStickman : MonoBehaviour
{
    [SerializeField] private Transform transformPoint1, transformPoint2;
    [SerializeField] private Animator animator;
    [SerializeField] private float speed;
    [SerializeField] private Stickman stickman;
    public bool IsEndHelp { get; private set; }
    public void Init(Stickman stickman)
    {
        IsEndHelp = true;
        this.stickman = stickman;
    }

    public  void Attack()
    {
        IsEndHelp = false;
        StartCoroutine(CorAttack());
    }

  private  IEnumerator CorAttack()
    {
        CoreEnivroment.Instance.cameraMachine.ShowTower();

        foreach (var item in CoreEnivroment.Instance.enemiesService.SpawnSystem.AllEnemies)
        {
            if(item != null)
            {
                item.SetTarget(null, null);
            }
        }
        
       
        stickman.Increadible = true;
        while(transform.position.x > transformPoint1.position.x)
        {
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
            yield return null;
        }
        animator.Play("Attack");
        yield return new WaitForFixedUpdate();
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        yield return new WaitForSeconds(clipInfo.Length+0.3f);

        animator.Play("Walk");

        animator.transform.rotation = Quaternion.Euler(0, 180, 0);
        while (transform.position.x < transformPoint2.position.x)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            yield return null;
        }
        animator.transform.rotation = Quaternion.Euler(0, 0, 0);
        IsEndHelp = true;
        stickman.Increadible = false;

        foreach (var item in CoreEnivroment.Instance.enemiesService.SpawnSystem.AllEnemies)
        {
            if (item != null)
            {
                item.SetTarget(stickman, CoreEnivroment.Instance.tower);
            }
        }
        CoreEnivroment.Instance.cameraMachine.ShowStickman();
    }
  
}
