using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NopeusMittari : MonoBehaviour
{
    public Rigidbody target;

    public float maxSpeed = 0.0f;

    public float minArrowAngle;
    public float maxArrowAngle;

    public TMP_Text Nopeus;
    public RectTransform arrow;

    private float speed = 0.0f;
    private void Update()
    {
        speed = target.velocity.magnitude * 3.6f;

        if (Nopeus != null)
            Nopeus.text = ((int)speed) + " km/h";
        if (arrow != null)
            arrow.localEulerAngles =
                new Vector3(0, 0, Mathf.Lerp(minArrowAngle, maxArrowAngle, speed / maxSpeed));
    }
}
