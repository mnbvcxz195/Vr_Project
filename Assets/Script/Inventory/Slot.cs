using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image imgItem;
    [SerializeField] private GameObject goCount;
    [SerializeField] private Text txtCount;
    public Button btnUsed;


    private ItemBase _item;                            //획득한 아이템
    private int itemCount;                             //획득한 아이템 개수

    public int ItemCount                               //itemCount get set
    {
        get => itemCount;
        set => itemCount = value;
    }

    public ItemBase Item
    {
        get                                             //_item get
        { return _item; }

        set                                             //_item set
        {
            _item = value;
            if (_item != null)                          //아이템의 값이 null이 아니라면
            {                                           //아이템 이미지 가시화
                imgItem.sprite = _item.ItemImage;
                imgItem.color = new Color(1, 1, 1, 1);
                //itemCount += 1;

                if (_item.Type == ItemType.Weapon)
                    goCount.SetActive(false);

                else if (_item.Type == ItemType.ETC)
                    goCount.SetActive(false);

                else
                {
                    goCount.SetActive(true);
                    txtCount.text = $"{itemCount}";       //아이템의 타입이 무기, 기타가 아니라면
                }                                         //카운트 이미지 활성화
            }
            else
            {                                             //아이템의 값이 null이면
                imgItem.color = new Color(1, 1, 1, 0);    //아이템 이미지 비가시화
                itemCount = 0;
                goCount.SetActive(false);
            }
        }
    }
}