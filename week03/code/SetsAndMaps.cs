using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// Finds symmetric pairs of two-letter words using a set
    /// </summary>
    public static string[] FindPairs(string[] words)
    {
        // Store all words in a set for O(1) lookup
        var wordsSet = new HashSet<string>(words);
        var result = new List<string>();
        var used = new HashSet<string>();

        foreach (var word in words)
        {
            // Skip words with same letters like "aa"
            if (word[0] == word[1])
            {
                continue;
            }

            // Create the reversed word
            var reversed = $"{word[1]}{word[0]}";

            // Check if reversed exists and we haven't used this word yet
            if (wordsSet.Contains(reversed) && !used.Contains(word))
            {
                result.Add($"{word} & {reversed}");
                used.Add(word);
                used.Add(reversed);
            }
        }
        return result.ToArray();
    }

    /// <summary>
    /// Reads a census file and counts degrees from column 4
    /// </summary>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        
        // Read file line by line
        foreach (var line in File.ReadLines(filename))
        {
            // Split by comma and get the 4th column (index 3)
            var fields = line.Split(",");
            var degree = fields[3];
            
            // Count occurrences of each degree
            if (degrees.ContainsKey(degree))
            {
                degrees[degree]++;
            }
            else
            {
                degrees[degree] = 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Checks if two words are anagrams using a dictionary
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Remove spaces and convert to lowercase
        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();

        // If lengths are different, they can't be anagrams
        if (word1.Length != word2.Length)
            return false;

        // Dictionary to count characters
        var charCount = new Dictionary<char, int>();

        // Count characters in first word
        foreach (char c in word1)
        {
            if (charCount.ContainsKey(c))
                charCount[c]++;
            else
                charCount[c] = 1;
        }

        // Subtract characters using second word
        foreach (char c in word2)
        {
            if (!charCount.ContainsKey(c))
                return false;
            
            charCount[c]--;
            if (charCount[c] < 0)
                return false;
        }

        return true;
    }

    /// <summary>
    /// Gets earthquake data from USGS API and returns formatted summaries
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        
        // Get JSON data from the API
        using var client = new HttpClient();
        using var response = client.GetAsync(uri).Result;
        var json = response.Content.ReadAsStringAsync().Result;
        
        // Deserialize JSON into objects
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // Format each earthquake
        var summaries = new List<string>();
        foreach (var feature in featureCollection.Features)
        {
            summaries.Add($"{feature.Properties.Place} - Mag {feature.Properties.Mag}");
        }

        return summaries.ToArray();
    }
}

// Classes needed to deserialize the JSON data from USGS
public class FeatureCollection
{
    public Feature[] Features { get; set; }
}

public class Feature
{
    public Properties Properties { get; set; }
}

public class Properties
{
    public string Place { get; set; }
    public double Mag { get; set; }
}