using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class videoControl : MonoBehaviour
{
    private VideoPlayer current_videoPlayer;
    private RawImage current_rawImage;
    private int current;

    //设置相关参数以及视频列表
    public Button button_PlayOrPause;
    public Button button_switch;
    public VideoPlayer videoPlayer;
    public RawImage rawImage1;
    public RawImage rawImage2;

    // Start is called before the first frame update
    void Start()
    {
        //获取VideoPlayr和RawImage组件
        current_videoPlayer = videoPlayer;
        current_rawImage = rawImage2;
        rawImage2.texture=null;
        rawImage1.texture=null;
        current=2;
        //设置相关按钮监听事件
        button_PlayOrPause.onClick.AddListener(OnplayOrPauseVideo);
        button_switch.onClick.AddListener(SwitchTV);
    }

    // Update is called once per frame
    void Update()
    {
        if(current_videoPlayer.texture == null)
        {
            return;
        }
        current_rawImage.texture = current_videoPlayer.texture;
    }

    private void OnplayOrPauseVideo()
    {
        //这里是判断视频的播放情况，播放的情况下就暂停，反之；
        //然后更新相关文本
        if(current_videoPlayer.isPlaying == true)
        {
            current_videoPlayer.Pause();
        }
        else
        {
            current_videoPlayer.Play();
        }
    }

    private void SwitchTV()
    {
        //这里是判断视频的播放情况，播放的情况下就暂停，反之；
        //然后更新相关文本
        current_videoPlayer.Pause();
        current_rawImage.texture=null;
        if(current==1){
            current_rawImage = rawImage2;
            current=2;
        }
        else{ 
            current_rawImage = rawImage1;
            current=1;
        }
        current_rawImage.texture = videoPlayer.texture;
        current_videoPlayer.Play();
    }

}
