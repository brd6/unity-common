using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoroutine : MonoBehaviour
{
    [SerializeField]
    private Transform cubeContainer;

    [SerializeField]
    private Transform maxPosition;

    [SerializeField]
    private float speed;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(MoveCubes());
    }

    IEnumerator MoveCubes()
    {
        yield return new WaitForSeconds(2f);

        foreach (Transform cube in cubeContainer)
        {
            while (cube.position.y < maxPosition.position.y)
            {
                cube.Translate(0, speed * Time.deltaTime, speed * Time.deltaTime);
                yield return null;
            }
        }

        yield return new WaitForSeconds(1f);

        Debug.Log("Done");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
