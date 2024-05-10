using UnityEngine;

public class AnimationCharacter : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterMove _characterMove;
    
    private void OnEnable()
    {
        _characterMove.Moved += AnimationRun;
    }

    private void OnDisable()
    {
        _characterMove.Moved -= AnimationRun;
    }
    
    private void AnimationRun(bool isMoved)
    {
        _animator.SetBool(nameof(isMoved), isMoved);
    }
}
