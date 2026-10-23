using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public RectTransform gridParent;
    public GridLayoutGroup grid;
    public Sprite[] frontSprites;   // need at least 10
    public Sprite backSprite;
    public TMP_Text movesText;
    public TMP_Text levelText;
    public GameObject winPanel;
    public GameObject nextButton;
    public Image[] stars;   // your 3 star images in the win panel
    public GameObject menuPanel;

    // Level 1..5
    int[] pairsPerLevel = { 2, 3, 6, 8, 10 };
    int[] columnsPerLevel = { 2, 2, 3, 4, 4 };

    Card first, second;
    bool busy;
    int moves, matchedPairs, totalPairs;

    public AudioSource sfx;
    public AudioClip flipClip, matchClip, wrongClip, winClip;

    void Play(AudioClip clip)
    {
        if (clip != null && sfx != null) sfx.PlayOneShot(clip);
    }

    void Start()
    {
        winPanel.SetActive(false);
        menuPanel.SetActive(false);
        BuildLevel(GameData.SelectedLevel);
    }

    void BuildLevel(int level)
    {
        int idx = Mathf.Clamp(level - 1, 0, pairsPerLevel.Length - 1);
        totalPairs = pairsPerLevel[idx];
        int cols = columnsPerLevel[idx];

        levelText.text = "Level " + level;
        movesText.text = "Moves: 0";

        // pick random pictures, make pairs, shuffle
        List<int> pool = new List<int>();
        for (int i = 0; i < frontSprites.Length; i++) pool.Add(i);
        Shuffle(pool);

        List<int> ids = new List<int>();
        for (int i = 0; i < totalPairs; i++)
        {
            ids.Add(pool[i]);
            ids.Add(pool[i]);
        }
        Shuffle(ids);

        // size the grid to fit the screen
        int total = ids.Count;
        int rows = Mathf.CeilToInt((float)total / cols);
        float spacing = grid.spacing.x;
        float availW = gridParent.rect.width - grid.padding.left - grid.padding.right - spacing * (cols - 1);
        float availH = gridParent.rect.height - grid.padding.top - grid.padding.bottom - spacing * (rows - 1);
        float cellW = availW / cols;
        float cellH = cellW * 1.4f;               // card shape is taller than wide
        if (cellH * rows > availH)
        {
            cellH = availH / rows;
            cellW = cellH / 1.4f;
        }
        grid.cellSize = new Vector2(cellW, cellH);
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = cols;

        // spawn cards
        foreach (int id in ids)
        {
            GameObject obj = Instantiate(cardPrefab, gridParent);
            obj.GetComponent<Card>().Setup(id, frontSprites[id], backSprite, this);
        }
    }

    public void CardClicked(Card card)
    {
        if (busy) return;

        card.ShowFront();
        Play(flipClip);

        if (first == null)
        {
            first = card;
            return;
        }

        second = card;
        moves++;
        movesText.text = "Moves: " + moves;
        StartCoroutine(CheckMatch());
    }

    IEnumerator CheckMatch()
    {
        busy = true;
        yield return new WaitForSeconds(0.8f);

        if (first.id == second.id)
        {
            first.SetMatched();
            second.SetMatched();
            matchedPairs++;
            Play(matchClip);
            if (matchedPairs == totalPairs) Win();
        }
        else
        {
            Play(wrongClip);
            first.ShowBack();
            second.ShowBack();
        }

        first = null;
        second = null;
        busy = false;
    }

    void Win()
    {
        int next = GameData.SelectedLevel + 1;
        int unlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (next > unlocked && next <= pairsPerLevel.Length)
        {
            PlayerPrefs.SetInt("UnlockedLevel", next);
            PlayerPrefs.Save();
        }

        nextButton.SetActive(GameData.SelectedLevel < pairsPerLevel.Length);
        ShowStars();
        Play(winClip);
        winPanel.SetActive(true);
    }

    public void OpenMenu()
    {
        if (winPanel.activeSelf) return;   // no menu on the win screen
        menuPanel.SetActive(true);
    }

    public void CloseMenu()
    {
        menuPanel.SetActive(false);
    }

    void ShowStars()
    {
        int earned;
        if (moves <= Mathf.CeilToInt(totalPairs * 1.5f)) earned = 3;
        else if (moves <= Mathf.CeilToInt(totalPairs * 2.5f)) earned = 2;
        else earned = 1;

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].color = i < earned
                ? Color.white                          // earned: normal color
                : new Color(0.25f, 0.25f, 0.25f, 0.6f); // not earned: dark and faded
        }
    }

    public void NextLevel()
    {
        GameData.SelectedLevel++;
        SceneManager.LoadScene("Game_Screen");
    }

    public void Restart() { SceneManager.LoadScene("Game_Screen"); }
    public void Home() { SceneManager.LoadScene("Level_Select"); }

    void Shuffle(List<int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (menuPanel.activeSelf) CloseMenu();
            else OpenMenu();
        }
    }
}