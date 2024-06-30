using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameUi : MonoBehaviour {
    public Text label;

    public Text enemiesLabel;

    void Update() {
        
        var dedDragons = FindObjectsOfType<DragonAI>().Where(x => x.IsDed).ToList().Count;
        var livDragons = FindObjectsOfType<DragonAI>().Where(y => !y.IsDed).ToList().Count;

        enemiesLabel.text = "Enemies: " + livDragons + "\nKilled: " + dedDragons;
    }

    public void ShowTextAndQuit(string message)
    {
        StartCoroutine(ShowTextAneQuitCoroutine(message));
    }

    IEnumerator ShowTextAneQuitCoroutine(string message) {
        yield return new WaitForSeconds(0.3f);
        ShowLabel(message);
        yield return new WaitForSeconds(0.5f);
        Stop();
    }

    void ShowLabel(string message) {
        label.text = message;
        label.gameObject.SetActive(true);
    }

    void Stop() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Debug.Log("Level complete, stopping playmode\n");
    }
}