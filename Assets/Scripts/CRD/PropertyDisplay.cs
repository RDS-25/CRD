using System;
using UnityEngine;
using UnityEngine.AI;

public class PropertyDisplay : MonoBehaviour ,ISelectable
{
    private Animator animator;

    public bool isDead= false;
    public float speed;
    public float ammor;
    public float maxhp;
    public float currenthp;

    void Start()
    {
        currenthp = maxhp;
        animator = GetComponent<Animator>();
        animator.SetTrigger("Run");
    }

    private void Update()
    {
        if (currenthp <= 0 && !isDead)
        {
            isDead = true;
            transform.GetComponent<Follow>().enabled = false;
            transform.GetComponent<NavMeshAgent>().enabled = false;
            animator.SetTrigger("Dead");
            Invoke("retunobj",0.5f);
        }
    }

    private void retunobj()
    {
        ObjectPool.Instance.ReturnObject(gameObject);
    }

    public void Select()
    {
       
    }

    public void Deselect()
    {
        
    }

    public bool IsSelected()
    {
        return false;
    }

    public void ExecuteCommand(ICommand command)
    {
        
    }
}
