using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class videoControl : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private RawImage rawImage;

    //设置相关参数以及视频列表
    public Button button_PlayOrPause;

    // Start is called before the first frame update
    void Start()
    {
        //获取VideoPlayr和RawImage组件
        videoPlayer = this.GetComponent<VideoPlayer>();
        rawImage = this.GetComponent<RawImage>();
        //设置相关按钮监听事件
        button_PlayOrPause.onClick.AddListener(OnplayOrPauseVideo);
    }

    // Update is called once per frame
    void Update()
    {
        if(videoPlayer.texture == null)
        {
            return;
        }
        rawImage.texture = videoPlayer.texture;
    }

    private void OnplayOrPauseVideo()
    {
        //这里是判断视频的播放情况，播放的情况下就暂停，反之；
        //然后更新相关文本
        if(videoPlayer.isPlaying == true)
        {
            videoPlayer.Pause();
        }
        else
        {
            videoPlayer.Play();
        }
    }

}
