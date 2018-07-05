using UnityEngine;

public class LevelsStorage : MonoBehaviour, IStorage
{
    [SerializeField]
    private SceneFader sceneFader;
    public SceneFader SceneFader => sceneFader;

    [SerializeField]
    private GameObject contentView;
    public GameObject ContentView => contentView;

    [SerializeField]
    private GameObject cellPrefabUnlocked;
    public GameObject CellPrefabUnlocked => cellPrefabUnlocked;

    [SerializeField]
    private GameObject cellPrefabLocked;
    public GameObject CellPrefabLocked => cellPrefabLocked;
}
