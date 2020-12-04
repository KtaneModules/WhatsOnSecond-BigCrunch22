using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Newtonsoft.Json;

public class WhatsonSecondScript : MonoBehaviour
{
	public KMAudio Audio;
    public KMBombInfo Bomb;
	public KMBombModule Module;
	
	public KMSelectable[] buttons;
	public Transform[] LEDs;
	public TextMesh Display;

	string[] Phrase = new string[] {"GOT IT", "SAYS", "DISPLAY", "LEED", "THEIR", "BLANK", "RIGHT", "REED", "HOLD", "THEY ARE", "LOUDER", "LEAD", "REPEAT", "READY", "NONE", "LED", "UR", "YOU’RE", "NO", "YOU", "NOTHING", "MIDDLE", "DONE", "EMPTY", "YOUR", "HOLD ON", "LIKE", "READ", "WAIT", "LEFT", "PRESS", "WHAT?", "UH UH", "THEY’RE", "UHHH", "C", "ERROR", "YOU ARE", "NEXT", "YES", "U", "SURE", "OKAY", "WHAT", "CEE", "FIRST", "SEE", "UH HUH", "THERE", "RED"};
    int[][] DisplayPosition = new int[][]{
		new int[] {1, 5, 3, 5, 5, 4, 5, 0, 1, 1, 2, 0, 2, 1, 2, 1, 2, 2, 4, 5, 3, 2, 3, 0, 0, 1, 1, 0, 4, 2, 2, 5, 2, 0, 0, 2, 1, 4, 0, 0, 0, 1, 0, 4, 0, 0, 1, 1, 4, 4},
		new int[] {1, 2, 5, 3, 1, 1, 4, 2, 2, 5, 1, 3, 5, 4, 4, 5, 0, 5, 3, 5, 5, 3, 5, 2, 3, 5, 2, 5, 0, 5, 4, 2, 0, 3, 3, 0, 4, 1, 3, 2, 1, 3, 3, 2, 4, 5, 5, 5, 4, 1},
		new int[] {3, 4, 2, 1, 1, 3, 1, 2, 4, 0, 1, 4, 4, 0, 3, 2, 1, 0, 2, 3, 4, 5, 2, 4, 4, 2, 2, 0, 4, 1, 2, 0, 2, 5, 3, 5, 5, 1, 0, 4, 1, 3, 2, 2, 0, 4, 3, 0, 2, 1},
		new int[] {4, 5, 3, 4, 2, 0, 4, 1, 2, 5, 3, 1, 0, 3, 0, 3, 3, 3, 4, 2, 5, 1, 1, 3, 3, 0, 1, 3, 4, 5, 5, 5, 3, 3, 1, 0, 1, 2, 4, 1, 2, 5, 4, 2, 3, 0, 0, 1, 3, 0},
		new int[] {3, 4, 2, 4, 4, 3, 4, 1, 1, 5, 0, 3, 5, 2, 2, 4, 1, 5, 0, 4, 4, 2, 1, 2, 0, 1, 4, 3, 5, 3, 2, 0, 1, 3, 0, 0, 5, 4, 5, 5, 3, 5, 3, 1, 3, 0, 4, 3, 2, 0},
		new int[] {1, 0, 0, 4, 3, 3, 0, 2, 4, 4, 5, 2, 5, 5, 1, 0, 1, 0, 5, 4, 4, 2, 5, 0, 5, 2, 5, 5, 1, 3, 3, 4, 2, 5, 3, 3, 5, 1, 0, 4, 3, 5, 2, 4, 1, 2, 0, 0, 4, 1}
	};
	
