using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace RightChoice
{
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set;}
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    public class GameScript : MonoBehaviour
    {
        [SerializeField] private GameObject _targetPref;
        [SerializeField] private Text _textPoints;
        [SerializeField] private Text _textTime;
        [SerializeField] private Text _countPoints;
        [SerializeField] private Text _countTime;
        [SerializeField] private GameObject _endPanel;
        [SerializeField] public float _scaleSpeed;
        [SerializeField] private float _spawnTime;
        [SerializeField] private GameObject _canvas;
        public float ScaleSpeed 
        {
            get { return _scaleSpeed; }
        }
        public GameObject Canvas
        {
            get { return _canvas; }
        }
        public static GameScript Instance { get; set; }
        public List<Point> FreePoints = new List<Point>();
        private List<GameObject> targets = new List<GameObject>();
        private int points = 0;
        private float time = 0;
        private float offsetX = -4.2f * Screen.width / 567;
        private float offsetY = -4f * Screen.height / 498;
        private float stepX = 2.2f * Screen.width / 567;
        private float stepY = 2 * Screen.height / 498;

        private void Start()
        {
            if (MenuScript.ScaleSpeed != 0) _scaleSpeed = MenuScript.ScaleSpeed;
            else _scaleSpeed = 1f;
            _spawnTime = 1f;
            Instance = this;
            ClearPoints();
            AddPoints(0);
            _endPanel.SetActive(false);
            StartCoroutine(Spawn());
            AddTime(0f);
        }

        private void ClearPoints()
        {
            FreePoints.Clear();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++) FreePoints.Add(new Point(i, j));
            }
        }

        private void Restart()
        {
            SceneManager.LoadScene("Menu");
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                if (FreePoints.Count > 0)
                {
                    int n = Random.Range(0, FreePoints.Count);
                    targets.Add(Instantiate(_targetPref, new Vector2(offsetX + FreePoints[n].X * stepX, offsetY + FreePoints[n].Y * stepY), Quaternion.identity));
                    targets[targets.Count - 1].GetComponent<TargetScript>().currentPoint = FreePoints[n];
                    FreePoints.RemoveAt(n);
                    yield return new WaitForSeconds(_spawnTime);
                    AddTime(_spawnTime);
                }
            }
        }

        private void AddTime(float f)
        {
            time+=f;
            _textTime.text = "Времени прошло: " + (int)time;
        }

        public void AddPoints(int n)
        {
            points += n;
            _textPoints.text = "Очки: " + points;
        }

        public void GameOver()
        {
            _endPanel.SetActive(true);
            _countPoints.text = "Счет: " + points;
            _countTime.text = "Время: " + (int)time;
            _textPoints.text = "";
            _textTime.text = "";
            StopAllCoroutines();
            foreach (GameObject target in targets) Destroy(target);
            targets.Clear();
            ClearPoints();
        }
    }
}
