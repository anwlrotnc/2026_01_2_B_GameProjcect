using UnityEngine;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;

public class ItemDataLoader : MonoBehaviour
{
    [SerializeField]
    private string jsonFileName = "items";                  //Resource 폴더에서 가져올 JSON 파일 이름

    private List<ItemData> itemList;

    void Start()
    {
        LoadItemData();
    }


    //한글 인코딩을 위한 헬퍼 함수
    private string EncodeKorean(string text)
    {
        if (string.IsNullOrEmpty(text)) return "";       //텍스트가 NULL 갑이면 함수를 끝낸다.
        byte[] bytes = Encoding.Default.GetBytes(text);     //string 을 Byte 배열로 변환환 후
        return Encoding.UTF8.GetString(bytes);
    }

    void LoadItemData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonFileName);               //TextAsset 형태로 Json 파일을 로딩한다.

        if (jsonFile != null)
        {
            //원본 텍스트에서 UTF8로 변환 처리
            byte[] bytes = Encoding.Default.GetBytes(jsonFile.text);
            string currentText = Encoding.UTF8.GetString(bytes);

            //변환 된 텍스트 사용
            itemList = JsonConvert.DeserializeObject<List<ItemData>>(currentText);

            Debug.Log($"로드된 아이템 수 : {itemList.Count}");

            foreach(var item in itemList)
            {
                Debug.Log($"아이템 : {EncodeKorean(item.itemName)}, 설명 : {EncodeKorean(item.description)}");
            }
        }
        else
        {
            Debug.LogError($"JSON 파일을 찾을 수 없습니다. : {jsonFileName}");
        }
    }
}
