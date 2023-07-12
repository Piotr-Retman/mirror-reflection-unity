using UnityEngine;

public class PlayMusicEffect : MonoBehaviour
{
    public void PlayMusic(string musicEffectName)
    {
        if (CurrentUserOptions.soundsOn)
        {
            AudioClip musicEffect = Resources.Load<AudioClip>("Sounds/" + musicEffectName);
            //Play the sound
            GameObject m = new GameObject("Music");
            m.AddComponent<AudioSource>();
            m.GetComponent<AudioSource>().clip = musicEffect;
            m.GetComponent<AudioSource>().Play();
        }
    }
}