	int[][] ColorPosition = new int[][]{
		new int[] {2, 2, 0, 5, 2, 2, 5, 4, 5, 2, 3, 1, 2, 4, 0, 4, 3, 5, 5, 0, 1, 5, 2, 2, 1, 0, 2, 1, 3, 1, 2, 2, 0, 2, 2, 5, 1, 4, 4, 5, 0, 5, 0, 5, 3, 2, 0, 3, 2, 3},
		new int[] {2, 3, 4, 5, 5, 3, 1, 4, 0, 0, 3, 1, 2, 3, 2, 2, 1, 3, 4, 1, 2, 3, 5, 4, 0, 3, 1, 3, 3, 4, 4, 4, 1, 4, 0, 1, 0, 5, 1, 4, 5, 1, 0, 4, 5, 0, 5, 4, 5, 5},
		new int[] {1, 0, 3, 2, 0, 2, 0, 0, 3, 1, 4, 0, 0, 0, 1, 4, 5, 1, 3, 1, 4, 3, 4, 5, 5, 4, 3, 0, 5, 2, 5, 3, 1, 1, 1, 1, 0, 3, 3, 0, 1, 1, 2, 3, 5, 0, 0, 1, 4, 5},
		new int[] {0, 0, 5, 3, 0, 3, 2, 3, 4, 5, 5, 0, 4, 2, 4, 5, 3, 0, 3, 1, 4, 4, 5, 3, 1, 5, 4, 2, 1, 3, 1, 5, 3, 2, 3, 4, 5, 5, 1, 1, 1, 3, 4, 0, 3, 5, 5, 0, 2, 1},
		new int[] {2, 3, 3, 1, 5, 4, 2, 5, 2, 1, 3, 3, 0, 5, 4, 3, 4, 0, 2, 5, 5, 4, 3, 2, 0, 0, 2, 0, 0, 2, 2, 1, 0, 2, 4, 0, 0, 5, 2, 4, 4, 2, 2, 1, 5, 1, 4, 1, 4, 2},
		new int[] {3, 0, 2, 3, 4, 4, 0, 1, 2, 1, 4, 1, 2, 1, 4, 5, 0, 2, 3, 0, 3, 1, 2, 3, 4, 4, 0, 3, 4, 5, 2, 2, 4, 4, 5, 0, 1, 2, 5, 3, 4, 5, 1, 3, 1, 1, 0, 3, 4, 5}
	};
	
