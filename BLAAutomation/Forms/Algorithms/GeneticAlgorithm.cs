using System;
using System.Collections.Generic;
using System.Linq;
using BLAAutomation.Models;

namespace BLAAutomation.Forms.Algorithms
{
    public class GeneticAlgorithm
    {
        private Project SelectedProject;
        private int PopulationsCount;
        private int GenerationsCount;
        private int ChildrenNumber;
        private int MutationNumber;
        private Random rnd;

        private string[,] BeginPopulation;
        private string[,] ChildrenPopulation;
        private List<string[]> MutationPopulation;
        private List<string[]> ReproductionGroup;
        private string[,] PopulationAfterSelection;
        private double[] Fitnesses;
        public string[] BestSolution;
        public double BestFitness;

        private bool[] indexesPopulationsForCross;
        private int[] SortedParentsIndexesForChildrenPopulation;
        private int[] SortedParentsIndexesForMutationPopulation;
        private bool FirstGeneration;

        public GeneticAlgorithm(Project selectedProject, int populationsCount, int generationsCount, int childrenNumber, int mutationNumber)
        {
            SelectedProject = selectedProject;
            PopulationsCount = populationsCount;
            GenerationsCount = generationsCount;
            rnd = new Random();
            FirstGeneration = true;
            ChildrenNumber = childrenNumber;
            MutationNumber = mutationNumber;
        }

        private bool IsCarryingLimitExceeded(string[] deviceIndexes)
        {
            var compartments = SelectedProject.Compartments.ToArray();
            double[] compartmentsMass = new double[compartments.Length];
            for (int i = 0; i < compartments.Length; i++)
            {
                compartmentsMass[i] = 0.0;
            }

            var devices = SelectedProject.DevicesForPlacement.ToArray();
            for (int i = 0; i < compartments.Length; i++)
            {
                var tempPosition = compartments[i];
                var checkingDeviceToPosition = devices[int.Parse(deviceIndexes[i]) - 1];
                compartmentsMass[tempPosition.CompartmentNumber - 1] += checkingDeviceToPosition.Weight;
            }

            for (int i = 0; i < compartmentsMass.Length; i++)
            {
                if (compartmentsMass[i] > compartments[i].LoadCapacity)
                    return true;
            }
            return false;
        }

        private void CreateOneParentChromosome(out string[] Population)
        {
            var devicesCount = SelectedProject.DevicesForPlacement.Count;
            Population = new string[devicesCount];
            int[] pop = new int[devicesCount];
            for (int i = 0; i < devicesCount; i++)
            {
                pop[i] = i + 1;
            }

            for (int i = 0; i < devicesCount; i++)
            {
                int index = rnd.Next(0, devicesCount);
                int temp = pop[i];
                pop[i] = pop[index];
                pop[index] = temp;
            }
            for (int i = 0; i < devicesCount; i++)
            {
                Population[i] = pop[i].ToString();
            }
        }

        private void CreateBeginPopulation()
        {
            var devicesCount = SelectedProject.DevicesForPlacement.Count;
            BeginPopulation = new string[PopulationsCount, devicesCount];
            for (int i = 0; i < PopulationsCount; i++)
            {
                string[] Pop;
                CreateOneParentChromosome(out Pop);
                for (int j = 0; j < devicesCount; j++)
                {
                    BeginPopulation[i, j] = Pop[j];
                }
                bool isPopulationCorrect = !IsCarryingLimitExceeded(Pop);
                if (!isPopulationCorrect)
                {
                    while (!isPopulationCorrect)
                    {
                        CreateOneParentChromosome(out Pop);
                        for (int j = 0; j < devicesCount; j++)
                        {
                            BeginPopulation[i, j] = Pop[j];
                        }
                        isPopulationCorrect = !IsCarryingLimitExceeded(Pop);
                    }
                }
            }
        }

