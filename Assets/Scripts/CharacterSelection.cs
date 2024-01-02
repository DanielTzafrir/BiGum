using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters;
    public int indexCharacter = 0;
    public int selectedCharacter = 0;
    public GameObject chooseBT;
    public GameObject BuyBT;
    public GameObject selectedImage;
    public bool[] IsOpened;

    public void NextCharacter()
    {
        characters[indexCharacter].SetActive(false);
        indexCharacter = (indexCharacter + 1) % characters.Length;
        characters[indexCharacter].SetActive(true);
        changeChooseBT();
    }

    public void PreviousCharacter()
    {
        characters[indexCharacter].SetActive(false);
        indexCharacter--;
        if (indexCharacter < 0)
        {
            indexCharacter += characters.Length;
        }
        characters[indexCharacter].SetActive(true);
        changeChooseBT();
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedCharcter", selectedCharacter);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void chosenCharacter()
    {
        //adding the 'v' to the chosen character and removing the 'v' from the prev
        characters[selectedCharacter].transform.GetChild(0).gameObject.SetActive(false);
        selectedCharacter = indexCharacter;
        characters[selectedCharacter].transform.GetChild(0).gameObject.SetActive(true);

    }
    private void changeChooseBT()
    {
        //check if the button should be the buy button or the choose button
        if (!IsOpened[indexCharacter])
        {
            chooseBT.SetActive(false);
            BuyBT.SetActive(true);
            characters[indexCharacter].transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            chooseBT.SetActive(true);
            BuyBT.SetActive(false);
            characters[indexCharacter].transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        selectedCharacter = PlayerPrefs.GetInt("selectedCharcter", selectedCharacter);
        characters[selectedCharacter].transform.GetChild(0).gameObject.SetActive(true);
    }

    private void Update()
    {
        //check if selected then change to selectedImage on the choose button
        if (selectedCharacter == indexCharacter)
        {
            selectedImage.SetActive(true);
        }
        else
        {
            selectedImage.SetActive(false);
        }
    }
}
