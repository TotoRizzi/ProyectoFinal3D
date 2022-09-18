using System.Collections;
using UnityEngine;
public class PlayerView
{
    Animator _anim;
    Material _playerMaterial;
    ParticleSystem _doubleJumpPS, _pogoPS;
    public PlayerView(Animator anim, Material playerMaterial, ParticleSystem doubleJumpPS, ParticleSystem pogoPS)
    {
        _anim = anim;
        _playerMaterial = playerMaterial;
        _doubleJumpPS = doubleJumpPS;
        _pogoPS = pogoPS;
    }
    public void InGrounded(bool inGrounded)
    {
        _anim.SetBool("inGrounded", inGrounded);
    }
    public void RunAnimation(float xAxis)
    {
        _anim.SetFloat("xAxis", Mathf.Abs(xAxis));
    }
    public void JumpAnimation(bool doubleJump)
    {
        _anim.SetTrigger("Jumping");
        if (!doubleJump)
            _doubleJumpPS.Play();
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
    public void ThrowAnimation()
    {
        _anim.SetTrigger("Throw");
    }
    public void PogoFeedback()
    {
        _pogoPS.Play();
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
