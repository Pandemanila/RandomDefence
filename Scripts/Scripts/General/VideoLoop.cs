using UnityEngine.Video;
using UnityEngine;
using UnityEngine.UI;

public class VideoLoop : MonoBehaviour
{
    public RawImage rawImage; // RawImage�� ����
    public VideoClip videoClip; // ���� Ŭ���� ����

    private VideoPlayer videoPlayer;

    void Start()
    {
        // RawImage ��ü�� ������
        rawImage = GetComponent<RawImage>();

        // VideoPlayer ������Ʈ�� �߰�
        videoPlayer = gameObject.AddComponent<VideoPlayer>();

        // ���� Ŭ�� ����
        videoPlayer.clip = videoClip;

        // ������ RawImage�� ���
        videoPlayer.targetTexture = new RenderTexture((int)rawImage.rectTransform.rect.width, (int)rawImage.rectTransform.rect.height, 0);
        rawImage.texture = videoPlayer.targetTexture;

        // ���� ����
        videoPlayer.isLooping = true;

        // ���� ���
        videoPlayer.Play();
    }
}
