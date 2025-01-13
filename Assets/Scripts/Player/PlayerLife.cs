using TMPro;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider;
    
    [Header("Life counts")]
    [SerializeField] private TextMeshProUGUI heartsCount;
    // [SerializeField] private GameObject[] hearts;
    
    [Header("Respawn Fields")]
    [SerializeField] private Vector3 initSpawnPos;
    [SerializeField] private Vector3 respawnOffset;
    
    [Header("Manager Refs")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameOverMenu gameOverMenu;
    [SerializeField] private CheckpointManager checkpointManager;

    [SerializeField] private CharacterDataSO charData;
    
    private static readonly int deathAnim = Animator.StringToHash("death");

    void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        
        if (animator == null)
            animator = GetComponent<Animator>();

        if (boxCollider == null)
            boxCollider = GetComponent<BoxCollider2D>();

        if (playerMovement == null)
            playerMovement = GetComponent<PlayerMovement>();

        if (gameOverMenu == null)
            gameOverMenu = FindObjectOfType<GameOverMenu>();

        if (checkpointManager == null)
            checkpointManager = FindObjectOfType<CheckpointManager>();

        
    }

    public void Die()
    {
        boxCollider.enabled = false;
        rb.gravityScale = 0;
        CameraShaker.Instance.ShakeCamera(5f, 0.25f);
        AudioManager.Instance.PlaySound(AudioType.characterDeath);
        animator.SetTrigger(deathAnim);

    }


    // private void SetHearts()
    // {
    //     for (int i = 0; i < LifeCount; i++)
    //     {
    //         hearts[i].SetActive(true);
    //     }
    // }

    // private void DecreaseHearts()
    // {
    //     hearts[LifeCount].SetActive(false);
    // }


    private void CheckLifeStatus()
    {
        // DecreaseHearts();


        Respawn();

        /*
        if (LifeCount > 0)
        {

        }
        else
        {
            gameOverMenu.OpenGameOverMenu();
        }
        */
    }

    private void Respawn()
    {
        boxCollider.enabled = true;
        playerMovement.Flip(true);
        rb.gravityScale = 3;
        if (checkpointManager.hasPassedAnyCheckPoints())
        {
            Vector3 pos = checkpointManager.GetLatestCheckPoint().position;
            transform.position = new Vector3(pos.x + respawnOffset.x, pos.y + respawnOffset.y, pos.z + respawnOffset.z);
        }
        else
        {
            transform.position = initSpawnPos;
        }
    }
}