using UnityEngine;

public class AnimationEnemy : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private EnemyMove _enemyMove;
    
    private void OnEnable()
    {
        _enemyMove.Moved += AnimationRun;
    }

    private void OnDisable()
    {
        _enemyMove.Moved -= AnimationRun;
    }
    
    private void AnimationRun(bool isMoved)
    {
        _animator.SetBool(nameof(isMoved), isMoved);
    }
}
