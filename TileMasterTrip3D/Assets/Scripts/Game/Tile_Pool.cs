using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile_Pool : MonoBehaviour
{
    [SerializeField, Range(0, 360f)]
    float rotationSpeed;
    float force;

    [Header("Configure")]
    [SerializeField] Transform SpawnPoint;
    GameObject obj;

    List<GameObject> listTile = new List<GameObject>();
    int Multiple = 9;
    private void OnEnable()
    {
        StartCoroutine(SpawnTile());
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.fixedDeltaTime);
    }

    IEnumerator SpawnTile()
    {
        foreach(GameObject tile in Game_Manager.Instance.level_Scriptable.Level_Entity.ListTile)
        {
            for(int i = 0; i < Multiple; i++)
            {
                listTile.Add(tile);
            }
        }

        var random = new System.Random();
        var randomized = listTile.OrderBy(item => random.Next());

        foreach (GameObject tile in randomized)
        {
            obj = Instantiate(tile, SpawnPoint.position, SpawnPoint.rotation);

            Game_Manager.Instance.listTile.Add(obj);

            force = UnityEngine.Random.Range(0.1f, 1.5f);

            obj.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.VelocityChange);

            obj.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            yield return new WaitForSeconds(0.05f);
        }

        gameObject.SetActive(false);
        Game_Manager.Instance.Init_Start();
    }

}
