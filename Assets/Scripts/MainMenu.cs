using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject pressAnyKey, lvlSelect;
    Text lvlSelectText;

    int currentLevelIndex = 1;
    
    public int minLevel, maxLevel;

    void Start()
    {
        lvlSelectText = lvlSelect.GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RenderSettings.skybox.SetFloat("_Rotation", 2 * Time.time);

    }

    private void Update()
    {
        if (pressAnyKey.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                pressAnyKey.SetActive(false);
                lvlSelect.SetActive(true);
            }
        }
        else if (lvlSelect.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentLevelIndex++;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                currentLevelIndex--;
            }
            currentLevelIndex = Mathf.Clamp(currentLevelIndex, minLevel, maxLevel);
            lvlSelectText.text = "Level - " + currentLevelIndex;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(currentLevelIndex);
            }
        }

    }
}
