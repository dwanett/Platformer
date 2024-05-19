using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private Vision _vision;
    private Player _player;

    public PatrollingWay PatrollingWay { get; private set; }
    
    private void OnEnable()
    {
        _vision.EnterVisionPlayer += SetPlayer;
    }

    private void OnDisable()
    {
        _vision.ExitVisionPlayer -= ResetPlayer;
    }

    private void FixedUpdate()
    {
        if (_player is not null && TryFindSkill(out Attack attack))
            CastSkill(attack);
    }

    private void SetPlayer(Player player)
    {
        _player = player;
    }
    
    private void ResetPlayer()
    {
        _player = null;
    }
    
    public void ReplacePatrollingWay(PatrollingWay patrollingWay)
    {
        PatrollingWay = patrollingWay;
    }

    public Transform GetNextPointTarget()
    {
       return PatrollingWay.GetPositionTarget();
    }
}
