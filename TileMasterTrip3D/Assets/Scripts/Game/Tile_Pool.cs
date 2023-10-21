using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile_Pool : MonoBehaviour
{
    [SerializeField, Range(0, 360f)]
    float rotationSpeed;

    [Header("Configure")]
    [SerializeField] Transform SpawnPoint;
    float force;

    [Header("Object Pool")]
    [SerializeField] List<GameObject> listTile = new List<GameObject>();

    public void SpawnTile()
    {
        Toggle_TilePool(true);
        Game_Manager.Instance.Load_Level();
        StartCoroutine(SpawnTile_Start());
    }

    public void Toggle_TilePool(bool value)
    {
        gameObject.SetActive(value);
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.fixedDeltaTime);
    }


    IEnumerator SpawnTile_Start()
    {
        foreach (Tiles_ID tiles_ID in Game_Manager.Instance.CurrentLevel.ListTile)
        {
            foreach (GameObject tile in listTile)
            {
                if (tile.GetComponent<Tile>().tile_Scriptable.Tile_Entity.Id == tiles_ID)
                {
                    Game_Manager.Instance.listTile.Add(tile);
                }
            }
        }

        var random = new System.Random();
        var randomized = Game_Manager.Instance.listTile.OrderBy(item => random.Next());

        foreach (GameObject tile in randomized)
        {
            tile.transform.position = SpawnPoint.position;
            tile.transform.rotation = SpawnPoint.rotation;
            tile.SetActive(true);

            force = UnityEngine.Random.Range(0.1f, 1f);

            tile.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.VelocityChange);

            tile.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            yield return new WaitForSeconds(0.02f);
        }

        Toggle_TilePool(false);
        Game_Manager.Instance.Init_Start();
    }

}
