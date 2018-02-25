using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    public string[] nameArray;

    public float offsetH = 20;
    public float offsetW = 20;
    public float timeForSpawn = 1f;

    float timeFromLastSpawn = 0;
    Vector3 spawnPos;
    GameObject lastSpawnedObject;

    void SpawnObject(string _name, Vector3 _spawnPos)
    {
        Instantiate(transform.Find(_name), _spawnPos, Quaternion.identity).gameObject.SetActive(true);
        Debug.Log("ObjectSpawner : Spawn " + _name);
    }

    string GetRandomStringFromArray(string[] _array)
    {
        return _array[Random.Range(0, _array.Length)];
    }
    
	void Update () {
        timeFromLastSpawn += Time.deltaTime;
        timeForSpawn = 1 / GameManager.instance.gameSpeed;
        if (timeFromLastSpawn >= timeForSpawn)
        {
            spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + offsetW, Random.Range(offsetH, Screen.height - offsetH), -Camera.main.transform.position.z));
            SpawnObject(GetRandomStringFromArray(nameArray), spawnPos);
            timeFromLastSpawn = 0f;
        }
	}
}
