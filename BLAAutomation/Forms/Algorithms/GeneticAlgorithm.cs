using BLAAutomation.Models;
using LiveCharts.Wpf.Charts.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
            ChildrenNumber = childrenNumber;
            MutationNumber = mutationNumber;

            // Проверка на наличие устройств и отсеков
            if (SelectedProject.DevicesForPlacement.Count == 0 || SelectedProject.UavParameters.Compartments.Count == 0)
            {
                throw new Exception("Проект должен содержать устройства и отсеки.");
            }

            rnd = new Random();
            FirstGeneration = true;
        }

        private bool isCarryingLimitExceeded(string[] deviceIndexes)
        {
            if (deviceIndexes.Length != SelectedProject.UavParameters.Compartments.Count)
            {
                throw new Exception("Количество индексов устройств не соответствует количеству отсеков.");
            }

            double[] compartmensMass = new double[SelectedProject.UavParameters.Compartments.Count];
            for (int i = 0; i < compartmensMass.Length; i++)
            {
                compartmensMass[i] = 0.0;
            }

            for (int i = 0; i < deviceIndexes.Length; i++)
            {
                if (i >= SelectedProject.UavParameters.Compartments.Count)
                {
                    return true;
                }

                var tempPosition = SelectedProject.UavParameters.Compartments.ElementAt(i);

                if (!int.TryParse(deviceIndexes[i], out int deviceIndex) || deviceIndex - 1 >= SelectedProject.DevicesForPlacement.Count)
                {
                    return true;
                }

                var checkingDeviceToPosition = SelectedProject.DevicesForPlacement
                    .Where(dfp => dfp.Id_Project == SelectedProject.ProjectNumber)
                    .Select(dfp => dfp.UavDevice)
                    .ElementAt(deviceIndex - 1);

                compartmensMass[tempPosition.CompartmentNumber - 1] += checkingDeviceToPosition.Weight;
            }

            for (int i = 0; i < compartmensMass.Length; i++)
            {
                if (compartmensMass[i] > SelectedProject.UavParameters.Compartments.ElementAt(i).LoadCapacity)
                {
                    return true;
                }
            }
            return false;
        }

        private void CreateOneParentChromosome(out string[] Population)
        {
            try
            {
                Population = new string[SelectedProject.DevicesForPlacement.Count];
                int[] pop = new int[SelectedProject.DevicesForPlacement.Count];
                for (int i = 0; i < SelectedProject.DevicesForPlacement.Count; i++)
                {
                    pop[i] = i + 1;
                }

                for (int i = 0; i < SelectedProject.DevicesForPlacement.Count; i++)
                {
                    int index = rnd.Next(0, SelectedProject.DevicesForPlacement.Count);
                    int temp = pop[i];
                    pop[i] = pop[index];
                    pop[index] = temp;
                }

                for (int i = 0; i < SelectedProject.DevicesForPlacement.Count; i++)
                {
                    Population[i] = pop[i].ToString();
                }

                MessageBox.Show($"CreateOneParentChromosome: Population = {string.Join(",", Population)}", "Отладка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в CreateOneParentChromosome: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Population = null;
            }
        }

        private void CreateBeginPopulation()
        {
            try
            {
                BeginPopulation = new string[PopulationsCount, SelectedProject.DevicesForPlacement.Count];
                for (int i = 0; i < PopulationsCount; i++)
                {
                    string[] Pop;
                    CreateOneParentChromosome(out Pop);
                    for (int j = 0; j < SelectedProject.DevicesForPlacement.Count; j++)
                    {
                        BeginPopulation[i, j] = Pop[j];
                    }
                    bool isPopulationCorrect = !isCarryingLimitExceeded(Pop);
                    if (!isPopulationCorrect)
                    {
                        while (!isPopulationCorrect)
                        {
                            CreateOneParentChromosome(out Pop);
                            for (int j = 0; j < SelectedProject.DevicesForPlacement.Count; j++)
                            {
                                BeginPopulation[i, j] = Pop[j];
                            }
                            isPopulationCorrect = !isCarryingLimitExceeded(Pop);
                        }
                    }
                }

                // Проверка размеров начальной популяции
                MessageBox.Show($"BeginPopulation: {BeginPopulation.GetLength(0)} x {BeginPopulation.GetLength(1)}", "Отладка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в CreateBeginPopulation: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CrossingBetweenTwoParents(int Parent1, int Parent2, int BreakingPoint, out string[] Children1, out string[] Children2)
        {
            try
            {
                // Проверяем входные значения
                MessageBox.Show($"CrossingBetweenTwoParents: Parent1 = {Parent1}, Parent2 = {Parent2}, BreakingPoint = {BreakingPoint}", "Отладка", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Children1 = new string[SelectedProject.DevicesForPlacement.Count];
                Children2 = new string[SelectedProject.DevicesForPlacement.Count];
                for (int i = 0; i < SelectedProject.DevicesForPlacement.Count; i++)
                {
                    Children1[i] = BeginPopulation[Parent1, i];
                    Children2[i] = BeginPopulation[Parent2, i];
                }
                int k = 0;
                for (int i = 0; i < SelectedProject.DevicesForPlacement.Count; i++)
                {
                    if (i + BreakingPoint < SelectedProject.DevicesForPlacement.Count)
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

                // Проверяем результаты после кроссинговера
                MessageBox.Show($"CrossingBetweenTwoParents: Children1 = {string.Join(",", Children1)}, Children2 = {string.Join(",", Children2)}", "Отладка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в CrossingBetweenTwoParents: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Children1 = null;
                Children2 = null;
            }
        }

        private void CreateChildrenPopulation()
        {
            try
            {
                ChildrenPopulation = new string[ChildrenNumber, SelectedProject.DevicesForPlacement.Count];
                SortedParentsIndexesForChildrenPopulation = new int[ChildrenNumber];
                int BreakingPoint = rnd.Next(1, SelectedProject.DevicesForPlacement.Count - 1);
                indexesPopulationsForCross = new bool[PopulationsCount];
                for (int j = 0; j < PopulationsCount; j++)
                {
                    indexesPopulationsForCross[j] = false;
                }

                for (int i = 0; i < ChildrenNumber / 2; i++)
                {
                    int Parent1 = 0, Parent2 = 0;

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

                string[] Children1, Children2;
                CrossingBetweenTwoParents(Parent1, Parent2, BreakingPoint, out Children1, out Children2);
                for (int j = 0; j < SelectedProject.DevicesForPlacement.Count; j++)
                {
                    ChildrenPopulation[2 * i, j] = Children1[j];
                    ChildrenPopulation[2 * i + 1, j] = Children2[j];
                }

                string[] chPop1 = new string[SelectedProject.DevicesForPlacement.Count];
                string[] chPop2 = new string[SelectedProject.DevicesForPlacement.Count];
                for (int t = 0; t < SelectedProject.DevicesForPlacement.Count; t++)
                {
                    chPop1[t] = ChildrenPopulation[2 * i, t];
                    chPop2[t] = ChildrenPopulation[2 * i + 1, t];
                }

                bool isPopulationCorrect1 = !isCarryingLimitExceeded(chPop1);
                bool isPopulationCorrect2 = !isCarryingLimitExceeded(chPop2);
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
                        for (int j = 0; j < SelectedProject.DevicesForPlacement.Count; j++)
                        {
                            ChildrenPopulation[2 * i, j] = Children1[j];
                            ChildrenPopulation[2 * i + 1, j] = Children2[j];
                        }
                        chPop1 = new string[SelectedProject.DevicesForPlacement.Count];
                        chPop2 = new string[SelectedProject.DevicesForPlacement.Count];
                        for (int t = 0; t < SelectedProject.DevicesForPlacement.Count; t++)
                        {
                            chPop1[t] = ChildrenPopulation[2 * i, t];
                            chPop2[t] = ChildrenPopulation[2 * i + 1, t];
                        }
                        isPopulationCorrect1 = !isCarryingLimitExceeded(chPop1);
                        isPopulationCorrect2 = !isCarryingLimitExceeded(chPop2);
                    }
                }
            }

                // Добавляем сообщение для проверки размеров ChildrenPopulation
                MessageBox.Show($"ChildrenPopulation: {ChildrenPopulation.GetLength(0)} x {ChildrenPopulation.GetLength(1)}", "Отладка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в CreateChildrenPopulation: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}

private void CreateMutationPopulation()
{
    try
    {
        MutationPopulation = new List<string[]>();
        for (int i = 0; i < MutationNumber; i++)
        {
            MutationPopulation.Add(new string[SelectedProject.DevicesForPlacement.Count]);
        }

        SortedParentsIndexesForMutationPopulation = new int[MutationNumber];

        int k = 0;
        for (int i = 0; i < PopulationsCount; i++)
        {
            if (!indexesPopulationsForCross[i])
            {
                for (int j = 0; j < SelectedProject.DevicesForPlacement.Count; j++)
                {
                    MutationPopulation[k][j] = BeginPopulation[i, j];
                }
                SortedParentsIndexesForMutationPopulation[k] = i;
                k++;
            }
        }

        for (int i = 0; i < MutationNumber; i++)
        {
            int index1 = rnd.Next(0, SelectedProject.DevicesForPlacement.Count);
            int index2 = rnd.Next(0, SelectedProject.DevicesForPlacement.Count);
            while (index2 == index1)
                index2 = rnd.Next(0, SelectedProject.DevicesForPlacement.Count);
            string temp = MutationPopulation[i][index1];
            MutationPopulation[i][index1] = MutationPopulation[i][index2];
            MutationPopulation[i][index2] = temp;

            bool isPopulationCorrect = !isCarryingLimitExceeded(MutationPopulation[i]);
            if (!isPopulationCorrect)
            {
                while (!isPopulationCorrect)
                {
                    index1 = rnd.Next(0, SelectedProject.DevicesForPlacement.Count);
                    index2 = rnd.Next(0, SelectedProject.DevicesForPlacement.Count);
                    while (index2 == index1)
                        index2 = rnd.Next(0, SelectedProject.DevicesForPlacement.Count);
                    temp = MutationPopulation[i][index1];
                    MutationPopulation[i][index1] = MutationPopulation[i][index2];
                    MutationPopulation[i][index2] = temp;
                    isPopulationCorrect = !isCarryingLimitExceeded(MutationPopulation[i]);
                }
            }
        }

        // Добавляем сообщение для проверки размеров MutationPopulation
        MessageBox.Show($"MutationPopulation: {MutationPopulation.Count}", "Отладка", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Ошибка в CreateMutationPopulation: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}

private void CreateReproductionGroup()
{
    try
    {
        ReproductionGroup = new List<string[]>();
        for (int i = 0; i < PopulationsCount; i++)
        {
            string[] toAdd = new string[SelectedProject.DevicesForPlacement.Count];
            for (int j = 0; j < SelectedProject.DevicesForPlacement.Count; j++)
            {
                toAdd[j] = BeginPopulation[i, j];
            }
            ReproductionGroup.Add(toAdd);
        }
        for (int i = 0; i < ChildrenNumber; i++)
        {
            string[] toAdd = new string[SelectedProject.DevicesForPlacement.Count];
            for (int j = 0; j < SelectedProject.DevicesForPlacement.Count; j++)
            {
                toAdd[j] = ChildrenPopulation[i, j];
            }
            ReproductionGroup.Add(toAdd);
        }
        for (int i = 0; i < MutationNumber; i++)
        {
            string[] toAdd = new string[SelectedProject.DevicesForPlacement.Count];
            for (int j = 0; j < SelectedProject.DevicesForPlacement.Count; j++)
            {
                toAdd[j] = MutationPopulation[i][j];
            }
            ReproductionGroup.Add(toAdd);
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Ошибка в CreateReproductionGroup: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}

private double CalculateFitness(string[] chromosome)
{
    double fitness = 0;
    try
    {
        for (int i = 0; i < chromosome.Length; i++)
        {
            fitness += SelectedProject.DevicesForPlacement
                .Where(dfp => dfp.Id_Project == SelectedProject.ProjectNumber)
                .Select(dfp => dfp.UavDevice)
                .ElementAt(int.Parse(chromosome[i]) - 1).NoiseImmunity -
                       SelectedProject.UavParameters.Compartments.ElementAt(i).LoadCapacity;
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Ошибка в CalculateFitness: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    return fitness;
}

private bool ValidateData()
{
    // Проверка количества отсеков и устройств для каждого проекта
    if (SelectedProject.UavParameters.Compartments.Count != SelectedProject.DevicesForPlacement.Count)
    {
        MessageBox.Show("Количество индексов устройств не соответствует количеству отсеков.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
    }
    return true;
}

public void MainProcess()
{
    try
    {
        if (!ValidateData())
        {
            return;
        }

        for (int g = 0; g < GenerationsCount; g++)
        {
            if (FirstGeneration)
            {
                CreateBeginPopulation();
                FirstGeneration = false;
            }
            CreateChildrenPopulation();
            CreateMutationPopulation();
            CreateReproductionGroup();
            Fitnesses = new double[ReproductionGroup.Count];
            for (int i = 0; i < ReproductionGroup.Count; i++)
            {
                Fitnesses[i] = CalculateFitness(ReproductionGroup[i]);
            }

            // Добавляем сообщение для проверки значений Fitnesses
            MessageBox.Show($"Fitnesses Length: {Fitnesses.Length}, ReproductionGroup Count: {ReproductionGroup.Count}", "Отладка", MessageBoxButtons.OK, MessageBoxIcon.Information);

            BestSolution = new string[SelectedProject.DevicesForPlacement.Count];
            int max = 0;
            for (int i = 0; i < ReproductionGroup.Count; i++)
            {
                if (Fitnesses[i] > Fitnesses[max])
                    max = i;
            }

            // Добавляем сообщение для проверки индекса max
            MessageBox.Show($"Max Index: {max}, Fitnesses[max]: {Fitnesses[max]}", "Отладка", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        // Вызов DrawScheme после завершения алгоритма
        Bitmap image = new Bitmap(800, 600); // Размеры изображения можно скорректировать по необходимости
        DrawScheme drawScheme = new DrawScheme(SelectedProject, image);
        drawScheme.DrawProject();

        // Создание формы для отображения результатов
        Form displayForm = new Form
        {
            Text = "Результаты размещения оборудования",
            Size = new Size(820, 620)
        };
        drawScheme.DisplayOnForm(displayForm);
        displayForm.ShowDialog();
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Ошибка в MainProcess: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
    }
}