        private void CrossingBetweenTwoParents(int Parent1, int Parent2, int BreakingPoint, out string[] Children1, out string[] Children2)
        {
            var devicesCount = SelectedProject.DevicesForPlacement.Count;
            Children1 = new string[devicesCount];
            Children2 = new string[devicesCount];
            for (int i = 0; i < devicesCount; i++)
            {
                Children1[i] = BeginPopulation[Parent1, i];
                Children2[i] = BeginPopulation[Parent2, i];
            }
            int k = 0;
            for (int i = 0; i < devicesCount; i++)
            {
                if (i + BreakingPoint < devicesCount)
                {
                    Children1[i] = BeginPopulation[Parent1, i + BreakingPoint];
                    Children2[i] = BeginPopulation[Parent2, i + BreakingPoint];
                }
                else
                {
                    Children1[i] = BeginPopulation[Parent1, k];
                    Children2[i] = BeginPopulation[Parent2, k];
                    k++;
                }
            }
        }

        private void CreateChildrenPopulation()
        {
            var devicesCount = SelectedProject.DevicesForPlacement.Count;
            ChildrenPopulation = new string[ChildrenNumber, devicesCount];
            SortedParentsIndexesForChildrenPopulation = new int[ChildrenNumber];
            int BreakingPoint = rnd.Next(1, devicesCount - 1);
            indexesPopulationsForCross = new bool[PopulationsCount];
            for (int j = 0; j < PopulationsCount; j++)
            {
                indexesPopulationsForCross[j] = false;
            }

            for (int i = 0; i < ChildrenNumber / 2; i++)
            {
                int Parent1 = rnd.Next(0, PopulationsCount);
                while (indexesPopulationsForCross[Parent1])
                    Parent1 = rnd.Next(0, PopulationsCount);
                indexesPopulationsForCross[Parent1] = true;

                int Parent2 = rnd.Next(0, PopulationsCount);
                while (indexesPopulationsForCross[Parent2])
                    Parent2 = rnd.Next(0, PopulationsCount);
                indexesPopulationsForCross[Parent2] = true;

                SortedParentsIndexesForChildrenPopulation[2 * i] = Parent1;
                SortedParentsIndexesForChildrenPopulation[2 * i + 1] = Parent2;
                string[] Children1, Children2;
                CrossingBetweenTwoParents(Parent1, Parent2, BreakingPoint, out Children1, out Children2);
                for (int j = 0; j < devicesCount; j++)
                {
                    ChildrenPopulation[2 * i, j] = Children1[j];
                    ChildrenPopulation[2 * i + 1, j] = Children2[j];
                }

                bool isPopulationCorrect1 = !IsCarryingLimitExceeded(Children1);
                bool isPopulationCorrect2 = !IsCarryingLimitExceeded(Children2);
                if (!isPopulationCorrect1 || !isPopulationCorrect2)
                {
                    while (!isPopulationCorrect1 || !isPopulationCorrect2)
                    {
                        Parent1 = rnd.Next(0, PopulationsCount);
                        while (indexesPopulationsForCross[Parent1])
                            Parent1 = rnd.Next(0, PopulationsCount);
                        indexesPopulationsForCross[Parent1] = true;

                        Parent2 = rnd.Next(0, PopulationsCount);
                        while (indexesPopulationsForCross[Parent2])
                            Parent2 = rnd.Next(0, PopulationsCount);
                        indexesPopulationsForCross[Parent2] = true;

                        SortedParentsIndexesForChildrenPopulation[2 * i] = Parent1;
                        SortedParentsIndexesForChildrenPopulation[2 * i + 1] = Parent2;

                        CrossingBetweenTwoParents(Parent1, Parent2, BreakingPoint, out Children1, out Children2);
                        for (int j = 0; j < devicesCount; j++)
                        {
                            ChildrenPopulation[2 * i, j] = Children1[j];
                            ChildrenPopulation[2 * i + 1, j] = Children2[j];
                        }
                        isPopulationCorrect1 = !IsCarryingLimitExceeded(Children1);
                        isPopulationCorrect2 = !IsCarryingLimitExceeded(Children2);
                    }
                }
            }
        }

