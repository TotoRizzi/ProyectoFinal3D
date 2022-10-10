using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerView
{
    Animator _anim;
    Material _playerMaterial;
    ParticleSystem _doubleJumpPS, _pogoPS;
    Color _initialColor;
    Image _staminaFill;
    Image _hpFill;
    Image _invulnerabily;
    float _invulnerabilityTime;
    TrailRenderer _tr;

    float _invulnerabilityTimer;
    public PlayerView(Animator anim, Material playerMaterial, ParticleSystem doubleJumpPS, ParticleSystem pogoPS, Image staminaFill, Image hpFill,
        Image invulneravility, float invulnerabilityTime, TrailRenderer tr)
    {
        _anim = anim;
        _playerMaterial = playerMaterial;
        _doubleJumpPS = doubleJumpPS;
        _pogoPS = pogoPS;
        _initialColor = _playerMaterial.color;
        _staminaFill = staminaFill;
        _hpFill = hpFill;
        _invulnerabily = invulneravility;
        _invulnerabilityTime = invulnerabilityTime;
        _tr = tr;

        _invulnerabily.gameObject.SetActive(false);
    }

    #region Animations
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
    public void DashAnimation(bool dashing)
    {
        if (dashing)
        {
            _anim.SetTrigger("Dash");
            _tr.emitting = true;
        }
        else
            _tr.emitting = false;
    }
    public void AttackAnimation(int yAxis)
    {
        _anim.SetInteger("yAxis", yAxis);
        _anim.SetTrigger("Attack");
    }
    public void ThrowAnimation()
    {
        _anim.SetTrigger("Throw");
    }
    public void PogoFeedback()
    {
        _pogoPS.Play();
    }
    #endregion
    public IEnumerator TakeDamageFeedback()
    {
        _anim.SetTrigger("GetHit");
        _invulnerabilityTimer = _invulnerabilityTime;
        _invulnerabily.gameObject.SetActive(true);
        _playerMaterial.color = Color.red;
        yield return new WaitForSeconds(.1f);
        _playerMaterial.color = Color.grey;
        Physics.IgnoreLayerCollision(9, 8, true);
        Physics.IgnoreLayerCollision(9, 10, true);
        Physics.IgnoreLayerCollision(9, 12, true);
        yield return new WaitForSeconds(_invulnerabilityTime);
        _invulnerabily.gameObject.SetActive(false);
        Physics.IgnoreLayerCollision(9, 8, false);
        Physics.IgnoreLayerCollision(9, 10, false);
        Physics.IgnoreLayerCollision(9, 12, false);
        _playerMaterial.color = _initialColor;
    }
    public void OnUpdate()
    {
        _invulnerabilityTimer -= Time.deltaTime;
        _invulnerabily.fillAmount = _invulnerabilityTimer / _invulnerabilityTime;
    }
    public void UpdateStaminaBar(float amount)
    {
        _staminaFill.fillAmount = amount;
    }
    public void UpdateLifeBar(float amount)
    {
        _hpFill.fillAmount = amount;
    }
}
