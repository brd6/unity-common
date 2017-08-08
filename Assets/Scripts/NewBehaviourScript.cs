using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

[System.Serializable]
public struct MyStruct
{
    public int a;
    public int b;
}

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

        MyStruct data = new MyStruct();

        //data.a = 12;
        //data.b = 42;

        data = DataSerializer.LoadBinary<MyStruct>(Application.persistentDataPath + "/myData.data");

        //DataSerializer.SaveBinary(data, Application.persistentDataPath + "/myData.data");

        Debug.Log(data.a + " | " + data.b);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
