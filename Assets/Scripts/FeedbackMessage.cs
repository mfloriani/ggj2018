﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.VersionControl;

[Serializable]
class MessageItem
{
	public string Message;
	public float DurationTime;
}

class FeedbackMessage : MonoBehaviour
{
	private static FeedbackMessage _instance = null;
	
	public List<MessageItem> Messages = new List<MessageItem>();
	
	void Awake() {
		if (_instance == null) {
			_instance = this;
		}
	}

	public static FeedbackMessage getInstance() {
		if (_instance == null) {
			_instance = new FeedbackMessage();
		} else {
		}
		return _instance;
	}
     
	void Update()
	{
		/*
		currentTime = Time.time;
		if(someRandomCondition)
			showText = true;
		else
			showText = false;
         
		if(executedTime != 0.0f)
		{
			if(currentTime - executedTime > timeToWait)
			{
				executedTime = 0.0f;
				someRandomCondition = false;
			}
		}
		*/

		foreach (var message in Messages.ToList())
		{
			message.DurationTime -= Time.deltaTime;

			if (message.DurationTime < 0)
			{
				Messages.Remove(message);
			}
		}
	}

	public void AddMessage(string message, float durationTime)
	{
		var item = new MessageItem
		{
			Message = message,
			DurationTime = durationTime
		};
		Messages.Add(item);
	}
	
	void OnGUI()
	{
		var yOffset = 0;

		foreach (var message in Messages)
		{
			GUI.Label(new Rect(10, yOffset, 300, 100), message.Message);
			yOffset += 20;
		}
	}
}