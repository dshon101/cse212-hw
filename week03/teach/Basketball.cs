/*
 * CSE 212 Lesson 6C 
 * 
 * This code will analyze the NBA basketball data and create a table showing
 * the players with the top 10 career points.
 * 
 * Note about columns:
 * - Player ID is in column 0
 * - Points is in column 8
 * 
 * Each row represents the player's stats for a single season with a single team.
 */

using Microsoft.VisualBasic.FileIO;

public class Basketball
{
    public static void Run()
    {
        var players = new Dictionary<string, int>();

        using var reader = new TextFieldParser("basketball.csv");
        reader.TextFieldType = FieldType.Delimited;
        reader.SetDelimiters(",");
        reader.ReadFields(); // ignore header row
        while (!reader.EndOfData)
        {
            var fields = reader.ReadFields()!;
            var playerId = fields[0];
            var points = int.Parse(fields[8]);

            // If player already exists in dictionary, add to their total.
            // Otherwise, create a new entry starting at 0.
            if (!players.ContainsKey(playerId))
                players[playerId] = 0;

            players[playerId] += points;
        }

        Console.WriteLine($"Players: {{{string.Join(", ", players)}}}");

        // Sort by points descending and take the top 10
        var topPlayers = players
            .OrderByDescending(p => p.Value)
            .Take(10)
            .ToArray();

        Console.WriteLine("\nTop 10 Players by Career Points:");
        Console.WriteLine($"{"Rank",-6} {"Player ID",-15} {"Total Points",12}");
        Console.WriteLine(new string('-', 35));
        for (int i = 0; i < topPlayers.Length; i++)
        {
            Console.WriteLine($"{i + 1,-6} {topPlayers[i].Key,-15} {topPlayers[i].Value,12}");
        }
    }
}