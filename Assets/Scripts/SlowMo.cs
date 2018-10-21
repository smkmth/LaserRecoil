﻿using UnityEngine;

public class SlowMo : MonoBehaviour {

	public float slowdownFactor = 0.05f;
	public float slowdownLength = 2f;

	void Update ()
	{
		Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
		Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        if (Input.GetButtonDown("SlowMo"))
        {
            DoSlowmotion();
        }
	}

	public void DoSlowmotion ()
	{
		Time.timeScale = slowdownFactor;
		Time.fixedDeltaTime = Time.timeScale * .02f;
	}

}
