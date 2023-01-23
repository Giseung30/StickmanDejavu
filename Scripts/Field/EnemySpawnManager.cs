using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemySpawnManager : MonoBehaviour
{
    [Header("Component")]
    public Transform spawnEnemiesTransform; //Spawn Enemies의 Transform 컴포넌트
    public Transform[] spawnPosTransforms; //Spawn Pos의 Transform 컴포넌트들

    [Header("Level-1")]
    public GameObject Slime_1;
    public GameObject Crow_1;
    public GameObject Rat_1;
    public GameObject Bat_1;
    public GameObject Spider_1;
    public GameObject Beholder_1;
    public GameObject Worm_1;
    public GameObject Orc_Cub_1;
    public GameObject Orc_Katana_1;
    public GameObject Orc_Polearm_1;
    public GameObject Orc_Bow_1;
    public GameObject Cyclope_Cub_1;
    public GameObject Cyclope_Katana_1;
    public GameObject Cyclope_Polearm_1;
    public GameObject Cyclope_Bow_1;
    public GameObject Demon_Cub_1;
    public GameObject Demon_Katana_1;
    public GameObject Demon_Polearm_1;
    public GameObject Demon_Bow_1;
    public GameObject Goblin_Cub_1;
    public GameObject Goblin_Katana_1;
    public GameObject Goblin_Polearm_1;
    public GameObject Goblin_Bow_1;
    public GameObject Zombie_Cub_1;
    public GameObject Zombie_Katana_1;
    public GameObject Zombie_Polearm_1;
    public GameObject Zombie_Bow_1;
    public GameObject Ghost_Cub_1;
    public GameObject Ghost_Katana_1;
    public GameObject Ghost_Polearm_1;
    public GameObject Ghost_Bow_1;
    public GameObject Skeleton_Dagger_1;
    public GameObject Skeleton_Sword_1;
    public GameObject Skeleton_Spear_1;
    public GameObject Skeleton_Bow_1;

    [Header("Level-2")]
    public GameObject Slime_2;
    public GameObject Crow_2;
    public GameObject Rat_2;
    public GameObject Bat_2;
    public GameObject Spider_2;
    public GameObject Beholder_2;
    public GameObject Worm_2;
    public GameObject Orc_Cub_2;
    public GameObject Orc_Katana_2;
    public GameObject Orc_Polearm_2;
    public GameObject Orc_Bow_2;
    public GameObject Cyclope_Cub_2;
    public GameObject Cyclope_Katana_2;
    public GameObject Cyclope_Polearm_2;
    public GameObject Cyclope_Bow_2;
    public GameObject Demon_Cub_2;
    public GameObject Demon_Katana_2;
    public GameObject Demon_Polearm_2;
    public GameObject Demon_Bow_2;
    public GameObject Goblin_Cub_2;
    public GameObject Goblin_Katana_2;
    public GameObject Goblin_Polearm_2;
    public GameObject Goblin_Bow_2;
    public GameObject Zombie_Cub_2;
    public GameObject Zombie_Katana_2;
    public GameObject Zombie_Polearm_2;
    public GameObject Zombie_Bow_2;
    public GameObject Ghost_Cub_2;
    public GameObject Ghost_Katana_2;
    public GameObject Ghost_Polearm_2;
    public GameObject Ghost_Bow_2;
    public GameObject Skeleton_Dagger_2;
    public GameObject Skeleton_Sword_2;
    public GameObject Skeleton_Spear_2;
    public GameObject Skeleton_Bow_2;

    [Header("Level-3")]
    public GameObject Slime_3;
    public GameObject Crow_3;
    public GameObject Rat_3;
    public GameObject Bat_3;
    public GameObject Spider_3;
    public GameObject Beholder_3;
    public GameObject Worm_3;
    public GameObject Orc_Cub_3;
    public GameObject Orc_Katana_3;
    public GameObject Orc_Polearm_3;
    public GameObject Orc_Bow_3;
    public GameObject Cyclope_Cub_3;
    public GameObject Cyclope_Katana_3;
    public GameObject Cyclope_Polearm_3;
    public GameObject Cyclope_Bow_3;
    public GameObject Demon_Cub_3;
    public GameObject Demon_Katana_3;
    public GameObject Demon_Polearm_3;
    public GameObject Demon_Bow_3;
    public GameObject Goblin_Cub_3;
    public GameObject Goblin_Katana_3;
    public GameObject Goblin_Polearm_3;
    public GameObject Goblin_Bow_3;
    public GameObject Zombie_Cub_3;
    public GameObject Zombie_Katana_3;
    public GameObject Zombie_Polearm_3;
    public GameObject Zombie_Bow_3;
    public GameObject Ghost_Cub_3;
    public GameObject Ghost_Katana_3;
    public GameObject Ghost_Polearm_3;
    public GameObject Ghost_Bow_3;
    public GameObject Skeleton_Dagger_3;
    public GameObject Skeleton_Sword_3;
    public GameObject Skeleton_Spear_3;
    public GameObject Skeleton_Bow_3;

    [Header("Cache")]
    private Queue<GameObject> enemyQueue = new Queue<GameObject>(); //Enemy 적들을 담는 큐
    private int spawnMax; //최대 생성
    private float spawnDelay; //생성 지연시간

    private void Start()
    {
        SetLevel();
        StartCoroutine(SpawnEnemy());
    }

    /* Enemy를 생성하는 코루틴 함수 */
    private IEnumerator SpawnEnemy()
    {
        float timer = 0f;

        while (enemyQueue.Count > 0) //생성할 Enemy가 남아있으면
        {
            while ((timer > 0f || spawnEnemiesTransform.childCount >= spawnMax) && spawnEnemiesTransform.childCount != 0) //Timer가 0이 안되거나 생성된 Enemy가 최대 생성을 넘기면 대기
            {
                timer -= Time.deltaTime;
                yield return null;
            }

            GameObject enemyClone = Instantiate(enemyQueue.Dequeue()); //Enemy 복사
            enemyClone.transform.position = spawnPosTransforms[Random.Range(0, spawnPosTransforms.Length)].position; //생성 위치 지정
            enemyClone.transform.SetParent(spawnEnemiesTransform); //부모 설정
            enemyClone.SetActive(true); //Enemy 활성화

            timer = spawnDelay; //타이머 지정

            yield return null;
        }

        while (spawnEnemiesTransform.childCount > 0) yield return null; //아직 생성된 Enemy가 있으면 대기

        StartCoroutine(FieldManager.fieldManager.FinishStage(true)); //단계 종료
    }

    /* 레벨을 설정하는 함수 */
    private void SetLevel()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Stage" + '/' + WaitingInfo.stage % Definition.maxStage); //파일 읽기
        StringReader stringReader = new StringReader(textAsset.text); //텍스트 변환

        spawnMax = int.Parse(stringReader.ReadLine()); //최대 생성 지정
        spawnDelay = float.Parse(stringReader.ReadLine()); //생성 지연 시간 지정

        while (stringReader.Peek() > 0) //남은 줄이 있으면
        {
            string enemy = stringReader.ReadLine();
            switch (enemy)
            {
                case "Slime_1":
                    enemyQueue.Enqueue(Slime_1);
                    break;
                case "Crow_1":
                    enemyQueue.Enqueue(Crow_1);
                    break;
                case "Rat_1":
                    enemyQueue.Enqueue(Rat_1);
                    break;
                case "Bat_1":
                    enemyQueue.Enqueue(Bat_1);
                    break;
                case "Spider_1":
                    enemyQueue.Enqueue(Spider_1);
                    break;
                case "Beholder_1":
                    enemyQueue.Enqueue(Beholder_1);
                    break;
                case "Worm_1":
                    enemyQueue.Enqueue(Worm_1);
                    break;
                case "Orc_Cub_1":
                    enemyQueue.Enqueue(Orc_Cub_1);
                    break;
                case "Orc_Katana_1":
                    enemyQueue.Enqueue(Orc_Katana_1);
                    break;
                case "Orc_Polearm_1":
                    enemyQueue.Enqueue(Orc_Polearm_1);
                    break;
                case "Orc_Bow_1":
                    enemyQueue.Enqueue(Orc_Bow_1);
                    break;
                case "Cyclope_Cub_1":
                    enemyQueue.Enqueue(Cyclope_Cub_1);
                    break;
                case "Cyclope_Katana_1":
                    enemyQueue.Enqueue(Cyclope_Katana_1);
                    break;
                case "Cyclope_Polearm_1":
                    enemyQueue.Enqueue(Cyclope_Polearm_1);
                    break;
                case "Cyclope_Bow_1":
                    enemyQueue.Enqueue(Cyclope_Bow_1);
                    break;
                case "Demon_Cub_1":
                    enemyQueue.Enqueue(Demon_Cub_1);
                    break;
                case "Demon_Katana_1":
                    enemyQueue.Enqueue(Demon_Katana_1);
                    break;
                case "Demon_Polearm_1":
                    enemyQueue.Enqueue(Demon_Polearm_1);
                    break;
                case "Demon_Bow_1":
                    enemyQueue.Enqueue(Demon_Bow_1);
                    break;
                case "Goblin_Cub_1":
                    enemyQueue.Enqueue(Goblin_Cub_1);
                    break;
                case "Goblin_Katana_1":
                    enemyQueue.Enqueue(Goblin_Katana_1);
                    break;
                case "Goblin_Polearm_1":
                    enemyQueue.Enqueue(Goblin_Polearm_1);
                    break;
                case "Goblin_Bow_1":
                    enemyQueue.Enqueue(Goblin_Bow_1);
                    break;
                case "Zombie_Cub_1":
                    enemyQueue.Enqueue(Zombie_Cub_1);
                    break;
                case "Zombie_Katana_1":
                    enemyQueue.Enqueue(Zombie_Katana_1);
                    break;
                case "Zombie_Polearm_1":
                    enemyQueue.Enqueue(Zombie_Polearm_1);
                    break;
                case "Zombie_Bow_1":
                    enemyQueue.Enqueue(Zombie_Bow_1);
                    break;
                case "Ghost_Cub_1":
                    enemyQueue.Enqueue(Ghost_Cub_1);
                    break;
                case "Ghost_Katana_1":
                    enemyQueue.Enqueue(Ghost_Katana_1);
                    break;
                case "Ghost_Polearm_1":
                    enemyQueue.Enqueue(Ghost_Polearm_1);
                    break;
                case "Ghost_Bow_1":
                    enemyQueue.Enqueue(Ghost_Bow_1);
                    break;
                case "Skeleton_Dagger_1":
                    enemyQueue.Enqueue(Skeleton_Dagger_1);
                    break;
                case "Skeleton_Sword_1":
                    enemyQueue.Enqueue(Skeleton_Sword_1);
                    break;
                case "Skeleton_Spear_1":
                    enemyQueue.Enqueue(Skeleton_Spear_1);
                    break;
                case "Skeleton_Bow_1":
                    enemyQueue.Enqueue(Skeleton_Bow_1);
                    break;

                case "Slime_2":
                    enemyQueue.Enqueue(Slime_2);
                    break;
                case "Crow_2":
                    enemyQueue.Enqueue(Crow_2);
                    break;
                case "Rat_2":
                    enemyQueue.Enqueue(Rat_2);
                    break;
                case "Bat_2":
                    enemyQueue.Enqueue(Bat_2);
                    break;
                case "Spider_2":
                    enemyQueue.Enqueue(Spider_2);
                    break;
                case "Beholder_2":
                    enemyQueue.Enqueue(Beholder_2);
                    break;
                case "Worm_2":
                    enemyQueue.Enqueue(Worm_2);
                    break;
                case "Orc_Cub_2":
                    enemyQueue.Enqueue(Orc_Cub_2);
                    break;
                case "Orc_Katana_2":
                    enemyQueue.Enqueue(Orc_Katana_2);
                    break;
                case "Orc_Polearm_2":
                    enemyQueue.Enqueue(Orc_Polearm_2);
                    break;
                case "Orc_Bow_2":
                    enemyQueue.Enqueue(Orc_Bow_2);
                    break;
                case "Cyclope_Cub_2":
                    enemyQueue.Enqueue(Cyclope_Cub_2);
                    break;
                case "Cyclope_Katana_2":
                    enemyQueue.Enqueue(Cyclope_Katana_2);
                    break;
                case "Cyclope_Polearm_2":
                    enemyQueue.Enqueue(Cyclope_Polearm_2);
                    break;
                case "Cyclope_Bow_2":
                    enemyQueue.Enqueue(Cyclope_Bow_2);
                    break;
                case "Demon_Cub_2":
                    enemyQueue.Enqueue(Demon_Cub_2);
                    break;
                case "Demon_Katana_2":
                    enemyQueue.Enqueue(Demon_Katana_2);
                    break;
                case "Demon_Polearm_2":
                    enemyQueue.Enqueue(Demon_Polearm_2);
                    break;
                case "Demon_Bow_2":
                    enemyQueue.Enqueue(Demon_Bow_2);
                    break;
                case "Goblin_Cub_2":
                    enemyQueue.Enqueue(Goblin_Cub_2);
                    break;
                case "Goblin_Katana_2":
                    enemyQueue.Enqueue(Goblin_Katana_2);
                    break;
                case "Goblin_Polearm_2":
                    enemyQueue.Enqueue(Goblin_Polearm_2);
                    break;
                case "Goblin_Bow_2":
                    enemyQueue.Enqueue(Goblin_Bow_2);
                    break;
                case "Zombie_Cub_2":
                    enemyQueue.Enqueue(Zombie_Cub_2);
                    break;
                case "Zombie_Katana_2":
                    enemyQueue.Enqueue(Zombie_Katana_2);
                    break;
                case "Zombie_Polearm_2":
                    enemyQueue.Enqueue(Zombie_Polearm_2);
                    break;
                case "Zombie_Bow_2":
                    enemyQueue.Enqueue(Zombie_Bow_2);
                    break;
                case "Ghost_Cub_2":
                    enemyQueue.Enqueue(Ghost_Cub_2);
                    break;
                case "Ghost_Katana_2":
                    enemyQueue.Enqueue(Ghost_Katana_2);
                    break;
                case "Ghost_Polearm_2":
                    enemyQueue.Enqueue(Ghost_Polearm_2);
                    break;
                case "Ghost_Bow_2":
                    enemyQueue.Enqueue(Ghost_Bow_2);
                    break;
                case "Skeleton_Dagger_2":
                    enemyQueue.Enqueue(Skeleton_Dagger_2);
                    break;
                case "Skeleton_Sword_2":
                    enemyQueue.Enqueue(Skeleton_Sword_2);
                    break;
                case "Skeleton_Spear_2":
                    enemyQueue.Enqueue(Skeleton_Spear_2);
                    break;
                case "Skeleton_Bow_2":
                    enemyQueue.Enqueue(Skeleton_Bow_2);
                    break;

                case "Slime_3":
                    enemyQueue.Enqueue(Slime_3);
                    break;
                case "Crow_3":
                    enemyQueue.Enqueue(Crow_3);
                    break;
                case "Rat_3":
                    enemyQueue.Enqueue(Rat_3);
                    break;
                case "Bat_3":
                    enemyQueue.Enqueue(Bat_3);
                    break;
                case "Spider_3":
                    enemyQueue.Enqueue(Spider_3);
                    break;
                case "Beholder_3":
                    enemyQueue.Enqueue(Beholder_3);
                    break;
                case "Worm_3":
                    enemyQueue.Enqueue(Worm_3);
                    break;
                case "Orc_Cub_3":
                    enemyQueue.Enqueue(Orc_Cub_3);
                    break;
                case "Orc_Katana_3":
                    enemyQueue.Enqueue(Orc_Katana_3);
                    break;
                case "Orc_Polearm_3":
                    enemyQueue.Enqueue(Orc_Polearm_3);
                    break;
                case "Orc_Bow_3":
                    enemyQueue.Enqueue(Orc_Bow_3);
                    break;
                case "Cyclope_Cub_3":
                    enemyQueue.Enqueue(Cyclope_Cub_3);
                    break;
                case "Cyclope_Katana_3":
                    enemyQueue.Enqueue(Cyclope_Katana_3);
                    break;
                case "Cyclope_Polearm_3":
                    enemyQueue.Enqueue(Cyclope_Polearm_3);
                    break;
                case "Cyclope_Bow_3":
                    enemyQueue.Enqueue(Cyclope_Bow_3);
                    break;
                case "Demon_Cub_3":
                    enemyQueue.Enqueue(Demon_Cub_3);
                    break;
                case "Demon_Katana_3":
                    enemyQueue.Enqueue(Demon_Katana_3);
                    break;
                case "Demon_Polearm_3":
                    enemyQueue.Enqueue(Demon_Polearm_3);
                    break;
                case "Demon_Bow_3":
                    enemyQueue.Enqueue(Demon_Bow_3);
                    break;
                case "Goblin_Cub_3":
                    enemyQueue.Enqueue(Goblin_Cub_3);
                    break;
                case "Goblin_Katana_3":
                    enemyQueue.Enqueue(Goblin_Katana_3);
                    break;
                case "Goblin_Polearm_3":
                    enemyQueue.Enqueue(Goblin_Polearm_3);
                    break;
                case "Goblin_Bow_3":
                    enemyQueue.Enqueue(Goblin_Bow_3);
                    break;
                case "Zombie_Cub_3":
                    enemyQueue.Enqueue(Zombie_Cub_3);
                    break;
                case "Zombie_Katana_3":
                    enemyQueue.Enqueue(Zombie_Katana_3);
                    break;
                case "Zombie_Polearm_3":
                    enemyQueue.Enqueue(Zombie_Polearm_3);
                    break;
                case "Zombie_Bow_3":
                    enemyQueue.Enqueue(Zombie_Bow_3);
                    break;
                case "Ghost_Cub_3":
                    enemyQueue.Enqueue(Ghost_Cub_3);
                    break;
                case "Ghost_Katana_3":
                    enemyQueue.Enqueue(Ghost_Katana_3);
                    break;
                case "Ghost_Polearm_3":
                    enemyQueue.Enqueue(Ghost_Polearm_3);
                    break;
                case "Ghost_Bow_3":
                    enemyQueue.Enqueue(Ghost_Bow_3);
                    break;
                case "Skeleton_Dagger_3":
                    enemyQueue.Enqueue(Skeleton_Dagger_3);
                    break;
                case "Skeleton_Sword_3":
                    enemyQueue.Enqueue(Skeleton_Sword_3);
                    break;
                case "Skeleton_Spear_3":
                    enemyQueue.Enqueue(Skeleton_Spear_3);
                    break;
                case "Skeleton_Bow_3":
                    enemyQueue.Enqueue(Skeleton_Bow_3);
                    break;
            }
        }
    }
}