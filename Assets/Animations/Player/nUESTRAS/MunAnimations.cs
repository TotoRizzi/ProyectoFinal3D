using UnityEngine;
public class MunAnimations : MonoBehaviour
{
    Animator _anim;
    void Start()
    {
        _anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) _anim.SetTrigger("MeleAttack");
        else if (Input.GetKeyDown(KeyCode.K)) _anim.SetTrigger("Throw");
        else if (Input.GetKeyDown(KeyCode.T)) _anim.SetTrigger("Die");
    }
}
