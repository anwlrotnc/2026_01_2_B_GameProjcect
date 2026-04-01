using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DialogSO", menuName = "Dialog System/DialogSO")]
public class DialogSO : ScriptableObject
{
    public int id;
    public string characterName;
    public string text;
    public int nextid;


    public List<DialogChoiceSO> choices = new List <DialogChoiceSO>();
    public Sprite portrait;

    public string portraitPath;
}
