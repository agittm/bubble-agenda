using UnityEngine;

[CreateAssetMenu]
public class StereotypeDatabase : ScriptableObject
{
    [SerializeField] private Stereotype[] stereotypes;

    public Stereotype[] Stereotypes => stereotypes;

    public Stereotype GetRandom()
    {
        return stereotypes[Random.Range(0, stereotypes.Length)];
    }
}
