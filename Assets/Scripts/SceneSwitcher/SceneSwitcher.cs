using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitcher : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject == this.gameObject)
                {
                    ChangeScene();
                }
            }

        }
    }
    void ChangeScene()
    {
        SceneManager.LoadScene("Level2OrganOnChip");
    }
}
