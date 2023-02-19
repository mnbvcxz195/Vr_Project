using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurotialGuide
{
    public string imgGuide;
    public string txtAbout;
    public bool isCheck;

    public TurotialGuide(string imgGuide, string txtAbout, bool isCheck)
    {
        this.imgGuide = imgGuide;
        this.txtAbout = txtAbout;
        this.isCheck = isCheck;
    }
}

public class GuideList : MonoBehaviour
{
    #region instance

    private static GuideList instance = null;

    public static GuideList GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("@GuideList");
            instance = go.AddComponent<GuideList>();

            DontDestroyOnLoad(go);
        }
        return instance;

    }

    #endregion

    private TurotialGuide[] guides;

    private void Start()
    {
        guides = new TurotialGuide[7];

        guides[0] = new TurotialGuide("�κ� ����", "Any Key�� ���� �κ��丮 â�� Ȱ��ȭ�� �� �ֽ��ϴ�.", false); //o
        guides[1] = new TurotialGuide("������ �����ϱ�", "�κ��丮�� �ִ� �������� Ŭ���ϸ� �ش� �������� ���(����)�� �� �ֽ��ϴ�", false); //o
        guides[2] = new TurotialGuide("��� ã��", "���� ������Ʈ�� ��ȣ�ۿ��ذ��� �� �ȿ� ������ ��� �������� ã�� �� �ֽ��ϴ�.", false); //o
        guides[3] = new TurotialGuide("���� ã��", "���� ���� ���ؼ� Ư���� �������� ����ؾ��� �� �����ϴ�.", false); //o
        guides[4] = new TurotialGuide("������ ȹ��", "ȹ���� �������� �κ��丮 â���� Ȯ���� �� �ֽ��ϴ�.", false); //o
        guides[5] = new TurotialGuide("������ ����", "������ ���� â�� ��� �������� �÷� ���ο� �������� ������ �� �ֽ��ϴ�.", false); //o
        guides[6] = new TurotialGuide("���� ���", "���۵� ���� �������� ����� ���� �� �� �ֽ��ϴ�.", false); //o
    }

    public TurotialGuide[] Guides
    {
        get
        {
            return guides;
        }
    }

    public void SetCheck(int num)
    {
        guides[num].isCheck = true;
    }
}

public class UITurotialGuide : MonoBehaviour
{
    public GameObject objGuide;
    public Text txtGuide;
    public Button btnConfirm;
    GuideList guideList;

    [SerializeField] private Transform inventoryParent;
    [SerializeField] private Transform UIPos;
    [SerializeField] private Transform UIRot;

    void Start()
    {
        btnConfirm.onClick.AddListener(OnConfirm);
        guideList = GuideList.GetInstance();
    }

    public void OnConfirm()
    {
        objGuide.SetActive(false);
    }

    public void OnTrigger(int num)
    {
        if (guideList.Guides[num].isCheck != true)
        {
            inventoryParent.position = UIPos.position;
            inventoryParent.rotation = UIRot.rotation;

            objGuide.SetActive(true);
            txtGuide.text = $"{guideList.Guides[num].txtAbout}";
            guideList.SetCheck(num);
        }
    }
}