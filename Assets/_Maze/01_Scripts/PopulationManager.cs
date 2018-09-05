using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PopulationManager : MonoBehaviour {

    public GameObject botPrefab;
    public int populationSize = 2;
    List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0f;
    public float trialTime = 25;
    int generation = 1;

    public Text generationText;
    public Text trialTimeText;
    public Text populationText;

	void Start () {
        for (int i = 0; i < populationSize; i++)
        {
            Vector3 startingPos = new Vector3(transform.position.x, // + Random.Range(-1f, 1f),
                                              transform.position.y,
                                              transform.position.z); // + Random.Range(-1f, 1f));
            GameObject bot = Instantiate(botPrefab, startingPos, Quaternion.identity);
            bot.GetComponent<Brain>().Init();
            population.Add(bot);
        }
	}
	
    private GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 startingPos = new Vector3(transform.position.x, // + Random.Range(-1f, 1f),
                                            transform.position.y,
                                          transform.position.z); // + Random.Range(-1f, 1f));
        GameObject offspring = Instantiate(botPrefab, startingPos, Quaternion.identity);
        Brain brain = offspring.GetComponent<Brain>();

        if (Random.Range(1, 100) == 1)
        {
            brain.Init();
            brain.dna.Mutate();
        }
        else
        {
            brain.Init();
            brain.dna.Combine(parent1.GetComponent<Brain>().dna, parent2.GetComponent<Brain>().dna);
        }
        return offspring;
    }

    private void BreedNewPopulation()
    {
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<Brain>().maxDistanceFromOrigin).ToList();
        population.Clear();

        // breed upper half of sorted list
        for (int i = (int)(sortedList.Count / 2f) - 1; i < sortedList.Count - 1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        // destroy all parents in previous population
        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }
        generation++;
    }

	void Update () {
        elapsed += Time.deltaTime;
        if (elapsed >= trialTime)
        {
            BreedNewPopulation();
            elapsed = 0;
        }

        UpdateUI();
	}

    private void UpdateUI()
    {
        generationText.text = "Generation: " + generation.ToString();
        trialTimeText.text = "Trial time: " + ((int)elapsed).ToString();
        populationText.text = "Population: " + population.Count.ToString();
    }
}
