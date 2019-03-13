using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    [SerializeField] GameObject targetPref;
    [SerializeField] Text textPoints;
    [SerializeField] GameObject endPanel;
    List<GameObject> targets = new List<GameObject>();
    int points = 0;
    const float offsetX = -4.5f;
    const float offsetY = -4;
    const float stepX = 2.2f;
    const float stepY = 2;
    List<Dictionary<int, bool>> positions = new List<Dictionary<int, bool>>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            positions.Add(new Dictionary<int, bool>());
            for (int j = 0; j < 5; j++) positions[i].Add(j, false);
        }
        Restart();
    }

    public void Restart()
    {
        AddPoints(0);
        endPanel.SetActive(false);
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++) positions[i][j] = false;
        }
        StartCoroutine(Spawn());
    }

    bool CheckEnd()
    {
        int n = 0;
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 5; j++)
                if (positions[i][j]) n++;
        if (n == 25) return true;
        return false;
    }

    bool CheckRow(int i)
    {
        int n = 0;
        for (int j = 0; j < 5; j++)
            if (positions[i][j]) n++;
        if (n == 5) return true;
        return false;
    }

    IEnumerator Spawn()
    {
        while (!CheckEnd())
        {
            int m = Random.Range(0, 5);
            while(CheckRow(m)) m = Random.Range(0, 5);
            int n = Random.Range(0, 5);
            while(positions[m][n]) n = Random.Range(0, 5);
            targets.Add(Instantiate(targetPref, new Vector2(offsetX + n * stepX, offsetY + m * stepY), Quaternion.identity));
            targets[targets.Count - 1].GetComponent<TargetScript>().gameManager = gameObject;
            positions[m][n] = true;
            yield return new WaitForSeconds(1f);
        }
    }

    public void AddPoints(int n)
    {
        points += n;
        textPoints.text = "Очки: " + points;
    }

    public void GameOver()
    {
        endPanel.SetActive(true);
        StopAllCoroutines();
        foreach (GameObject target in targets) Destroy(target);
        targets.Clear();
        points = 0;
    }
}
