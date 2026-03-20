using System.Text.Json;

public static class SetsAndMaps
{
    /// Problem 1 - Find symmetric pairs in O(n) using a HashSet
    public static string[] FindPairs(string[] words)
    {
        var seen = new HashSet<string>();
        var results = new List<string>();

        foreach (var word in words)
        {
            // Skip words where both letters are the same (e.g. "aa")
            if (word[0] == word[1])
                continue;

            string reversed = $"{word[1]}{word[0]}";

            if (seen.Contains(reversed))
                results.Add($"{word} & {reversed}");
            else
                seen.Add(word);
        }

        return results.ToArray();
    }

    /// Problem 2 - Summarize degrees from a census file
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            if (fields.Length > 3)
            {
                var degree = fields[3].Trim();
                if (degrees.ContainsKey(degree))
                    degrees[degree]++;
                else
                    degrees[degree] = 1;
            }
        }
        return degrees;
    }

    /// Problem 3 - Check if two words are anagrams using a dictionary
    public static bool IsAnagram(string word1, string word2)
    {
        var counts = new Dictionary<char, int>();

        // Count letters in word1 (ignore spaces, lowercase)
        foreach (var c in word1.ToLower())
        {
            if (c == ' ') continue;
            counts[c] = counts.GetValueOrDefault(c, 0) + 1;
        }

        // Subtract letter counts using word2
        foreach (var c in word2.ToLower())
        {
            if (c == ' ') continue;
            if (!counts.ContainsKey(c)) return false;
            counts[c]--;
            if (counts[c] == 0)
                counts.Remove(c);
        }

        // If the dictionary is empty, both words have the same letters
        return counts.Count == 0;
    }

    /// Problem 5 - Fetch and summarize earthquake data from USGS
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        return featureCollection?.Features
            .Select(f => $"{f.Properties.Place} - Mag {f.Properties.Mag}")
            .ToArray() ?? [];
    }
}