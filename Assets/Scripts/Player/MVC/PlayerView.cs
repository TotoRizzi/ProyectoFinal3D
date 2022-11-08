using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PlayerView
{
    Animator _anim;
    Material _playerMaterial;
    ParticleSystem _doubleJumpPS;
    Color _initialColor;
    Image _staminaFill;
    Image _hpFill;
    Image _invulnerabily;
    float _invulnerabilityTime;
    TrailRenderer _dashTr;

    float _invulnerabilityTimer;
    public string[] _footSteps = { "FootStep00", "FootStep01", "FootStep02", "FootStep03", "FootStep04" };
    public PlayerView(Animator anim, Material playerMaterial, ParticleSystem doubleJumpPS, Image staminaFill, Image hpFill,
        Image invulneravility, float invulnerabilityTime, TrailRenderer dashTr)
    {
        _anim = anim;
        _playerMaterial = playerMaterial;
        _doubleJumpPS = doubleJumpPS;
        _initialColor = _playerMaterial.color;
        _staminaFill = staminaFill;
        _hpFill = hpFill;
        _invulnerabily = invulneravility;
        _invulnerabilityTime = invulnerabilityTime;
        _dashTr = dashTr;

        _invulnerabily.gameObject.SetActive(false);
        _hpFill.color = Color.green;

        Physics.IgnoreLayerCollision(9, 8, false);
        Physics.IgnoreLayerCollision(9, 10, false);
        Physics.IgnoreLayerCollision(9, 12, false);
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
            _dashTr.emitting = true;
        }
        else
            _dashTr.emitting = false;
    }
    public void AttackAnimation(int yAxis)
    {
        AudioManager.Instance.PlaySFX("Slash");
        _anim.SetInteger("yAxis", yAxis);
        _anim.SetTrigger("Attack");
    }
    public void ThrowAnimation()
    {
        _anim.SetTrigger("Throw");
    }
    public void DieAnimation()
    {
        _anim.SetTrigger("Die");
        Physics.IgnoreLayerCollision(9, 8, true);
        Physics.IgnoreLayerCollision(9, 10, true);
        Physics.IgnoreLayerCollision(9, 12, true);
    }
    #endregion
    public IEnumerator TakeDamageFeedback()
    {
        _anim.SetTrigger("GetHit");
        AudioManager.Instance.PlaySFX("PlayerHurt");
        _invulnerabilityTimer = _invulnerabilityTime;
        _invulnerabily.gameObject.SetActive(true);
        _playerMaterial.color = Color.red;
        Physics.IgnoreLayerCollision(9, 8, true);
        Physics.IgnoreLayerCollision(9, 10, true);
        Physics.IgnoreLayerCollision(9, 12, true);
        yield return new WaitForSeconds(.1f);
        _playerMaterial.color = Color.grey;
        yield return new WaitForSeconds(_invulnerabilityTime);
        _invulnerabily.gameObject.SetActive(false);
        Physics.IgnoreLayerCollision(9, 8, false);
        Physics.IgnoreLayerCollision(9, 10, false);
        Physics.IgnoreLayerCollision(9, 12, false);
        _playerMaterial.color = _initialColor;
    }
    public void PlayFootSteps()
    {
        AudioManager.Instance.PlaySFX(_footSteps[Random.Range(0, 5)]);
    }
    public void OnUpdate()
    {
        _invulnerabilityTimer -= Time.deltaTime;
        _invulnerabily.fillAmount = _invulnerabilityTimer / _invulnerabilityTime;
    }
    public void UpdateStaminaBar(float amount)
    {
        _staminaFill.DOFillAmount(amount, .2f);
    }
    public void UpdateLifeBar(float amount)
    {
        _hpFill.DOFillAmount(amount, amount);
        Color newColor = amount < 0.25f ? Color.red : amount < 0.75f ? new Color(1f, .64f, 0f, 1f) : Color.green;

        _hpFill.DOColor(newColor, amount);
    }
}
