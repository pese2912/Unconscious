using UnityEngine;
using System.Collections;

public class MoveObject : MonoBehaviour {

    public Transform endPosition;
    public Transform startPosition;
    public Transform _target;

    public float speed = 1.0f;
    private float startTime;
    private float journeyLength;

    void OnEnable()
    {
        _target = GameObject.Find("Player").GetComponent<Transform>(); // 이동할 객체
        startTime = Time.time; // 시간
        journeyLength = Vector3.Distance(startPosition.position, endPosition.position);  // 이동 거리
        StartCoroutine(move());
    }
  

    public IEnumerator move()
    {
        yield return null;
        while (true)
        {
          
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            _target.position = Vector3.Lerp(startPosition.position, endPosition.position, fracJourney); // 시작위치에서 끝위치까지 이동

            if (_target.position == endPosition.position)
            {
                
                break;
            }
            yield return null;
        }
    }
}
