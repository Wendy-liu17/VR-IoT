using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Events;

[System.Serializable]
public class ThrowEvent : UnityEvent<int> { }


public class videoControl : MonoBehaviour
{
    public UnityAction action_grab;
    public UnityAction<int> action_throw;
    public UnityAction action_pause_play;
    public UnityEvent grabEvent = new UnityEvent();
    public ThrowEvent throwEvent = new ThrowEvent();

    public VideoPlayer videoPlayer;
    public RawImage rawImage1;
    public RawImage rawImage2;

    private VideoPlayer current_videoPlayer;
    private RawImage current_rawImage;
    private int current;

    // Start is called before the first frame update
    void Start()
    {
        current_videoPlayer = videoPlayer;
        current_rawImage = rawImage2;
        rawImage2.texture = null;
        rawImage1.texture = null;
        current = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (current_videoPlayer.texture == null) return;
        current_rawImage.texture = current_videoPlayer.texture;
    }

    public void OnPlayOrPauseVideo()
    {
        if (current_videoPlayer.isPlaying)
            current_videoPlayer.Pause();
        else
            current_videoPlayer.Play();
    }

    public void OnPlayVideo()
    {
        if (!current_videoPlayer.isPlaying)
            current_videoPlayer.Play();
    }

    public void OnPauseVideo()
    {
        if (current_videoPlayer.isPlaying)
            current_videoPlayer.Pause();
    }

    public void GrabVideo()
    {
        current_videoPlayer.Pause();
    }

    public void ThrowVideo(int num)
    {
        current_rawImage.texture = null;
        switch (num)
        {
            case 1:
                current_rawImage = rawImage1;
                current = 1;
                break;
            case 2:
                current_rawImage = rawImage2;
                current = 2;
                break;
            default:
                return;
                break;
        }
        current_rawImage.texture = videoPlayer.texture;
        current_videoPlayer.Play();
    }

    public void SwitchTV(int num)
    {
        current_videoPlayer.Pause();
        current_rawImage.texture = null;
        if (current == 1)
        {
            current_rawImage = rawImage2;
            current = 2;
        }
        else
        {
            current_rawImage = rawImage1;
            current = 1;
        }
        current_rawImage.texture = videoPlayer.texture;
        current_videoPlayer.Play();
    }
}