	string[][] ShufflePlay = new string[][]{
		new string[] {"BLANK", "SEE", "EMPTY", "RED", "PRESS", "WAIT", "YOU’RE", "DISPLAY", "NEXT", "YOU", "NOTHING", "YOU ARE", "DONE", "UHHH", "LED", "C", "UR", "LEFT", "THEY’RE", "HOLD", "NONE", "RIGHT", "GOT IT", "THEY ARE", "SAYS", "READY", "LIKE", "YOUR", "NO", "LEED", "FIRST", "WHAT", "U", "UH HUH", "WHAT?", "REED", "HOLD ON", "LEAD", "MIDDLE", "READ", "UH UH", "THEIR", "ERROR", "OKAY", "SURE", "REPEAT", "LOUDER", "YES", "CEE", "THERE"},
		new string[] {"NONE", "YOU’RE", "READY", "THEY’RE", "SEE", "HOLD", "ERROR", "EMPTY", "FIRST", "BLANK", "UH HUH", "THERE", "UHHH", "LEAD", "MIDDLE", "THEIR", "U", "LOUDER", "GOT IT", "NEXT", "YOU ARE", "UR", "REPEAT", "LEED", "NOTHING", "REED", "RED", "THEY ARE", "LIKE", "YOUR", "YES", "SURE", "WHAT", "UH UH", "NO", "C", "CEE", "LEFT", "WAIT", "HOLD ON", "PRESS", "OKAY", "WHAT?", "RIGHT", "LED", "SAYS", "YOU", "READ", "DONE", "DISPLAY"},
		new string[] {"YES", "YOU", "UHHH", "UH UH", "LED", "SEE", "THEY’RE", "FIRST", "ERROR", "GOT IT", "RED", "NO", "C", "HOLD", "YOU’RE", "THEIR", "SURE", "YOUR", "WHAT?", "YOU ARE", "NOTHING", "LIKE", "UH HUH", "READY", "THEY ARE", "LEFT", "NEXT", "HOLD ON", "U", "REED", "READ", "DONE", "MIDDLE", "CEE", "SAYS", "EMPTY", "PRESS", "OKAY", "WHAT", "LEAD", "LOUDER", "LEED", "DISPLAY", "WAIT", "UR", "REPEAT", "THERE", "NONE", "RIGHT", "BLANK"},
		new string[] {"THEY ARE", "LOUDER", "NONE", "C", "YOU ARE", "YOU’RE", "READ", "HOLD ON", "UH HUH", "WHAT?", "THEY’RE", "LEFT", "EMPTY", "OKAY", "FIRST", "LEAD", "SURE", "REED", "READY", "YOUR", "PRESS", "THERE", "U", "WHAT", "YOU", "UR", "UHHH", "MIDDLE", "CEE", "LEED", "UH UH", "ERROR", "HOLD", "DONE", "SAYS", "DISPLAY", "WAIT", "BLANK", "THEIR", "RED", "REPEAT", "LED", "YES", "LIKE", "NEXT", "RIGHT", "NOTHING", "NO", "SEE", "GOT IT"},
		new string[] {"LEFT", "OKAY", "WHAT", "WAIT", "CEE", "MIDDLE", "REED", "DONE", "YOU ARE", "C", "NONE", "NEXT", "THEIR", "WHAT?", "THEY ARE", "LIKE", "YES", "HOLD", "BLANK", "RIGHT", "RED", "YOU", "PRESS", "UR", "SURE", "LEED", "UHHH", "FIRST", "SAYS", "UH HUH", "YOUR", "LEAD", "EMPTY", "DISPLAY", "REPEAT", "NO", "UH UH", "U", "THEY’RE", "ERROR", "GOT IT", "LOUDER", "LED", "YOU’RE", "SEE", "NOTHING", "READY", "THERE", "READ", "HOLD ON"},
		new string[] {"DISPLAY", "WAIT", "WHAT?", "LIKE", "THERE", "THEY ARE", "READ", "READY", "RED", "THEIR", "SEE", "LED", "YOU", "NEXT", "REPEAT", "YOUR", "YOU’RE", "MIDDLE", "LEFT", "LOUDER", "ERROR", "YES", "SAYS", "NOTHING", "NONE", "LEED", "LEAD", "UHHH", "THEY’RE", "SURE", "DONE", "FIRST", "GOT IT", "U", "OKAY", "CEE", "HOLD ON", "PRESS", "C", "EMPTY", "HOLD", "BLANK", "REED", "UH HUH", "UR", "RIGHT", "WHAT", "UH UH", "YOU ARE", "NO"},
		new string[] {"NEXT", "NO", "WAIT", "HOLD", "YOU ARE", "HOLD ON", "THEIR", "FIRST", "U", "YOU’RE", "SAYS", "THEY ARE", "UHHH", "LEED", "NOTHING", "BLANK", "LOUDER", "YOU", "NONE", "C", "READ", "READY", "THEY’RE", "LEFT", "LEAD", "CEE", "UR", "MIDDLE", "RED", "DONE", "OKAY", "ERROR", "REED", "DISPLAY", "SEE", "SURE", "GOT IT", "PRESS", "WHAT?", "YES", "RIGHT", "UH HUH", "REPEAT", "LED", "YOUR", "THERE", "WHAT", "LIKE", "UH UH", "EMPTY"},
		new string[] {"LEFT", "CEE", "ERROR", "LED", "YOU", "THEIR", "WHAT", "DONE", "UH UH", "UHHH", "UR", "YES", "MIDDLE", "BLANK", "C", "SAYS", "OKAY", "SEE", "PRESS", "READY", "RIGHT", "SURE", "HOLD ON", "LOUDER", "REED", "NONE", "DISPLAY", "YOU ARE", "NO", "U", "LEAD", "HOLD", "NOTHING", "YOU’RE", "THERE", "READ", "YOUR", "WAIT", "THEY ARE", "FIRST", "THEY’RE", "UH HUH", "NEXT", "LEED", "REPEAT", "EMPTY", "GOT IT", "WHAT?", "RED", "LIKE"},
		new string[] {"SURE", "THEY’RE", "BLANK", "HOLD", "YOU ARE", "REED", "NEXT", "THEY ARE", "UHHH", "NONE", "UH HUH", "READY", "DISPLAY", "WHAT", "GOT IT", "YOU’RE", "UR", "THEIR", "WAIT", "READ", "UH UH", "HOLD ON", "LEAD", "LEED", "THERE", "LEFT", "MIDDLE", "EMPTY", "YOU", "SAYS", "PRESS", "NO", "FIRST", "OKAY", "LOUDER", "RED", "YES", "LIKE", "RIGHT", "SEE", "C", "LED", "DONE", "ERROR", "NOTHING", "REPEAT", "YOUR", "WHAT?", "CEE", "U"},
		new string[] {"THERE", "HOLD ON", "NOTHING", "UR", "WHAT?", "REPEAT", "LOUDER", "WAIT", "NEXT", "HOLD", "READ", "DISPLAY", "CEE", "WHAT", "LIKE", "RED", "MIDDLE", "FIRST", "YES", "YOU’RE", "YOU", "NONE", "LEFT", "BLANK", "YOU ARE", "C", "UHHH", "SAYS", "YOUR", "OKAY", "READY", "EMPTY", "THEY’RE", "SEE", "DONE", "U", "LED", "RIGHT", "ERROR", "REED", "THEIR", "UH HUH", "THEY ARE", "LEAD", "NO", "UH UH", "SURE", "LEED", "GOT IT", "PRESS"}
	};
	
