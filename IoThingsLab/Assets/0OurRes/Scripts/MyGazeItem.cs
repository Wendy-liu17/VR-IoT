using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class MyGazeItem : MySensorItem
{
    private Camera mainCamera;
    public VideoPlayer videoPlayer;

	void Start() {
        Debug.Log("Gaze Start!!!");
		if (!mainCamera)
			mainCamera = Camera.main;
        // videoPlayer = this.GetComponent<VideoPlayer>();
        UnityAction triggeredAction = new UnityAction(playVideo);
        UnityAction untriggeredAction = new UnityAction(pasueVideo);
        setTriggered(triggeredAction);
        setUnTriggered(untriggeredAction);
	}

	void Update() {
        Vector3 vec = mainCamera.WorldToViewportPoint(transform.position);
        if (vec.x > 0 && vec.x < 1 && vec.y > 0 && vec.y < 1 && vec.z > mainCamera.nearClipPlane && vec.z < mainCamera.farClipPlane)
            SensorTrigger();
        else
            SensorUntrigger();
	}

    void playVideo() {
        Debug.Log("In sight!");
        if (!videoPlayer.isPlaying)
            videoPlayer.Play();
    }

    void pasueVideo() {
        Debug.Log("Out of sight!");
        if (videoPlayer.isPlaying)
            videoPlayer.Pause();
    }
}
