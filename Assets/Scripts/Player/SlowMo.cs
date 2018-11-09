using UnityEngine;

public class SlowMo : MonoBehaviour {

	public float fSlowdownFactor = 0.05f;
	public float fSlowdownLength = 2f;

	void Update ()
	{
		Time.timeScale += (1f / fSlowdownLength) * Time.unscaledDeltaTime;
		Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        if (Input.GetButtonDown("SlowMo"))
        {
            DoSlowmotion();
        }
	}

	public void DoSlowmotion ()
	{
		Time.timeScale = fSlowdownFactor;
		Time.fixedDeltaTime = Time.timeScale * .02f;
	}

}
