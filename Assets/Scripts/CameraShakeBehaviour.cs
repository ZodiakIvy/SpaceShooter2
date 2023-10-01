using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraShakeBehaviour : MonoBehaviour
{
    public Vector3 shakerPos;
    private float _shakeTimer = 0;
    [SerializeField]
    private float _shakeDuration = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
       shakerPos = this.transform.position;
    }

    public void Shake()
    {
        StartCoroutine(CameraShakeRoutine());
    }

    IEnumerator CameraShakeRoutine()
    {
        while (_shakeTimer < _shakeDuration)
        {
            float magnitude = Random.Range(1f, 3f);
            float x = Random.Range(-.25f, .25f) * magnitude;
            float y = Random.Range(-.25f, .25f) * magnitude;

            this.transform.position = new Vector3(x, y, shakerPos.z);
            _shakeTimer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = shakerPos;
        _shakeTimer = 0;
    }
}
