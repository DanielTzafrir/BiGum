using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadScenes : MonoBehaviour
{
    public int numscene;
    // Start is called before the first frame update
    private void Start()
    {
        // Get a reference to the Button component
        Button button = GetComponent<Button>();

        // Add a click event listener to the button
        button.onClick.AddListener(LoadScene);
        Debug.Log(numscene);

    }

    // Method to load the specified scene
    private void LoadScene()
    {
        // Load the specified scene by name
        SceneManager.LoadScene(numscene);
        Debug.Log(numscene);
    }
}

