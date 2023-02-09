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


    private ItemBase _item;                            //ȹ���� ������
    private int itemCount;                             //ȹ���� ������ ����

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
            if (_item != null)                          //�������� ���� null�� �ƴ϶��
            {                                           //������ �̹��� ����ȭ
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
                    txtCount.text = $"{itemCount}";       //�������� Ÿ���� ����, ��Ÿ�� �ƴ϶��
                }                                         //ī��Ʈ �̹��� Ȱ��ȭ
            }
            else
            {                                             //�������� ���� null�̸�
                imgItem.color = new Color(1, 1, 1, 0);    //������ �̹��� �񰡽�ȭ
                itemCount = 0;
                goCount.SetActive(false);
            }
        }
    }
}