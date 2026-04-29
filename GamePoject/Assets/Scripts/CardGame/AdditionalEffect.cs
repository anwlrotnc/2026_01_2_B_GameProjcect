using UnityEngine;
[System.Serializable]

public class AdditionalEffect
{
    public CardData.AdditionalEffectType effectType;
    public int effectAmount;

    public string GetDescription()
    {
        switch(effectType)
        {
            case CardData.AdditionalEffectType.DrawCard:
                return $"카드 {effectAmount} 장 드로우";

            case CardData.AdditionalEffectType.DiscardCard:
                return $"카드 {effectAmount} 장 버리기";

            case CardData.AdditionalEffectType.GainMana:
                return $"마나 {effectAmount} 획득";

            case CardData.AdditionalEffectType.ReduceEnemyMana:
                return $"적 마나 {effectAmount} 감소";

            case CardData.AdditionalEffectType.ReduceCardCost:
                return $"다음 카드 비용 {effectAmount} 감소";

            default:
                return "";
        }
    }
}
