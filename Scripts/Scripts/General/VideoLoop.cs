using UnityEngine.Video;
using UnityEngine;
using UnityEngine.UI;

public class VideoLoop : MonoBehaviour
{
    public RawImage rawImage; // RawImage를 참조
    public VideoClip videoClip; // 비디오 클립을 참조

    private VideoPlayer videoPlayer;

    void Start()
    {
        // RawImage 객체를 가져옴
        rawImage = GetComponent<RawImage>();

        // VideoPlayer 컴포넌트를 추가
        videoPlayer = gameObject.AddComponent<VideoPlayer>();

        // 비디오 클립 설정
        videoPlayer.clip = videoClip;

        // 비디오를 RawImage로 출력
        videoPlayer.targetTexture = new RenderTexture((int)rawImage.rectTransform.rect.width, (int)rawImage.rectTransform.rect.height, 0);
        rawImage.texture = videoPlayer.targetTexture;

        // 루프 설정
        videoPlayer.isLooping = true;

        // 비디오 재생
        videoPlayer.Play();
    }
}
