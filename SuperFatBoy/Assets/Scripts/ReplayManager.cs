using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayManager : MonoBehaviour {

    //Итак. Есть словарь с листом фреймов и индексом. Индекс будет обозначать количество копий игрока. 
    //В лист с фреймами записываем движения игрока и анимацию
    public static ReplayManager instance;

    public GameObject replayObj;
    Transform currentObj;
    Animator currentAnim;

    Dictionary<int, List<FrameInfo>> replayInfo = new Dictionary<int, List<FrameInfo>>();
    int recordCount;

    bool isRecording;
    bool startReplay;

    Transform replayCameraFolowObj;
    void Awake()
    {
        if (instance == null)
            instance = this;

        recordCount = 0;
        isRecording = false;
        startReplay = false;
    }

    void LateUpdate()
    {
        if (isRecording && currentObj!=null)
        {
            FrameInfo frame = new FrameInfo(currentObj.position, currentObj.rotation, new FrameInfo.AnimationFameInfo
                (
                    currentAnim.GetFloat("run"),
                    currentAnim.GetBool("jump"),
                    currentAnim.GetBool("wallSlide"),
                    currentAnim.GetBool("dead")
                ));

            replayInfo[replayInfo.Count - 1].Add(frame);
        }
    }
    public void SetRecordObj(Transform recordingObj)
    {
        replayInfo.Add(recordCount, new List<FrameInfo>());
        currentObj = recordingObj;
        currentAnim = recordingObj.GetComponent<Animator>();

        recordCount++;
    }
    public void StartRecording()
    {
        isRecording = true;
    }
    public void StopRecording()
    {
        isRecording = false;
        //recordCount++;
    }
    public void StartReplay()
    {
        for (int i = 0; i < replayInfo.Count; i++)
        {
            StartCoroutine(Replay(replayInfo[i]));
        }
        Camera.main.GetComponent<CameraFollow>().SetTarget(replayCameraFolowObj);//гавнецо
        //После того как реплей закончил воспроизводится, перезапустить его. Что бы повторы крутились по кругу.
        //StartReplay();
    }
    IEnumerator Replay(List<FrameInfo> frames)
    {
        GameObject player = Instantiate(replayObj, frames[0].GetPosition(), frames[0].GetRotatoin());
        Animator playerAnim = player.GetComponent<Animator>();

        //нужно сделать так, что бы камера следила за последним обьектом в реплей листе(то есть за тем кто дойдёт до финиша)
        replayCameraFolowObj = player.transform;
        foreach (var frame in frames)
        {
            player.transform.position = frame.GetPosition();
            player.transform.rotation = frame.GetRotatoin();
            playerAnim.SetFloat("run", frame.GetAnimation().run);
            playerAnim.SetBool("jump", frame.GetAnimation().jump);
            playerAnim.SetBool("wallSlide", frame.GetAnimation().wallSlide);
            playerAnim.SetBool("dead", frame.GetAnimation().isDead);
            yield return new WaitForEndOfFrame();
        }
        Destroy(player.gameObject);
    }
    public void IncreseRecordCount()
    {
        recordCount++;
    }
    public class FrameInfo
    {
        private Vector3 position;
        private Quaternion rotatoin;
        private AnimationFameInfo animation;

        public FrameInfo(Vector3 _position, Quaternion _rotatoin, AnimationFameInfo _animation)
        {
            position = _position;
            rotatoin = _rotatoin;
            animation = _animation;
        }
        public Vector2 GetPosition()
        {
            return position;
        }
        public Quaternion GetRotatoin()
        {
            return rotatoin;
        }
        public AnimationFameInfo GetAnimation()
        {
            return animation;
        }

        public struct AnimationFameInfo
        {
            public float run;
            public bool jump;
            public bool wallSlide;
            public bool isDead;
            public AnimationFameInfo(float _run, bool _jump, bool _wallslide, bool _isDead)
            {
                run = _run;
                jump = _jump;
                wallSlide = _wallslide;
                isDead = _isDead;
            }
        }
    }
    
}
