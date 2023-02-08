using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UITitle : MonoBehaviour
{
    [SerializeField] Text gameStory;
    [SerializeField] Button bntGameStory;
    int storyCount;

    void Start()
    {
        gameStory.DOText("�׻� Ž�谡�� �����ؿԴ�..", 2f);
        bntGameStory.onClick.AddListener(gameStorynext);
    }

    void Update()
    {
        
    }

    void gameStorynext()
    {
        switch (storyCount)
        {
            case 0:
                TitleText("�����ϸ� ���� �i�ٺ��� ����� ������ ���� �ְ��� Ž�谡�� �θ�������ߴ�.");
                storyCount += 1;
                break;
            case 1:
                TitleText("�׻� ���ο� �������� ã���� ���ϸ��� �����ϱ�����ߴ�.");
                storyCount += 1;
                break;
            case 2:
                TitleText("���� �׻� ������ �����ؿ԰� �̹� ������ ���Ӱ� �߰ߵ� �������...");
                storyCount += 1;
                break;
            case 3:
                TitleText("������ �ʹ����� �����Ͽ��� �׻� �ؿ������̿��⿡");
                storyCount += 1;
                break;
            case 4:
                TitleText("�̹����� ���� ������ ������ ������ ���縦 ���������̿���.");
                storyCount += 1;
                break;
            case 5:
                TitleText("������....");
                storyCount += 1;
                break;
            case 6:
                ScenesManager.GetInstance().ChangeSceneTutorial();

                break;

        }
    }
    
    void TitleText(string story)
    {
        gameStory.DOText(story, 2f);
    }
}
