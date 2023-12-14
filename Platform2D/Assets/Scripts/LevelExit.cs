using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StartCoroutine(LoadLvl());
        }


    }

   
    IEnumerator LoadLvl()
    {
        yield return new WaitForSeconds(2f);
        var curentIndexScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(curentIndexScene + 1);
    }
}
