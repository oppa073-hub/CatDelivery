using UnityEngine;

public class HpUI : MonoBehaviour
{
    public static HpUI instance;
    public GameObject[] heart;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateHeart(int hp)
    {
        for (int i = 0; i < heart.Length; i++)
        {
            heart[i].SetActive(i < hp);
        }
    }
}