        private void CreateMutationPopulation()
        {
            var devicesCount = SelectedProject.DevicesForPlacement.Count;
            MutationPopulation = new List<string[]>();
            for (int i = 0; i < MutationNumber; i++)
            {
                MutationPopulation.Add(new string[devicesCount]);
            }

            SortedParentsIndexesForMutationPopulation = new int[MutationNumber];
            int k = 0;
            for (int i = 0; i < PopulationsCount; i++)
            {
                if (!indexesPopulationsForCross[i])
                {
                    for (int j = 0; j < devicesCount; j++)
                    {
                        MutationPopulation[k][j] = BeginPopulation[i, j];
                    }
                    SortedParentsIndexesForMutationPopulation[k] = i;
                    k++;
                }
            }

            for (int i = 0; i < MutationNumber; i++)
            {
                int index1 = rnd.Next(0, devicesCount);
                int index2 = rnd.Next(0, devicesCount);
                while (index2 == index1)
                    index2 = rnd.Next(0, devicesCount);
                string temp = MutationPopulation[i][index1];
                MutationPopulation[i][index1] = MutationPopulation[i][index2];
                MutationPopulation[i][index2] = temp;

                bool isPopulationCorrect = !IsCarryingLimitExceeded(MutationPopulation[i]);
                if (!isPopulationCorrect)
                {
                    while (!isPopulationCorrect)
                    {
                        index1 = rnd.Next(0, devicesCount);
                        index2 = rnd.Next(0, devicesCount);
                        while (index2 == index1)
                            index2 = rnd.Next(0, devicesCount);
                        temp = MutationPopulation[i][index1];
                        MutationPopulation[i][index1] = MutationPopulation[i][index2];
                        MutationPopulation[i][index2] = temp;
                        isPopulationCorrect = !IsCarryingLimitExceeded(MutationPopulation[i]);
                    }
                }
            }
        }

        private void CreateReproductionGroup()
        {
            var devicesCount = SelectedProject.DevicesForPlacement.Count;
            ReproductionGroup = new List<string[]>();
            for (int i = 0; i < PopulationsCount; i++)
            {
                string[] toAdd = new string[devicesCount];
                for (int j = 0; j < devicesCount; j++)
                {
                    toAdd[j] = BeginPopulation[i, j];
                }
                ReproductionGroup.Add(toAdd);
            }
            for (int i = 0; i < ChildrenNumber; i++)
            {
                string[] toAdd = new string[devicesCount];
                for (int j = 0; j < devicesCount; j++)
                {
                    toAdd[j] = ChildrenPopulation[i, j];
                }
                ReproductionGroup.Add(toAdd);
            }
            for (int i = 0; i < MutationNumber; i++)
            {
                string[] toAdd = new string[devicesCount];
                for (int j = 0; j < devicesCount; j++)
                {
                    toAdd[j] = MutationPopulation[i][j];
                }
                ReproductionGroup.Add(toAdd);
            }
        }

        private double CalculateFitness(string[] chromosome)
        {
            var positions = SelectedProject.SelectedFuselage.ToArray();
            var devices = SelectedProject.DevicesForPlacement.ToArray();
            double fitness = 0;
            for (int i = 0; i < chromosome.Length; i++)
            {
                fitness += devices[int.Parse(chromosome[i]) - 1].NoiseImmunity;
            }
            return fitness;
        }

        public void MainProcess()
        {
            for (int g = 0; g < GenerationsCount; g++)
            {
                if (FirstGeneration)
                    CreateBeginPopulation();
                CreateChildrenPopulation();
                CreateMutationPopulation();
                CreateReproductionGroup();
                Fitnesses = new double[ReproductionGroup.Count];
                for (int i = 0; i < ReproductionGroup.Count; i++)
                {
                    Fitnesses[i] = CalculateFitness(ReproductionGroup[i]);
                }
                BestSolution = new string[SelectedProject.DevicesForPlacement.Count];
                int max = 0;
                for (int i = 0; i < ReproductionGroup.Count; i++)
                {
                    if (Fitnesses[i] > Fitnesses[max])
                        max = i;
                }
                for (int i = 0; i < SelectedProject.DevicesForPlacement.Count; i++)
                    BestSolution[i] = ReproductionGroup[max][i];
                BestFitness = Fitnesses[max];
                PopulationAfterSelection = new string[PopulationsCount, SelectedProject.DevicesForPlacement.Count];
                for (int i = 0; i < PopulationsCount; i++)
                {
                    for (int j = 0; j < SelectedProject.DevicesForPlacement.Count; j++)
                    {
                        PopulationAfterSelection[i, j] = ReproductionGroup[i][j];
                    }
                }
                BeginPopulation = PopulationAfterSelection;
            }
        }
    }
}
