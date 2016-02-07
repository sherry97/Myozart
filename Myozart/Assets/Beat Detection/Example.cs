/*
 * Copyright (c) 2015 Allan Pichardo
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *  http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using UnityEngine;
using UnityEngine.UI;
using System;

/*
 * Make your class implement the interface AudioProcessor.AudioCallbaks
 */
public class Example : MonoBehaviour, AudioProcessor.AudioCallbacks
{
	public Text countText;
	public Text endText;
	public Text finalScore;
	private int score;
	private Vector3 prevPos;
	public AudioClip otherClip;
	AudioSource audio;
	public ParticleSystem ps;

    void Start()
    {
        //Select the instance of AudioProcessor and pass a reference
        //to this object
        AudioProcessor processor = FindObjectOfType<AudioProcessor>();
		processor.addAudioCallback(this);
		prevPos = GetComponent<Transform>().position;
		score = 100;
		countText.text = "SCORE: "+score.ToString ();
		audio = GetComponent<AudioSource>();
		endText.text = "";
		finalScore.text = "";
    }

    
    void Update()
    {
		if (!audio.isPlaying) {
			endGame ();
		}
		Vector3 n = prevPos - transform.position;
		ps.transform.position = transform.position;
		Debug.Log (n.magnitude.ToString ());
		if (n.magnitude == 0) {
			ps.transform.position = transform.position;
			ps.Simulate (1);
		}
		prevPos = GetComponent<Transform>().position;
    }

    //this event will be called every time a beat is detected.
    //Change the threshold parameter in the inspector
    //to adjust the sensitivity
    public void onOnbeatDetected()
    {
		Vector3 n = prevPos - transform.position;
		if (n.magnitude > 0.1){
			score--;
		}
		countText.text = "SCORE: "+score.ToString ();
    }

    //This event will be called every frame while music is playing
    public void onSpectrum(float[] spectrum)
    {
        //The spectrum is logarithmically averaged
        //to 12 bands

        for (int i = 0; i < spectrum.Length; ++i)
        {
            Vector3 start = new Vector3(i, 0, 0);
            Vector3 end = new Vector3(i, spectrum[i], 0);
            Debug.DrawLine(start, end);
        }
    }

	public void endGame()
	{
		endText.text = "GAME OVER";
		finalScore.text = "SCORE: " + score.ToString ();
	}
}