	int[][] Correspondance = new int[][]{
		new int[] {7, 0, 9, 2, 9, 3},
		new int[] {4, 2, 5, 4, 0, 4},
		new int[] {8, 6, 0, 1, 1, 8},
		new int[] {4, 9, 5, 5, 9, 1},
		new int[] {7, 3, 1, 6, 4, 1},
		new int[] {2, 9, 3, 4, 6, 2},
		new int[] {8, 7, 9, 9, 8, 7},
		new int[] {6, 4, 1, 3, 0, 7},
		new int[] {6, 0, 1, 5, 0, 2},
		new int[] {4, 2, 8, 9, 8, 1},
		new int[] {8, 8, 1, 1, 8, 4},
		new int[] {3, 5, 8, 8, 7, 6},
		new int[] {0, 2, 9, 2, 4, 5},
		new int[] {6, 0, 5, 0, 9, 3},
		new int[] {1, 0, 5, 4, 8, 3},
		new int[] {9, 5, 5, 5, 9, 8},
		new int[] {1, 8, 6, 7, 4, 6},
		new int[] {4, 0, 2, 1, 0, 7},
		new int[] {6, 1, 8, 6, 0, 0},
		new int[] {3, 4, 7, 3, 2, 2},
		new int[] {1, 3, 9, 4, 0, 6},
		new int[] {1, 2, 0, 3, 3, 5},
		new int[] {2, 5, 0, 3, 9, 8},
		new int[] {3, 3, 9, 2, 3, 1},
		new int[] {7, 5, 1, 1, 9, 7},
		new int[] {3, 7, 6, 7, 7, 8},
		new int[] {6, 8, 0, 8, 2, 1},
		new int[] {2, 8, 6, 0, 3, 0},
		new int[] {7, 7, 5, 8, 9, 5},
		new int[] {9, 2, 0, 7, 6, 7},
		new int[] {6, 4, 9, 2, 9, 4},
		new int[] {2, 5, 5, 6, 6, 3},
		new int[] {6, 4, 5, 3, 0, 2},
		new int[] {2, 7, 2, 7, 7, 4},
		new int[] {3, 1, 5, 8, 9, 6},
		new int[] {8, 2, 9, 6, 4, 7},
		new int[] {3, 3, 9, 9, 5, 5},
		new int[] {5, 2, 6, 0, 5, 5},
		new int[] {5, 8, 1, 9, 9, 9},
		new int[] {7, 0, 0, 4, 2, 1},
		new int[] {2, 7, 4, 1, 1, 3},
		new int[] {3, 7, 5, 3, 6, 0},
		new int[] {3, 9, 9, 0, 3, 7},
		new int[] {4, 8, 4, 1, 8, 8},
		new int[] {2, 8, 1, 4, 4, 3},
		new int[] {0, 5, 3, 2, 7, 6},
		new int[] {8, 6, 1, 6, 7, 7},
		new int[] {7, 8, 4, 5, 2, 0},
		new int[] {2, 9, 5, 4, 1, 4},
		new int[] {4, 1, 6, 6, 0, 6}
	};
	
    int DisplayNumber, Color, Solution, Stage = 0;
	int[] ButtonColor = new int[6];
	bool IsActivated = false;
	string TrueAnswer = "";
	
	//Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private bool ModuleSolved;
	
	//Souvenir Dedicated
	string[] Answers = new string[2];
	string[] AnswerColors = new string[2];
	
	void Active()
	{
		moduleId = moduleIdCounter++;
	}
	
	void Start()
	{
		Init();
		Module.OnActivate += ActivateModule;
	}

