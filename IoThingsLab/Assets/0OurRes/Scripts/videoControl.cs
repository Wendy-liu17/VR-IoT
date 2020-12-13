using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Events;

// public class MyEvent1:UnityEvent<int>{}


public class videoControl : MonoBehaviour {
    private VideoPlayer current_videoPlayer;
    private RawImage current_rawImage;
    private int current;
    public UnityAction<int> action_throw;
    public UnityAction action_scratch;
    public UnityAction action_pause_play;
    public UnityEvent myEvent = new UnityEvent();
    public MyEvent1 myEvent1 = new MyEvent1();


    //设置相关参数以及视频列表
    public Button button_PlayOrPause;
    public Button button_switch;
    public VideoPlayer videoPlayer;
    public RawImage rawImage1;
    public RawImage rawImage2;

    // Start is called before the first frame update
    void Start() {
        //获取VideoPlayr和RawImage组件
        current_videoPlayer = videoPlayer;
        current_rawImage = rawImage2;
        rawImage2.texture=null;
        rawImage1.texture=null;
        current=2;
        //设置相关按钮监听事件
        button_PlayOrPause.onClick.AddListener(fun1);
        button_switch.onClick.AddListener(fun2);
        action_throw = new UnityAction<int>(throw_video);
        action_scratch = new UnityAction(scratch);
        action_pause_play = new UnityAction(OnPlayOrPauseVideo);
        myEvent1.AddListener(action_throw);
        myEvent.AddListener(action_scratch);
    }

    void fun1() {
        myEvent.Invoke();
    }

    void fun2() {
        myEvent1.Invoke(1);
    }

    // Update is called once per frame
    void Update() {
        if (current_videoPlayer.texture == null) return;
        current_rawImage.texture = current_videoPlayer.texture;
    }

    public void OnPlayOrPauseVideo() {
        //这里是判断视频的播放情况，播放的情况下就暂停，反之；
        //然后更新相关文本
        if (current_videoPlayer.isPlaying)
            current_videoPlayer.Pause();
        else
            current_videoPlayer.Play();
    }

    public void OnPlayVideo() {
        if (!current_videoPlayer.isPlaying)
            current_videoPlayer.Play();
    }

    public void OnPauseVideo() {
        if (current_videoPlayer.isPlaying)
            current_videoPlayer.Pause();
    }

    public void scratch() {
        current_videoPlayer.Pause();
        // current_rawImage.texture=null;
    }

    public void throw_video(int num) {
        current_rawImage.texture = null;
        switch (num) {
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

    public void SwitchTV(int num) {
        //这里是判断视频的播放情况，播放的情况下就暂停，反之；
        //然后更新相关文本
        current_videoPlayer.Pause();
        current_rawImage.texture=null;
        if(current == 1) {
            current_rawImage = rawImage2;
            current = 2;
        }
        else { 
            current_rawImage = rawImage1;
            current = 1;
        }
        current_rawImage.texture = videoPlayer.texture;
        current_videoPlayer.Play();
    }
}
