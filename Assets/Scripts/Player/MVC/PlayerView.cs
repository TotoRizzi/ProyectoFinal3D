using System.Collections;
using UnityEngine;
public class PlayerView
{
    Animator _anim;
    Material _playerMaterial;
    ParticleSystem _doubleJumpPS, _pogoPS;
    Color _initialColor;
    public PlayerView(Animator anim, Material playerMaterial, ParticleSystem doubleJumpPS, ParticleSystem pogoPS)
    {
        _anim = anim;
        _playerMaterial = playerMaterial;
        _doubleJumpPS = doubleJumpPS;
        _pogoPS = pogoPS;
        _initialColor = _playerMaterial.color;
    }
    public void SetWeight(bool xRb)
    {
        int lowerBodyLayer = _anim.GetLayerIndex("LowerBody");

        if (xRb)
            _anim.SetLayerWeight(lowerBodyLayer, 0);
        else
            _anim.SetLayerWeight(lowerBodyLayer, 1);
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
    public void AttackAnimation(int yAxis)
    {
        _anim.SetInteger("yAxis", yAxis);
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
        _playerMaterial.color = Color.red;
        yield return new WaitForSeconds(.1f);
        _playerMaterial.color = _initialColor;
        yield return null;
    }
}
