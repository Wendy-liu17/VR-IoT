using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class MyDistItem : MySensorItem
{
    private Camera mainCamera;
    private VideoPlayer videoPlayer;

    void Start() {
        Debug.Log("Dist Start!!!");
		if (!mainCamera)
			mainCamera = Camera.main;
        videoPlayer = this.GetComponent<VideoPlayer>();
        UnityAction triggeredAction = new UnityAction(playVideo);
        UnityAction untriggeredAction = new UnityAction(pasueVideo);
        setTriggered(triggeredAction);
        setUnTriggered(untriggeredAction);
    }

    void Update() {
        if (transform.position[0] - mainCamera.transform.position[0] > 0 && transform.position[0] - mainCamera.transform.position[0] < 5 && System.Math.Abs(transform.position[2] - mainCamera.transform.position[2]) < 2.2)
            SensorTrigger();
        else
            SensorUntrigger();
    }

    void playVideo() {
        Debug.Log("In dist!");
        if (!videoPlayer.isPlaying)
            videoPlayer.Play();
    }

    void pasueVideo() {
        Debug.Log("Out of dist!");
        if (videoPlayer.isPlaying)
            videoPlayer.Pause();
    }
}
