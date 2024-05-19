using UnityEngine;

public class AnimationCharacter : MonoBehaviour
{
    private const string Attack = "isAttack";
    private const string TakeDamage = "isTakeDamage";
    
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterMove _characterMove;
    [SerializeField] private Character _character;
    
    private void OnEnable()
    {
        if (_character != null)
        {
            _character.BaseAttackEvent += AnimationAttack;
            _character.TakeDamageEvent += AnimationTakeDamage;
        }
        
        _characterMove.Moved += AnimationRun;
    }

    private void OnDisable()
    {
        if (_character != null)
        {
            _character.BaseAttackEvent -= AnimationAttack;
            _character.TakeDamageEvent -= AnimationTakeDamage;
        }
        
        _characterMove.Moved -= AnimationRun;
    }
    
    private void AnimationRun(bool isMoved)
    {
        _animator.SetBool(nameof(isMoved), isMoved);
    }
    
    private void AnimationAttack()
    {
        _animator.SetTrigger(Attack);
    }
    
    private void AnimationTakeDamage()
    {
        _animator.SetTrigger(TakeDamage);
    }
}
