using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class TurotialGuide
{
    public string imgGuide;
    public string txtAbout;
    public string txtAbout2;
    public bool isCheck;

    public TurotialGuide(string imgGuide, string txtAbout, string txtAbout2, bool isCheck)
    {
        this.imgGuide = imgGuide;
        this.txtAbout = txtAbout;
        this.txtAbout2 = txtAbout2;
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

        guides[0] = new TurotialGuide("�κ� ����", "�������. \nBŰ�� ���� �κ��丮 â�� Ȱ��ȭ�� �� �ֽ��ϴ�. \n\n�κ��丮�� �ִ� �������� Ŭ���ϸ� ", "�ش� �������� ���(����)�� �� �ֽ��ϴ�.\n������ �������� ��������: GŰ", false); //o
        guides[1] = new TurotialGuide("������ �����ϱ�", ".", "������ �������� ��������: GŰ", false); //o
        guides[2] = new TurotialGuide("��� ã��", "���� ������Ʈ�� ��ȣ�ۿ��ذ��� �� �ȿ� ������ ��� �������� ã�� �� �ֽ��ϴ�.", "������ �ݱ�/��������: GŰ", false); //o
        guides[3] = new TurotialGuide("���� ã��", "���� ���� ���ؼ� Ư���� �������� ����ؾ��� �� �����ϴ�.", "", false); //o
        guides[4] = new TurotialGuide("������ ȹ��", "ȹ���� �������� �κ��丮 â���� Ȯ���� �� �ֽ��ϴ�.", "", false); //o
        guides[5] = new TurotialGuide("������ ����", "������ ���� â�� ��� �������� �÷� ���ο� �������� ������ �� �ֽ��ϴ�.", "��� ������ �ø���: �巡�� �� ���", false); //o
        guides[6] = new TurotialGuide("���� ���", "���۵� ���� �������� ����� ���� �� �� �ֽ��ϴ�.", "", false);  //o
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
    public Text txtGuide2;
    public Button btnConfirm;
    GuideList guideList;
    Inventory _Inventory;

    public XRRayInteractor ryInteractor;
    public XRDirectInteractor drInteractor;

    [SerializeField] private Transform inventoryParent;
    [SerializeField] private Transform UIPos;
    [SerializeField] private Transform UIRot;

    void Start()
    {
        btnConfirm.onClick.AddListener(OnConfirm);
        guideList = GuideList.GetInstance();

        _Inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
    }

    public void OnConfirm()
    {
        objGuide.SetActive(false);
        if (!_Inventory.inventoryActivated)
        {
            ryInteractor.gameObject.SetActive(false);
            drInteractor.gameObject.SetActive(true);
        }
    }

    public void OnTrigger(int num)
    {
        if (guideList.Guides[num].isCheck != true)
        {
            inventoryParent.position = UIPos.position;
            inventoryParent.rotation = UIRot.rotation;

            objGuide.SetActive(true);
            txtGuide.text = $"{guideList.Guides[num].txtAbout}";
            txtGuide2.text = $"{guideList.Guides[num].txtAbout2}";
            guideList.SetCheck(num);
            drInteractor.gameObject.SetActive(false);
            ryInteractor.gameObject.SetActive(true);
        }
    }
}
