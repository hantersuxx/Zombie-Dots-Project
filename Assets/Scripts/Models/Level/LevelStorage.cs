using UnityEngine;
using UnityEngine.UI;

public class LevelStorage : MonoBehaviour
{
    [Header("Damage image")]
    [SerializeField]
    private Image damageImage;
    public Image DamageImage { get => damageImage; private set => damageImage = value; }

    [SerializeField]
    private float flashSpeed = 0.5f;
    public float FlashSpeed { get => flashSpeed; private set => flashSpeed = value; }

    [SerializeField]
    private Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    public Color FlashColor { get => flashColor; private set => flashColor = value; }

    [Header("UI")]
    [SerializeField]
    private GameObject levelMenu;
    public GameObject LevelMenu { get => levelMenu; private set => levelMenu = value; }

    [SerializeField]
    private GameObject pauseMenu;
    public GameObject PauseMenu { get => pauseMenu; private set => pauseMenu = value; }

    [SerializeField]
    private GameObject gameOver;
    public GameObject GameOver { get => gameOver; private set => gameOver = value; }

    [SerializeField]
    private GameObject completeLevel;
    public GameObject CompleteLevel { get => completeLevel; private set => completeLevel = value; }

    //[SerializeField]
    //private Image healthBar;
    //public Image HealthBar => healthBar;

    [SerializeField]
    private SceneFader sceneFader;
    public SceneFader SceneFader { get => sceneFader; private set => sceneFader = value; }
}
