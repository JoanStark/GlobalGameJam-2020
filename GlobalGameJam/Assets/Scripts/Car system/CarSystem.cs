﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSystem : MonoBehaviour
{
    public GameObject carStop;
    public GameObject carQuest;

    public List<GameObject> carEffects = new List<GameObject>();

    private GameObject mom;

    public List<CarEffects> errors = new List<CarEffects>();

    public int numberOfErrors = 3;
    private int randomProblem;

    public bool debugFinish = false;

    void Awake()
    {
        for (int i = 0; i < numberOfErrors; i++)
        {
            CarEffects item = null;

            int counter = 0;
            bool whil = false;

            do
            {
                whil = false;
                randomProblem = Random.Range(1, 7);
                switch (randomProblem)
                {
                    case 1: // pintura
                        item = new Paint();
                        break;

                    case 2: //ruedasWheels
                        item = new Wheels();
                        break;

                    case 3: // luces
                        item = new Lights();
                        break;

                    case 4: // nada
                        item = null;
                        break;

                    case 5:
                        item = new Engine();
                        break;

                    case 6:
                        item = new Oil();
                        break;
                }

                counter++;
                if (counter > 10)
                {
                    print("PENE");
                    break;
                }

                if(item != null)
                {
                    foreach (CarEffects x in errors)
                    {
                        if(x.typeOfEffect() == "oil" || x.typeOfEffect() == "engine" || x.typeOfEffect() == "paint")
                        {
                            if (x.typeOfEffect() == item.typeOfEffect())
                            {
                                whil = true;
                            }
                        }
                        else if(x.typeOfEffect() == "wheels" || x.typeOfEffect() == "lights")
                        {
                            if (x.typeOfEffect() == item.typeOfEffect())
                            {
                                if(x.effPosition() == item.effPosition())
                                {
                                    whil = true;
                                }
                            }
                        } 
                    }
                }
                
            } 
            while (whil);

            if(item != null)
            {
                errors.Add(item);
            }           
        }

        for (int i = 0; i < carEffects.Count; i++)
        {
            int fails = 0;
            foreach (CarEffects item in errors)
            {
                if (item.typeOfEffect() == "oil" || item.typeOfEffect() == "engine" || item.typeOfEffect() == "paint")
                {
                    print(carEffects[i].GetComponent<FixObject>().whatToDetect.ToString() + " " + item.typeOfEffect());

                    if(carEffects[i].GetComponent<FixObject>().whatToDetect.ToString() != item.typeOfEffect())
                    {
                        fails++;
                    }
                    else 
                }
                else if (x.typeOfEffect() == "wheels" || x.typeOfEffect() == "lights")
                {
                    if (x.typeOfEffect() == item.typeOfEffect())
                    {
                        if (x.effPosition() == item.effPosition())
                        {
                            whil = true;
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (debugFinish)
        {
            errors.Clear();
            this.GetComponent<CarMovement>().move = true;
            carStop.GetComponent<CarStop>().carStopped = false;
            debugFinish = false;

            Destroy(this.gameObject, 5f);
        }

    }
    public void createQuest()
    {
        GameObject questCanvas = GameObject.FindGameObjectWithTag("Quest");

        mom = Instantiate(carQuest, questCanvas.transform);
        mom.GetComponent<QuestManager>().SetupQuest(errors);
      
    }

    public void repared(string repaired)
    {
        //carQuest.GetComponent<QuestManager>().Repaired(repaired, errors);

        int f = 0;

        foreach (CarEffects item in errors)
        {
            if (item.typeOfEffect() == repaired)
            {
                mom.transform.GetChild(f).GetComponent<Image>().enabled = false;
                break;
            }   
            f++;
        }
    }
}
