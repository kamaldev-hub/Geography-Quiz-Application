using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GeographyQuizApp.BLL
{
    /// <summary>
    /// A simple INI file parser.
    /// Reads configuration settings from a .ini file.
    /// </summary>
    public class IniConfigParser
    {
        private Dictionary<string, Dictionary<string, string>> _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="IniConfigParser"/> class.
        /// </summary>
        public IniConfigParser()
        {
            _config = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Loads and parses the INI file from the specified path.
        /// </summary>
        /// <param name="filePath">The path to the .ini file.</param>
        /// <returns>A dictionary representing the parsed INI data. Outer key is section, inner key is parameter.</returns>
        public Dictionary<string, Dictionary<string, string>>? LoadConfig(string filePath)
        {
            _config.Clear();
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"INI file not found: {filePath}");
                return null;
            }

            string? currentSection = null;
            try
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    string trimmedLine = line.Trim();
                    if (string.IsNullOrWhiteSpace(trimmedLine) || trimmedLine.StartsWith(";") || trimmedLine.StartsWith("#")) // Skip empty lines and comments
                    {
                        continue;
                    }

                    if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]")) // Section
                    {
                        currentSection = trimmedLine.Substring(1, trimmedLine.Length - 2).Trim();
                        if (!string.IsNullOrEmpty(currentSection) && !_config.ContainsKey(currentSection))
                        {
                            _config[currentSection] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                        }
                    }
                    else if (currentSection != null && trimmedLine.Contains("=")) // Key-value pair
                    {
                        int equalsIndex = trimmedLine.IndexOf('=');
                        string key = trimmedLine.Substring(0, equalsIndex).Trim();
                        string value = trimmedLine.Substring(equalsIndex + 1).Trim();

                        if (!string.IsNullOrEmpty(key))
                        {
                            _config[currentSection][key] = value;
                        }
                    }
                }
                return _config;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing INI file '{filePath}': {ex.Message}");
                return null; // Or throw, depending on desired error handling
            }
        }

        /// <summary>
        /// Gets a configuration value.
        /// </summary>
        /// <param name="section">The section name.</param>
        /// <param name="key">The key name.</param>
        /// <returns>The configuration value, or null if not found.</returns>
        public string? GetValue(string section, string key)
        {
            if (_config.TryGetValue(section, out var sectionDict))
            {
                if (sectionDict.TryGetValue(key, out var value))
                {
                    return value;
                }
            }
            return null;
        }
    }

    /// <summary>
    /// Extension methods for easier access to INI configuration values.
    /// </summary>
    public static class IniConfigExtensions
    {
        /// <summary>
        /// Gets a value from the parsed INI dictionary, returning a default if not found.
        /// </summary>
        /// <param name="config">The configuration dictionary.</param>
        /// <param name="sectionName">The name of the section.</param>
        /// <param name="keyName">The name of the key.</param>
        /// <param name="defaultValue">The default value to return if the key is not found.</param>
        /// <returns>The configuration value or the default value.</returns>
        public static string? GetValueOrDefault(this Dictionary<string, Dictionary<string, string>> config, string sectionName, string keyName, string? defaultValue = null)
        {
            if (config.TryGetValue(sectionName, out var section))
            {
                if (section.TryGetValue(keyName, out var value))
                {
                    return value;
                }
            }
            return defaultValue;
        }
    }
}
