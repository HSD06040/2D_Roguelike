using System.Collections.Generic;
using UnityEngine;

public class EffectRandomAttacker : MonoBehaviour
{
    private GameObject prefab;
    private int monsterCount;
    private float damage;
    private float offset;
    private HashSet<int> usedIndexes = new HashSet<int>();
    private List<Collider2D> selected = new List<Collider2D>();

    public void Init(int _monsterCount, float _damage, GameObject _prefab, float _offset)
    {        
        usedIndexes.Clear();
        selected.Clear();

        offset = _offset;
        monsterCount = _monsterCount;
        damage = Manager.Data.PlayerStatus.curWeapon.curAttackDamage * _damage;
        prefab = _prefab;
        
        Attack();

        Destroy(gameObject, 2);
    }

    private void Attack()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 30f, 1 << 6);

        if (cols.Length <= monsterCount)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i] == null) continue;
                Manager.Resources.Instantiate(prefab, (Vector2)cols[i].transform.position + new Vector2(0, offset), Quaternion.identity, true)
                    .GetComponent<PassiveObject>().Init(damage, 1);
            }

            return;
        }

        while (selected.Count < monsterCount)
        {
            int randIdx = Random.Range(0, cols.Length);
            if (usedIndexes.Add(randIdx))
            {
                selected.Add(cols[randIdx]);
            }
        }

        foreach (var col in selected)
        {
            if (col == null) continue;
            Manager.Resources.Instantiate(prefab, (Vector2)col.transform.position + new Vector2(0, offset), Quaternion.identity, true)
                .GetComponent<PassiveObject>().Init(damage, 1);
        }
    }
}
