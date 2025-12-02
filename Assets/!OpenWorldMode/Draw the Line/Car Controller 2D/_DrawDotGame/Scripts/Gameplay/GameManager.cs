using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DrawDotGame
{
    public enum GameState
    {
        Prepare,
        Playing,
        Paused,
        PreGameOver,
        GameOver
    }

    public enum Mode
    {
        LevelEditorMode,
        GameplayMode
    }

    public class GameManager : MonoBehaviour
    {
        public static event System.Action<GameState, GameState> GameStateChanged = delegate { };
        public static event System.Action<bool, bool> GameEnded = delegate { };
        public SoundManager soundManager;

        public GameState GameState
        {
            get
            {
                return _gameState;
            }
            private set
            {
                if (value != _gameState)
                {
                    GameState oldState = _gameState;
                    _gameState = value;

                    GameStateChanged(_gameState, oldState);
                }
            }
        }

        [SerializeField]
        private GameState _gameState = GameState.Prepare;

        public static int GameCount
        {
            get { return _gameCount; }
            private set { _gameCount = value; }
        }

        private static int _gameCount = 0;

        public const string LAST_SELECTED_LEVEL_KEY = "LAST_SELECTED_LEVEL";
        private static int levelLoaded = -1;

        //baka
        public GameObject pausePanel, WinPanel, LosePanel;
        // The selected level
        public static int LevelLoaded
        {
            get
            {
                if (levelLoaded == -1)
                {
                    levelLoaded = PlayerPrefs.GetInt(LAST_SELECTED_LEVEL_KEY, 0);
                }
                return levelLoaded;
            }
            set
            {
                levelLoaded = value;
                PlayerPrefs.SetInt(LAST_SELECTED_LEVEL_KEY, levelLoaded);
            }
        }

        public ObstacleManager obstacleManager;
        public GameplayUIManager gameplayUIManager;
        public GameObject pinkBallPrefab;
        //public GameObject blueBallPrefab;
        public GameObject hintPrefab;
        public ParticleSystem explores;
        public ParticleSystem winning;
        [HideInInspector]
        public bool gameOver;
        [HideInInspector]
        public bool win = false;
        public Mode mode;
        [HideInInspector]
        public string failedScreenshotName = "failedLevel.png";

        [Header("Gameplay Config")]
        [Tooltip("The color of the drawn lines")]
        public Color lineColor;
        public Material lineMaterial;
        [Tooltip("How many hearts spent to view 1 hint")]
        public int heartsPerHint = 1;
        [Tooltip("How many hearts should be awarded when the player solves a level for the first time, put 0 to disable this feature")]
        public int heartsPerWin = 0;

        private LevelManager levelManager;
        private List<GameObject> listLine = new List<GameObject>();
        private List<Vector2> listPoint = new List<Vector2>();
        private GameObject currentLine;
        private GameObject currentColliderObject;
        private GameObject hintTemp;
        private BoxCollider2D currentBoxCollider2D;
        private LineRenderer currentLineRenderer;
        private Rigidbody2D pinkBallRigid;
        private Rigidbody2D blueBallRigid;
        public bool stopHolding;
        public bool allowDrawing = true;

        private List<Rigidbody2D> listObstacleNonKinematic = new List<Rigidbody2D>();
        private GameObject[] obstacles;
        public bool enableCarPhysics = false;

        public GameObject confetti;


       


        void Start()
        {
            confetti.SetActive(false);
            lineMaterial.SetColor("_Color", lineColor);
            if (mode == Mode.GameplayMode) //On gameplay mode
            {
                GameState = GameState.Prepare;
                string path = LevelScroller.JSON_PATH;
                TextAsset textAsset = Resources.Load<TextAsset>(path);
                string[] data = textAsset.ToString().Split(';');
                foreach (string o in data)
                {
                    LevelData levelData = JsonUtility.FromJson<LevelData>(o);
                    if (levelData.levelNumber == LevelLoaded)
                    {
                        CreateLevel(levelData);
                        GameState = GameState.Playing;
                        break;
                    }
                }
            }
            else //On level editor mode
            {
                BallController[] ballsController = FindObjectsOfType<BallController>();
                foreach (BallController o in ballsController)
                {
                    if (o.name.Split('(')[0].Equals("PinkBall"))
                        pinkBallRigid = o.gameObject.GetComponent<Rigidbody2D>();
                    else
                        blueBallRigid = o.gameObject.GetComponent<Rigidbody2D>();
                }
                //pinkBallRigid.isKinematic = true;
                //blueBallRigid.isKinematic = true;
                levelManager = FindObjectOfType<LevelManager>();
            }

            obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
            foreach (GameObject o in obstacles)
            {
                Rigidbody2D rigid = o.GetComponent<Rigidbody2D>();
                if (rigid != null && !rigid.isKinematic)
                {
                    listObstacleNonKinematic.Add(rigid);
                    rigid.isKinematic = true;
                }
            }
        }

        public void Win()
        {
            win = true;
            confetti.SetActive(true);
            StopAllPhysics();
            
           SoundManager.Instance.PlaySound(SoundManager.Instance.win, true);
            Invoke(nameof(SetWinPanelActive), 2f);
            if (mode == Mode.GameplayMode)
            {
                bool firstWin = !LevelManager.IsLevelSolved(LevelLoaded);   // solved for the first time
                if (firstWin)
                {
                    LevelManager.MarkLevelAsSolved(LevelLoaded);
                }

                StartCoroutine(CRTakeScreenshot());
                GameEnded(win, firstWin);
                // fire event
                enableCarPhysics = false;
            }
        }

        public void SetWinPanelActive()
        {
           
            WinPanel.SetActive(true);
            SoundManager.Instance.StopMusic();




        }
        public void SetLosePanelActive()
        {
            LosePanel.SetActive(true);
            SoundManager.Instance.StopMusic();
        }
        public void GameOver()
        {
            win = false;
            gameOver = true;
            GameState = GameState.GameOver;
            SoundManager.Instance.PlaySound(SoundManager.Instance.gameOver);

            if (mode == Mode.GameplayMode)
            {
                StartCoroutine(CRTakeScreenshot());
                GameEnded(win, false);
            }
            enableCarPhysics = false;
            Invoke(nameof(SetLosePanelActive), 2f);
        }

        public void Restart()
        {
            Time.timeScale = 1f;
            SoundManager.Instance.PlaySound(SoundManager.Instance.click, true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        //Create level base on level data
        void CreateLevel(LevelData levelData)
        {
            GameObject pinkBall = Instantiate(pinkBallPrefab, levelData.pinkBallPosition, Quaternion.identity);
            //GameObject blueBall = Instantiate(blueBallPrefab, levelData.blueBallPosition, Quaternion.identity);

            pinkBallRigid = pinkBall.GetComponent<Rigidbody2D>();
            pinkBallRigid.isKinematic = true;

            //blueBallRigid = blueBall.GetComponent<Rigidbody2D>();
            //blueBallRigid.isKinematic = true;

            foreach (ObstacleData o in levelData.listObstacleData)
            {
                foreach (GameObject a in obstacleManager.obstacles)
                {
                    if (a.name.Equals(o.id))
                    {
                        GameObject obstacle = Instantiate(a, o.position, o.rotation) as GameObject;
                        obstacle.transform.localScale = o.scale;
                        ConveyorController cv = obstacle.GetComponent<ConveyorController>();
                        if (cv != null)
                        {
                            cv.rotateSpeed = o.rotatingSpeed;
                            cv.rotateDirection = o.rotateDirection;
                        }
                        break;
                    }
                }
            }
        }

        void Update()
        {
            if (!gameOver && !win && !enableCarPhysics)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject thisButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

                    if (thisButton != null)
                    {
                        allowDrawing = false;
                    }
                    else
                    {

                        allowDrawing = true;
                        if (mode == Mode.GameplayMode)
                        {
                            if (gameplayUIManager.btnHint.activeSelf)
                                gameplayUIManager.btnHint.SetActive(false);
                            if (hintTemp != null)
                                Destroy(hintTemp);
                        }
                        stopHolding = false;
                        listPoint.Clear();

                        CreateLine(Input.mousePosition);
                    }
                }
                else if (Input.GetMouseButton(0) && !stopHolding && allowDrawing)
                {
                    //pencil draw sound
                    //SoundManager.Instance.PlaySound(SoundManager.Instance.draw, true);
                    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if (!listPoint.Contains(mousePos))
                    {
                        listPoint.Add(mousePos);
                        currentLineRenderer.positionCount = listPoint.Count;
                        currentLineRenderer.SetPosition(listPoint.Count - 1, listPoint[listPoint.Count - 1]);

                        if (listPoint.Count >= 2)
                        {
                            Vector2 point_1 = listPoint[listPoint.Count - 2];
                            Vector2 point_2 = listPoint[listPoint.Count - 1];

                            currentColliderObject = new GameObject("Collider");
                            currentColliderObject.transform.position = (point_1 + point_2) / 2;
                            currentColliderObject.transform.right = (point_2 - point_1).normalized;
                            currentColliderObject.transform.SetParent(currentLine.transform);

                            currentBoxCollider2D = currentColliderObject.AddComponent<BoxCollider2D>();
                            currentBoxCollider2D.size = new Vector3((point_2 - point_1).magnitude, 0.1f, 0.1f);
                            currentBoxCollider2D.enabled = false;

                            Vector2 rayDirection = currentColliderObject.transform.TransformDirection(Vector2.right);

                            Vector2 pointDir = currentColliderObject.transform.TransformDirection(Vector2.up);

                            Vector2 rayPoint_1 = (Vector2)currentColliderObject.transform.position + (-rayDirection) * (currentBoxCollider2D.size.x);
                            Vector2 rayPoint_2 = ((Vector2)currentColliderObject.transform.position + pointDir * (currentBoxCollider2D.size.y / 2f)) + ((-rayDirection) * (currentBoxCollider2D.size.x));
                            Vector2 rayPoint_3 = ((Vector2)currentColliderObject.transform.position + (-pointDir) * (currentBoxCollider2D.size.y / 2f)) + ((-rayDirection) * (currentBoxCollider2D.size.x));

                            float rayLength = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - rayPoint_1).magnitude;

                            RaycastHit2D hit_1 = Physics2D.Raycast(rayPoint_1, rayDirection, rayLength);
                            RaycastHit2D hit_2 = Physics2D.Raycast(rayPoint_2, rayDirection, rayLength);
                            RaycastHit2D hit_3 = Physics2D.Raycast(rayPoint_3, rayDirection, rayLength);

                            if (hit_1.collider != null || hit_2.collider != null || hit_3.collider != null)
                            {
                                GameObject hit = (hit_1.collider != null) ? (hit_1.collider.gameObject) :
                                                ((hit_2.collider != null) ? (hit_2.collider.gameObject) : (hit_3.collider.gameObject));
                                if (currentColliderObject.transform.parent != hit.transform.parent)
                                {
                                    Destroy(currentBoxCollider2D.gameObject);
                                    currentLineRenderer.positionCount = (listPoint.Count - 1);
                                    listPoint.Remove(listPoint[listPoint.Count - 1]);

                                    for (int i = 0; i < currentLine.transform.childCount; i++)
                                    {
                                        currentLine.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = true;
                                    }

                                    if (mode == Mode.LevelEditorMode)
                                    {
                                        levelManager.listLineRendererPos = listPoint;
                                    }

                                    listLine.Add(currentLine);
                                    //if (currentLine.GetComponent<Rigidbody2D>() == false)
                                    //    currentLine.AddComponent<Rigidbody2D>().useAutoMass = true;

                                    foreach (Rigidbody2D rigid in listObstacleNonKinematic)
                                    {
                                        rigid.isKinematic = false;
                                    }

                                    if (Input.GetMouseButtonUp(0) && listPoint.Count >= 2)
                                    {
                                        stopHolding = true;

                                    }
                                }
                            }
                        }
                    }
                }
                else if (Input.GetMouseButtonUp(0) && !stopHolding && allowDrawing)
                {
                    if (currentLine.transform.childCount > 0)
                    {
                        for (int i = 0; i < currentLine.transform.childCount; i++)
                        {
                            currentLine.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = true;

                        }
                        if (currentLine)
                        {
                            listLine.Add(currentLine);
                            if (currentLine.GetComponent<Rigidbody2D>() == false)
                                currentLine.AddComponent<Rigidbody2D>().mass = 5500f;
                        }
                    }
                    else
                    {
                        Destroy(currentLine);
                    }

                    foreach (Rigidbody2D rigid in listObstacleNonKinematic)
                    {
                        rigid.isKinematic = false;
                    }
                    if (!enableCarPhysics && listPoint.Count >= 6)
                        Invoke(nameof(bakaTweak), 0.2f);
                }
                //if (Camera.main != null && currentLineRenderer != null)
                //{
                //    float orthoSize = Camera.main.orthographicSize;
                //    float widthMultiplier = 0.02f; // Adjust this multiplier as needed
                //    float newLineWidth = orthoSize * widthMultiplier;
                //    currentLineRenderer.startWidth = newLineWidth;
                //    currentLineRenderer.endWidth = newLineWidth;
                //}
            }

            else if (currentLine != null && !enableCarPhysics)
            {
                Destroy(currentLine);
                currentLine = null;
            }
        }


        public void bakaTweak()
        {
           
            enableCarPhysics = true;
            allowDrawing = false;
        }
        
        void CreateLine(Vector2 mousePosition)
        {
            currentLine = new GameObject("Line");
            currentLine.tag = "Obstacle";
            //currentLine.AddComponent<Rigidbody2D>();
            //currentLine.GetComponent<Rigidbody2D>().gravityScale = 0;
            currentLineRenderer = currentLine.AddComponent<LineRenderer>();
            //currentLineRenderer.material = new Material(Shader.Find("Standard"));
            //currentLineRenderer.material.EnableKeyword("_EMISSION");
            //currentLineRenderer.material.SetColor("_EmissionColor", lineColor);
            currentLineRenderer.sharedMaterial = lineMaterial;
            currentLineRenderer.positionCount = 0;
            currentLineRenderer.startWidth = 0.1f;
            currentLineRenderer.endWidth = 0.1f;
            currentLineRenderer.startColor = lineColor;
            currentLineRenderer.endColor = lineColor;
            currentLineRenderer.useWorldSpace = false;
        }

        public void StopAllPhysics()
        {
            if (pinkBallRigid)
            {
                pinkBallRigid.bodyType = RigidbodyType2D.Kinematic;
                pinkBallRigid.simulated = false;
            }
            //blueBallRigid.bodyType = RigidbodyType2D.Kinematic;
            //blueBallRigid.simulated = false;
            for (int i = 0; i < listLine.Count; i++)
            {
                Rigidbody2D rigid = listLine[i].GetComponent<Rigidbody2D>();
                rigid.bodyType = RigidbodyType2D.Kinematic;
                rigid.simulated = false;
            }
        }

        // Capture a screenshot when game ends
        private IEnumerator CRTakeScreenshot()
        {
            // Capture the screenshot that is 2x bigger than the displayed one for crisper image on retina displays
            int height = LevelManager.ssHeight * 2;
            int width = (int)(height * Screen.width / Screen.height);
            RenderTexture rt = new RenderTexture(width, height, LevelManager.bitType, RenderTextureFormat.ARGB32);
            yield return new WaitForEndOfFrame();
            Camera.main.targetTexture = rt;
            Camera.main.Render();
            yield return null;
            Camera.main.targetTexture = null;
            yield return null;

            RenderTexture.active = rt;
            Texture2D tx = new Texture2D(width, height, TextureFormat.ARGB32, false);
            tx.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            tx.Apply();
            RenderTexture.active = null;
            Destroy(rt);

            byte[] bytes = tx.EncodeToPNG();

            // Store the screenshot with the level number and overwrite the old level screenshot if win, otherwise store a failed screenshot
            string filename = win ? LevelLoaded.ToString() + ".png" : failedScreenshotName;
            string imgPath = Path.Combine(Application.persistentDataPath, filename);
            File.WriteAllBytes(imgPath, bytes);
        }


        public bool ShowHint()
        {
            // Only show hint if there's enough hearts
            if (CoinManager.Instance.Coins >= heartsPerHint)
            {
                string path = LevelScroller.JSON_PATH;
                TextAsset textAsset = Resources.Load<TextAsset>(path);
                string[] data = textAsset.ToString().Split(';');
                foreach (string o in data)
                {
                    LevelData levelData = JsonUtility.FromJson<LevelData>(o);
                    if (levelData.levelNumber == LevelLoaded)
                    {
                        hintTemp = Instantiate(hintPrefab, levelData.hintData.position, levelData.hintData.rotation) as GameObject;
                        hintTemp.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Hints/hint" + LevelLoaded.ToString());
                        hintTemp.gameObject.transform.localScale = levelData.hintData.scale;

                        // Remove hearts
                        CoinManager.Instance.RemoveCoins(heartsPerHint);

                        return true;
                    }
                }
            }

            return false;
        }

        public void PauseGame()
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.click, true);
            Time.timeScale = 0;
        }


        public void ResumeGame()
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.click, true);
            Time.timeScale = 1;
        }
        
    }
}
