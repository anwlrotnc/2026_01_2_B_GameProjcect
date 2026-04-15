using UnityEngine;
using TMPro;
public class CardDisplay : MonoBehaviour
{
    public CardData cardData;               //카드 데이터
    public int cardIndex;                   //손페에서의 인덱스 (나중에 사용)

    public MeshRenderer cardRenderer;       //카드 렌더러
    public TextMeshPro nameText;            //이름 텍스트
    public TextMeshPro costText;            //비용 텍스트
    public TextMeshPro attackText;          //공격력/효과 테스트
    public TextMeshPro descriptionText;     //설명 텍스트

    //카드 상태
    public bool isDragging = false;
    private Vector3 originalPosition;       //드래그 전 원본 위치

    //레이어 마스크
    public LayerMask enemyLayer;            //적 레이어
    public LayerMask playerLayer;           //플레이어 레이어

    private CardManager cardManager;        //카드 매니저 참조 추가
    public void Start()
    {
        playerLayer = LayerMask.GetMask("Player");
        enemyLayer = LayerMask.GetMask("Enemy");

        

        SetupCard(cardData);
    }
    //카드 데이터 설정
    public void SetupCard(CardData data)
    {
        cardData = data;

        //3D 텍스트 업데이트
        if (nameText != null) nameText.text = data.cardName;
        if(costText != null) costText.text = data.manaCost.ToString();
        if(attackText != null) attackText.text = data.effectAmount.ToString();
        if (descriptionText != null) descriptionText.text = data.description;
        
        //카드 텍스쳐 설정
        if(cardRenderer !!= null && data.artwork != null)
        {
            Material cardMaterial = cardRenderer.material;
            cardMaterial.mainTexture = data.artwork.texture;
        }
    }

    private void OnMouseDown()
    {
        //드레그 시작 시 원래 위치 저장
        originalPosition = transform.position;
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            //마우스 위치로 카드 이동
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = new Vector3(worldPos.x, worldPos.y, transform.position.z);
        }
    }

    private void OnMouseUp()
    {
        if(CardManager.Instance.playerStats == null || CardManager.Instance.playerStats.currentMana < cardData.manaCost)        //마나검사
        {
            Debug.Log($"마나가 부족합니다.! (필요 : {cardData.manaCost} , 현재 : {CardManager.Instance.playerStats.currentMana}");
            transform.position = originalPosition;
            return;
        }


        isDragging = false;

        //레이캐스트로 타켓 감지
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //카드 사용 판정
        bool cardUsed = false;

        //적 위에 드롭 했는지 검사
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayer))
        {
            //적에게 공격 효과 적용
            CharacterStats enemyStats = hit.collider.GetComponent<CharacterStats>();

            if(enemyStats != null)
            {
                if(cardData.cardType == CardData.CardType.Attack)           //카드효과에 따라 다르게
                {
                    //공격 카드면 데미지 추가
                    enemyStats.TakeDamage(cardData.effectAmount);
                    Debug.Log($"{cardData.cardName} 카드로 적에게 {cardData.effectAmount} 데미지를 입혔습니다.");
                    cardUsed = true;
                }
                else
                {
                    Debug.Log("이 카드는 적에게 사용할 수 없습니다.");
                }
            }
        }
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity, playerLayer))
        {
            CharacterStats playerStats = hit.collider.GetComponent<CharacterStats>();

            if(playerStats != null)
            {
                if (cardData.cardType == CardData.CardType.Heal)
                {
                    //힐카드면 회복하기
                    playerStats.Heal(cardData.effectAmount);
                    Debug.Log($"{cardData.cardName} 카드로 플레이어의 체력을 {cardData.effectAmount} 회복했습니다.");
                    cardUsed = true;
                }
                else
                {
                    Debug.Log("이 카드는 플레이어에게 사용할 수 없습니다.");
                }
            }
        }
        else if(CardManager.Instance != null)
        {
            //버린 카드 더미 근처에 드롭했는지 검사
            float distToDiscard = Vector3.Distance(transform.position, CardManager.Instance.discardPosition.position);
            if(distToDiscard > 2.0f)
            {
                CardManager.Instance.DiscardCard(cardIndex);
                return;
            }
        }
        //카드를 사용하지 않으면 원래 위치로 되돌리기
        if (!cardUsed)
        {
            transform.position = originalPosition;
            CardManager.Instance.ArrangeHand();
        }
        else
        {
            //카드를 사용했다면 버린 카드로 이동
            if (CardManager.Instance != null)
                CardManager.Instance.DiscardCard(cardIndex);

            //카드 사용시 마나 소모 (카드가 성공적으로 사용된 후 추가)
            CardManager.Instance.playerStats.UseMana(cardData.manaCost);
            Debug.Log($"마나를 {cardData.manaCost} 사용 했습니다.");
        }

    }
}