	void Init()
	{
		for (int i = 0; i < buttons.Length; i++)
		{
			TextMesh buttonTextMesh = buttons[i].GetComponentInChildren<TextMesh>();
			buttonTextMesh.text = " ";
			int Collect = i;
			buttons[i].OnInteract += delegate
			{
				PressButton(Collect);
				return false; 
			};
		}
		
		for (int i = 0; i < LEDs.Length; i++)
		{
            foreach (MeshRenderer Render in LEDs[i].GetComponentsInChildren<MeshRenderer>())
			{
				if (Render.name == "Off")
				{
					Render.enabled = true;
				}
				else if (Render.name == "On")
                {
					Render.enabled = false;
				}
			}
		}
	}
	
	void PressButton(int Collect)
	{
		buttons[Collect].AddInteractionPunch(0.3f);
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
		if (!ModuleSolved)
		{
			if (IsActivated)
			{
				TextMesh buttonTextMesh = buttons[Collect].GetComponentInChildren<TextMesh>();
				Debug.LogFormat("[What's on Second #{0}] You pressed the button with the label \"{1}\".", moduleId, buttonTextMesh.text);
				if (buttonTextMesh.text == TrueAnswer)
				{
					foreach (MeshRenderer Render in LEDs[Stage].GetComponentsInChildren<MeshRenderer>())
                    {
                        if (Render.name == "On")
                        {
                            Render.enabled = true;
                        }
                        else if(Render.name == "Off")
                        {
                            Render.enabled = false;
                        }
                    }
                    Stage++;
					if (Stage == 3)
					{
						Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.CorrectChime, transform);
						Debug.LogFormat("[What's on Second #{0}] All three stage solved successfully. Module solved.", moduleId, (Stage+1).ToString());
						Module.HandlePass();
						ModuleSolved = true;
						Stage = 0;
					}
					else
					{
						Answers[Stage - 1] = TrueAnswer;
						string[] Colors = {"Red", "Green", "Blue", "Yellow", "Magenta", "Cyan"};
						AnswerColors[Stage - 1] = Colors[Color];
						Debug.LogFormat("[What's on Second #{0}] You pressed the correct label. Moving forward to another stage.", moduleId);
						StartCoroutine(NewStage(false));
					}
				}
				
				else
				{
					Stage = 0;
					Debug.LogFormat("[What's on Second #{0}] You pressed the incorrect label. The module striked and performed a reset.", moduleId);
					for (int i = 0; i < LEDs.Length; i++)
					{
						foreach (MeshRenderer Render in LEDs[i].GetComponentsInChildren<MeshRenderer>())
						{
							if (Render.name == "On")
							{
								Render.enabled = false;
							}
						}
					}
					Module.HandleStrike();
					StartCoroutine(NewStage(false));
				}
			}
		}
	}

	void ActivateModule()
	{
		IsActivated = true;
		StartCoroutine(NewStage(true));
	}

	protected IEnumerator NewStage(bool isFirstStage)
	{
		IsActivated = false;
		if(!isFirstStage)
		{
			Display.text = " ";
			yield return new WaitForSeconds (1.1f);
			for (int i = 0; i < buttons.Length; i++)
			{
				TextMesh buttonTextMesh = buttons[i].GetComponentInChildren<TextMesh>();
				buttonTextMesh.text = " ";
				foreach (MeshRenderer Render in buttons[i].GetComponentsInChildren<MeshRenderer>()) 
				{
					if (Render.name == "Model")
					{
						Render.enabled = false;
					}
				}
				yield return new WaitForSeconds (0.05f);
			}
			yield return new WaitForSeconds (0.8f);
		}
		
		yield return new WaitForSeconds (0.3f);
		List<string>Mecha = Phrase.ToList().Shuffle();
		for(int i = 0; i < buttons.Length; i++)
		{
			TextMesh buttonTextMesh = buttons[i].GetComponentInChildren<TextMesh>();
			buttonTextMesh.text = Mecha[i];
			ButtonColor[i] = UnityEngine.Random.Range(0, 6);
			switch (ButtonColor[i])
			{
				case 0:
					buttonTextMesh.color = new Color (255, 0, 0);
					break;
				case 1:
					buttonTextMesh.color = new Color (0, 255, 0);
					break;
				case 2:
					buttonTextMesh.color = new Color (0, 0, 255);
					break;
				case 3:
					buttonTextMesh.color = new Color (255, 255, 0);
					break;
				case 4:
					buttonTextMesh.color = new Color (255, 0, 255);
					break;
				case 5:
					buttonTextMesh.color = new Color (0, 255, 255);
					break;
				default:
					break;
			}
            foreach (MeshRenderer Render in buttons[i].GetComponentsInChildren<MeshRenderer>())
            {
                if (Render.name == "Model")
                {
                    Render.enabled = true;
                }
            }
            yield return new WaitForSeconds (0.05f);
		}
		
		DisplayNumber = UnityEngine.Random.Range(0, Phrase.Length);
		Color = UnityEngine.Random.Range(0,6);
		Display.text = Phrase[DisplayNumber];
		switch (Color)
		{
			case 0:
				Display.color = new Color (255, 0, 0);
				break;
			case 1:
				Display.color = new Color (0, 255, 0);
				break;
			case 2:
				Display.color = new Color (0, 0, 255);
				break;
			case 3:
				Display.color = new Color (255, 255, 0);
				break;
			case 4:
				Display.color = new Color (255, 0, 255);
				break;
			case 5:
				Display.color = new Color (0, 255, 255);
				break;
			default:
				break;
		}
		
		IsActivated = true;
		int Focus = Correspondance[Array.IndexOf(Phrase, buttons[DisplayPosition[Color][DisplayNumber]].GetComponentInChildren<TextMesh>().text)][ButtonColor[ColorPosition[Color][DisplayNumber]]];
		
		List<int> Coordinates = new List<int>();
		for (int x = 0; x < 6; x++)
		{
			Coordinates.Add(Array.IndexOf(ShufflePlay[Focus], Mecha[x]));
		}
		Coordinates.Remove(Array.IndexOf(ShufflePlay[Focus], buttons[DisplayPosition[Color][DisplayNumber]].GetComponentInChildren<TextMesh>().text));
		Coordinates.Sort();
		TrueAnswer = ShufflePlay[Focus][Coordinates[2]];
		IsActivated = true;
		string[] Colors = {"Red", "Green", "Blue", "Yellow", "Magenta", "Cyan"};
		Debug.LogFormat("[What's on Second #{0}] Stage {1} ", moduleId, (Stage+1).ToString());
		Debug.LogFormat("[What's on Second #{0}] Main display text/color: {1}/{2}", moduleId, Phrase[DisplayNumber], Colors[Color]);
		string ButtonsLite = "Button texts/color in reading order: ";
		for (int y = 0; y < 6; y++)
		{
			ButtonsLite += y != 5 ? buttons[y].GetComponentInChildren<TextMesh>().text + "/" + Colors[ButtonColor[y]] + " - " : buttons[y].GetComponentInChildren<TextMesh>().text + "/" + Colors[ButtonColor[y]];
		}
		Debug.LogFormat("[What's on Second #{0}] {1}", moduleId, ButtonsLite);
		Debug.LogFormat("[What's on Second #{0}] Correct combination that must be used: {1}/{2}", moduleId, buttons[DisplayPosition[Color][DisplayNumber]].GetComponentInChildren<TextMesh>().text, Colors[ButtonColor[ColorPosition[Color][DisplayNumber]]]);
		Debug.LogFormat("[What's on Second #{0}] Correct list that must be used: List {1}", moduleId, (Focus + 1).ToString());
		Debug.LogFormat("[What's on Second #{0}] The button label that appears in the middle of the list: {1}", moduleId, TrueAnswer);
    }
	
	//twitch plays
    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"To press the corresponding button position on the module, use the command !{0} press [1-6]";
    #pragma warning restore 414
	
	string[] ValidNumbers = {"1", "2", "3", "4", "5", "6"};
    IEnumerator ProcessTwitchCommand(string command)
    {
		string[] parameters = command.Split(' ');
		if (!IsActivated)
		{
			yield return "sendtochaterror Unable to interact with the module currently. The command was not processed.";
			yield break;
		}
		
		if (parameters.Length != 2)
		{
			yield return "sendtochaterror Invalid parameter length. The command was not processed.";
			yield break;
		}
			
        if (Regex.IsMatch(parameters[0], @"^\s*press\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
			if (!parameters[1].EqualsAny(ValidNumbers))
			{
				yield return "sendtochaterror Invalid button position sent. The command was not processed.";
				yield break;
			}
			buttons[Int32.Parse(parameters[1]) - 1].OnInteract();
        }
	}
}
