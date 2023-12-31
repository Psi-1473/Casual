using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InGameMask : UI_Popup
{
    enum GameObjects
    {
        H_Transform_1,
        H_Transform_2,
        H_Transform_3,
        H_Transform_4,
        H_Transform_5,
        E_Transform_1,
        E_Transform_2,
        E_Transform_3,
        E_Transform_4,
        E_Transform_5,
        Transform_Center,
    }

    void Awake()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GetComponent<Canvas>().sortingOrder = 6;
        SpawnHero();
        SpawnEnemy();
        // 여기서 코루틴 설정 후 n초 뒤에 배틀 스타트 하기

        StartCoroutine(Co_BattleStart());
    }

    void SpawnHero()
    {
        for (int i = 1; i < 6; i++)
        {
            Hero hero = Managers.GetPlayer.HeroComp.HeroFormation[i];
            if (hero != null)
            {
                GameObject obj = Managers.Resource.Instantiate($"Heros/{hero.Id}");
                GameObjects enumObj = (GameObjects)(i - 1);
                obj.transform.position = Get<GameObject>((int)enumObj).transform.position;
                obj.transform.position = new Vector3(obj.transform.position.x - 10f, obj.transform.position.y, obj.transform.position.z);
                obj.transform.localScale = new Vector3(-1f, 1f, 1f);
                obj.transform.localScale *= 2;


                obj.GetComponent<AIController>().SetCreatureStat(hero, hero.Id, Get<GameObject>((int)enumObj).transform, i, Get<GameObject>((int)GameObjects.Transform_Center).transform);
                Managers.Battle.Heros[i] = obj;
                // Battle 매니저에서 세팅
            }
        }
    }

    void SpawnEnemy()
    {
        int chapter = Managers.Battle.NowChapter;
        int stage = Managers.Battle.NowStage;
        StageInfo _sInfo = Managers.Data.StageDicts[chapter][stage];

        List<int> enemies = new List<int>();

        enemies.Add(_sInfo.frontTop);
        enemies.Add(_sInfo.frontBottom);
        enemies.Add(_sInfo.backTop);
        enemies.Add(_sInfo.backMiddle);
        enemies.Add(_sInfo.backBottom);

        for (int i = 0; i < 5; i++)
        {
            int enemyId = enemies[i];
            if (enemyId != -1)
            {
                GameObject enemy = Managers.Resource.Instantiate($"Enemies/{enemyId}");
                GameObjects enumObj = (GameObjects)(i + 5);
                enemy.transform.position = Get<GameObject>((int)enumObj).transform.position;
                enemy.transform.position = new Vector3(enemy.transform.position.x + 10f, enemy.transform.position.y, enemy.transform.position.z);
                enemy.transform.localScale *= 2;

                enemy.GetComponent<AIController>().SetCreatureStat(null, enemyId, Get<GameObject>((int)enumObj).transform, i + 1, Get<GameObject>((int)GameObjects.Transform_Center).transform);
                Managers.Battle.Enemies[1 + i] = enemy;
            }
        }
    }

    IEnumerator Co_BattleStart()
    {
        yield return new WaitForSeconds(2.5f);
        Managers.Battle.BeginBattle();
        yield break;
    }
}
