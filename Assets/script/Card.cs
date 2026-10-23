using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int id;
    public bool isFlipped;
    public bool isMatched;

    Image image;
    Sprite front;
    Sprite back;
    GameManager manager;

    public void Setup(int id, Sprite front, Sprite back, GameManager manager)
    {
        this.id = id;
        this.front = front;
        this.back = back;
        this.manager = manager;

        image = GetComponent<Image>();
        image.sprite = back;
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (isFlipped || isMatched) return;
        manager.CardClicked(this);
    }

    public void ShowFront() { isFlipped = true; image.sprite = front; }
    public void ShowBack() { isFlipped = false; image.sprite = back; }
    public void SetMatched() { isMatched = true; }
}