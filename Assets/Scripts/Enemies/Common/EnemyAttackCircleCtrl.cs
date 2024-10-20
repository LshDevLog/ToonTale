using UnityEngine;

public class EnemyAttackCircleCtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject _attackCircle;
    public void OnAttackCol()
    {
        _attackCircle.gameObject.SetActive(true);
    }
}
