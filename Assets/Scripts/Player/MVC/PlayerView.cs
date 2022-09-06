using UnityEngine;
public class PlayerView
{
    Animator _anim;
    public PlayerView(Animator anim)
    {
        _anim = anim;
    }
    public void RunAnimation(float xAxis)
    {
        _anim.SetFloat("xAxis", Mathf.Abs(xAxis));
    }
    public void JumpAnimation()
    {
        _anim.SetTrigger("Jumping");
    }
    public void FallingAnimation(bool falling)
    {
        _anim.SetBool("Falling", falling);
    }
    public void DashAnimation()
    {
        _anim.SetTrigger("Dash");
    }
    public void AttackAnimation()
    {
        _anim.SetTrigger("Attack");
    }
}
