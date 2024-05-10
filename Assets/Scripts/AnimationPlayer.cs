using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMove _playerMove;
    
    private void OnEnable()
    {
        _playerMove.Moved += AnimationRun;
    }

    private void OnDisable()
    {
        _playerMove.Moved -= AnimationRun;
    }
    
    private void AnimationRun(bool isMoved)
    {
        _animator.SetBool(nameof(isMoved), isMoved);
    }
}
