using UnityEngine;

[CreateAssetMenu]
public class StereotypeDatabase : ScriptableObject
{
    [SerializeField] private Stereotype[] stereotypes;

    public Stereotype[] Stereotypes => stereotypes;

    public Stereotype GetRandom(int maxRange)
    {
        return stereotypes[Random.Range(0, Mathf.Min(maxRange, stereotypes.Length))];
    }
}
