using ConsoleTools;
using EWYRYV_HFT_202223.Models;
using EWYRYV_HFT_202223.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamDbApp.Client
{
    internal class Program
    {
        static RestService rest;
        static void Create(string entity)
        {
            Console.WriteLine("Enter the following parameter(s): ");
            if (entity == "Manager")
            {
                Manager mg = new Manager();
                Console.Write(" Name: ");
                string name = Console.ReadLine();
                Console.Write(" Nationality: ");
                string? nationality = Console.ReadLine();
                Console.Write(" Team ID: ");
                int? teamId = int.Parse(Console.ReadLine());

                rest.Post(new Manager() { Name = name, Nationality = nationality, TeamId = teamId }, "manager");
            }

            else if (entity == "Player")
            {
                Console.Write(" Name: ");
                string name = Console.ReadLine();
                Console.Write(" Team ID: ");
                int teamId = int.Parse(Console.ReadLine());
                Console.Write(" Birth Date: ");
                string? birthDate = Console.ReadLine();
                Console.Write(" Kit Number: ");
                int? kitNumber = int.Parse(Console.ReadLine());
                Console.Write(" Value: ");
                int? value = int.Parse(Console.ReadLine());
                rest.Post(new Player() { Name = name, TeamId = teamId, BirthDate = birthDate, KitNumber = kitNumber, Value = value }, "player");
                
            }else if(entity == "Team")
            {
                Console.Write(" Name: ");
                string name = Console.ReadLine();
                Console.Write(" Year of Fundation: ");
                int? fundationYear = int.Parse(Console.ReadLine());

                rest.Post(new Team(name, fundationYear), "team");
            }
            Console.WriteLine(entity + " added succesfully!");
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }
        static void List(string entity)
        {
            Console.WriteLine("List of "+entity+"'s containd by the database: ");
            List<Manager> managers = rest.Get<Manager>("manager");
            List<Team> teams = rest.Get<Team>("team");
            List<Player> players = rest.Get<Player>("player");
            if (entity == "Manager")
            {
                foreach (var item in managers)
                {
                    Console.Write("ID: " + item.ManagerId + " | Name: " + item.Name);  
                    if (item.Nationality != null)
                    {
                        Console.Write(" | Nationality: " + item.Nationality);
                    }
                    else
                    {
                        Console.Write(" | Nationality = null");
                    }
                    if (item.Nationality != null)
                    {
                        Console.Write(" | Managed Team ID: " + item.TeamId);
                    }
                    else
                    {
                        Console.Write(" | Managed Team ID: null");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("Press ENTER to continue...");
            }
            else if (entity == "Team")
            {   
                foreach (var item in teams)
                {
                    Console.WriteLine("ID: " + item.TeamId + " | Name: " + item.Name + " | Year Of Foundation: " + item.FoundationYear);
                }
                Console.WriteLine("Press ENTER to continue...");
            }
            else if (entity == "Player")
            {
                foreach (var item in players)
                {
                    
                    Console.Write("ID: " + item.PlayerId + " | Name:" + item.Name + " |" + " TeamID: " + item.TeamId+ " |" );
                    
                    if(item.KitNumber != null)
                    {
                        Console.Write(" Birth date: " + item.BirthDate +" |");
                    }
                    else
                    {
                        Console.Write(" Birth date: null |");
                    }
                    if (item.KitNumber != null)
                    {
                        Console.Write(" Kit Number: " + item.KitNumber + " |");
                    }
                    else
                    {
                        Console.Write(" Kit number: null |");
                    }
                    if (item.KitNumber != null)
                    {
                        Console.Write(" Value: " + item.Value);
                    }
                    else
                    {
                        Console.Write(" Value: null");
                    }
                    Console.WriteLine();
                   
                }
                Console.WriteLine("Press ENTER to continue...");
            }
            Console.ReadLine();
        }
        static void Read(string entity)
        {
            if (entity == "Manager")
            {
                Console.Write($"ID of requested {entity}: ");
                int id = int.Parse(Console.ReadLine());
                Manager item = rest.Get<Manager>(id, entity.ToLower());
                Console.Write("ID: " + item.ManagerId + " | Name: " + item.Name);
                if (item.Nationality != null)
                {
                    Console.Write(" | Nationality: " + item.Nationality);
                }
                else
                {
                    Console.Write(" | Nationality = null");
                }
                if (item.TeamId != null)
                {
                    Console.Write(" | Managed Team ID: " + item.TeamId);
                }
                else
                {
                    Console.Write(" | Managed Team ID: null");
                }
                Console.WriteLine();
            }
            else if(entity == "Player")
            {
                Console.Write($"ID of requested {entity}: ");
                int id = int.Parse(Console.ReadLine());
                Player item = rest.Get<Player>(id, entity.ToLower());
                Console.Write("ID: " + item.PlayerId + " | Name:" + item.Name + " |" + " TeamID: " + item.TeamId + " |");

                if (item.KitNumber != null)
                {
                    Console.Write(" Birth date: " + item.BirthDate + " |");
                }
                else
                {
                    Console.Write(" Birth date: null |");
                }
                if (item.KitNumber != null)
                {
                    Console.Write(" Kit Number: " + item.KitNumber + " |");
                }
                else
                {
                    Console.Write(" Kit number: null |");
                }
                if (item.KitNumber != null)
                {
                    Console.Write(" Value: " + item.Value);
                }
                else
                {
                    Console.Write(" Value: null");
                }
                Console.WriteLine();
            }
            else if(entity == "Team")
            {
                Console.Write($"ID of requested {entity}: ");
                int id = int.Parse(Console.ReadLine());
                Team item = rest.Get<Team>(id, entity.ToLower());
                Console.WriteLine("ID: " + item.TeamId + " | Name: " + item.Name);
            }
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            Console.WriteLine("Enter "+entity+"'s id to update: ");
            if (entity == "Manager")
            {
                int id = int.Parse(Console.ReadLine());
                Manager one = rest.Get<Manager>(id, "manager");
                Console.Write($" New Name [old: {one.Name}]: ");
                string name = Console.ReadLine();
                Console.Write($" New Nationality [old: {one.Nationality}]: ");
                string? nationality = Console.ReadLine();
                Console.Write($" New Team ID [old: {one.TeamId}]: ");
                int? teamId = int.Parse(Console.ReadLine());

                one.Name = name;
                one.Nationality = nationality;
                one.TeamId = teamId;
                rest.Put(one, "manager");
            }
            else if (entity == "Team")
            {
                int id = int.Parse(Console.ReadLine());
                Team one = rest.Get<Team>(id, "team");
                Console.Write($" New Name [old: {one.Name}]: ");
                string name = Console.ReadLine();
                one.Name = name;
                rest.Put(one, "team");
            }
            else if (entity == "Player")
            {
                int id = int.Parse(Console.ReadLine());
                Player one = rest.Get<Player>(id, "player");
                Console.Write($" New Name [old: {one.Name}]: ");
                string name = Console.ReadLine();
                Console.Write($" New TeamID [old: {one.TeamId}]: ");
                int? teamId = int.Parse(Console.ReadLine());
                Console.Write($" New Kit Number [old: {one.KitNumber}]: ");
                int? kitNumber = int.Parse(Console.ReadLine());
                Console.Write($" New Birth Date [old: {one.BirthDate}]: ");
                string? birthDate = Console.ReadLine();
                Console.Write($" New Value [old: {one.Value}]: ");
                int? value = int.Parse(Console.ReadLine());
                one.Name = name;
                one.TeamId = teamId;
                one.KitNumber = kitNumber;
                one.BirthDate = birthDate;
                one.Value = value;
                rest.Put(one, "player");
            }
            Console.WriteLine(entity + " Successfully Updated!");
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }
        static void Delete(string entity)
        {
            Console.Write("Enter" + entity + "'s id to delete: ");
            if (entity == "Manager")
            {
                
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "manager");
            }
            else if (entity == "Team")
            {
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "team");
            }
            else if (entity == "Player")
            {
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "player");
            }
            Console.WriteLine(entity + " Successfully Deleted!");
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }
        static void CountPlayers(string endpoint)
        {
            Console.WriteLine("Count of players in teams: ");
            var data = rest.Get<object>(endpoint);
            foreach (var item in data)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }
        static void TeamValue(string endpoint)
        {
            Console.WriteLine("The total value of the players in teams: ");
            var data = rest.Get<object>(endpoint);
            foreach (var item in data)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }
        static void MostValuable(string endpoint)
        {
            Console.WriteLine("The most valuable player and its manager: ");
            var data = rest.Get<object>(endpoint);
            foreach (var item in data)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }
        static void HungarianManagers(string endpoint)
        {
            Console.WriteLine("Teams with hungarian manager and its managers: ");
            var data = rest.Get<object>(endpoint);
            foreach (var item in data)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }
        static void TopPlayerData(string endpoint)
        {
            Console.WriteLine("Teams' most valuable player and its manager");
            var data = rest.Get<object>(endpoint);
            foreach (var item in data)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:15885");

            var managerSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Read By ID", () => Read("Manager"))
                .Add("List", () => List("Manager"))
                .Add("Create", () => Create("Manager"))
                .Add("Delete", () => Delete("Manager"))
                .Add("Update", () => Update("Manager"))
                .Add("Main Menu", ConsoleMenu.Close);

            var teamSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Read By ID", () => Read("Team"))
                .Add("List", () => List("Team"))
                .Add("Create", () => Create("Team"))
                .Add("Delete", () => Delete("Team"))
                .Add("Update", () => Update("Team"))
                .Add("Main Menu", ConsoleMenu.Close);

            var playerSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Read By ID", () => Read("Player"))
                .Add("List", () => List("Player"))
                .Add("Create", () => Create("Player"))
                .Add("Delete", () => Delete("Player"))
                .Add("Update", () => Update("Player"))
                .Add("Main Menu", ConsoleMenu.Close);

            var nonCrudSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Count of players in teams", () => CountPlayers("stat/countPlayers"))
                .Add("Total value of players in each teams", () => TeamValue("stat/teamValue"))
                .Add("The most valuable player in database", () => MostValuable("stat/mostValuable"))
                .Add("Teams with Hungarian managers", () => HungarianManagers("stat/hungarianManagers"))
                .Add("Most valuable player in each teams", () => TopPlayerData("stat/topPlayerData"))
                .Add("Main Menu", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Managers", () => managerSubMenu.Show())
                .Add("Teams", () => teamSubMenu.Show())
                .Add("Players", () => playerSubMenu.Show())
                .Add("Non-CRUD methods", () => nonCrudSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();

        }
    }
}
