using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InGame : UI_Scene
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
    }

    void Awake()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));


        SpawnHero();
        SpawnEnemy();
        // 여기서 코루틴 설정 후 n초 뒤에 배틀 스타트 하기

        StartCoroutine(Co_BattleStart());
    }

    void SpawnHero()
    {
        for (int i = 1; i < 6; i++)
        {
            int heroId = Managers.GetPlayer.HeroComp.HeroFormation[i];
            if (heroId != -1)
            {
                GameObject hero = Managers.Resource.Instantiate($"Heros/{heroId}");
                GameObjects enumObj = (GameObjects)(i - 1);
                hero.transform.position = Get<GameObject>((int)enumObj).transform.position;
                hero.transform.position = new Vector3(hero.transform.position.x - 10f, hero.transform.position.y, hero.transform.position.z);
                hero.transform.localScale = new Vector3(-1f, 1f, 1f);
                hero.transform.localScale *= 2;
                hero.GetComponent<Creature>().MoveTo(Get<GameObject>((int)enumObj).transform);

                
                Managers.Battle.Heros[i] = hero;
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

                enemy.GetComponent<Creature>().MoveTo(Get<GameObject>((int)enumObj).transform);
                Managers.Battle.Enemies[1 + i] = enemy;
            }
        }
    }

    IEnumerator Co_BattleStart()
    {
        yield return new WaitForSeconds(3.5f);
        Managers.Battle.BeginBattle();
        yield break;
    }
}
