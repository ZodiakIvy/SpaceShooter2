using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBG : MonoBehaviour
{
    [SerializeField]
    private float _speed = .2f;

    [SerializeField]
    private Renderer _renderer;

    // Update is called once per frame
    void Update()
    {
        _renderer.material.mainTextureOffset = new Vector2(0, Time.time * _speed);
    }
}
