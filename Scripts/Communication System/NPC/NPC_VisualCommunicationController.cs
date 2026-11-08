using UnityEngine;

/// <summary>
/// Main manager for regular NPC communication, responding to customer events and managing the display of order information and customer reactions.
/// </summary>

[RequireComponent(typeof(NPC_Base))]
public class NPC_VisualCommunicationController : MonoBehaviour
{
    [Header("Info Panel Data")]
    [SerializeField] private GameObject panel;
    [SerializeField] private SpriteRenderer panelImage;

    [Header("Info Images")]
    [SerializeField] private SpriteRenderer mainInfoImage;
    [SerializeField] private SpriteRenderer leftInfoImage;
    [SerializeField] private SpriteRenderer rightInfoImage;

    private NPC_Base npcBase;

    #region Subscription

    private void Awake()
    {
        npcBase = GetComponent<NPC_Base>();
    }

    private void OnEnable()
    {
        npcBase.EventBus.ShowInformation += ShowInformation;
        npcBase.EventBus.ShowMultipleInformation += ShowMultipleInformation;
        npcBase.EventBus.HideInformation += HideInformation;
    }

    private void OnDisable()
    {
        npcBase.EventBus.ShowInformation -= ShowInformation;
        npcBase.EventBus.ShowMultipleInformation -= ShowMultipleInformation;
        npcBase.EventBus.HideInformation -= HideInformation;
    }

    #endregion

    private void ShowInformation(NPC_VisualCommunicationData_SO data, int index)  
    {
        NPC_EventBus.ChangeInfoState();
        
        panelImage.sprite = data.Background[0];
        mainInfoImage.sprite = data.InfoImages[index];
        panel.SetActive(true);
    }

    private void ShowMultipleInformation(NPC_VisualCommunicationData_SO data, int index, int secondIndex)
    {
        NPC_EventBus.ChangeInfoState();

        panelImage.sprite = data.Background[1];
        mainInfoImage.sprite = data.InfoImages[^1];
        leftInfoImage.sprite = data.InfoImages[index];
        rightInfoImage.sprite = data.InfoImages[secondIndex];

        leftInfoImage.gameObject.SetActive(true);
        rightInfoImage.gameObject.SetActive(true);
        panel.SetActive(true);
    }

    private void HideInformation()
    {
        NPC_EventBus.ChangeInfoState();
        leftInfoImage.gameObject.SetActive(false);
        rightInfoImage.gameObject.SetActive(false);
        panel.SetActive(false);
    }

}
