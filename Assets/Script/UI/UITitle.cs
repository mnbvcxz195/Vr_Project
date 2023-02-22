using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UITitle : MonoBehaviour
{
    [SerializeField] Text gameStory;
    [SerializeField] Button bntGameStory;
    [SerializeField] AudioSource sfx;

    int storyCount;

    void Start()
    {
        gameStory.DOText("�׻� Ž�谡�� �����ؿԴ�..", 2f);
        bntGameStory.onClick.AddListener(gameStorynext);
        AudioManager.GetInstance().SfxPlay(sfx, 0);

    }

    void gameStorynext()
    {
        switch (storyCount)
        {
            case 0:
                TitleText("����� Ž���� �޲ٴ� ����̿��� ���� \n�׻� �縷�� ���� ���� ������ ��� �Ķ���� �׵��� �������� �̾߱⿡ �ŷ�Ǿ� �־���.");
                TitleCountUP();
                break;
            case 1:
                TitleText("���� �׻� ������ Ž�谡���� �����븦 ���� �������� ����� �߰��ϱ⸦ �����ߴ�.");
                TitleCountUP();
                break;
            case 2:
                TitleText("������ �ð����귯 ���� ������� ���� ���� ���� �Ҿ���� ����� ����� �Ǿ���־���.");
                TitleCountUP();
                break;
            case 3:
                TitleText("�׷��� ��³� ��� ������� �Ⱦ�ٰ�\n���϶� �縷 ����� ���� �ִ� ��� ������ ��ġ�� �����ִ� ������ �쿬�� �߰��ߴ�.\n������ ���� ��������� ���� �˷��� ��ŭ �����ߴ�.");
                TitleCountUP();
                break;
            case 4:
                TitleText("��ĥ ���� ���� �ۿ��ϴ� �縷�� �����ϸ� ��������� �ϰ� ������ ���󰬴�.\n ���� ��� ���� ������ ������ �𷡾���� �ο����� \n�����ִ� ���� ���迡 ���� ������ ���� ��� �������״�.");
                TitleCountUP();
                break;
            case 5:
                TitleText("��ħ��, �縷���� ���� ���� ��Ȳ�� ��, ���� ������ �Ա��� ���Ҵ�.\n���� ��� ������ �������鼭 ��а� �η������� ���� �� �־���. ����� �β��� ���γ��� ����\n������ �������ڿ� �ŵ��� ����� ������ �־���.");
                TitleCountUP();
                break;
            case 6:
                TitleText("���� �������� �� ���� Ž���ϸ鼭. \nŽ�谡�ν��� ��ܽɸ��� ����־���.");
                TitleCountUP();
                break;
            case 7:
                TitleText("������ ��ġ �� �մ԰��� ���� �������ִ°�ó��\n���� �ֻ������� �ε��Ͽ� �־���.");
                TitleCountUP();
                break;
            case 8:
                TitleText("�翬�� ���� �̹������� Ž�縦 ������ ���� �����ϴ� Ž�谡������� ������ Ž�谡�� Ī�۹��������̿���.");
                TitleCountUP();
                break;
            case 9:
                TitleText("�׷���...");
                TitleCountUP();
                break;
            case 10:
                ScenesManager.GetInstance().ChangeScene(Scene.Tutorial);
                break;


        }
    }

    void TitleText(string story)
    {
        gameStory.DOText(story, 2f);
    }
    void TitleCountUP()
    {
        AudioManager.GetInstance().SfxPlay(sfx, 0);
        storyCount += 1;
    }
}
