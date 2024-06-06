using LiveCharts.Wpf.Charts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BLAAutomation.Models
{
    public class GeneticAlgorithm
    {
        private Project SelectedProject;
        private int PopulationsCount;
        private int GenerationsCount;
        private int ChildrenNumber;
        private int MutationNumber;
        private Random rnd;

        private List<string[]> Population;
        private List<string[]> ChildrenPopulation;
        private List<string[]> MutationPopulation;
        private List<string[]> ReproductionGroup;
        private double[] Fitnesses;
        public string[] BestSolution;
        public double BestFitness;

        public GeneticAlgorithm(Project selectedProject, int populationsCount, int generationsCount, int childrenNumber, int mutationNumber)
        {
            SelectedProject = selectedProject;
            PopulationsCount = populationsCount;
            GenerationsCount = generationsCount;
            ChildrenNumber = childrenNumber;
            MutationNumber = mutationNumber;
            rnd = new Random();
            InitializePopulation();
        }

        private void InitializePopulation()
        {
            Population = new List<string[]>();
            for (int i = 0; i < PopulationsCount; i++)
            {
                string[] chromosome = CreateRandomChromosome();
                while (IsCarryingLimitExceeded(chromosome))
                {
                    chromosome = CreateRandomChromosome();
                }
                Population.Add(chromosome);
            }

            // Проверка и вывод начальной популяции
            //ShowPopulation("Начальная популяция", Population);
        }

        private string[] CreateRandomChromosome()
        {
            string[] chromosome = new string[SelectedProject.DevicesForPlacement.Count];
            for (int i = 0; i < chromosome.Length; i++)
            {
                chromosome[i] = (i + 1).ToString();
            }
            chromosome = chromosome.OrderBy(x => rnd.Next()).ToArray(); // исправленное название метода OrderBy
            return chromosome;
        }

        private bool IsCarryingLimitExceeded(string[] chromosome)
        {
            double[] compartmentsWeight = new double[SelectedProject.UavParameters.Compartments.Count];
            for (int i = 0; i < chromosome.Length; i++)
            {
                int deviceIndex = int.Parse(chromosome[i]) - 1;
                var device = SelectedProject.DevicesForPlacement.ElementAt(deviceIndex).UavDevice;
                compartmentsWeight[i] += device.Weight;
                if (compartmentsWeight[i] > SelectedProject.UavParameters.Compartments.ElementAt(i).LoadCapacity)
                {
                    return true;
                }
            }
            return false;
        }

        public void Run()
        {
            for (int generation = 0; generation < GenerationsCount; generation++)
            {
                EvaluateFitness();
                Selection();
                Crossover();
                Mutation();
                CreateNewPopulation();
            }
        }

        private void EvaluateFitness()
        {
            Fitnesses = new double[Population.Count];
            for (int i = 0; i < Population.Count; i++)
            {
                Fitnesses[i] = CalculateFitness(Population[i]);
            }
            // Проверка и вывод значений приспособленности
            //ShowFitness("Приспособленность популяции", Fitnesses);
        }

        private double CalculateFitness(string[] chromosome)
        {
            double fitness = 0;
            for (int i = 0; i < chromosome.Length; i++)
            {
                int deviceIndex = int.Parse(chromosome[i]) - 1;
                var device = SelectedProject.DevicesForPlacement.ElementAt(deviceIndex).UavDevice;
                fitness += device.NoiseImmunity;
            }
            return fitness;
        }

        private void Selection()
        {
            int tournamentSize = 5;
            List<string[]> selected = new List<string[]>();
            for (int i = 0; i < PopulationsCount; i++)
            {
                string[] best = null;
                double bestFitness = double.MinValue;
                for (int j = 0; j < tournamentSize; j++)
                {
                    string[] candidate = Population[rnd.Next(PopulationsCount)];
                    double candidateFitness = CalculateFitness(candidate);
                    if (candidateFitness > bestFitness)
                    {
                        best = candidate;
                        bestFitness = candidateFitness;
                    }
                }
                selected.Add(best);
            }
            Population = selected;

            // Проверка и вывод популяции после селекции
            //ShowPopulation("Популяция после селекции", Population);
        }

        private void Crossover()
        {
            ChildrenPopulation = new List<string[]>();
            for (int i = 0; i < ChildrenNumber; i += 2)
            {
                string[] parent1 = Population[rnd.Next(PopulationsCount)];
                string[] parent2 = Population[rnd.Next(PopulationsCount)];
                int crossoverPoint = rnd.Next(1, parent1.Length - 1);
                string[] child1 = new string[parent1.Length];
                string[] child2 = new string[parent1.Length];
                for (int j = 0; j < crossoverPoint; j++)
                {
                    child1[j] = parent1[j];
                    child2[j] = parent2[j];
                }
                for (int j = crossoverPoint; j < parent1.Length; j++)
                {
                    child1[j] = parent2[j];
                    child2[j] = parent1[j];
                }
                ChildrenPopulation.Add(child1);
                ChildrenPopulation.Add(child2);
            }

            // Проверка и вывод популяции после кроссинговера
            //ShowPopulation("Популяция после кроссинговера", ChildrenPopulation);
        }

        private void Mutation()
        {
            MutationPopulation = new List<string[]>();
            for (int i = 0; i < MutationNumber; i++)
            {
                string[] mutant = Population[rnd.Next(PopulationsCount)].ToArray();
                int index1 = rnd.Next(mutant.Length);
                int index2 = rnd.Next(mutant.Length);
                while (index2 == index1)
                {
                    index2 = rnd.Next(mutant.Length);
                }
                string temp = mutant[index1];
                mutant[index1] = mutant[index2];
                mutant[index2] = temp;
                MutationPopulation.Add(mutant);
            }

            // Проверка и вывод популяции после мутации
            //ShowPopulation("Популяция после мутации", MutationPopulation);
        }

        private void CreateNewPopulation()
        {
            ReproductionGroup = new List<string[]>(Population);
            ReproductionGroup.AddRange(ChildrenPopulation);
            ReproductionGroup.AddRange(MutationPopulation);
            Fitnesses = new double[ReproductionGroup.Count];
            for (int i = 0; i < ReproductionGroup.Count; i++)
            {
                Fitnesses[i] = CalculateFitness(ReproductionGroup[i]);
            }

            List<string[]> newPopulation = new List<string[]>();
            for (int i = 0; i < PopulationsCount; i++)
            {
                double maxFitness = double.MinValue;
                int maxIndex = -1;
                for (int j = 0; j < Fitnesses.Length; j++)
                {
                    if (Fitnesses[j] > maxFitness)
                    {
                        maxFitness = Fitnesses[j];
                        maxIndex = j;
                    }
                }
                newPopulation.Add(ReproductionGroup[maxIndex]);
                Fitnesses[maxIndex] = double.MinValue; // Обеспечиваем, что данная хромосома не будет выбрана снова
            }
            Population = newPopulation;

            // Проверка и вывод новой популяции
            //ShowPopulation("Новая популяция", Population);
        }

        public string[] GetBestSolution()
        {
            double maxFitness = double.MinValue;
            int maxIndex = -1;
            for (int i = 0; i < Population.Count; i++)
            {
                double fitness = CalculateFitness(Population[i]);
                if (fitness > maxFitness)
                {
                    maxFitness = fitness;
                    maxIndex = i;
                }
            }
            BestFitness = maxFitness;
            BestSolution = Population[maxIndex];
            return BestSolution;
        }
        /*
        private void ShowPopulation(string title, List<string[]> population)
        {
            string message = $"{title}:\n";
            foreach (var chromosome in population)
            {
                message += string.Join(", ", chromosome) + "\n";
            }
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowFitness(string title, double[] fitnesses)
        {
            string message = $"{title}:\n";
            for (int i = 0; i < fitnesses.Length; i++)
            {
                message += $"Хромосома {i + 1}: {fitnesses[i]}\n";
            }
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }*/
    }
}
