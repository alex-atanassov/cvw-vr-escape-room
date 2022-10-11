﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FadeToBlack : MonoBehaviour
{
	public GameEvent death;
	
	[SerializeField] private float _speed = 1.0f;
	[SerializeField] private float _intesity = 0.0f;
	[SerializeField] private Color _color = Color.black;
	[SerializeField] private Material _fadeMaterial = null;

	private void OnEnable()
	{ death.RegisterListener(GetComponent<GameEventListener>()); }

	private void OnDisable()
	{ death.UnregisterListener(GetComponent<GameEventListener>()); }

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		_fadeMaterial.SetFloat("_Intensity", _intesity);
		_fadeMaterial.SetColor("_FadeColor", _color);
		Graphics.Blit(source, destination, _fadeMaterial);
	}

	public Coroutine StartFadeIn()
	{
		StopAllCoroutines();
		return StartCoroutine(FadeIn());
	}

	private IEnumerator FadeIn()
	{
		while(_intesity <= 1.0f)
		{
			_intesity += _speed * Time.deltaTime;
			yield return null;
		}
	}

	public Coroutine StartFadeOut()
	{
		StopAllCoroutines();
		return StartCoroutine(FadeOut());
	}

	private IEnumerator FadeOut()
	{
		while (_intesity >= 0.0f)
		{
			_intesity -= _speed * Time.deltaTime;
			yield return null;
		}
	}

	public void FadeBlack()
	{
		StartFadeIn();
	}
}
