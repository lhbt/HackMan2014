using System;
using System.Collections.Generic;
using System.Linq;

namespace DatScheduleServer.Model
{
    public static class RandomTaskGenerator
    {
        public static List<Task> GetUniqueSet(List<Task> listOfTasks, List<Task> leftOverTasks)
        {
            var randomTaskList = leftOverTasks.Where(task => !task.Scheduled).ToList();

            return PopulateTasksList(randomTaskList);
        }

        private static List<Task> PopulateTasksList(List<Task> tasks)
        {
            if (tasks.Count == 9)
                return tasks;

            var rand = new Random();

            var staticTasks = GameTasks.ListOfTasks;
            staticTasks.Shuffle();

            var threeHoursMeetings = GameTasks.ListOfTasks.Where(x => x.Type == TaskType.Meeting && x.Duration == 3 && !tasks.Contains(x)).ToList();
            threeHoursMeetings.Shuffle();
            tasks.AddRange(threeHoursMeetings.Take(rand.Next(0,2)));

            if (tasks.Count == 9)
                return tasks;

            var twoHoursMeetings = GameTasks.ListOfTasks.Where(x => x.Type == TaskType.Meeting && x.Duration == 2 && !tasks.Contains(x)).ToList();
            twoHoursMeetings.Shuffle();
            tasks.AddRange(twoHoursMeetings.Take(rand.Next(0, 3)));

            if (tasks.Count == 9)
                return tasks;

            var meals = GameTasks.ListOfTasks.Where(x => x.Type == TaskType.Meal && !tasks.Contains(x)).ToList();
            meals.Shuffle();
            tasks.Add(meals.First());

            if (tasks.Count == 9)
                return tasks;

            var leisureBReaks = GameTasks.ListOfTasks.Where(x => x.Type == TaskType.Leisure && !tasks.Contains(x)).ToList();
            leisureBReaks.Shuffle();
            tasks.AddRange(leisureBReaks.Take(rand.Next(1,3)));

            if (tasks.Count == 9)
                return tasks;

            var oneHourMeetingsOrSleep =
                GameTasks.ListOfTasks.Where(x => ((x.Type == TaskType.Meeting && x.Duration == 1) || x.Type == TaskType.Sleep)).ToList();

            while (tasks.Count != 9)
            {
                oneHourMeetingsOrSleep.Shuffle();
                tasks.Add(oneHourMeetingsOrSleep.First(x => !tasks.Contains(x)));
            }

            return tasks;
        }
    }
}