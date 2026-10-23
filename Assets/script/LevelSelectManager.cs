using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelectManager : MonoBehaviour
{
    public Button[] levelButtons;
    public Sprite unlockedSprite;
    public Sprite lockedSprite;

    void Start()
    {
        int unlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int level = i + 1;
            bool isUnlocked = level <= unlocked;

            levelButtons[i].interactable = isUnlocked;
            levelButtons[i].image.sprite = isUnlocked ? unlockedSprite : lockedSprite;

            TMP_Text label = levelButtons[i].GetComponentInChildren<TMP_Text>();
            if (label != null) label.gameObject.SetActive(isUnlocked);

            levelButtons[i].onClick.AddListener(() => LoadLevel(level));
        }
    }

    void LoadLevel(int level)
    {
        GameData.SelectedLevel = level;
        SceneManager.LoadScene("Game_Screen");
    }

    public void Back()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}