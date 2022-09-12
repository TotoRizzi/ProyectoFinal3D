using System.Collections;
using UnityEngine;
public class PlayerView
{
    Animator _anim;
    Material _playerMaterial;
    public PlayerView(Animator anim, Material playerMaterial)
    {
        _anim = anim;
        _playerMaterial = playerMaterial;
    }
    public void InGrounded(bool inGrounded)
    {
        _anim.SetBool("inGrounded", inGrounded);
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
    public void PogoAnimation(bool pogoBool, float xAxis)
    {
        _anim.SetBool("Poging", pogoBool);
        _anim.SetFloat("xPog", Mathf.Abs(xAxis));
    }
    public IEnumerator TakeDamageFeedback()
    {
        Color initialColor = _playerMaterial.color;

        _playerMaterial.color = Color.red;
        yield return new WaitForSeconds(.1f);
        _playerMaterial.color = initialColor;
        yield return null;
    }
}
