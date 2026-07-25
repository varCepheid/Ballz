using System.Collections.Generic;
using UnityEngine;

public class SpawnBlocksAndTokens : MonoBehaviour
{
  public GameManager gameManager;
  public GameObject blockPrefab;
  public GameObject tokenPrefab;
  private GameObject thisObject;
  public List<GameObject> spawnedBlocks;
  public List<GameObject> spawnedTokens;

  private readonly System.Random rand = new();
  private int levelNumber;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    spawnedBlocks = new();
    spawnedTokens = new();
    levelNumber = 1;
  }

  public void PrepareNextLevel()
  {
    // move all existing blocks and tokens down
    foreach (GameObject block in spawnedBlocks)
    {
      block.GetComponent<MoveDownEachLevel>().MoveDown();
    }
    foreach (GameObject token in spawnedTokens)
    {
      token.GetComponent<MoveDownEachLevel>().MoveDown();
    }

    // spawn new row of blocks and token
    levelNumber = gameManager.levelNumber + 1;
    SpawnRow();

    // move to ready phase
    gameManager.levelNumber++;
    gameManager.SetPhase("ready");
  }

  private void SpawnRow() // spawns new row with one token and collection of blocks
  {
    // spawn token
    int tokenPlace = rand.Next(7); // so that a block does not spawn on top
    thisObject = Instantiate(tokenPrefab, GetSpawnLocation(tokenPlace), tokenPrefab.transform.rotation);
    spawnedTokens.Add(thisObject);

    // spawn blocks
    for (int place = 0; place < 7; place++)
    {
      if (place == tokenPlace) // don't spawn block over token
      {
        continue;
      }

      switch (rand.Next(4))
      {
        case 0:
        case 1:
          // spawn normal-value block
          thisObject = Instantiate(blockPrefab, GetSpawnLocation(place), blockPrefab.transform.rotation);
          thisObject.GetComponent<UpdateNumberOnBlock>().number = levelNumber;
          spawnedBlocks.Add(thisObject);
          break;
        case 2:
          // spawn double-value block
          thisObject = Instantiate(blockPrefab, GetSpawnLocation(place), blockPrefab.transform.rotation);
          thisObject.GetComponent<UpdateNumberOnBlock>().number = levelNumber * 2;
          spawnedBlocks.Add(thisObject);
          break;
          // case 3: spawn nothing here
      }
    }
  }

  private Vector2 GetSpawnLocation(int place) // places are 0 to 6 from left to right
  {
    return new(place - 3.5f, 2.5f);
  }
}
