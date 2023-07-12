using UnityEngine;

class MusicHandler : MonoBehaviour
{
    void Start()
    {
        CheckAndPlayMusic();
    }

    void Awake()
    {
        CheckAndPlayMusic();
    }

    void CheckAndPlayMusic()
    {
        GameObject[] musicObjects = GameObject.FindGameObjectsWithTag("music");
        if (CurrentUserOptions.playMusic)
        {
            musicObjects[0].gameObject.GetComponent<AudioSource>().Play();
        }
        else
        {
            musicObjects[0].gameObject.GetComponent<AudioSource>().Stop();
        }
        DestroyOtherMusicObjects(musicObjects);
        DontDestroyOnLoad(musicObjects[0]);
    }

    private void DestroyOtherMusicObjects(GameObject[] musicObjects)
    {
        if (musicObjects.Length > 1)
        {
            for (int i = 1; i < musicObjects.Length; i++)
            {
                Destroy(musicObjects[i]);
            }
        }
    }
}
