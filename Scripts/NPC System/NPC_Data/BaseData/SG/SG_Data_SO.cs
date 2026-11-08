using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Data storage for special guests, including preferences, dish, and dialogue options.
/// </summary>

[CreateAssetMenu(fileName = "SO_SpecialGuest", menuName = "NPC_SO/Special")]
public class SG_Data_SO : BaseNPC_Data_SO
{
    //SG Private Data
    [SerializeField] private List<ProductSO> preferences;
    [SerializeField] private Sprite icon;
    [SerializeField] private DishesEnum dish;

    // Dialogues Data
    [SerializeField] private SO_Dialogue[] sgDialogues;
    [SerializeField] private List<StructForDictionary<FinalResultEnum, SO_Dialogue[]>> resultDialogues;

    //Public Access to the Data
    public List<ProductSO> Preferences => preferences;
    public Sprite Icon => icon;
    public DishesEnum Dish => dish;

    public SO_Dialogue GetDialogue(int index) => sgDialogues[index]; //starter dialogue
    public SO_Dialogue GetDialogue(FinalResultEnum res, int index) //result dialogue
    {
        StructForDictionary<FinalResultEnum, SO_Dialogue[]> rD = resultDialogues.FirstOrDefault(x => x.Key == res);
        return rD.Value[index];
    }
}
