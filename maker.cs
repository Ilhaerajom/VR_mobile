using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maker : MonoBehaviour {

    private float spawn_Time = 0.0f;
    public GameObject prefab_spawn;
    public int max_count = 30;
    public int prefab_count = 0;

	void Start () {
#if UNITY_ANDROID

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

#endif
        prefab_spawn.SetActive(false);
    }
	
	void Update () {
        spawn_Time += Time.deltaTime;
        if (spawn_Time > 1.0f&&max_count>=prefab_count) {
            spawn_Time = 0.0f;
            GameObject obj_spawn = GameObject.Instantiate(prefab_spawn, this.transform) as GameObject;
            obj_spawn.SetActive(true);
            obj_spawn.transform.localPosition = new Vector3(Random.RandomRange(-2.0f, 2.0f),
                Random.RandomRange(0.5f, 3.0f), 30.0f);
            prefab_count++;
        }

        for
             (int i = 0; i<transform.childCount; i++) {
            Transform child_prefab = transform.GetChild(i);
            child_prefab.Translate(new Vector3(0, 0, 1));
            if(child_prefab.localPosition.z > 400.0f) {
                child_prefab.transform.localPosition = new Vector3(Random.RandomRange(-2.0f, 2.0f),
                                                                   Random.RandomRange(0.5f, 3.0f), 30.0f);
            }
        }
		

	}
}
